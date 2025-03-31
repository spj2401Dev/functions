using Functions.Server.Model;
using Functions.Server.Services;
using Functions.Shared.DTOs;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Functions.Server.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly BerufsmesseDbContext context;
        private readonly IMapper mapper;

        public PersonsController(BerufsmesseDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PersonDTO>>> GetPersons()
        {
            return mapper.Map<List<PersonDTO>>(context.Persons.ToList());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PersonDTO>> GetPersonById(Guid Id)
        {
            var person = await context.Persons.FindAsync(Id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<PersonDTO>(person));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PersonDTO person)
        {
            context.Persons.Add(mapper.Map<Person>(person));
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(PersonDTO person)
        {
            var dbPerson = await context.Persons.FindAsync(person.Id);

            if (dbPerson == null)
            {
                return NotFound();
            }

            mapper.From(person).AdaptTo(dbPerson);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var person = await context.Persons.FindAsync(Id);

            if (person == null)
            {
                return NotFound();
            }

            context.Persons.Remove(person);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
