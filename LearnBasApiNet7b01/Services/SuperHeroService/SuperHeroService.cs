

using LearnBasApiNet7b01.Data;
using Microsoft.EntityFrameworkCore;

namespace LearnBasApiNet7b01.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private static List<SuperHero> supperHeroes = new List<SuperHero>
        {
            new SuperHero
                {
                    Id = 1,
                    Name= "Spider Man",
                    FirstName="Phuc",
                    LastName="Do",
                    Place= "Ho Chi Minh City"
                },
                new SuperHero
                {
                    Id = 2,
                    Name= "Bat Man",
                    FirstName="TestFirst01",
                    LastName="TestLast01",
                    Place= "Bangkok City"
                },
                new SuperHero
                {
                    Id = 3,
                    Name= "Ant Man",
                    FirstName="TedtFirst02",
                    LastName="TestLast02",
                    Place= "Bengjing City"
                },
                new SuperHero
                {
                    Id = 4,
                    Name= "Iron Man",
                    FirstName="TestFirst03",
                    LastName="TestLast03",
                    Place= "Seoul City"
                },
        };

        private readonly DataContext _dataContext;

        public SuperHeroService(DataContext dataContext)
        {
            _dataContext= dataContext;
        }

        public SuperHero AddHero(SuperHero newHero)
        {
            if (newHero is null)
            {
                return null;
            }
            

            supperHeroes.Add(newHero);
            return newHero;

        }

        public async Task<SuperHero?> AddHeroAsync(SuperHero newHero)
        {
            if (newHero is null)
            {
                return null;
            }

            _dataContext.SuperHeroes.Add(newHero);
            await _dataContext.SaveChangesAsync();

            return newHero;
            /*
                        _dataContext.SuperHeroes.Add(newHero);
                        await _dataContext.SaveChangesAsync();
                        return newHero;*/
        }

        public List<SuperHero>? DeleteHero(int id)
        {
            var heroExisted = supperHeroes.Find(x=> x.Id == id);
            if (heroExisted is null) 
            {
                return null;
            }
            supperHeroes.Remove(heroExisted);

            return supperHeroes;
        }

        public async Task<List<SuperHero>?> DeleteHeroAsync(int id)
        {
            var heroExisted = await _dataContext.SuperHeroes.FindAsync(id);
            if (heroExisted is null)
            {
                return null;
            }
            _dataContext.SuperHeroes.Remove(heroExisted);

            await _dataContext.SaveChangesAsync();

            return await _dataContext.SuperHeroes.ToListAsync();
        }

        public async Task<List<SuperHero>> GetAllHeroesAsync()
        {
            var heroes = await _dataContext.SuperHeroes.ToListAsync();
            return heroes;
        }

        public List<SuperHero> GetAllHeros()
        {
            return supperHeroes;
        }

        public SuperHero GetSingleHeroById(int id)
        {
            var userById = supperHeroes.Find(x => x.Id == id);
            if (userById is null)
            {
                return null;
            }
            return userById;
        }

        public async Task<SuperHero?> GetSingleHeroByIdAsync(int id)
        {
            var userById = await _dataContext.SuperHeroes.FindAsync(id);
            if (userById is null)
            {
                return null;
            }
            return userById;
        }

        public List<SuperHero>? UpdateSupperHero(int id, SuperHero hero)
        {
            var heroIsExisted = supperHeroes.Find(x => x.Id == id);
            if (heroIsExisted is null)
            {
                return null;
            }

            heroIsExisted.FirstName = hero.FirstName;
            heroIsExisted.LastName = hero.LastName;
            heroIsExisted.Name = hero.Name;
            heroIsExisted.Place = hero.Place;

            return supperHeroes;
        }

        public async Task<List<SuperHero>?> UpdateSupperHeroAsync(int id, SuperHero hero)
        {
            var heroIsExisted = await _dataContext.SuperHeroes.FindAsync(id);
            if (heroIsExisted is null)
            {
                return null;
            }

            heroIsExisted.FirstName = hero.FirstName;
            heroIsExisted.LastName = hero.LastName;
            heroIsExisted.Name = hero.Name;
            heroIsExisted.Place = hero.Place;

            await _dataContext.SaveChangesAsync();

            return await _dataContext.SuperHeroes.ToListAsync();
        }
    }
}
