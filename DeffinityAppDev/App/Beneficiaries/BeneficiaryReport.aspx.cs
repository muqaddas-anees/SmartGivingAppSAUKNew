
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;

using System.Web.UI.WebControls;

using DeffinityAppDev.App.Beneficiaries.Entities;
using iTextSharp.text;

using iTextSharp.text.pdf;

namespace DeffinityAppDev.App.Beneficiaries

{
    public partial class BeneficiaryReport : System.Web.UI.Page
    {
        protected void ProducePdfReport_Click(object sender, EventArgs e)
        {
            // Get selected dates and options from UI controls
            DateTime frmDate = DateTime.Parse(fromDate.Text);
            DateTime tDate = DateTime.Parse(toDate.Text);
            bool includePersonalInfo = personalInfo.Checked;
            bool includeContacts = contacts.Checked;
            bool includeSupport = supportReceived.Checked;
            bool includeActivity = activity.Checked;
            bool includeCommunication = communication.Checked;

            // Define the file path where the PDF will be saved
            string pdfFilePath = Server.MapPath("~/Reports/BeneficiaryReport.pdf");

            // Call the method to generate the PDF report
            GenerateReport(frmDate, tDate, includePersonalInfo, includeContacts, includeSupport, includeActivity, includeCommunication, pdfFilePath);

            // Check if the PDF file was created successfully
            if (File.Exists(pdfFilePath))
            {
                // Optionally, provide feedback or trigger download
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AppendHeader("Content-Disposition", $"attachment; filename=BeneficiaryReport_{DateTime.Now:yyyyMMdd}.pdf");
                Response.WriteFile(pdfFilePath);
                Response.Flush(); // Send the headers and content to the client
                Response.End(); // End the response
            }
            else
            {
                // Handle the case where the PDF was not created
                // Display an error message or log the error
                Response.Write("Error: The report could not be generated. Please try again.");
            }
        }


        public void GenerateReport(DateTime fromDate, DateTime toDate, bool includePersonalInfo, bool includeContacts, bool includeSupport, bool includeActivity, bool includeCommunication, string pdfFilePath)
        {
            // Create a new PDF document
            string directoryPath = Path.GetDirectoryName(pdfFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            Document pdfDoc = new Document(iTextSharp.text.PageSize.A4, 25, 25, 30, 30);

            try
            {
                using (var stream = new FileStream(pdfFilePath, FileMode.Create))
                {
                    // Initialize the PDF writer
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    // Add Title
                    var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);

                    Paragraph title = new Paragraph("Beneficiary Report", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    Paragraph dateRange = new Paragraph($"Date Range: {fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd}", normalFont);
                    dateRange.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(dateRange);
                    pdfDoc.Add(new Paragraph("\n"));

                    using (var context = new MyDatabaseContext())
                    {
                        // Add each section if selected
                        if (includePersonalInfo)
                        {
                            InsertSection(pdfDoc, "Personal Information", normalFont,
                                context.Beneficiaries
                                       .Where(b => b.CreatedAt >= fromDate && b.CreatedAt <= toDate && b.TithingDefaultDetailsID == sessionKeys.PortfolioID)
                                       .AsEnumerable()
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
                                           b.ProfileImage, // Include image
                                           b.DocumentFront, // Include image
                                           b.DocumentBack  // Include image
                                       })
                                       .ToList(), true); // Pass true for image sections
                        }

                        if (includeContacts)
                        {
                            InsertSection(pdfDoc, "Contacts", normalFont,
                                context.BeneficiaryContacts
                                       .Where(c => c.CreatedAt >= fromDate && c.CreatedAt <= toDate && c.TithingID == sessionKeys.PortfolioID)
                                       .AsEnumerable()
                                       .Select(c => new
                                       {
                                           c.FirstName,
                                           c.LastName,
                                           c.EmailAddress,
                                           c.CountryCode,
                                           c.PhoneNumber,
                                           c.Position,
                                           c.Notes
                                       })
                                       .ToList());
                        }

                        if (includeSupport)
                        {
                            InsertSection(pdfDoc, "Support Received", normalFont,
                                context.BeneficiaryDonations
                                       .Where(d => d.DonationDate >= fromDate && d.DonationDate <= toDate && d.TithingID == sessionKeys.PortfolioID)
                                       .AsEnumerable()
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
                                           d.DocumentUpload // Include image
                                       })
                                       .ToList(), true); // Pass true for image sections
                        }

                        if (includeActivity)
                        {
                            InsertSection(pdfDoc, "Activity", normalFont,
                                context.BeneficiaryActivities
                                       .Where(a => a.ActivityDate >= fromDate && a.ActivityDate <= toDate && a.TithingDefaultDetailsID == sessionKeys.PortfolioID)
                                       .AsEnumerable()
                                       .Select(a => new
                                       {
                                           a.ActivityDate,
                                           a.LoggedBy,
                                           a.ProgressDetails,
                                           a.ImageData // Include image
                                       })
                                       .ToList(), true); // Pass true for image sections
                        }

                        if (includeCommunication)
                        {
                            InsertSection(pdfDoc, "Communication", normalFont,
                                context.BeneficiariesFeedBack
                                       .Where(f => f.FeedbackDate >= fromDate && f.FeedbackDate <= toDate && f.TithingID == sessionKeys.PortfolioID)
                                       .AsEnumerable()
                                       .Select(f => new
                                       {
                                           f.FeedbackDate,
                                           f.FeedbackText,
                                           f.CreatedAt,
                                           f.Attachments // Include image
                                       })
                                       .ToList(), true); // Pass true for image sections
                        }
                    }

                    // Close the document in the 'using' block itself after adding content
                    pdfDoc.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it here
                throw;
            }
        }





        private void InsertSection(Document pdfDoc, string sectionTitle, Font font, IEnumerable<dynamic> records, bool includeImages = false)
        {
            // Section Header
            Paragraph sectionHeader = new Paragraph(sectionTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            sectionHeader.SpacingAfter = 15;
            pdfDoc.Add(sectionHeader);

            if (records.Any())
            {
                foreach (var record in records)
                {
                    // Create a new paragraph for each record in the section
                    Paragraph recordParagraph = new Paragraph();
                    recordParagraph.SpacingAfter = 10;

                    // Loop through the fields and add them to the paragraph
                    foreach (var prop in record.GetType().GetProperties())
                    {
                        var fieldName = prop.Name.Replace("_", " "); // Replace underscores for readability
                        var fieldValue = prop.GetValue(record);

                        if (fieldValue != null)
                        {
                            if (includeImages && fieldValue is byte[] imageData)
                            {
                                try
                                {
                                    // Convert the byte[] into an iTextSharp image
                                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageData);
                                    image.ScaleToFit(150f, 150f); // Scale the image if necessary
                                    pdfDoc.Add(image);
                                }
                                catch (Exception ex)
                                {
                                    recordParagraph.Add(new Chunk($"{fieldName}: [Invalid Image]\n"));
                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    // Append the field name followed by the value
                                    recordParagraph.Add(new Chunk($"{fieldName}: ", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                    recordParagraph.Add(new Chunk(fieldValue.ToString(), font));
                                    recordParagraph.Add(new Chunk("\n"));
                                }
                                catch (Exception ex)
                                {
                                    recordParagraph.Add(new Chunk($"{fieldName}: [Invalid Data]\n"));
                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }

                    // Add the record to the document
                    pdfDoc.Add(recordParagraph);
                }
            }
            else
            {
                pdfDoc.Add(new Paragraph("No data available for this section", new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC)));
            }

            // Add a separator line between sections
            pdfDoc.Add(new Paragraph("\n──────────────────────────────────────────────\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
        }


    }
}
