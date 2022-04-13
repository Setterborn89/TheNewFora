
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TheNewFora.Server.Data;
using TheNewFora.Shared;

namespace ForaForum.Server.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInterestsController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public UserInterestsController(AuthDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserInterestModel>>> GetAsync([FromQuery] string id)
        {
            List<UserInterestModel> list = await _context.UsersInterests.Where(x => x.UserId == id).ToListAsync();
            return Ok(list);
        }
        [HttpGet]
        [Route("getall")]
        public async Task<ActionResult<List<UserInterestModel>>> GetAllAsync()
        {
            return Ok(await _context.UsersInterests.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            return Ok(await _context.UsersInterests.FindAsync(id));
        }

        [HttpGet("{interestId}")]
        [Route("finduserinterest")]
        public ActionResult<UserInterestModel>? FindAsync([FromQuery]string userId,[FromRoute]int interestId)
        {
            return Ok(_context.UsersInterests.Where(x =>
            x.UserId == userId &&
            x.InterestId == interestId)
                .FirstOrDefault());
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] UserInterestModel userInterest)
        {
            await _context.UsersInterests.AddAsync(userInterest);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync(UserInterestModel userInterest)
        {
            _context.UsersInterests.Update(userInterest);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync([FromBody]UserInterestModel userInterest)
        {
            _context.UsersInterests.Remove(userInterest);
            return Ok(await _context.SaveChangesAsync());
        }
    }
        
}
