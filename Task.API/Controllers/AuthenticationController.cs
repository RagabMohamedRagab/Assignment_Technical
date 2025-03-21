using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Commands;
using Task.Application.Queries;

namespace Task.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AuthenticationController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Regsiter([FromForm]CreateUserRegisterCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response)
                {
                    var token = await _mediator.Send(new TokenUserQuery());
                    return Ok(token);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> login([FromForm] LoginUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response)
                {
                    var token = await _mediator.Send(new TokenUserQuery());
                    return Ok(token);
                }
            }
            return BadRequest(ModelState);
        }
       
    }
}
