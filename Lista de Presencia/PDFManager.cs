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
        // In Landscape orientation
        private static readonly float s_PageWidthCM = 29.7f;
        private static readonly float s_PageHeightCM = 21f;

        private static readonly float s_BoldBorderWidth = 1.25f;

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
            table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(s_PageWidthCM / 4f));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph();
            paragraph.AddFormattedText("Nom infant:", TextFormat.Bold);
            paragraph.Format.SpaceBefore = 12;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(4));
            paragraph.AddTab();
            paragraph.AddText("Child's name");

            // Service's name
            table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(s_PageWidthCM / 4f));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph();
            paragraph.AddFormattedText("Nom servei:", TextFormat.Bold);
            paragraph.Format.SpaceBefore = 5;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(4));
            paragraph.AddTab();
            paragraph.AddText("SERVICE'S NAME");

            // Group's ID
            table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(s_PageWidthCM / 4f));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph();
            paragraph.AddFormattedText("Número grup:", TextFormat.Bold);
            paragraph.Format.SpaceBefore = 5;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(4));
            paragraph.AddTab();
            paragraph.AddText("Group's ID");

            // Educator's name
            table = section.AddTable();
            table.AddColumn(Unit.FromCentimeter(s_PageWidthCM / 4f));
            row = table.AddRow();
            paragraph = row.Cells[0].AddParagraph();
            paragraph.AddFormattedText("Nom professional:", TextFormat.Bold);
            paragraph.Format.SpaceBefore = 5;
            paragraph.Format.ClearAll();
            paragraph.Format.AddTabStop(Unit.FromCentimeter(4));
            paragraph.AddTab();
            paragraph.AddText("Educator's name");
            paragraph.Format.SpaceAfter = 5;

            // Create the item table
            table = section.AddTable();
            table.Style = "Table";
            table.Format.Alignment = ParagraphAlignment.Center;
            table.Rows.HeightRule = RowHeightRule.AtLeast;
            table.Rows.Height = 20;
            table.Borders.Width = 0.25;
            //table.Borders.Left.Width = 0.5;
            //table.Borders.Right.Width = 0.5;
            table.Rows.LeftIndent = 0;
                        
            // Month name
            Column column = table.AddColumn("2.1cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            column.Borders.Width = s_BoldBorderWidth;

            // Month days
            for(int j=1; j<=31; j++)
            {
                column = table.AddColumn("0.6cm");
            }
            
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

            return document;
        }
    }
}
