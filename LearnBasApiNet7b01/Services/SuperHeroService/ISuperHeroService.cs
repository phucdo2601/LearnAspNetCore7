//using LearnBasApiNet7b01.Models;

namespace LearnBasApiNet7b01.Services.SuperHeroService
{
    public interface ISuperHeroService
    {
        List<SuperHero> GetAllHeros();

        SuperHero GetSingleHeroById(int id);

        SuperHero AddHero(SuperHero newHero);

        List<SuperHero> UpdateSupperHero(int id, SuperHero hero);

        List<SuperHero> DeleteHero(int id);

        Task<List<SuperHero>> GetAllHeroesAsync();

        Task<SuperHero?> GetSingleHeroByIdAsync(int id);

        Task<SuperHero?> AddHeroAsync(SuperHero newHero);
        Task<List<SuperHero>?> UpdateSupperHeroAsync(int id, SuperHero hero);
        Task<List<SuperHero>?> DeleteHeroAsync(int id);


    }
}
