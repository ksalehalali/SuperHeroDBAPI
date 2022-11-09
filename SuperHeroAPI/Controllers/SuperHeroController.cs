using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero
                { Id = 1, Name = "Spider Man",
                FirstName = "Peter",
                LastName ="Parker",
                Palce ="New Yourk City"
                },
                 new SuperHero
                { Id = 2, Name = "Ironman",
                FirstName = "Tony",
                LastName ="Stark",
                Palce ="New Yourk aa"
                }
            };
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
    
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero superHero)
        {
            _dataContext.SuperHeroes.Add(superHero);
            await _dataContext.SaveChangesAsync();
            return Ok(superHero);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = _dataContext.SuperHeroes.FirstOrDefault(h => h.Id == id);
            if(hero == null)
            {
                return NotFound();
            }
            return Ok(hero);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero req)
        {
            var hero = _dataContext.SuperHeroes.FirstOrDefault(h => h.Id == req.Id);
            if (hero == null)
            {
                return NotFound();
            }

            hero.Name = req.Name;
            hero.FirstName = req.FirstName; 
            hero.LastName = req.LastName;   
            hero.Palce = req.Palce;
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = _dataContext.SuperHeroes.FirstOrDefault(h => h.Id == id);
            if (hero == null)
            {
                return NotFound();
            }

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }
    }
}
