using System;


namespace ND.ManagementSvcs.Framework.Errors
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
