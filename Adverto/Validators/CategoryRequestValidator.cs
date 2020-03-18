using Adverto.Dto.Category;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(c=>c.Name)
                .NotEmpty()
                 .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
