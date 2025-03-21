using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Commands;
using Task.Application.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Task.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> GetList([FromForm] GetAllProductQuery query) {
            if (ModelState.IsValid)
            {
                var response=await _mediator.Send(query);
                if (response is not null) {
                    return Ok(response);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateQuantity([FromForm]UpdateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response)
                {
                    return Ok("Update Product Succesfully");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
