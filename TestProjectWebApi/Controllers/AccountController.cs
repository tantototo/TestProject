using Microsoft.AspNetCore.Mvc;
using TestProjectWebApi.Data;
using TestProjectWebApi.Dtos;
using TestProjectWebApi.Models;
using TestProjectWebApi.Services;

namespace TestProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices services;

        public AccountController(AppDBContext context)
        {
            services = new AccountServices(context);
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AccountDto>> Get(int id)
        {
            var account = services.Get(id);
            if(account == null)
            {
                return BadRequest("Acc not found.");
            }
            return Ok((account).AsDto());
        }

        // POST api/<AccountController>
        [HttpPost("{personId}")]
        public ActionResult<AccountDto> Post(int personId, CreateAccountDto dto)
        {
            Account account = new()
            {
                AccNumber = services.GenerateAccNumber(),
                Sum = dto.Sum,
                PersonId = personId
            };
            services.Add(account);

            return CreatedAtAction(nameof(Get), new {id= account.Id}, account.AsDto());
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id},{sum}")]
        public ActionResult Update(int id, int sum)
        {
            var account = services.Get(id);
            if(account == null)
            {
                return NotFound();
            }

            if (sum > 0)
                services.Put(account, sum);
            else
                services.Withdraw(account, Math.Abs(sum));
            return NoContent();
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var account = services.Get(id);
            if (account == null)
            {
                return NotFound();
            }
            services.Delete(account);
            return NoContent();
        }
    }
}
