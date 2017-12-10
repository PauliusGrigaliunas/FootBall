using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Razor.Generator;

namespace WebApplication.Controllers
{
    public class ValuesController : ApiController
    {
        private static List<string> Abc = new List<string>()
        {
            "value0",
            "value1",
            "value2",
            "value3",
            "value4",
            "value5"
        };
        //public string[] Abc { get; set; } = new string[] {  };

        // GET api/values
        public IEnumerable<string> Get()
        {
            return Abc;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return Abc[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            Abc.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            Abc[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            Abc.RemoveAt(id);
        }
    }
}
