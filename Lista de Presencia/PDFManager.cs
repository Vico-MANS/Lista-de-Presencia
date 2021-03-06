﻿using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using PdfSharp.Pdf.IO;
using System.IO;

namespace Lista_de_Presencia {
    class PDFManager 
    {
        // In Landscape orientation
        private static readonly float s_PageWidthCM = 29.7f;
        private static readonly float s_PageHeightCM = 21f;

        private static readonly float s_BoldBorderWidth = 1.25f;

        // Creates and displays the attendance sheet for one child
        public static void CreateSingleAttendanceSheet(int personID)
        {
            Document document = CreateDocument(personID);
            // Create a renderer for the MigraDoc document.
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

            // Associate the MigraDoc document with a renderer
            pdfRenderer.Document = document;

            // Layout and render document to PDF
            pdfRenderer.RenderDocument();
            
            // Save the document
            const string filename = "AttendanceSheet.pdf";
            pdfRenderer.PdfDocument.Save(filename);
            // Open it in a viewer
            Process.Start(filename);
        }

        // Creates a PDF containing the attendance sheet of every person
        public static void CreateMultipleAttendanceSheets(List<int> personIDs)
        {
            PdfDocumentRenderer pdfFinalRenderer = new PdfDocumentRenderer();
            pdfFinalRenderer.PdfDocument = new PdfDocument();
            string tmpFile = "tmp.pdf";
            foreach (int personID in personIDs)
            {
                Document doc = CreateDocument(personID);
                PdfDocumentRenderer pdfTempRenderer = new PdfDocumentRenderer();
                pdfTempRenderer.Document = doc;
                pdfTempRenderer.RenderDocument();
                // This seems weird but I don't know any way around it
                pdfTempRenderer.PdfDocument.Save(tmpFile);
                PdfDocument pdf = PdfReader.Open(tmpFile, PdfDocumentOpenMode.Import);
                pdfFinalRenderer.PdfDocument.AddPage(pdf.Pages[0]);
            }
            File.Delete(tmpFile);
            pdfFinalRenderer.PdfDocument.Save("AttendanceSheets.pdf");
            Process.Start("AttendanceSheets.pdf");
        }

        private static Document CreateDocument(int personID)
        {
            /**
             * RETRIEVE THE INFORMATION FROM THE DATABASE
             * */
            string personName, serviceName, groupID, educatorName;

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                SqlCommand command = new SqlCommand("SELECT p.LASTNAME+', '+p.FIRSTNAME as 'PERSON NAME', g.GRUPO_ID as 'GROUP ID', s.NAME as 'SERVICE NAME', (SELECT FIRSTNAME+' '+LASTNAME FROM PERSON WHERE PERSON_ID = g.ID_PERSON) as EDUCATOR "
                                                    +"FROM PERSON p, PERSON_GRUPO pg, GRUPO g, SERVICIO s "
                                                    +"WHERE p.PERSON_ID = @personID AND pg.ID_PERSON = p.PERSON_ID AND g.GRUPO_ID = pg.ID_GRUPO AND S.SERVICIO_ID = g.ID_SERVICIO", conn);
                command.Parameters.AddWithValue("personID", personID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    personName = reader["PERSON NAME"].ToString();
                    serviceName = reader["SERVICE NAME"].ToString();
                    groupID = reader["GROUP ID"].ToString();
                    educatorName = reader["EDUCATOR"].ToString();
                }
            }

            /**
             * CREATE THE PDF
            * */

            Document document = new Document();            
            document.DefaultPageSetup.Orientation = Orientation.Landscape;

            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            section.PageSetup.RightMargin = Unit.FromCentimeter(2);
            section.PageSetup.TopMargin = Unit.FromCentimeter(2);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2);

            // Add the MANS logo
            Image imageMANS = section.AddImage("../../../logo_mansalesmans.png");
            imageMANS.Height = "2cm";
            imageMANS.LockAspectRatio = true;
            imageMANS.WrapFormat.Style = WrapStyle.Through;

            // Add the CAIXA logo
            Image imageCAIXA = section.AddImage("../../../logo_proinfancia.jpg");
            imageCAIXA.Height = "2.5cm";
            imageCAIXA.LockAspectRatio = true;
            //imageCAIXA.RelativeVertical = RelativeVertical.Line;
            //imageCAIXA.RelativeHorizontal = RelativeHorizontal.Margin;
            //imageCAIXA.Top = ShapePosition.Top;
            imageCAIXA.Left = ShapePosition.Right;
            imageCAIXA.WrapFormat.Style = WrapStyle.Through;
            
            // Don't know how to do this properly, else the text starts on the images since they're WrapStyle.Through
            TextFrame textFrame = section.AddTextFrame();
            textFrame.Top = "2cm";
            textFrame.Height = 0;

            // Address
            Paragraph paragraph = section.AddParagraph("C. del Foc 100-106 Barcelona 08038");
            paragraph.Format.Font.Size = 8;
            paragraph.Format.SpaceAfter = 3;

            // Email
            paragraph = section.AddParagraph("direccio@mansalesmans.org");
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Font.Underline = Underline.Dash;
            paragraph.Format.SpaceAfter = 3;

            // Phone number
            paragraph = section.AddParagraph("Tel.932237923 / 625218941");
            paragraph.Format.Font.Size = 8;
            paragraph.Format.SpaceAfter = 3;

            // Title & Course year
            Table table = section.AddTable();
            table.AddColumn((s_PageWidthCM - section.PageSetup.LeftMargin.Centimeter - section.PageSetup.RightMargin.Centimeter) + "cm");
            Row row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph("FULL D'ASSISTÈNCIA");
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(19));
            paragraph.AddTab();
            paragraph.AddText("Curs 2019-2020");
            paragraph.Format.Font.Size = 15;
            paragraph.Format.Font.Bold = true;
            table.Borders.Width = 1;

            // Child's name
            AddInfoRow(section, "Nom infant:", personName);

            // Service's name
            AddInfoRow(section, "Nom servei:", serviceName);

            // Group's ID
            AddInfoRow(section, "Número grup:", groupID);

            // Educator's name
            paragraph = AddInfoRow(section, "Nom professional:", educatorName);
            paragraph.Format.SpaceAfter = 5;

            // Create the item table
            table = section.AddTable();
            table.Style = "Table";
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.HeightRule = RowHeightRule.AtLeast;
            table.Rows.Height = 20;
            table.Borders.Width = 0.25;
            table.Rows.LeftIndent = 0;
                        
            // Month name
            Column column = table.AddColumn("2.1cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column.Borders.Width = s_BoldBorderWidth;

            // Month days
            for(int j=1; j<=31; j++)
                column = table.AddColumn("0.6cm");
            
            // Family's signature
            column = table.AddColumn("2.45cm");

            // Educator's signature
            column = table.AddColumn("2.45cm");

            row = table.AddRow();
            row.Borders.Top.Width = s_BoldBorderWidth;
            row.Borders.Bottom.Width = s_BoldBorderWidth;
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = new Color(166, 166, 166);
            row.Cells[0].Shading.Color = new Color(255, 255, 255);
            int i;
            for(i=1; i<=31; i++)
            {
                row.Cells[i].AddParagraph(i.ToString());
                row.Cells[i].VerticalAlignment = VerticalAlignment.Center;
            }
            row.Cells[i].AddParagraph("Signatura professional");
            row.Cells[i].Borders.Left.Width = s_BoldBorderWidth;
            row.Cells[i + 1].AddParagraph("Signatura família (*)");
            row.Cells[i + 1].Borders.Right.Width = s_BoldBorderWidth;

            // TODO: This depends on when the child joined the Submari, sometimes September will be part of it sometimes it starts in October
            // Add the rows for every month
            string[] monthNames = new string[10] { "Setembre", "Octubre", "Noviembre", "Diciembre", "Gener", "Febrer", "Març", "Abril", "Maig", "Juny" };

            for(int j=0; j<10; j++)
            {
                row = table.AddRow();
                row.VerticalAlignment = VerticalAlignment.Center;
                paragraph = row.Cells[0].AddParagraph();
                row.Cells[0].Shading.Color = new Color(166, 166, 166);
                paragraph.AddFormattedText(monthNames[j], TextFormat.Bold);
                // 34 is the cell count in the rows: we have 1 month name + 31 days + 2 signatures
                row.Cells[34 - 2].Borders.Left.Width = s_BoldBorderWidth;
                row.Cells[34 - 1].Borders.Right.Width = s_BoldBorderWidth;
                if (j == 9)
                {
                    row.Cells[34 - 2].Borders.Bottom.Width = s_BoldBorderWidth;
                    row.Cells[34 - 1].Borders.Bottom.Width = s_BoldBorderWidth;
                }
            }
                        
            // Add annotation
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = 10;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(1.5));
            paragraph.AddTab();
            paragraph.AddFormattedText("(*) ", TextFormat.Bold);
            paragraph.AddText("Amb la signatura d'aquest llistat d'assistència declaro que he rebut el servei i no he efectuat cap pagament per tal que " +
                "els membres de la meva unitat familiar assisteixin a l'activitat del programa Caixa Proinfància que s'indica anteriorment.");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Add the date as a footer
            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.Format.Font.Size = 8;
            paragraph.Format.Alignment = ParagraphAlignment.Right;
            paragraph.AddDateField("dd/MM/yyyy");

            Color noWorkColor = new Color(100, 100, 100);
            Color holidayColor = new Color(150, 150, 150);

            // Getting the start and end date of this school year (Sep - Jun)
            DateTime startDate, endDate;
            // The school year started the same year as we are in
            if(DateTime.Now.Month >= 9)
            {
                startDate = new DateTime(DateTime.Now.Year, 09, 01);
                endDate = new DateTime(DateTime.Now.Year + 1, 06, 30);
            }
            // The school year started last year
            else
            {
                startDate = new DateTime(DateTime.Now.Year - 1, 09, 01);
                endDate = new DateTime(DateTime.Now.Year, 06, 30);
            }

            using (SqlConnection conn = new SqlConnection())
            {
                DatabaseConnection.OpenConnection(conn);

                /**
                 * SHOW THE WEEKENDS AND DAYS OUTSIDE OF THE MONTHS
                 * */

                SqlCommand command = new SqlCommand("SELECT DATE_ID AS DATE, WEEKDAY_ID AS WEEKDAY " +
                                                    "FROM CALENDAR WHERE DATE_ID BETWEEN " +
                                                    "CONVERT(VARCHAR(30), CAST(@start AS DATETIME), 102) AND CONVERT(VARCHAR(30), CAST(@end AS DATETIME), 102)", conn);
                command.Parameters.AddWithValue("start", startDate);
                command.Parameters.AddWithValue("end", endDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // When we switch to the next month we gray out all the cells that weren't in the previous month
                    int lastDay = 31;
                    int monthOfLastDay = -1;
                    while (reader.Read())
                    {
                        // We start on September and end in June, therefore we go from 09 to 06.
                        DateTime date = reader.GetDateTime(reader.GetOrdinal("DATE"));
                        int day = date.Day;
                        int month = date.Month;

                        // We started a new month
                        if (day == 1)
                        {                            
                            // We gray out all the cells that weren't part of the previous month
                            /*
                             * This is not the most optimal solution
                             * It is very costly because we had to retrieve all the days for the given range to be able to do that
                             * Normally we would've only retrieved the weekend days and therefore the query wouldn't have been that huge
                             * */
                            while(lastDay < 31)
                            {
                                lastDay++;
                                if(monthOfLastDay < 9)
                                    table.Rows[4 + monthOfLastDay].Cells[lastDay].Shading.Color = noWorkColor;
                                else
                                    table.Rows[monthOfLastDay - 8].Cells[lastDay].Shading.Color = noWorkColor;
                            }
                        }
                        
                        if(reader.GetByte(reader.GetOrdinal("WEEKDAY")) == 7 || reader.GetByte(reader.GetOrdinal("WEEKDAY")) == 1)
                        {
                            // We are in the next year. Jan - Jun
                            if (month < 9)
                                table.Rows[4 + month].Cells[day].Shading.Color = noWorkColor;
                            // Sep - Dec
                            else
                                table.Rows[month - 8].Cells[day].Shading.Color = noWorkColor;
                        }

                        lastDay = day;
                        monthOfLastDay = month;
                    }
                }

                /**
                 * SHOW THE HOLIDAYS (TODO: Find a way to join the query with the weekends one?)
                 * */

                command = new SqlCommand("SELECT DATE_DAY AS DATE " +
                                        "FROM PUBLIC_HOLIDAY " +
                                        "WHERE DATE_DAY BETWEEN " +
                                        "CONVERT(VARCHAR(30), CAST(@start AS DATETIME), 102) AND CONVERT(VARCHAR(30), CAST(@end AS DATETIME), 102)", conn);
                command.Parameters.AddWithValue("start", startDate);
                command.Parameters.AddWithValue("end", endDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // We start on September and end in June, therefore we go from 09 to 06.
                        DateTime date = reader.GetDateTime(reader.GetOrdinal("DATE"));
                        int day = date.Day;
                        int month = date.Month;
                        // We are in the next year. Jan - Jun
                        if (month < 9)
                            table.Rows[4 + month].Cells[day].Shading.Color = holidayColor;
                        // Sep - Dec
                        else
                            table.Rows[month - 8].Cells[day].Shading.Color = holidayColor;
                    }
                }

                /**
                 * RETRIEVE ATTENDANCE INFORMATION FROM THE DATABASE
                 * */

                command = new SqlCommand("SELECT DIA " +
                    "FROM PRESENCE " +
                    "WHERE ID_PERSON = @personID " +
                    "AND DIA BETWEEN CONVERT(VARCHAR(30), CAST(@start AS DATETIME), 102) AND CONVERT(VARCHAR(30), CAST(@end AS DATETIME), 102)", conn);
                command.Parameters.AddWithValue("personID", personID);
                command.Parameters.AddWithValue("start", startDate);
                command.Parameters.AddWithValue("end", endDate);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime(reader.GetOrdinal("DIA"));
                        int day = date.Day;
                        int month = date.Month;
                        // We are in the next year. Jan - Jun
                        if (month < 9)
                            table.Rows[4 + month].Cells[day].AddParagraph("X");
                        // Sep - Dec
                        else
                            table.Rows[month - 8].Cells[day].AddParagraph("X");
                    }
                }
            }
            return document;
        }

        private static Paragraph AddInfoRow(Section section, string infoName, string infoData)
        {
            Table table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(s_PageWidthCM / 2f));
            Row row = table.AddRow();
            Paragraph paragraph = row.Cells[0].AddParagraph();
            paragraph.AddFormattedText(infoName, TextFormat.Bold);
            paragraph.Format.SpaceBefore = 5;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(4));
            paragraph.AddTab();
            paragraph.AddText(infoData);
            return paragraph;
        }
    }
}
