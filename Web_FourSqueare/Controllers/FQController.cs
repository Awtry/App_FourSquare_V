using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_FourSqueare.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Web_FourSqueare.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FQController : ControllerBase
    {

        private readonly IConfiguration Configuration;

        public FQController(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // GET: <FQController>
        [HttpGet]
        public ResponseModel Get()
        {
            return new FQModel().GetAll(Configuration.GetConnectionString("MySQL"));
        }

        // GET api/<FQController>/5
        /* [HttpGet("{id}")]
         public string Get(int id)
         {
             return "value";
         }*/

        // POST api/<FQController>
        [HttpPost]
        public ResponseModel Post([FromBody] FQModel FQ)
        {
            return FQ.Add(Configuration.GetConnectionString("MySQL"));
        }

        // PUT api/<FQController>/5
        /* [HttpPut("{id}")]
         public void Put(int id, [FromBody] string value)
         {
         }*/

        // DELETE api/<FQController>/5
        /* [HttpDelete("{id}")]
         public void Delete(int id)
         {
         }*/
    }
}