using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonDatabaseContext _context;

        public PersonController(PersonDatabaseContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var persons = _context.Persons;
            return Ok(persons);
        }
    }
}