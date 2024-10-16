
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;

using System.Web.UI.WebControls;

using DeffinityAppDev.App.Beneficiaries.Entities;
using iTextSharp.text;

using iTextSharp.text.pdf;
using PortfolioMgt.DAL;

namespace DeffinityAppDev.App.Beneficiaries

{
    public partial class BeneficiaryReport : System.Web.UI.Page
    {
        public static string file_section_beneficiary_doc = "beneficiarydoc";

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
                    using(var portfoliocontext=new PortfolioDataContext())
                    using (var context = new MyDatabaseContext())
                    {
                        // Add each section if selected
                        if (includePersonalInfo)
                        {
                            InsertSection(true,pdfDoc, "Personal Information", normalFont,
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
                                           b.ProfileImage,
                                           // Include multiple images up to 5 from the FileDatas for each beneficiary
                                           Images = portfoliocontext.FileDatas
                                                       .Where(f => f.FileID == b.PersonID.ToString() && f.Section == file_section_beneficiary_doc)
                                                       .Take(5)  // Limit to 5 images
                                                       .Select(f => f.FileData1.ToArray())  // Assuming FileData1 contains the image byte[]
                                                       .ToList() // Convert to list
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


        private void InsertSection(bool isPersonalInformation, Document pdfDoc, string sectionTitle, Font font, IEnumerable<dynamic> records, bool includeImages = false)
        {
            // Section Header
            Paragraph sectionHeader = new Paragraph(sectionTitle, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14));
            sectionHeader.SpacingAfter = 15;
            pdfDoc.Add(sectionHeader);

            if (records.Any())
            {
                foreach (var record in records)
                {
                    // Create a table with two columns for field name and value
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100; // Full width of the page
                    table.SetWidths(new float[] { 30f, 70f }); // Column widths for name and value (30% and 70%)

                    // Loop through the fields and add them to the table
                    foreach (var prop in record.GetType().GetProperties())
                    {
                        var fieldName = prop.Name.Replace("_", " "); // Replace underscores for readability
                        var fieldValue = prop.GetValue(record);

                        if (fieldValue != null)
                        {
                            // Handle list of images (FileDatas)
                            if (includeImages && fieldValue is List<byte[]> imageList)
                            {
                                PdfPCell nameCell = new PdfPCell(new Phrase(fieldName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                nameCell.Border = PdfPCell.NO_BORDER;
                                table.AddCell(nameCell);

                                PdfPCell imageCell = new PdfPCell();
                                imageCell.Border = PdfPCell.NO_BORDER;

                                foreach (var imageData in imageList)
                                {
                                    try
                                    {
                                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageData);
                                        image.ScaleToFit(150f, 150f);
                                        imageCell.AddElement(image);
                                    }
                                    catch (Exception ex)
                                    {
                                        imageCell.Phrase = new Phrase("[Invalid Image]", font);
                                        LogExceptions.WriteExceptionLog(ex);
                                        System.Diagnostics.Debug.WriteLine(ex.Message);
                                    }
                                }
                                table.AddCell(imageCell);
                            }
                            // Handle single ProfileImage
                            else if (includeImages && fieldValue is byte[] profileImageData)
                            {
                                PdfPCell nameCell = new PdfPCell(new Phrase(fieldName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                nameCell.Border = PdfPCell.NO_BORDER;
                                table.AddCell(nameCell);

                                PdfPCell imageCell = new PdfPCell();
                                imageCell.Border = PdfPCell.NO_BORDER;
                                try
                                {
                                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(profileImageData);
                                    image.ScaleToFit(150f, 150f);
                                    imageCell.AddElement(image);
                                }
                                catch (Exception ex)
                                {
                                    imageCell.Phrase = new Phrase("[Invalid Image]", font);
                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                                table.AddCell(imageCell);
                            }
                            else
                            {
                                // Add field name and value to the table
                                try
                                {
                                    PdfPCell nameCell = new PdfPCell(new Phrase($"{fieldName}:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                    nameCell.Border = PdfPCell.NO_BORDER;
                                    table.AddCell(nameCell);

                                    PdfPCell valueCell = new PdfPCell(new Phrase(fieldValue.ToString(), font));
                                    valueCell.Border = PdfPCell.NO_BORDER;
                                    table.AddCell(valueCell);
                                }
                                catch (Exception ex)
                                {
                                    PdfPCell errorCell = new PdfPCell(new Phrase($"{fieldName}: [Invalid Data]", font));
                                    errorCell.Colspan = 2; // Span across both columns for errors
                                    table.AddCell(errorCell);
                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }

                    // Add the table for the record to the document
                    pdfDoc.Add(table);

                    // Add some spacing after each record
                    pdfDoc.Add(new Paragraph("\n"));
                }
            }
            else
            {
                pdfDoc.Add(new Paragraph("No data available for this section", new Font(Font.FontFamily.HELVETICA, 12, Font.ITALIC)));
            }

            // Add a separator line between sections
            pdfDoc.Add(new Paragraph("\n──────────────────────────────────────────────\n", FontFactory.GetFont(FontFactory.HELVETICA, 8)));
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
                    // Create a table with two columns for field name and value
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100; // Make the table span the full width of the page
                    table.SetWidths(new float[] { 30f, 70f }); // Define relative widths of columns (30% and 70%)

                    // Loop through the fields and add them to the table
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
                                    image.ScaleToFit(100f, 100f); // Scale the image to fit in the table

                                    // Add the field name as a cell
                                    PdfPCell nameCell = new PdfPCell(new Phrase(fieldName, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                    nameCell.Border = PdfPCell.NO_BORDER; // No border for cleaner look
                                    table.AddCell(nameCell);

                                    // Add the image in the second column
                                    PdfPCell imageCell = new PdfPCell(image);
                                    imageCell.Border = PdfPCell.NO_BORDER;
                                    table.AddCell(imageCell);
                                }
                                catch (Exception ex)
                                {
                                    PdfPCell errorCell = new PdfPCell(new Phrase($"{fieldName}: [Invalid Image]", font));
                                    errorCell.Colspan = 2; // Span across both columns
                                    table.AddCell(errorCell);

                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                            else
                            {
                                try
                                {
                                    // Add the field name as a bold cell
                                    PdfPCell nameCell = new PdfPCell(new Phrase($"{fieldName}:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
                                    nameCell.Border = PdfPCell.NO_BORDER; // No border for cleaner look
                                    table.AddCell(nameCell);

                                    // Add the field value as a regular text cell
                                    PdfPCell valueCell = new PdfPCell(new Phrase(fieldValue.ToString(), font));
                                    valueCell.Border = PdfPCell.NO_BORDER;
                                    table.AddCell(valueCell);
                                }
                                catch (Exception ex)
                                {
                                    PdfPCell errorCell = new PdfPCell(new Phrase($"{fieldName}: [Invalid Data]", font));
                                    errorCell.Colspan = 2; // Span across both columns
                                    table.AddCell(errorCell);

                                    LogExceptions.WriteExceptionLog(ex);
                                    System.Diagnostics.Debug.WriteLine(ex.Message);
                                }
                            }
                        }
                    }

                    // Add the table for the record to the document
                    pdfDoc.Add(table);

                    // Add some spacing after each record
                    pdfDoc.Add(new Paragraph("\n"));
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
