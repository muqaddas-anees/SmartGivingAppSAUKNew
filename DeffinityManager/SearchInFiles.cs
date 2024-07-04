using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
//using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Office;
//using Microsoft.Office.Core;
using Microsoft.Office.Interop;
using msExcel = Microsoft.Office.Interop.Excel;
//using msOutlook = Microsoft.Office.Interop.Outlook;
//using msPowerPoint = Microsoft.Office.Interop.PowerPoint;
//using msVisio = Microsoft.Office.Interop.Visio;
//using msWord = Microsoft.Office.Interop.Word;

namespace Deffinity.DocumentSearch
{
    public class SearchInFiles
    {

        public SearchResultList getAc2pDocument(Dictionary<int, string> ac2pFileID, string searchText)
        {
            string contentType = string.Empty;
            string documentName = string.Empty;
            //ArrayList searchResults = new ArrayList();
            SearchResultList searchResults = new SearchResultList();
            MemoryStream stream = new MemoryStream();

            foreach (int fileId in ac2pFileID.Keys)
            {
                using (SqlConnection conn = new SqlConnection(connectionString.retrieveConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("GetDocumentByID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", fileId);
                        cmd.CommandType = CommandType.StoredProcedure;
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    contentType = reader["ContentType"].ToString();
                                    documentName = reader["DocumentName"].ToString();
                                    stream = new MemoryStream((byte[])(reader["Document"]));
                                    SearchResult result = new SearchResult();
                                    result.fileName = documentName;
                                    result.fileID = (int)reader["Id"];
                                    result.uploadedBy = reader["UploadedBy"].ToString();
                                    result.uploadedTime = (DateTime)reader["UploadDateTime"];
                                    result.version = reader["Version"].ToString();
                                    result.matches = 0;
                                    result.matches = getSearchResults(stream, searchText, documentName);
                                    searchResults.Add(result);
                                }
                                catch
                                {
                                    //do nothing
                                }
                            }
                        }
                    }
                }
            }
            return searchResults;
        }

        public int matchWords(string originalString, string searchString)
        {
            string s = originalString;
            s = s.ToLower();
            string e = searchString.Replace(" ", @"|").ToLower();
            string result = string.Empty;
            int count = 0;
            foreach (Match m in Regex.Matches(s, e))
            {
                result += m.Value;
                GroupCollection myColl = m.Groups;
                count++;
                //sProper += char.ToUpper(m.Value[0]) + m.Value.Substring(1, m.Length - 1);
            }
            return count;
        }

        private int getSearchResults(MemoryStream stream, string searchString, string documentName)
        {
            string[] document = documentName.Split('.');
            string extension = document[document.Length - 1];
            SearchResult searchResult = new SearchResult();

            string[] searchWords = searchString.Split(' ');
            string fileText = string.Empty;
            string filePath = string.Empty;
            switch (extension.ToLower())
            {
                case "txt":
                default:
                    StreamReader streamReader = new StreamReader(stream);
                    if (stream != null)
                    {
                        fileText = streamReader.ReadToEnd();
                    }
                    searchResult.matches = matchWords(fileText, searchString);
                    break;
                case "doc":
                case "docx":
                    filePath = string.Format(@"{0}ChartImages\{1}.{2}", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath), Guid.NewGuid(), extension.ToLower());
                    using (FileStream file = File.Create(filePath))
                    {
                        file.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        file.Dispose();
                    }
                    searchResult.matches = readDocumentFile(filePath, searchString);
                    File.Delete(filePath);
                    break;
                case "xls":
                case "xlsx":
                    filePath = string.Format(@"{0}ChartImages\{1}.{2}", HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath), Guid.NewGuid(), extension.ToLower());
                    using (FileStream file = File.Create(filePath))
                    {
                        file.Write(stream.ToArray(), 0, stream.ToArray().Length);
                        file.Dispose();
                    }
                    searchResult.matches = readExcelFile(filePath, searchString);
                    File.Delete(filePath);
                    break;
            }
            return searchResult.matches;
        }

        int readExcelFile(string filePath, string searchString)
        {
            int count = 0;
            string urlPath = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1;\"", @filePath);
            using (OleDbConnection conn = new OleDbConnection(urlPath))
            {
                conn.Open();
                using (OleDbCommand cmd = new OleDbCommand())
                {

                    cmd.Connection = conn;
                    DataTable table = new DataTable();
                    DataTable metadataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    using (DataTableReader metaTableReader = metadataTable.CreateDataReader())
                    {
                        while (metaTableReader.Read())
                        {
                            if (metaTableReader["Table_Name"].ToString().Replace("'", string.Empty).LastIndexOf('$') == metaTableReader["Table_Name"].ToString().Replace("'", string.Empty).Length - 1)
                            {
                                cmd.CommandText = string.Format("SELECT * FROM [{0}]", metaTableReader["Table_Name"].ToString().Replace("'", string.Empty));
                                using (OleDbDataReader reader = cmd.ExecuteReader())
                                {
                                    table.Load(reader);
                                    using (DataTableReader tableReader = table.CreateDataReader())
                                    {
                                        int fieldCount = tableReader.FieldCount;
                                        for (int i = 0; i < fieldCount; i++)
                                        {
                                            count += matchWords(tableReader.GetName(i), searchString);
                                        }
                                        while (tableReader.Read())
                                        {
                                            for (int i = 0; i < fieldCount; i++)
                                            {
                                                count += matchWords(tableReader[i].ToString(), searchString);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return count;
        }

        int readDocumentFile(string filePath, string searchString)
        {
            //msWord.ApplicationClass wordApp = new msWord.ApplicationClass();
            //object file = filePath;
            //object nullobj = Missing.Value;
            //object isReadOnly = true;
            //object isVisible = true;
            //msWord.Document doc = wordApp.Documents.Open(
            //                                    ref file, ref nullobj, ref isReadOnly ,
            //                                      ref nullobj, ref nullobj, ref nullobj,
            //                                      ref nullobj, ref nullobj, ref nullobj,
            //                                      ref nullobj, ref nullobj, ref isVisible, ref nullobj, ref nullobj, ref nullobj, ref nullobj);
            //msWord.Words words = doc.Words;
            //doc.ActiveWindow.Selection.WholeStory();
            //doc.ActiveWindow.Selection.Copy();
            //string originalText = doc.ActiveWindow.Selection.Text;
            //doc.ActiveWindow.Close(ref nullobj, ref nullobj);
            //Marshal.ReleaseComObject(doc);
            //Marshal.ReleaseComObject(wordApp);
            
            //return matchWords(originalText, searchString);
            return 1;
        }
    }
}
