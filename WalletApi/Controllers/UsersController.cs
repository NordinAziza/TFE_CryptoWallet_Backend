using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUsersRepository _repository ;
        private readonly IHashService _hashService;
        #endregion
        #region Constructor
        public UsersController(IUsersRepository repository , IHashService hashService)
        {
            _repository = repository;
            _hashService = hashService;
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
        #endregion
    }
}
