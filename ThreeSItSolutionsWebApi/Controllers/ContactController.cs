using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreeSItSolutionsWebApi.Models;

namespace ThreeSItSolutionsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly Db3SItSoultion _context;

        public ContactController(Db3SItSoultion context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/values/5
        [HttpGet("{id}", Name = "GetbyID")]
        
        public string GetbyID(int id)
        {
            return "value";
        }

        //This action at /Person/IndexFromBody can bind JSON 
        [Route("setcontact"), HttpPost]
        // POST: api/values
        public void Post([FromBody] string value)
        {
        }
        // PUT: api/values/5
        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/values/5
        [HttpDelete]
        public void Delete(int id)
        {
        }
        //public IActionResult Index(MContactUs mEnquiry)
        //{
        //    if (mEnquiry == null)
        //    {
        //        return NotFound("Bad Request");
        //    }
        //    return Ok();

        //}
    }
}
