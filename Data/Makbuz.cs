using System.ComponentModel.DataAnnotations;

namespace SiteYonetimApp.Models
{
    public class Makbuz
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Makbuz No zorunludur.")]
        public string MakbuzNo { get; set; }

        [Required(ErrorMessage = "Ad alanı boş bırakılamaz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad alanı boş bırakılamaz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Daire No boş bırakılamaz.")]
        public string DaireNo { get; set; }      
        public int? BlokId { get; set; }

        [Required(ErrorMessage = "Aidat Dönemi seçilmelidir.")]
        public DateTime? AidatDonemi { get; set; }

        [Required(ErrorMessage = "Aidat Tutarı boş bırakılamaz.")]
        public decimal? AidatTutari { get; set; }

        public DateTime? Demirbasonemi { get; set; }
        public decimal? DemirbasTutari { get; set; }   
        
        public Blok Blok { get; set; }      
     
        public decimal? AidatFaizTutari { get; set; }
        public decimal? ToplamTutar { get; set; }
        public string TahsilEtmeText { get; set; }
        public bool Banka { get; set; }
        public bool Nakit { get; set; }

        public string OdemeyiAlanAd { get; set; }
        public string OdemeyiAlanSoyad { get; set; }
        public string OdemeyiAlanImza { get; set; }
        public DateTime? TahsilatTarih { get; set; }

    }
}
