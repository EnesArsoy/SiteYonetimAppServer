namespace SiteYonetimApp.Models
{
    public class Makbuz
    {
        public int Id { get; set; }
        public string MakbuzNo { get; set; }
        public string DaireNo { get; set; }
        public int BlokId { get; set; }
        public Blok Blok { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public DateTime? AidatDonemi { get; set; }
        public decimal? AidatTutari { get; set; }
        public DateTime? Demirbasonemi { get; set; }
        public decimal? DemirbasTutari { get; set; }
        public decimal? AidatFaizTutari { get; set; }
        public decimal? ToplamTutar { get; set; }
        public string TahsilEtmeText { get; set; }
        public bool Banka { get; set; }
        public bool Nakit { get; set; }

        public string OdemeyiAlanAd { get; set; }
        public string OdemeyiAlanSoyad { get; set; }
        public string OdemeyiAlanImza { get; set; }

    }
}
