using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Kernel.Pdf.Action;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace WpfApplication2
{
    public struct margin
    {
        float x;
        float y;
        float width;
        float height;
    };
    class CreateForm
    {
        public const String DEST = "results/chapter04/job_application.pdf";

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdf(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            PageSize ps = PageSize.A4;
            pdf.SetDefaultPageSize(ps);
            // Initialize document
            Document document = new Document(pdf);
            AddAcroForm(document);
            //Close document
            document.Close();
        }

        public static PdfAcroForm AddAcroForm(Document doc)
        {
            iText.Kernel.Font.PdfFont regular = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.FontConstants.TIMES_ROMAN);
            iText.Kernel.Font.PdfFont bold = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.FontConstants.TIMES_BOLD);
            Paragraph TimeSignature = new Paragraph("report prodused: " + DateTime.Now.ToString()).SetTextAlignment(TextAlignment.LEFT).SetFontSize(10);
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("logo.png");
            logo.ScaleToFit(300f, 250f);
            logo.SpacingBefore = 50f;
            logo.SpacingAfter = 10f;
            logo.Alignment = iTextSharp.text.Element.ALIGN_CENTER;

            Paragraph title = new Paragraph("TWM Airlines").SetTextAlignment(TextAlignment.CENTER).SetFontSize
                (16).SetFont(regular).SetBold().SetUnderline();
            doc.Add(title);
            
            Paragraph par;
            par = new Paragraph("THIS AGREEMENT").SetFontSize(12).SetBold();
            par.Add(" made effective as of:").SetFontSize(12).SetFont(regular);
            doc.Add(par);
            doc.Add(new Paragraph("PARTIES").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("1.                                                       hereinafter referred to as the 'Employer'").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("2.                                                       hereinafter referred to as the 'Employee'").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("TERMS OF EMPLOYMENT").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("The terms of employment are as follows:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("1. Remuneration:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("Your base salary is $                    . Generally, your salary will be reviewed annually, but the Firm reserves the right to change your compensation from time to time on reasonable notice").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("2. Work hours:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("Employee will be required to work         hours per week. At times, the needs of the business may require that you work overtime.It is essential that you be available for overtime work.").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("3. Job description:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("The Employee's job will be as follows:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("").SetFontSize(12));
            doc.Add(new Paragraph("").SetFontSize(12));
            doc.Add(new Paragraph("4. Employee Benefits:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("The Employee will be granted:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("").SetFontSize(12));
            doc.Add(new Paragraph("").SetFontSize(12));
            doc.Add(new Paragraph("5. Essential Requirements for the Role:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("   (a) Employee must be fluent in:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("   (b) Employee must be skilled in:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("CONTRACT EFFECTIVE BETWEEN THE DATES:").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("Effective Starting:               ").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("Date of Termination:               ").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("SIGNATURES").SetFontSize(12)).SetFont(regular).SetBold();
            doc.Add(new Paragraph("SIGNED by the Employee:").SetFontSize(12)).SetFont(regular);
            doc.Add(new Paragraph("SIGNED BY THE EMPLOYER").SetFontSize(12)).SetFont(regular);
            //Add acroform
            PdfAcroForm form = PdfAcroForm.GetAcroForm(doc.GetPdfDocument(), true);
            //Create text field
            PdfTextFormField nameField = PdfTextFormField.CreateText(doc.GetPdfDocument(), new Rectangle(250, 755, 200,
                15), "date", "");
            form.AddField(nameField);
            nameField = PdfTextFormField.CreateText(doc.GetPdfDocument(), new Rectangle(55, 703, 150,
                15), "employerName", "");
            form.AddField(nameField);
            nameField = PdfTextFormField.CreateText(doc.GetPdfDocument(), new Rectangle(55, 678, 150,
                15), "employeeName", "");
            form.AddField(nameField);
            
           
            nameField = PdfTextFormField.CreateText(doc.GetPdfDocument(), new Rectangle(170, 201, 150,
                15), "contractEnds", "");
            form.AddField(nameField);
            return form;
        }

    }
}
