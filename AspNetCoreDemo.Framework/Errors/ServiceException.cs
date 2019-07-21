using System;


namespace AspNetCoreDemo.Framework.Errors
{
    public class ServiceException : Exception
    {
        public ServiceException(ServiceError error)
        {
            Error = error;
        }

        public ServiceError Error { get; set; }
    }
}
