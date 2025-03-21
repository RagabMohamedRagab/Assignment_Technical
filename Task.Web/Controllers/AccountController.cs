using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Commands;
using Task.Application.Queries;

namespace Task.Web.Controllers
{
    [Authorize]
    public class AccountController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Register(CreateUserRegisterCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response) return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError(string.Empty, "try again");
            return View(command);
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(command);
                if (response) return RedirectToAction("Index", "Product");
            }
            ModelState.AddModelError(string.Empty, "try again");
            return View(command);
        }
        [HttpGet]
        public  IActionResult Logout()
        {
            var response = _mediator.Send(new LogOutUserQuery()).Result;
            if (response) return RedirectToAction("Login", "Account");
            return RedirectToAction("Index", "Product");

        }

    }
}
