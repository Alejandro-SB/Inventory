using FluentValidation;

namespace Inventory.API.Authentication.Login
{
    /// <summary>
    /// Class that acts as a validator for the LoginModel
    /// </summary>
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        /// <summary>
        /// Creates an instance of the LoginModelValidator class
        /// </summary>
        public LoginModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cannot be empty");
        }
    }
}