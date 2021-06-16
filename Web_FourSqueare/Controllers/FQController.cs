using Microsoft.AspNetCore.Mvc;
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
        // GET: <FQController>
        [HttpGet]
        public ResponseModel Get()
        {
            return new FQModel().GetAll();
        }

        // GET api/<FQController>/5
       /* [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }*/

        // POST api/<FQController>
       /* [HttpPost]
        public void Post([FromBody] string value)
        {
        }*/

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
