
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Task.Application.Commands
{
    public class CreateUserRegisterCommand:IRequest<bool>
    {
        public string? Name {  get; set; }
        [EmailAddress]
        public string Email {  get; set; }
        public string Password {  get; set; }
        [Display(Name ="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password Not Match")]
        public string ConfirmPassword {  get; set; }
    }
}
