using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Commands;
using Task.Application.Queries;
namespace Task.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult> GetList([FromForm] GetAllProductQuery query)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(query);
                if (response is not null)
                {
                    return Ok(response);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateQuantity([FromForm] UpdateProductCommand command)
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
        [HttpPost]
        public async Task<ActionResult> FilterByDate([FromForm] GetAllProductQueryByDate query)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(query);
                if (response is not null)
                {
                    return Ok(response);
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response)
                {
                    return Ok("Succesfully Create Product");
                }
            }
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete([FromForm] DeleteProductCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response)
                {
                    return Ok("Succesfully Delete Product");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
