using TheNewFora.Server.Data;
using TheNewFora.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ForaForum.Server.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AuthDbContext _context;

        public UsersController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ApplicationUser>>> GetAsync()
        {
            List<ApplicationUser> list = new();
            list = await _context.Users.ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public ActionResult<ApplicationUser> Get(string id)
        {
            var user = _context.Users.Where(x => x.UserName == id).Include(x => x.UserInterests).FirstOrDefault();
            return Ok(user);
        }

        [HttpGet]
        [Route("getuserbyid")]
        public ActionResult<string> GetById([FromQuery]string id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user is not null)
            {
                return Ok(user.UserName);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpGet]
        [Route("getuserimagebyid")]
        public ActionResult<string> GetUserImageById([FromQuery] string id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user is not null)
            {
                return Ok(user.PfpUrl);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpGet]
        [Route("getbanflag")]
        public ActionResult<bool> GetBanDeletFlag([FromQuery]string id)
        {
            var user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            if(user is not null)
            {
                if (user.Banned || user.Deleted)
                {
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            else 
            { 
                return Ok(false); 
            } 
        }
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ApplicationUser user)
        {
            await _context.Users.AddAsync(user);
            return Ok(await _context.SaveChangesAsync());
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] ApplicationUser user)
        {
            _context.Users.Update(user);
            return Ok(await _context.SaveChangesAsync());
        }
    }
}
