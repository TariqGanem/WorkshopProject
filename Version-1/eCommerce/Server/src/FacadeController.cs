using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Server.src
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    internal class FacadeController : ApiController
    {
    }
}
