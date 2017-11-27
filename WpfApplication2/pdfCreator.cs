using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Pdfa;
using iText.Forms;
using iText.IO;
using System.IO;
using iText.Forms.Fields;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Windows.Forms;

namespace WpfApplication2
{
    class pdfCreator
    {
        /// <exception cref="System.IO.IOException"/>
        public pdfCreator(String str)
        {

            FileInfo file = new FileInfo(str);
            file.Directory.Create();
            CreatePdf(str);
        }

        /// <exception cref="System.IO.IOException"/>
        public virtual void CreatePdf(String dest)
        {
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            // Initialize document
            Document doc = new Document(pdf);
            PdfAcroForm form = CreateForm.AddAcroForm(doc);
            IDictionary<String, PdfFormField> fields = form.GetFormFields();
            PdfFormField toSet;
            fields.TryGetValue("date", out toSet);
            toSet.SetValue(DateTime.Now.ToString());
            

            doc.Close();
        }
    }
}
