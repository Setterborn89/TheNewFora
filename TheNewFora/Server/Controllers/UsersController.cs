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
        public ActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<ApplicationUser> Get(string id)
        {
            var user = _context.Users.Where(x => x.UserName == id).Include(x => x.UserInterests).FirstOrDefault();
            return Ok(user);
        }

        [HttpGet]
        [Route("getuserbyid")]
        public ActionResult<string> GetByIdAsync([FromQuery]string id)
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
        public ActionResult<string> GetUserImageByIdAsync([FromQuery] string id)
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
        public ActionResult<bool> GetBanDeletFlagAsync([FromQuery]string id)
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
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] ApplicationUser user)
        {
            _context.Users.Update(user);
            var result = await _context.SaveChangesAsync();
            return result>1 ? Ok() : NotFound();
        }
    }
}
