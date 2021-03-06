using TheNewFora.Server.Data;
using TheNewFora.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForaForum.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public MessagesController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<MessageModel>>> GetAsync()
        {
            List<MessageModel> list = new();
            list = await _context.Messages.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _context.Messages.FindAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] MessageModel message)
        {
            await _context.Messages.AddAsync(message);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] MessageModel message)
        {
            _context.Messages.Update(message);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if(message is not null)
            {
                _context.Messages.Remove(message);
                return Ok(await _context.SaveChangesAsync());
            }
            else
            {
                return NotFound();
            } 
        }
    }
}
