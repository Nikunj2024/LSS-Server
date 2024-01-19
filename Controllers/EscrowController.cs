
using LSS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LSS.Controllers
{
    [Route("api/")]
    [ApiController]
    public class EscrowController : Controller
    {
        [HttpGet("escrow")]
        public List<EscrowDetails> GetEscrowDetails()
        {
            List<EscrowDetails> eList = new List<EscrowDetails>(){

                new EscrowDetails("January", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("February", 100, 0, 0, 50, 100, 0, 0),
                new EscrowDetails("March", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("April", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("May", 100, 0, 100, 50, 0, 0, 0),
                new EscrowDetails("June", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("July", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("August", 100, 0, 0, 50, 100, 0, 0),
                new EscrowDetails("September", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("October", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("November", 100, 0, 0, 50, 0, 100, 0),
                new EscrowDetails("December", 100, 50, 0, 50, 0, 0, 0)
        };
            return eList;


        }
    }
}