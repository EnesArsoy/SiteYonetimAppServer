using SiteYonetimApp.Models;

namespace SiteYonetimApp.Services
{
    public interface IGeneralService
    {
        Task<List<Blok>> GetBloksAsync();
        bool SaveBloks(List<Blok> bloks);
        Task<bool> DeleteBlok(int id);
        Task<bool> SaveBloksAsync(List<Blok> bloks);
        Task<bool> AddBlokAsync(Blok newBlok);
        Task SaveBloksListsAsync(List<Blok> bloks);

        Task<List<Makbuz>> GetMakbuzlarAsync();
        Task<bool> DeleteMakbuz(int id);
        Task<bool> AddMakbuzAsync(Makbuz newMakbuz);
        Task SaveMakbuzlarListAsync(List<Makbuz> makbuzlar);
        Task<string> GeneratePDF(Makbuz makbuz);

    }
}
