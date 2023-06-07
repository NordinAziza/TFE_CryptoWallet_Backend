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
    public class TradeRequestController : ControllerBase
    {
        #region Fields
        //Dependency Injection in the controller's constructor
        private readonly ITradeRequestRepository _repository;
        private readonly IHashService _hashService;
        private readonly IJwtUtility _jwtUtility;
        private readonly string _jwtSecret;

        public TradeRequestController(ITradeRequestRepository repository, IHashService hashService, IJwtUtility jwtUtility, IConfiguration config)
        {
            _repository = repository;
            _hashService = hashService;
            _jwtUtility = jwtUtility;
            _jwtSecret = config.GetValue<string>("JwtSecret");
        }
        #endregion

        #region Methods
        // Add the user in the db with the passsword cyphered
        [HttpPost]
        public IActionResult AddTradeRequest(TradeRequestDto tradeRequestDto)
        {
            IActionResult result = BadRequest();

            TradeRequest tradeRequest = new TradeRequest()
            {
                User = tradeRequestDto.User,
                Date = tradeRequestDto.Date,
                TokenToTrade = tradeRequestDto.TokenToTrade,
                AmountToTrade = tradeRequestDto.AmountToTrade,
                TokenToReceive = tradeRequestDto.TokenToReceive,
                AmountToReceive = tradeRequestDto.AmountToReceive,
                Status = tradeRequestDto.Status,
            };

            _repository.AddTrade(tradeRequest);
            _repository.UnitOfWork.SaveChanges();

            if (tradeRequest.Id != 0)
            {
                tradeRequestDto.Id = tradeRequest.Id;
                result = Ok(tradeRequestDto);
            }

            return result;
        }
        [HttpPut]
        public IActionResult ChangeStatus(int id, string newStatus)
        {
            IActionResult result = BadRequest();

            var tradeRequest = _repository.UpdateStatus(id, newStatus);

            if (tradeRequest != null)
            {
                result = Ok();
            }

            return result;
        }
        [HttpGet]
        public IActionResult GetAllTrade()
        {
           var tradeList = _repository.GetAll().ToList();
            return Ok(tradeList);
        }
        #endregion

    }
}
