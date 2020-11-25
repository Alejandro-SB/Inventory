using FluentValidation;

namespace Inventory.API.Authentication.Login
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}