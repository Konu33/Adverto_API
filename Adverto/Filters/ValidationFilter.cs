using Adverto.ErrorModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(c => c.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(c => c.ErrorMessage)).ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var sub in error.Value)
                    {
                        var errorModel = new ErrorSkeleton
                        {
                            FieldName = error.Key,
                            Message = sub
                        };


                        errorResponse.Errors.Add(errorModel);

                    }
                }
                context.Result = new BadRequestObjectResult(errorResponse);
                return;

            }

            await next();
        }
    }
}
