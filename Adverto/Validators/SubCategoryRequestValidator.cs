using Adverto.Dto.SubCategoriesDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Validators
{
    public class SubCategoryRequestValidator : AbstractValidator<SubCategoryRequest>
    {
        public SubCategoryRequestValidator()
        {
            RuleFor(c=>c.Name)
                .NotEmpty()
                 .Matches("^[a-zA-Z0-9 ]*$");

            RuleFor(c => c.CategoryId)
                .NotEmpty();
        }
    }
}
