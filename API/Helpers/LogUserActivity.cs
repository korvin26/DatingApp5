using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var result = await next();

            if (result.HttpContext.User.Identity != null
                && !result.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var userId = result.HttpContext.User.GetUserId();
            var repo = result.HttpContext.RequestServices.GetService<IUserRepository>();

            if (repo == null) return;

            var user = await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();

            return;
        }
    }
}
