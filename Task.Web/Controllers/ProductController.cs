using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Commands;
using Task.Application.Queries;

namespace Task.Web.Controllers
{
    [Authorize]
    public class ProductController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetList(GetAllProductQuery query)
        {
            var response = await _mediator.Send(query);
            return Json(new
            {
                draw = query.Draw,
                recordsTotal = response.TotalRecord,
                recordsFiltered = response.TotalRecord,
                data = response.datas,
            });
        }
        [HttpPost]
        public async Task<JsonResult> Create(CreateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Json(response);
        }
        [HttpPost]
        public async Task<JsonResult> Update(UpdateProductCommand command)
        {
            var response = await _mediator.Send(command);
            return Json(response);
        }
        [HttpGet]
        public async Task<JsonResult> Delete(Guid Id)
        {
            return Json(await _mediator.Send(new DeleteProductCommand() { Id = Id }));
        }

    }
}
