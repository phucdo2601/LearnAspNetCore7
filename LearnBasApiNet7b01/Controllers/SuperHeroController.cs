//using LearnBasApiNet7b01.Models;
using LearnBasApiNet7b01.Data;
using LearnBasApiNet7b01.Services.SuperHeroService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LearnBasApiNet7b01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;

        private readonly DataContext _dataContext;
        public SuperHeroController(ISuperHeroService superHeroService, DataContext dataContext)
        {
            _superHeroService = superHeroService;
            _dataContext = dataContext;
        }

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

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroes()
        {


            /*return Ok(supperHeroes);*/

            var heroList = _superHeroService.GetAllHeros();
            return Ok(heroList);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetSingleHero(int id)
        {
            /*var hero = supperHeroes.Find(x => x.Id == id);
            if (hero is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }
            return Ok(hero);*/

            var userById = _superHeroService.GetSingleHeroById(id);
            if (userById is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(userById);
        }

        [HttpPost("addNewHero")]
        public async Task<IActionResult> AddHero([FromBody] SuperHero hero)
        {
            /* if (hero is null)
             {
                 return BadRequest("The request object is not valid, please check again!");
             }

             var heroExistById = supperHeroes.Find(x => x.Id == hero.Id);
             var heroExistByName = supperHeroes.Find(x => x.Name == hero.Name);

             if (heroExistById is not null)
             {
                 return BadRequest("The hero with this id is existed, please check again!");

             }

             if (heroExistByName is not null)
             {
                 return BadRequest("The hero with this name is existed, please check again!");

             }

             supperHeroes.Add(hero);
             return Ok(hero);*/

            var heroExistById = supperHeroes.Find(x => x.Id == hero.Id);
            var heroExistByName = supperHeroes.Find(x => x.Name == hero.Name);

            if (heroExistById is not null)
            {
                return BadRequest("The hero with this id is existed, please check again!");
            }

            if (heroExistByName is not null)
            {
                return BadRequest("The hero with this name is existed, please check again!");
            }

            var addNewHero = _superHeroService.AddHero(hero);

            if (addNewHero is null)
            {
                return BadRequest("The request object is not valid, please check again!");
            }

            return Ok(addNewHero);
        }

        [HttpPut("updateHero/{id}")]
        public async Task<IActionResult> UpdateHeroById([FromRoute] int id, [FromBody] SuperHero hero)
        {
           /* var heroById = supperHeroes.Find(x => x.Id==id);
            if (heroById is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            heroById.FirstName = hero.FirstName;
            heroById.LastName = hero.LastName;  
            heroById.Name = hero.Name;
            heroById.Place= hero.Place;*/

            var result = _superHeroService.UpdateSupperHero(id, hero);
            if (result is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(result);
        }

        [HttpDelete("deleteHero/{id}")]
        public async Task<IActionResult> DeleteHeroById([FromRoute] int id)
        {
            /*var userExisted = supperHeroes.Find(x => x.Id == id);
            if (userExisted is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            } 
            supperHeroes.Remove(userExisted);
            return Ok("Delete hero is successfully!");*/

            var userDelete = _superHeroService.DeleteHero(id);
            if (userDelete is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(userDelete);
        }

        [HttpGet("async/getAllHeroes")]
        public async Task<ActionResult<List<SuperHero>>> GetAllHeroesAsync()
        {


            /*return Ok(supperHeroes);*/

            var heroList = await _superHeroService.GetAllHeroesAsync();
            return Ok(heroList);
        }

        [HttpGet("async/getHeroById/{id}")]
        public async Task<IActionResult> GetHeroByIdAsync([FromRoute] int id)
        {
            var userById = await _superHeroService.GetSingleHeroByIdAsync(id);
            if (userById is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(userById);
        }

        [HttpPost("async/addNewHero")]
        public async Task<IActionResult> AddHeroAsync([FromBody] SuperHero hero)
        {
            var heroExistById = await _superHeroService.GetSingleHeroByIdAsync(hero.Id);

            var heroExistByName = await _dataContext.SuperHeroes.SingleOrDefaultAsync(o => o.Name == hero.Name);
            if (heroExistById is not null)
            {
                return BadRequest("The hero with this id is existed, please check again!");

            }

            if (heroExistByName is not null)
            {
                return BadRequest("The hero with this name is existed, please check again!");

            }

            var addNewHero = await _superHeroService.AddHeroAsync(hero);

            if (addNewHero is null)
            {
                return BadRequest("The request object is not valid, please check again!");
            }

            return Ok(addNewHero);

            /*var result = await _superHeroService.AddHeroAsync(hero);
            return Ok(result);*/
        }

        [HttpPut("async/updateHero/{Id}")]
        public async Task<IActionResult> UpdateHeroByIdAsync([FromRoute(Name ="Id")] int id, [FromBody] SuperHero hero)
        {
            var result = await _superHeroService.UpdateSupperHeroAsync(id, hero);
            if (result is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(result);
        }

        [HttpDelete("async/deleteUser/{id}")]
        public async Task<IActionResult> DeleteHeroByIdAsync([FromRoute] int id)
        {
            var userDelete = await _superHeroService.DeleteHeroAsync(id);
            if (userDelete is null)
            {
                return NotFound("Sorry, but the hero does not exist.");
            }

            return Ok(userDelete);

        }
    }
}
