using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TransIT.DAL.Models.ViewModels;

namespace TransIT.API.EndpointFilters.OnException
{
    public class DataTableFilterExceptionFilterAttribute : Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new DataTableResponseViewModel { Error = context.Exception.Message });
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}
