using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Football;

namespace WebFootball.Controllers
{
    public class TeamController : ApiController
    {
       public IEnumerable<TeamsTable> Get()
        {
            using (FootballEntities1 contex = new FootballEntities1())
            {
                var teams = from i in contex.TeamsTables
                            select i;
                var list = teams.ToList();

                return list;
            }
            
        }


    }

}
