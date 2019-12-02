using MigraDoc.DocumentObjectModel;
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

namespace Lista_de_Presencia {
    class PDFManager 
    {
        public static void CreateExample()
        {
            Document document = CreateDocument();
            // Create a renderer for the MigraDoc document.
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer();

            // Associate the MigraDoc document with a renderer
            pdfRenderer.Document = document;

            // Layout and render document to PDF
            pdfRenderer.RenderDocument();

            // Save the document
            const string filename = "Example.pdf";
            pdfRenderer.PdfDocument.Save(filename);
            // Open it in a viewer
            Process.Start(filename);
        }

        public static Document CreateDocument()
        {
            Document document = new Document();            
            document.DefaultPageSetup.Orientation = Orientation.Landscape;

            // Each MigraDoc document needs at least one section.
            Section section = document.AddSection();

            Image imageMANS = section.Headers.Primary.AddImage("../../../logo_mansalesmans.png");
            imageMANS.Height = "5cm";
            imageMANS.LockAspectRatio = true;
            imageMANS.RelativeVertical = RelativeVertical.Line;
            imageMANS.RelativeHorizontal = RelativeHorizontal.Margin;
            imageMANS.Top = ShapePosition.Top;
            imageMANS.Left = ShapePosition.Left;
            imageMANS.WrapFormat.Style = WrapStyle.Through;

            // Put a logo in the header
            Image imageCAIXA = section.Headers.Primary.AddImage("../../../logo_proinfancia.jpg");
            imageCAIXA.Height = "2.5cm";
            imageCAIXA.LockAspectRatio = true;
            imageCAIXA.RelativeVertical = RelativeVertical.Line;
            imageCAIXA.RelativeHorizontal = RelativeHorizontal.Margin;
            imageCAIXA.Top = ShapePosition.Top;
            imageCAIXA.Left = ShapePosition.Right;
            imageCAIXA.WrapFormat.Style = WrapStyle.Through;

            // Create the text frame for the address
            TextFrame addressFrame;
            addressFrame = section.AddTextFrame();
            addressFrame.Height = "3.0cm";
            addressFrame.Width = "7.0cm";
            addressFrame.Left = ShapePosition.Left;
            addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
            addressFrame.Top = "4.5cm";
            addressFrame.RelativeVertical = RelativeVertical.Page;

            // Address
            Paragraph paragraph = addressFrame.AddParagraph("C. del Foc 100-106 Barcelona 08038");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Email
            paragraph = addressFrame.AddParagraph("direccio@mansalesmans.org");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.Font.Underline = Underline.Dash;
            paragraph.Format.SpaceAfter = 3;

            // Phone number
            paragraph = addressFrame.AddParagraph("Tel.932237923 / 625218941");
            paragraph.Format.Font.Name = "Times New Roman";
            paragraph.Format.Font.Size = 7;
            paragraph.Format.SpaceAfter = 3;

            // Create footer
            paragraph = section.Footers.Primary.AddParagraph();
            paragraph.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Add the print date field
            paragraph = section.AddParagraph();
            paragraph.Format.SpaceBefore = "8cm";
            paragraph.Style = "Reference";
            paragraph.AddFormattedText("INVOICE", TextFormat.Bold);
            paragraph.AddTab();
            paragraph.AddText("Cologne, ");
            paragraph.AddDateField("dd.MM.yyyy");

            // Create the item table
            Table table;
            table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = new Color(128, 128, 128);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;

            // Before you can add a row, you must define the columns
            Column column = table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("2.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("3.5cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            column = table.AddColumn("2cm");
            column.Format.Alignment = ParagraphAlignment.Center;

            column = table.AddColumn("4cm");
            column.Format.Alignment = ParagraphAlignment.Right;

            // Create the header of the table
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = new Color(0, 0, 255);
            row.Cells[0].AddParagraph("Item");
            row.Cells[0].Format.Font.Bold = false;
            row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[0].MergeDown = 1;
            row.Cells[1].AddParagraph("Title and Author");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].MergeRight = 3;
            row.Cells[5].AddParagraph("Extended Price");
            row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
            row.Cells[5].MergeDown = 1;

            row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = new Color(0, 0, 255);
            row.Cells[1].AddParagraph("Quantity");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[2].AddParagraph("Unit Price");
            row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph("Discount (%)");
            row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[4].AddParagraph("Taxable");
            row.Cells[4].Format.Alignment = ParagraphAlignment.Left;

            table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, Color.Empty);

            return document;
        }
    }
}
