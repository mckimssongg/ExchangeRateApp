using APITechTest.Utils;
using DB;
using DB.Models;
using DB.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APITechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private IConfiguration _configuration;
        private DI_MFSD_J_GarciaContext _context;

        public TransactionController(DI_MFSD_J_GarciaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Response<String>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transaction)
        {
            var rsp = new Response<String>();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity == null)
            {
                rsp.status = false;
                rsp.message = "no token found";
                return Ok(rsp);
            }
            var result = Jwt.validarToken(identity);

            if (result.status == false)
            {
                rsp.status = false;
                rsp.message = result.message;
                return Ok(rsp);
            }

            try {

                var dataToken = result.result;
                int userId = Convert.ToInt32(dataToken.UserID);
                int idRol = Convert.ToInt32(dataToken.RolID);
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

                if (currentUser != null)
                {
                    var transactionEntity = new Transaction
                    {
                        UserRefID = userId,
                        SourceCurrency = transaction.SourceCurrency,
                        DestinationCurrency = transaction.DestinationCurrency,
                        ExchangeRate = transaction.ExchangeRate,
                        DestinationExchangeRate = transaction.DestinationExchangeRate,
                        Amount = transaction.Amount,
                        CreatedDate = DateTime.Now
                    };

                    _context.Transactions.Add(transactionEntity);
                    await _context.SaveChangesAsync();

                    rsp.status = true;
                    rsp.message = "";
                    rsp.value = "Transaction created successfully";
                    return Ok(rsp);
                }
                else
                {
                    rsp.status = false;
                    rsp.message = "User not found";
                    return Ok(rsp);
                }
            }
            catch {
                rsp.status = false;
                rsp.message = "Error: server error";
                return Ok(rsp);
            }
        }
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionLog()
        {
            var rsp = new Response<List<TransactionDTO>>();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                rsp.status = false;
                rsp.message = "no token found";
                return Ok(rsp);
            }
            var result = Jwt.validarToken(identity);

            if (result.status == false)
            {
                rsp.status = false;
                rsp.message = result.message;
                return Ok(rsp);
            }
            try
            {

                var dataToken = result.result;
                int userId = Convert.ToInt32(dataToken.UserID);
                var currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userId);

                if (currentUser != null)
                {
                    var transactions = await _context.Transactions
                        .Where(t => t.UserRefID == currentUser.UserID)
                        .OrderByDescending(t => t.CreatedDate)
                        .Take(20)
                        .Select(t => new TransactionDTO
                        {
                            UserRefId = t.UserRefID,
                            UserName = t.User.Name,
                            SourceCurrency = t.SourceCurrency,
                            DestinationCurrency = t.DestinationCurrency,
                            ExchangeRate = t.ExchangeRate,
                            DestinationExchangeRate = t.DestinationExchangeRate,
                            Amount = t.Amount,
                            CreatedDate = t.CreatedDate
                        })
                        .ToListAsync();

                    rsp.status = true;
                    rsp.message = "Successfully";
                    rsp.value = transactions;
                    return Ok(rsp);
                }
                else
                {
                    rsp.status = false;
                    rsp.message = "User not found";
                    return Ok(rsp);
                }
            }
            catch
            {
                rsp.status = false;
                rsp.message = "Error: server error";
                return Ok(rsp);
            }
        }

    }
}
