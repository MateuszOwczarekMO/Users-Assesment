using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands.Users;
using Users.Application.Queries.Users;
using Users.Application.Requests.Users;
using Users.Application.Responses.Commands.Users;
using Users.Application.Responses.Queries.Users;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IMapper _mapper;
        private readonly IMediator _mediator;
        private ILogger<UsersController> _logger;
        public UsersController(IMapper mapper, IMediator mediator, ILogger<UsersController> logger)
        {
            _mapper = mapper;
            _mediator = mediator;   
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllUsersQueryResponse>> GetAllUsers()
        {
            try
            {
                var response = await _mediator.Send(new GetAllUsersQuery());
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting data from the database: {ex}");

                return BadRequest(new GetAllUsersQueryResponse
                {
                    Message = "Error getting data!",
                    Success = false
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UpdateUsersTableCommandResponse>> UpdateUsersTable([FromBody] UpdateUsersTableRequest request)
        {
            try
            {
                var response = await _mediator.Send(new UpdateUsersTableCommand(request));
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving changes: {ex}");
                return BadRequest(new UpdateUsersTableCommandResponse
                {
                    Success = false,
                    Message = "Error saving changes! Try again or contact website administrator!"
                });
            }
        }
    }
}
