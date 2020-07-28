using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ThreeSItSolutionsWebApi.Models;

namespace ThreeSItSolutionsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnquiryController : ControllerBase
    {
        private readonly Db3SItSoultion _context;

        public EnquiryController(Db3SItSoultion context)
        {
            _context = context;
        }
        //This action at /Person/IndexFromBody can bind JSON 
        [HttpPost]
        [Route("Index")]
        public IActionResult Index(MEnquiry mEnquiry)
        {
            if (mEnquiry == null)
            {
                return NotFound("Bad Request");
            }
            return Ok();
                
        }

       
    }
}
