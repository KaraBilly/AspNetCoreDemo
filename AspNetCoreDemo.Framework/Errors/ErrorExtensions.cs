using System;
using System.Collections.Generic;
using System.Text;

namespace ND.ManagementSvcs.Framework.Errors
{
    public static class ErrorExtensions
    {
        public static ServiceError WithMessageParameters(this ServiceError error, params object[] args)
        {
            var result = error.Copy();
            result.Message = string.Format(error.Message, args);
            return result;
        }

        public static ServiceError Copy(this ServiceError error)
        {
            return new ServiceError
            {
                StatusCode = error.StatusCode,
                Code = error.Code,
                Message = error.Message,
                Data = error.Data
            };
        }

        public static ServiceException ToException(this ServiceError error)
        {
            return new ServiceException(error);
        }
    }
}
