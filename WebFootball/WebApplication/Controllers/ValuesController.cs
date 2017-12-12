using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Razor.Generator;
using Football;

namespace WebApplication.Controllers
{
    public class ValuesController : ApiController
    {
        /*private static List<string> Abc = new List<string>()
        {
            "value0",
            "value1",
            "value2",
            "value3",
            "value4",
            "value5"
        };*/

        // GET api/values
        public List<Game> Get()
        {
             using (Football1Entities context = new Football1Entities())
            {
             return context.Games.ToList();   
            }
            
        }

        // GET api/values/5
        public Game Get(int id)
        {
            using (Football1Entities context = new Football1Entities())
            {
                return context.Games.FirstOrDefault(c => c.Id == id);
            }
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            //Abc.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
           // Abc[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            //Abc.RemoveAt(id);
        }
    }
}
