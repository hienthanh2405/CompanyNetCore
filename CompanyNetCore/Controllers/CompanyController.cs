using System;
using System.Linq;
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
        public IActionResult Get([FromQuery]int offset = 1, [FromQuery]int limit = 10, 
            [FromQuery]string filterParams = null, [FromQuery]string sortParams = null)
        {
            int skip = (offset - 1) * limit;
            int total = _context.Companies.Count();

            var query = _context.Companies.AsQueryable();

            if (!string.IsNullOrEmpty(filterParams))
            {
                filterParams = filterParams.Trim().ToLowerInvariant();
                query = query.Where(c => c.Name.ToLowerInvariant().Contains(filterParams)
                                            || c.Address.ToLowerInvariant().Contains(filterParams));
            }

            if (!string.IsNullOrEmpty(sortParams))
            {
                switch (sortParams.ToLowerInvariant())
                {
                    case "id":
                        query = query.OrderBy(c => c.Id);
                        break;
                    case "name":
                        query = query.OrderBy(c => c.Name);
                        break;
                }
            }
            

            //select the companies based on paging parameter
            var companies = query
                .Skip(skip)
                .Take(limit)
                .ToList();

            // Return the list of customers
            return Ok(new PagedResult<Company>(companies, offset, limit, total));
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
            var companyOlder = _context.Companies.Where(c => c.Id == id).FirstOrDefault();
            
            companyOlder.Name = company.Name;
            companyOlder.Address = company.Address;
            companyOlder.Description = company.Description;

            _context.Update(companyOlder);
            _context.SaveChanges();

            return Ok(companyOlder);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var companyOlder = _context.Companies.Where(c => c.Id == id).FirstOrDefault();
            _context.Remove(companyOlder);
            _context.SaveChanges();

            return Ok(companyOlder);
        }
    }
}
