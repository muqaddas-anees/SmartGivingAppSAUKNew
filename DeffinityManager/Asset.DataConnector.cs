using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.UI.WebControls;
using AssetConfigurator.Entity;
using AssetConfigurator.DAL;
using System.Text;

/// <summary>
/// Summary description for Asset
/// </summary>
/// 
namespace AssetConfigurator
{
    public class DataConnector
    {
        public void ExcelReader(List<AttributesEntity> assetSchema, ref int totalRecords, ref int updatedRecords, DataTable table,FileUpload fileCSVFile)
        {
            OleDbConnection cn = new OleDbConnection();
            string temporaryFolder = HttpContext.Current.Server.MapPath(@"~\ChartImages\Temporary Excel Files");

            HttpContext.Current.Session["ExcelFileName"] = Guid.NewGuid().ToString() + ".xls";
            string fullFilePath = temporaryFolder + @"\" + HttpContext.Current.Session["ExcelFileName"].ToString();
            cn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fullFilePath + ";" + "Extended Properties=Excel 8.0;";
            if (!Directory.Exists(temporaryFolder))
                Directory.CreateDirectory(temporaryFolder);
            fileCSVFile.SaveAs(fullFilePath);
            cn.Open();
            DataTable schemaTable = new DataTable();
            schemaTable = cn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            foreach (DataRow row in schemaTable.Rows)
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + row["Table_Name"].ToString() + "]", cn))
                {
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable inputTable = new DataTable();
                        inputTable.Load(reader);
                        if (inputTable.Rows.Count > 1)
                        {
                            foreach (DataRow inputRow in inputTable.Rows)
                            {
                                table.ImportRow(inputRow);
                            }
                        }

                        inputTable.Dispose();
                        reader.Close();
                        reader.Dispose();
                    }
                    cmd.Dispose();
                }
            }

            foreach (DataRow dataRow in table.Rows)
            {
                Guid recordID = Guid.NewGuid();
                for (int i = 0; i < dataRow.ItemArray.Length; i++)
                {
                    ValuesEntity valuesEntity = new ValuesEntity();
                    valuesEntity.AttributeId = assetSchema[i].Id;
                    valuesEntity.AttributeValue = dataRow[i].ToString();
                    valuesEntity.UniqueIdentifier = recordID;
                    valuesEntity.AssetID = assetSchema[i].AssetId;
                    ValuesHelper valuesHelper = new ValuesHelper();
                    valuesHelper.Insert(valuesEntity);
                }
            }

            cn.Close();
            cn.Dispose();
            File.Delete(fullFilePath);
        }

        public void CSVReader(StringBuilder errorData, ref bool failed, List<AttributesEntity> assetSchema, ref int totalRecords, ref int updatedRecords, DataTable table, Stream inputStream,FileUpload fileCSVFile)
        {
            using (StreamReader csvFileReader = new StreamReader(inputStream))
            {
                string csvRecord;
                errorData.Append((csvFileReader.ReadLine()));
                errorData.Append("\r\n");
                //Read from the second line.
                while ((csvRecord = csvFileReader.ReadLine()) != null)
                {
                    totalRecords++;
                    string[] data = csvRecord.Split(',');
                    if (data.Length != table.Columns.Count)
                    {
                        failed = true;
                        errorData.Append(csvRecord);
                        errorData.Append("\r\n");
                        continue;
                    }
                    try
                    {
                        table.Rows.Add(data);
                        Guid recordID = Guid.NewGuid();

                        for (int i = 0; i < data.Length; i++)
                        {
                            ValuesEntity valuesEntity = new ValuesEntity();
                            valuesEntity.AttributeId = assetSchema[i].Id;
                            valuesEntity.AttributeValue = data[i].Replace("'", "''");
                            valuesEntity.UniqueIdentifier = recordID;
                            valuesEntity.AssetID = assetSchema[i].AssetId;
                            ValuesHelper valuesHelper = new ValuesHelper();
                            valuesHelper.Insert(valuesEntity);
                        }
                        updatedRecords++;
                    }
                    catch
                    {
                        failed = true;
                        errorData.Append(csvRecord);
                        errorData.Append("\r\n");
                    }
                }
            }
        }
    }
}