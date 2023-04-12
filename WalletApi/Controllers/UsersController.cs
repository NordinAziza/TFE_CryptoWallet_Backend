using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WalletApi.Domain;
using WalletApi.DTOS;
using WalletApi.Utilities;

namespace WalletApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Fields
        //Dependency Injection in the controller's constructor
        private readonly IUsersRepository _repository;
        private readonly IHashService _hashService;
        private readonly IJwtUtility _jwtUtility;
        private readonly string _jwtSecret;

        public UsersController(IUsersRepository repository, IHashService hashService, IJwtUtility jwtUtility, IConfiguration config)
        {
            _repository = repository;
            _hashService = hashService;
            _jwtUtility = jwtUtility;
            _jwtSecret = config.GetValue<string>("JwtSecret");
        }

        #endregion

        #region Methods
        // get all users or user if id passed from the url fromQuery 
        [HttpGet]
        public IActionResult GetUsers([FromQuery] int? userId) //?=> optional parameter
        {
            var UsersList = _repository.GetAll(userId);
            var model = UsersList.Select(item => new {  id = item.Id, Username = item.Username, Email = item.Email, Password = item.Password ,Address = item.Address }).ToList() ;
            return this.Ok(model);
        }
        // Add the user in the db with the passsword cyphered
        [HttpPost]
        public IActionResult Register(UsersDto userDto)
        {
            IActionResult result = BadRequest();

            string hashedPassword = _hashService.HashPassword(userDto.Password);

            Users addUser = _repository.Register(new Users()
            {
                Address = userDto.Address,
                Username = userDto.Username,
                Email = userDto.Email,
                Password = hashedPassword,
            });

            _repository.UnitOfWork.SaveChanges();

            if (addUser != null)
            {
                userDto.Id = addUser.Id;
                result = Ok(userDto);
            }

            return result;
        }
        [HttpPost("login")]
        public IActionResult Login(AuthUsersDto loginDto)
        {
            var user = _repository.GetUserByEmail(loginDto.Email);

            if (user == null || !_hashService.VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized();
            }

            var token = _jwtUtility.GenerateJwtToken(user);

            return Ok(new { token });
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return Unauthorized();
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_jwtSecret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var email = jwtToken.Claims.First(x => x.Type == "email").Value;

                var user = _repository.GetUserByEmail(email);

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        #endregion
    }
}
