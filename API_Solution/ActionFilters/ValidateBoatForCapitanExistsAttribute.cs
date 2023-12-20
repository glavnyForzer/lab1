using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API_Solution.ActionFilters
{
    public class ValidateBoatForCapitanExistsAttribute: IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryManager _repository;

        public ValidateBoatForCapitanExistsAttribute(ILoggerManager logger, IRepositoryManager repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var capitanId = (Guid)context.ActionArguments["capitanId"];
            var capitan = await _repository.Capitan.GetCapitanAsync(capitanId, false);
            if (capitan == null)
            {
                _logger.LogInfo($"Company with id: {capitanId} doesn't exist in the database.");
                return;
                context.Result = new NotFoundResult();
            }
            var id = (Guid)context.ActionArguments["id"];
            var boat = await _repository.Boat.GetBoatByIdAsync(capitanId, id, trackChanges);
            if (boat == null)
            {
                _logger.LogInfo($"Boat with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
            }
            else
            {
                context.HttpContext.Items.Add("boat", boat);
                await next();
            }
        }
    }
}
