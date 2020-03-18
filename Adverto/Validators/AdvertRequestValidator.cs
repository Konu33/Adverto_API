using Adverto.Dto.AdvertDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Validators
{
    public class AdvertRequestValidator : AbstractValidator<AdvertRequest>
    {
        public AdvertRequestValidator()
        {
            RuleFor(c=>c.Name)
                .NotEmpty()
                 .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(c => c.Prize)
                .NotEmpty();

                RuleFor(c => c.Description)
                .NotEmpty()
                .MaximumLength(200);
            RuleFor(c=>c.Location)
                .NotEmpty()
                 .Matches("^[a-zA-Z0-9 ]*$");
            RuleFor(c => c.UserId)
                .NotEmpty();
            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}
