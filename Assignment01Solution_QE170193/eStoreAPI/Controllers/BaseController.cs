using Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using System.Net;

namespace eStoreAPI.Controllers
{
    [ApiController]
    public class BaseController<TController> : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BaseController<TController>> _logger;

        protected BaseController(IMediator mediator, ILogger<BaseController<TController>> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        private BadRequestObjectResult ValidateRequest<TRequest, TResponse>()
            where TRequest : class, IRequest<TResponse>
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(x => x.Errors.Select(e => e.ErrorMessage))
                    .ToList();

                _logger.LogWarning("Request validation failed for {ControllerName}: {ErrorMessages}",
                        typeof(TController).Name,
                        string.Join(", ", errors));

                var statusCode = Shared.Constants.StatusCode.ModelInvalid;

                return BadRequest(new ApiResponse<TRequest>
                {
                    StatusCode = (int)statusCode,
                    Message = ResponseMessages.GetMessage(statusCode),
                    Errors = errors
                });

            }

            return null!;
        }

        protected async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(TRequest request)
            where TRequest : class, IRequest<TResponse>
        {
            var validationResult = ValidateRequest<TRequest, TResponse>();
            if (validationResult != null)
            {
                return validationResult;
            }

            try
            {
                var response = await _mediator.Send(request);

                var successCode = Shared.Constants.StatusCode.RequestProcessedSuccessfully;

                return Ok(
                    new ApiResponse<TResponse>
                    {
                        StatusCode = (int)successCode,
                        Data = response,
                        Message = ResponseMessages.GetMessage(successCode)
                    }
                );
            }
            catch (Exception ex)
            {
                return HandleError<TResponse>(ex);
            }
        }

        private IActionResult HandleError<TResponse>(Exception ex)
        {
            if (ex is AuthException authException)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = (int)authException.StatusCode,
                    Message = authException.Message,
                    Errors = authException.Errors
                });
            }

            if (ex is AppException exception)
            {
                return Ok(new ApiResponse<TResponse>
                {
                    StatusCode = (int)exception.StatusCode,
                    Message = exception.Message,
                    Errors = exception.Errors
                });
            }

            _logger.LogError(ex, "An error occurred while processing request for {ControllerName}", typeof(TController).Name);

            var statusCode = (int)HttpStatusCode.InternalServerError;

            return StatusCode(statusCode, new ApiResponse<TResponse>
            {
                StatusCode = statusCode,
                Message = ex.Message,
                Errors = ex.StackTrace?.Split("\n")
            });
        }
    }
}
