using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using NaturalLanguageGeometry.API.Models;
using NaturalLanguageGeometry.Parser;

namespace NaturalLanguageGeometry.API.Controllers
{
    public class ParseController : ApiController
    {
        /// <summary>
        /// Parser
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GeometryResponse Post([FromBody]ParseRequest request)
        {
            return NaturalLanguageGeometry.Parser.NLPGP.Parse(request?.Text);
        }
        
    }
}
