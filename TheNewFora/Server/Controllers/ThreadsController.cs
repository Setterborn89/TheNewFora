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
    public class ThreadsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public ThreadsController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ThreadModel>>> GetAsync()
        {
            List<ThreadModel> list = await _context.Threads.ToListAsync();
            return Ok(list);
        }

        [HttpGet]
        [Route("getbyinterestid")]
        public async Task<ActionResult> GetByInterestId([FromQuery]int id)
        {
            return Ok(await _context.Threads.Where(x => x.InterestId == id).ToListAsync());
        }
        [HttpGet]
        [Route("getpostsbyid")]
        public async Task<ActionResult> GetPostsByIdAsync([FromQuery]int id)
        {
            return Ok(await _context.Messages.Where(x => x.ThreadId == id).CountAsync());
        } 

        [HttpGet("{id}")]
        public async Task<ActionResult<ThreadModel>> GetAsync(int id)
        {
            return Ok(await _context.Threads.Where(x => x.Id == id).Include("Messages").FirstOrDefaultAsync());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ThreadModel thread)
        {
            await _context.Threads.AddAsync(thread);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] ThreadModel thread)
        {
            _context.Threads.Update(thread);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            _context.Threads.Remove(thread);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
