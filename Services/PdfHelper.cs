using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;
using SiteYonetimApp.Models;

namespace SiteYonetimAppServer.Services
{
    public class PdfHelper
    {
        public byte[] CreatePdf(string text)
        {
            // Yeni bir PDF belgesi oluşturuyoruz.
            PdfDocument document = new PdfDocument();
            // Yeni bir sayfa ekliyoruz.
            PdfPage page = document.AddPage();
            // Sayfa üzerinde çizim yapmak için XGraphics nesnesi oluşturuyoruz.
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Font ve yazı stili belirliyoruz
            XFont font = new XFont("Verdana", 20, XFontStyle.Bold);

            // Metni sayfaya ekliyoruz
            gfx.DrawString(text, font, XBrushes.Black, new XPoint(100, 100));

            // PDF'i byte dizisi olarak döndürüyoruz.
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms, false);
                return ms.ToArray();
            }
        }
        public byte[] CreateReceiptPdf(Makbuz makbuz)
        {
            try
            {
                System.Globalization.CultureInfo.DefaultThreadCurrentCulture = new System.Globalization.CultureInfo("tr-TR");
                // Yeni bir PDF belgesi oluşturuyoruz.
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Sayfa genişliği ve yüksekliğini alıyoruz.
                double pageWidth = page.Width;
                double pageHeight = page.Height;

                // Fontlar
                XFont titleFont = new XFont("Verdana", 14, XFontStyle.Bold);
                XFont regularFont = new XFont("Verdana", 12);
                XFont boldFont = new XFont("Verdana", 12, XFontStyle.Bold);

                // Sayfa düzeni için dikey ve yatay boşlukları ayarlıyoruz.
                double xPosCenter = (pageWidth - gfx.MeasureString("AİDAT VE DEMİRBAŞ TAHSİLAT MAKBUZU", titleFont).Width) / 2;
                double yPos = 30; // Başlık için sabit yPos değeri
                double xPos = 40;
                double lineSpacing = 20;

                // Başlık
                gfx.DrawString("AİDAT VE DEMİRBAŞ TAHSİLAT MAKBUZU", titleFont, XBrushes.Black, new XPoint(xPosCenter, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // Makbuz Numarası
                gfx.DrawString($"Makbuz No: {makbuz?.MakbuzNo}", boldFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // Blok ve Daire No
                gfx.DrawString($"Blok / Daire: {makbuz?.Blok?.BlokName} / {makbuz?.DaireNo}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // Müşteri Bilgileri
                gfx.DrawString($"Adı - Soyadı: {makbuz?.Ad} {makbuz?.Soyad}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                gfx.DrawString($"Tahsilat Tarih: {makbuz.TahsilatTarih.Value.ToString("dd.MM.yyyy")}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;
                // Aidat Dönemi ve Tutar
                gfx.DrawString($"Aidat Dönemi: {makbuz.AidatDonemiString}                 Tutarı: {makbuz?.AidatTutari?.ToString("C", new System.Globalization.CultureInfo("tr-TR"))}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;


                // Demirbaş Dönemi ve Tutar
                // Demirbaş Dönemi ve Tutarı yan yana
                if (makbuz.Demirbasonemi.HasValue)
                {
                    gfx.DrawString($"Demirbaş Dönemi: {makbuz?.Demirbasonemi?.ToString("dd.MM.yyyy")}           Tutarı: {makbuz?.DemirbasTutari?.ToString("C", new System.Globalization.CultureInfo("tr-TR"))}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                    yPos += lineSpacing;
                    yPos += lineSpacing;
                }
                

                // Faiz Tutarı
                if (makbuz.AidatFaizTutari.HasValue)
                {
                    gfx.DrawString($"Aidat Faiz Tutarı:                               {makbuz?.AidatFaizTutari?.ToString("C", new System.Globalization.CultureInfo("tr-TR"))}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                    yPos += lineSpacing;
                    yPos += lineSpacing;
                }

                // Toplam Tutar
                gfx.DrawString($"Toplam Tutar: {makbuz?.ToplamTutar:C}", boldFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // Ödeme Türü ve Bilgileri yan yana
                string paymentMethod = makbuz.Banka ? "Banka" : (makbuz.Nakit ? "Nakit" : "Belirtilmemiş");
                gfx.DrawString($"Ödeme Türü: {paymentMethod}", boldFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // Ödemeyi Alan Bilgileri
                gfx.DrawString($"Ödemeyi Alan: {makbuz?.OdemeyiAlanImza}", regularFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing;

                // İmza Alanı
                gfx.DrawString("İmza:", boldFont, XBrushes.Black, new XPoint(xPos, yPos));
                yPos += lineSpacing;
                yPos += lineSpacing; // Biraz boşluk bırakıyoruz
                // İmza Görselini Ekliyoruz
                string imzaPath = "wwwroot/OzferahImza.png";        
                if (File.Exists(imzaPath))
                {
                    XImage imzaImage = XImage.FromFile(imzaPath);
                    double imzaWidth = 120; // İmzanın genişliği (pixel cinsinden)
                    double imzaHeight = imzaImage.PixelHeight * imzaWidth / imzaImage.PixelWidth; // Oranlı yükseklik

                    gfx.DrawImage(imzaImage, xPos, yPos, imzaWidth, imzaHeight);
                }



                // Alt kısımda ortalanmış metin
                string footerText = "Not: Aidatlar her ayın 01-20'i arasında tahsil edilecektir. Ödemeyi zamanında yapmayanlardan KMK 20/C maddesi gereği %5 Geciktirme tazminatı ile masraf ve vekalet ücreti alınacaktır. Bilginize sunulur";
                double footerXPos = (pageWidth - gfx.MeasureString(footerText, regularFont).Width) / 2;
                double footerYPos = pageHeight - 100; // Sayfanın alt kısmına yakın bir konum

                // Metni satırlara bölme
                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect = new XRect(40, footerYPos, pageWidth - 80, pageHeight - footerYPos);
                tf.Alignment = XParagraphAlignment.Center;
                tf.DrawString(footerText, regularFont, XBrushes.Black, rect, XStringFormats.TopLeft);

                // PDF'i byte dizisi olarak döndürüyoruz.
                using (MemoryStream ms = new MemoryStream())
                {
                    document.Save(ms, false);
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
