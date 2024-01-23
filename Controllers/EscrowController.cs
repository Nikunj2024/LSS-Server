
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

                new EscrowDetails("January - 2024", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("February - 2024", 100, 0, 0, 50, 100, 0, 0),
                new EscrowDetails("March - 2024", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("April - 2024", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("May - 2024", 100, 0, 100, 50, 0, 0, 0),
                new EscrowDetails("June - 2024", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("July - 2024", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("August - 2024", 100, 0, 0, 50, 100, 0, 0),
                new EscrowDetails("September - 2024", 100, 50, 0, 50, 0, 0, 0),
                new EscrowDetails("October - 2024", 100, 0, 0, 50, 0, 0, 50),
                new EscrowDetails("November - 2024", 100, 0, 0, 50, 0, 100, 0),
                new EscrowDetails("December - 2024", 100, 50, 0, 50, 0, 0, 0)
        };
            return eList;


        }
    }
}