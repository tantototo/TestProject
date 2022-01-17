using Microsoft.AspNetCore.Mvc;
using TestProjectWebApi.Data;
using TestProjectWebApi.Dtos;
using TestProjectWebApi.Models;
using TestProjectWebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonServices services;

        public PersonController(AppDBContext context)
        {
            services = new PersonServices(context);
        }
        //public PersonController(IPersonServices _services)
        //{
        //    services = _services;
        //}

        // GET: api/<PersonController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDto>>> Get()
        {
            return Ok(services.Get().Select(person => person.AsDto()));
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = services.Get(id);
            if(person == null)
            {
                return BadRequest("Person not found.");
            }
            return Ok(person.AsDto());
        }

        // POST api/<PersonController>
        [HttpPost]
        public ActionResult<PersonDto> Post(CreatePersonDto dto)
        {
            Person person = new()
            {
                Name = dto.Name,
                Age = dto.Age,
                Passport = dto.Passport
            };
            services.Add(person);

            return CreatedAtAction(nameof(Get), new {id=person.Id}, person.AsDto());
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public ActionResult Update(int id, UpdatePersonDto dto)
        {
            var person = services.Get(id);
            if(person == null)
            {
                return NotFound();
            }
            person.Name = dto.Name;
            person.Age = dto.Age;
            person.Passport = dto.Passport;

            services.Update(person);
            return NoContent();
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var person = services.Get(id);
            if (person == null)
            {
                return NotFound();
            }
            services.Delete(person);
            return NoContent();
        }
    }
}
