using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ND.ManagementSvcs.Dtos;
using ND.ManagementSvcs.Framework.Errors;
using ND.ManagementSvcs.Objects;

namespace ND.ManagementSvcs.Controllers
{
    [Controller]
    public abstract class ControllerBase : Controller
    {
        protected virtual ObjectResult Do<T>(Func<T> doFunc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorCode = AllServiceErrors.InvalidDto.Code,
                    ErrorMessage = AllServiceErrors.InvalidDto.Message,
                    ErrorData = new
                    {
                        errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList()
                    }
                });
            }

            try
            {
                var result = doFunc();
                return new OkObjectResult(result);
            }
            catch (ServiceException ex)
            {
                switch (ex.Error?.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return GetBadRequest(ex);
                    case HttpStatusCode.ExpectationFailed:
                        return GetExpFailedRequest(ex);
                    case HttpStatusCode.NotFound:
                        return GetNotFound(ex);
                    default:
                        throw;
                }

            }
        }

        protected virtual async Task<ObjectResult> DoAsync<T>(Func<Task<T>> doFunc)
        {
            if (!ModelState.IsValid) return GetDefaultBadRequest();

            try
            {
                var result = await doFunc();
                return new OkObjectResult(result);
            }
            catch (ServiceException ex)
            {
                switch (ex.Error?.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return GetBadRequest(ex);
                    case HttpStatusCode.ExpectationFailed:
                        return GetExpFailedRequest(ex);
                    case HttpStatusCode.NotFound:
                        return GetNotFound(ex);
                    default:
                        throw;
                }
            }
        }

        private BadRequestObjectResult GetDefaultBadRequest()
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = AllServiceErrors.InvalidDto.Code,
                ErrorMessage = AllServiceErrors.InvalidDto.Message,
                ErrorData = new
                {
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList()
                }
            });
        }

        private BadRequestObjectResult GetBadRequest(ServiceException ex)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = ex.Error.Code,
                ErrorMessage = ex.Error.Message,
                ErrorData = ex.Error.Data
            });
        }

        private NotFoundObjectResult GetNotFound(ServiceException ex)
        {
            return NotFound(new ErrorResponse
            {
                ErrorCode = ex.Error.Code,
                ErrorMessage = ex.Error.Message,
                ErrorData = ex.Error.Data
            });
        }

        private ExpectationFailedObjectResult GetExpFailedRequest(ServiceException ex)
        {
            return ExpectationFailedRequest(new ErrorResponse
            {
                ErrorCode = ex.Error.Code,
                ErrorMessage = ex.Error.Message,
                ErrorData = ex.Error.Data
            });
        }
        protected virtual ExpectationFailedObjectResult ExpectationFailedRequest(object error)
        {
            return new ExpectationFailedObjectResult(error);
        }

    }
}
