using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using LMS.Model;
namespace LMS.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private JwtSettings setting;


        private ILMSDataStore _dbstore;
        public UserController(ILMSDataStore dbstore, IOptions<JwtSettings> options)
        {
            _dbstore = dbstore;
            setting = options.Value;
        }

        // GET: api/<controller>
        [HttpGet("adminInfo")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAdminInfo()
        {
            IActionResult result;
            var getUser = _dbstore.GetUserByName("admin");
            if (getUser != null)
            {
                result = Ok(getUser);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // GET: api/<controller>
        [HttpGet("generalInfo/{UserName}")]
        //[Authorize(Roles = "admin")]
        public IActionResult GetGeneralUserInfo(string UserName)
        {
            IActionResult result;
            var getUser = _dbstore.GetUserByName(UserName);
            if (getUser != null)
            {
                result = Ok(getUser);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/<controller>
        [HttpPost("initial")]
        public IActionResult UserLSInitial([FromBody]UserLS value)
        {
            UserLS newUserLS = UserLS.CreateNewUserLSFromBody(value);
            _dbstore.AddUser(newUserLS);
            return Ok();
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult UserLogin([FromBody]UserLS value)
        {
            string returnText;
            int result = _dbstore.ValidateLoginFromDB(value);

            string DefaultRoleInClaim = "default";

            switch (result)
            {
                case 0:
                    returnText = "User name error!";
                    return NotFound(returnText);

                case 1:
                    returnText = "User password error!";
                    return NotFound(returnText);

                case 5:
                    DefaultRoleInClaim = "admin";
                    break;

                case 2:
                    returnText = "Login sccussfully!";
                    DefaultRoleInClaim = "user, General";
                    break;

                default:
                    returnText = "Login error!";
                    return NotFound(returnText);

            }
            var claims = new Claim[]
            {
                        new Claim(ClaimTypes.Name, value.UserName),
                        new Claim(ClaimTypes.Role, DefaultRoleInClaim)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: setting.Issuer,
                audience: setting.Audience,
                claims: claims,
                //DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), Id = "df", Role = "ff" } );
            //return Ok( new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }
/*
        [HttpPost("register")]
        public String UserRegister([FromBody]User value)
        {
            if (_dbstore.UserEmailCheck(value))
            {
                _dbstore.AddEmailAndPassword(value);
            }
            else
            {
                return ("Alreay exited email!");
            }
            return "register!";
        }

        [HttpPost("verification")]
        public String Post2([FromBody]String value)
        {
            return "verification!";
        }
*/
    }
}
