using TheNewFora.Server.Data;
using TheNewFora.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForaForum.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class InterestsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public InterestsController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InterestModel>>> GetAsync()
        {
            List<InterestModel> list = await _context.Interests.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _context.Interests.FindAsync(id));
        }
        [HttpGet]
        [Route("getthreadsbyid")]
        public async Task<ActionResult> GetPostsByIdAsync([FromQuery] int id)
        {
            return Ok(await _context.Threads.Where(x => x.InterestId == id).CountAsync());
        }
        [HttpGet]
        [Route("getinterestname")]
        public async Task<ActionResult> GetInterestNameAsync([FromQuery] int id)
        {
            var interest = await _context.Interests.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(interest is not null)
            {
                return Ok(interest.Name);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]InterestModel interest)
        {
            await _context.Interests.AddAsync(interest);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(InterestModel interest)
        {
            _context.Interests.Update(interest);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var interest = await _context.Interests.FindAsync(id);
            if(interest is not null)
            {
                _context.Interests.Remove(interest);
                return Ok(await _context.SaveChangesAsync());
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
