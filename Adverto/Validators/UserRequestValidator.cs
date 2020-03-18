using Adverto.Dto.UserDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(c => c.Email)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(c=>c.Surrname)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(c => c.PhoneNumber)
                .NotEmpty()
                .MaximumLength(9)
                .Matches("^[0-9]*$");
        }
    }
}
