using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CostController : ApiController
    {

        public List<Metal> mobj = new List<Metal>() {

            new Metal { Price = 31000 , Name = "Gold"},
             new Metal { Price = 10000 , Name = "Silver"}
        };

        [HttpGet]
        public IEnumerable<Metal> Get()
        {
            return mobj.ToList();

        }
        public IHttpActionResult Get(string name)
        {

            var met = mobj.Where(x =>   x.Name.Equals(name));
            if (met == null)
                return NotFound();
            return Ok(met);


        }


    }
}
