using MonoDay2.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MonoDay2.WebApi.Controllers
{
    public class ValuesController : ApiController
    {
        public static List<Club> clubs = new List<Club>();

        [HttpGet]
        [Route("initializeList")]
        // GET initializeList
        public HttpResponseMessage Initalize()
        {
            if (clubs.Count != 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Objects exist in List");
            }
            else
            {
                clubs.Add(new Club { Id = 1, Name = "Chelsea", Location = "England" });
                clubs.Add(new Club { Id = 2, Name = "Barcelona", Location = "Spain" });
                clubs.Add(new Club { Id = 3, Name = "Inter", Location = "Italy" });
                return Request.CreateResponse(HttpStatusCode.OK, clubs);
            }
        }
        
        [HttpGet]
        [Route("getAll")]
        // GET getAll
        public HttpResponseMessage Get()
        {
            if (clubs.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, clubs);
            }
        }

        [HttpGet]
        [Route("get")]
        // GET get?id={id}
        public HttpResponseMessage Get([FromUri] int id)
        {
            if (clubs.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list!");
            }
            else if(clubs.FirstOrDefault(clubId => clubId.Id == id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, clubs);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, clubs.FirstOrDefault(clubId => clubId.Id == id));
            }
            
        }

        [HttpPost]
        [Route("post")]
        // POST post
        public HttpResponseMessage Post([FromBody] Club club)
        {
            if (club == null) 
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, clubs);
            }
            else if(club.Name == null || club.Location == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name and Location not speciefed");
            }
            else if(clubs.FirstOrDefault(clubId => clubId.Id == club.Id) != null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, clubs);
            }
            else
            {
                clubs.Add(club);
                return Request.CreateResponse(HttpStatusCode.OK, clubs);
            }
        }

        [HttpPut]
        [Route("put")]
        // PUT put?id={id}&name={name}&location={location}
        public HttpResponseMessage Put([FromUri]int id, [FromUri] string name, [FromUri] string location)
        {
            Club club = clubs.FirstOrDefault(clubId => clubId.Id == id);
            if(clubs.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, clubs);
            }
            else if((name == null) || (location == null))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Name or Location not specified");
            }
            else if(clubs.FirstOrDefault(clubId => clubId.Id == id) == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Object under speciefed is not found");
            }
            else
            {
                club.Name = name;
                club.Location = location;
                return Request.CreateResponse(HttpStatusCode.OK, clubs);
            }
        }

        [HttpDelete]
        [Route("delete")]
        // DELETE delete?id={id}
        public HttpResponseMessage Delete([FromUri]int id)
        {
            if (clubs.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No objects in list");
            }
            else if(!clubs.Any(p=>p.Id==id))
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Object under this id does not exist in List");
            }
            else
            {
                clubs.Remove(clubs.First(clubId => clubId.Id == id));
                return Request.CreateResponse(HttpStatusCode.OK, clubs);
            }
        }

    }
}
