using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using DeffinityAppDev.App.Beneficiaries.Entities;
namespace DeffinityAppDev.App.Beneficiaries
{
    public partial class BeneficiaryReport : System.Web.UI.Page
    {

        protected void ProduceWordReport_Click(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.Parse(fromDate.Text);
            DateTime todate = DateTime.Parse(toDate.Text);

            bool includePersonalInfo = personalInfo.Checked;
            bool includeContacts = contacts.Checked;
            bool includeSupport = supportReceived.Checked;
            bool includeActivity = activity.Checked;
            bool includeCommunication = communication.Checked;

            // Define file paths for Word and PDF in the App_Data folder
            string wordFilePath = Server.MapPath("~/App_Data/BeneficiaryReport.docx");
            string pdfFilePath = Server.MapPath("~/App_Data/BeneficiaryReport.pdf");

            // Generate the report in Word format
            GenerateReport(fromdate, todate, includePersonalInfo, includeContacts, includeSupport, includeActivity, includeCommunication, wordFilePath);

            // Download the generated Word report
            Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            Response.AppendHeader("Content-Disposition", "attachment; filename=BeneficiaryReport.docx");
            Response.TransmitFile(wordFilePath);
            Response.End();
        }

        protected void ProducePdfReport_Click(object sender, EventArgs e)
        {
            DateTime fromdate = DateTime.Parse(fromDate.Text);
            DateTime todate = DateTime.Parse(toDate.Text);
            bool includePersonalInfo = personalInfo.Checked;  // Accessing server-side control
            bool includeContacts = contacts.Checked;          // Accessing server-side control
            bool includeSupport = supportReceived.Checked;    // Accessing server-side control
            bool includeActivity = activity.Checked;          // Accessing server-side control
            bool includeCommunication = communication.Checked;  // Accessing server-side control

            // Define file paths for Word and PDF in the App_Data folder
            string wordFilePath = Server.MapPath("~/App_Data/BeneficiaryReport.docx");
            string pdfFilePath = Server.MapPath("~/App_Data/BeneficiaryReport.pdf");

            // Generate the report in Word format first
            GenerateReport(fromdate, todate, includePersonalInfo, includeContacts, includeSupport, includeActivity, includeCommunication, wordFilePath);

            // Convert the Word report to PDF
            ConvertWordToPdf(wordFilePath, pdfFilePath);

            // Download the generated PDF report
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=BeneficiaryReport.pdf");
            Response.TransmitFile(pdfFilePath);
            Response.End();
        }

        public void GenerateReport(DateTime fromDate, DateTime toDate, bool includePersonalInfo, bool includeContacts, bool includeSupport, bool includeActivity, bool includeCommunication, string wordFilePath)
        {
            using (var document = Xceed.Words.NET.DocX.Create(wordFilePath))
            {
                // Add Title
                document.InsertParagraph("Beneficiary Report").FontSize(16).Bold().Alignment = Xceed.Document.NET.Alignment.center;
                document.InsertParagraph($"Date Range: {fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd}").FontSize(12).Alignment = Xceed.Document.NET.Alignment.center;
                document.InsertParagraph();

                using (var context = new MyDatabaseContext())
                {
                    // Add each section if selected
                    if (includePersonalInfo)
                    {
                        InsertSection(document, "Personal Information",
                            context.Beneficiaries
                                   .Where(b => b.CreatedAt >= fromDate && b.CreatedAt <= toDate).AsEnumerable()
                                   .Select(b => new
                                   {
                                       b.Type,
                                       b.Gender,
                                       b.Name,
                                       b.DateOfBirth,
                                       b.InternalIDNumber,
                                       b.Town,
                                       b.City,
                                       b.PostalCode,
                                       b.Country,
                                       b.GovernmentID,
                                       b.CreatedAt,
                                       b.Email,
                                       b.Phone,
                                       b.Background,
                                       b.EmploymentStatus,
                                       b.HealthCondition,
                                       b.ProfileImage,
                                       b.DocumentFront,
                                       b.DocumentBack
                                   })
                                   .ToList());
                    }

                    if (includeContacts)
                    {
                        InsertSection(document, "Contacts",
                            context.BeneficiaryContacts
                                   .Where(c => c.CreatedAt >= fromDate && c.CreatedAt <= toDate).AsEnumerable()
                                   .Select(c => new
                                   {
                                       c.FirstName,
                                       c.LastName,
                                       c.EmailAddress,
                                       c.CountryCode,
                                       c.PhoneNumber,
                                       c.Position,
                                       c.Notes,
                                       c.TithingID,
                                       c.CreatedAt
                                   })
                                   .ToList());
                    }

                    if (includeSupport)
                    {
                        InsertSection(document, "Support Received",
                            context.BeneficiaryDonations
                                   .Where(d => d.DonationDate >= fromDate && d.DonationDate <= toDate).AsEnumerable()
                                   .Select(d => new
                                   {
                                       d.DonationDate,
                                       d.LoggedBy,
                                       d.AssociatedFundraiser,
                                       d.DonationAmount,
                                       d.Currency,
                                       d.PaymentType,
                                       d.DonatedBy,
                                       d.Notes,
                                       d.CreatedAt,
                                       d.DocumentUpload
                                   })
                                   .ToList());
                    }

                    if (includeActivity)
                    {
                        InsertSection(document, "Activity",
                            context.BeneficiaryActivities
                                   .Where(a => a.ActivityDate >= fromDate && a.ActivityDate <= toDate).AsEnumerable()
                                   .Select(a => new
                                   {
                                       a.ActivityDate,
                                       a.LoggedBy,
                                       a.ProgressDetails,
                                       a.CreatedAt,
                                       a.ImageData
                                   })
                                   .ToList());
                    }

                    if (includeCommunication)
                    {
                        InsertSection(document, "Communication",
                            context.BeneficiariesFeedBack
                                   .Where(f => f.FeedbackDate >= fromDate && f.FeedbackDate <= toDate).AsEnumerable()
                                   .Select(f => new
                                   {
                                       f.FeedbackDate,
                                       f.FeedbackText,
                                       f.CreatedAt,
                                       f.Attachments
                                   })
                                   .ToList());
                    }
                }

                // Save Word Document
                document.Save();
            }
        }

        private void InsertSection(Xceed.Words.NET.DocX document, string sectionTitle, IEnumerable<dynamic> records)
        {
            // Section Header
            var sectionHeader = document.InsertParagraph(sectionTitle)
                                        .FontSize(16)
                                        .Bold()
                                        .SpacingAfter(15);
            sectionHeader.Alignment = Xceed.Document.NET.Alignment.left;

            if (records.Any())
            {
                foreach (var record in records)
                {
                    // Create a new paragraph for each record in the section
                    var recordParagraph = document.InsertParagraph();
                    recordParagraph.SpacingAfter(20);  // Add spacing between records for better visibility

                    // Loop through the fields and add them to the paragraph in a structured format
                    foreach (var prop in record.GetType().GetProperties())
                    {
                        var fieldName = prop.Name.Replace("_", " "); // Replace underscores for readability
                        var fieldValue = prop.GetValue(record);

                        if (fieldValue != null)
                        {
                            try
                            {
                                if (fieldValue is byte[]) // Handling image fields
                                {
                                    byte[] imageData = (byte[])fieldValue;

                                    using (MemoryStream ms = new MemoryStream(imageData))
                                    {
                                        // Adding the image to the document
                                        var image = document.AddImage(ms);
                                        var picture = image.CreatePicture(150, 150); // Adjust size as needed
                                        recordParagraph.AppendPicture(picture);
                                        recordParagraph.AppendLine(); // Add line break after picture
                                    }
                                }
                                else
                                {
                                    // Clean up invalid characters (removing null characters)
                                    string cleanedValue = fieldValue.ToString().Replace("\0", "");

                                    // Append the column name followed by the value in a structured way
                                    recordParagraph.Append($"{fieldName}: ")
                                                   .Bold()
                                                   .FontSize(12)
                                                   .Color(System.Drawing.Color.DarkBlue)
                                                   .Append(cleanedValue)
                                                   .FontSize(11)
                                                   .Color(System.Drawing.Color.Black)
                                                   .SpacingAfter(10);

                                    // Add a new line for each field for better readability
                                    recordParagraph.AppendLine();
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                // Log the problematic field and value if an exception occurs
                                Debug.WriteLine($"Error appending field '{fieldName}' with value '{fieldValue}': {ex.Message}");
                                recordParagraph.AppendLine($"{fieldName}: [Invalid Data]").FontSize(11).Italic().SpacingAfter(5);
                            }
                        }
                    }

                    // Add a separator line between records
                    document.InsertParagraph()
                            .AppendLine("──────────────────────────────────────────────")
                            .FontSize(8)
                            .Color(System.Drawing.Color.Gray)
                            .SpacingAfter(20);
                }
            }
            else
            {
                document.InsertParagraph("No data available for this section")
                        .Italic()
                        .FontSize(12)
                        .SpacingAfter(20);
            }
        }






        private string RemoveInvalidCharacters(string input)
        {
            // Remove or replace invalid surrogate pairs
            return new string(input.Where(c => !char.IsSurrogate(c) || char.IsSurrogatePair(input, input.IndexOf(c))).ToArray());
        }






        private void ConvertWordToPdf(string wordFilePath, string pdfFilePath)
        {
            // Open the Word document using FileStream
            using (FileStream fileStream = new FileStream(wordFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // Use WordDocument from Syncfusion.DocIO.NET
                using (Syncfusion.DocIO.DLS.WordDocument wordDocument = new Syncfusion.DocIO.DLS.WordDocument(fileStream, Syncfusion.DocIO.FormatType.Docx))
                {
                    // Initialize the DocIORenderer
                    using (DocIORenderer renderer = new DocIORenderer())
                    {
                        // Set the rendering settings (optional)
                        renderer.Settings.EmbedFonts = true;

                        // Convert the Word document to a PDF document
                        using (Syncfusion.Pdf.PdfDocument pdfDocument = renderer.ConvertToPDF(wordDocument))
                        {
                            // Save the PDF document to the specified file path using a FileStream
                            using (FileStream pdfStream = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write))
                            {
                                pdfDocument.Save(pdfStream);
                            }
                        }
                    }
                }
            }
        }

    }
}
