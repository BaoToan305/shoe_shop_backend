using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using shoe_shop_backend.Application.ResquestResponse;

namespace shoe_shop_backend.Extensions.Filters
{
    public class ApiResponseWrapperFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context.Result is ObjectResult objectResult
                && objectResult.Value is not null
                && objectResult.Value.GetType().Name != nameof(ApiResponse<object>))
            {
                var wrapped = ApiResponse<object>.Success(objectResult.Value);
                context.Result = new ObjectResult(wrapped) { StatusCode = objectResult.StatusCode ?? 200 };
            }

            await next();
        }
    }
}
