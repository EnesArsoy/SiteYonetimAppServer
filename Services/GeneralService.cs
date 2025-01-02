
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SiteYonetimApp.Models;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace SiteYonetimApp.Services
{
    public class GeneralService : IGeneralService
    {
        private readonly HttpClient _httpClient;
        private const string BloksFilePath = "wwwroot/Hubs/bloks.json";
        private const string MakbuzlarFilePath = "wwwroot/Hubs/makbuzlar.json";
       

        // private static readonly string BloksFilePath2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Hubs", "bloks.json");

    


        // JSON dosyasından blokları okuma
        public async Task<List<Blok>> GetBloksAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(BloksFilePath);
                var bloks = JsonSerializer.Deserialize<List<Blok>>(json);
                return bloks ?? new List<Blok>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri okuma hatası: {ex.Message}");
                return new List<Blok>();
            }
        }
        public async Task<bool> AddBlokAsync(Blok newBlok)
        {
            try
            {
                // Mevcut blokları oku
                var bloks = await GetBloksAsync();

                // Yeni blok ekle
                bloks.Add(newBlok);

                // Güncellenmiş listeyi dosyaya kaydet
                await SaveBloksListsAsync(bloks);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blok ekleme hatası: {ex.Message}");
                return false;
            }
        }

        // Güncellenmiş blokları JSON dosyasına yazma
        public async Task SaveBloksListsAsync(List<Blok> bloks)
        {
            try
            {
                var json = JsonSerializer.Serialize(bloks, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(BloksFilePath, json);
                var q = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri kaydetme hatası: {ex.Message}");
                throw;
            }
        }

        // JSON dosyasını kaydetmek için bir API veya sunucu tarafı işlem gerekebilir
        public Task<bool> SaveBloksAsync(List<Blok> bloks)
        {
            throw new NotImplementedException("Blazor WebAssembly'de dosya yazma işlemi desteklenmez. Sunucu taraflı bir çözüm gerekebilir.");
        }

        public bool SaveBloks(List<Blok> bloks)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteBlok(int id)
        {
            try
            {
                // Mevcut blokları oku
                var bloks = await GetBloksAsync();

                // Silinecek bloğu bul
                var blokToDelete = bloks.FirstOrDefault(b => b.Id == id);

                if (blokToDelete == null)
                {
                    // Blok bulunamazsa false döndür
                    return false;
                }

                // Bloğu sil
                bloks.Remove(blokToDelete);

                // Güncellenmiş listeyi dosyaya kaydet
                await SaveBloksListsAsync(bloks);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Blok silme hatası: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Makbuz>> GetMakbuzlarAsync()
        {
            try
            {
                var json = await File.ReadAllTextAsync(MakbuzlarFilePath);
                var makbuzlar = JsonSerializer.Deserialize<List<Makbuz>>(json);
                makbuzlar = makbuzlar.OrderByDescending(x => x.Id).ToList();
                return makbuzlar ?? new List<Makbuz>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri okuma hatası: {ex.Message}");
                return new List<Makbuz>();
            }
        }

        public async Task<bool> DeleteMakbuz(int id)
        {
            try
            {
                // Mevcut makbuzları oku
                var makbuzlar = await GetMakbuzlarAsync();

                // Silinecek makbuzu bul
                var makbuzToDelete = makbuzlar.FirstOrDefault(m => m.Id == id);

                if (makbuzToDelete == null)
                {
                    // Makbuz bulunamazsa false döndür
                    return false;
                }

                // Makbuzu sil
                makbuzlar.Remove(makbuzToDelete);

                // Güncellenmiş listeyi dosyaya kaydet
                await SaveMakbuzlarListAsync(makbuzlar);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Makbuz silme hatası: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AddMakbuzAsync(Makbuz newMakbuz)
        {
            try
            {
                // Mevcut makbuzları oku
                var makbuzlar = await GetMakbuzlarAsync();

                // Yeni makbuzu ekle
                makbuzlar.Add(newMakbuz);

                // Güncellenmiş listeyi dosyaya kaydet
                await SaveMakbuzlarListAsync(makbuzlar);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Makbuz ekleme hatası: {ex.Message}");
                return false;
            }
        }
        public async Task SaveMakbuzlarListAsync(List<Makbuz> makbuzlar)
        {
            try
            {
                var json = JsonSerializer.Serialize(makbuzlar, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(MakbuzlarFilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri kaydetme hatası: {ex.Message}");
                throw;
            }
        }


        public async Task<string> GeneratePDF(Makbuz makbuz)
        {
            //// PDF dosyasını oluştur
            //var pdfBytes = CreatePDF(makbuz);

            //// PDF dosyasını base64 formatına dönüştür
            //var base64 = Convert.ToBase64String(pdfBytes);

            //// Dosya adı oluştur
            //var fileName = $"Makbuz_{makbuz.MakbuzNo}.pdf";

            //// JavaScript ile PDF dosyasını indirmek için çağrı yap
            //await JSRuntime.InvokeVoidAsync("downloadFile", fileName, base64);

            return "";
        }


        private void CreatePDF(Makbuz makbuz)
        {
            try
            {
                //using var stream = new MemoryStream();
                //var document = Document.Create(container =>
                //{
                //    container.Page(page =>
                //    {
                //        page.Content().Column(column =>
                //        {
                //            column.Item().Text($"Makbuz No: {makbuz.MakbuzNo}");
                //            column.Item().Text($"Adı Soyadı: {makbuz.Ad} {makbuz.Soyad}");
                //            column.Item().Text($"Daire No: {makbuz.DaireNo}");
                //            column.Item().Text($"Toplam Tutar: {makbuz.ToplamTutar:C}");
                //            column.Item().Text($"Aidat Tutarı: {makbuz.AidatTutari:C}");
                //            column.Item().Text($"Demirbaş Tutarı: {makbuz.DemirbasTutari:C}");
                //        });
                //    });
                //});
                //document.GeneratePdf(stream);
                //return stream.ToArray();
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

    }
}
