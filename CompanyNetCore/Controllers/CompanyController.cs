using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyNetCore.Entities;
using CompanyNetCore.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public CompanyController(DatabaseContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get(int pageNo = 1, int pageSize = 10)
        {
            int skip = (pageNo - 1) * pageSize;

            int total = _context.Companies.Count();

            //select the companies based on paging parameter
            var companies = _context.Companies
                .OrderBy(c=>c.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // Return the list of customers
            return Ok(new PagedResult<Company>(companies, pageNo, pageSize, total));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var company = _context.Find(typeof(Company), id);
            return Ok(company);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Company company)
        {
            _context.Add(company);
            _context.SaveChanges();

            return Created("", company.Id);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]Company company)
        {
            var companyOlder = _context.Find(typeof(Company), id);
            companyOlder = new Company
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Description = company.Description
            };
            _context.Update(companyOlder);
            _context.SaveChanges();

            return Ok(companyOlder);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var companyOlder = _context.Find(typeof(Company), id);
            _context.Remove(companyOlder);
            _context.SaveChanges();

            return Ok(companyOlder);
        }
    }
}
