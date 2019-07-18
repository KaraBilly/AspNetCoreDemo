using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDemo.Dtos
{
    public abstract class RequestBase
    {
        public const string SubsidiaryName = "subsidiary";
        private const string PlatformName = "platform";
        private const string RequestIdName = "request_id";
        private string _platform;
        private Guid? _requestId;
        private string _subsidiary;

        /// <summary>
        ///     The subsidiary code, the default value is CN (header).
        /// </summary>
        [FromHeader(Name = SubsidiaryName)]
        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string Subsidiary
        {
            get => _subsidiary?.ToUpper() ?? "CN";
            // ReSharper disable once MemberCanBeProtected.Global
            set => _subsidiary = value?.ToUpper() ?? "CN";
        }

        /// <summary>
        ///     Currently only support mina, mdm_h5 (header).
        /// </summary>
        [FromHeader(Name = PlatformName)]
        [Required]
        public string Platform
        {
            get => _platform?.ToLower() ?? "mina";
            set => _platform = value?.ToLower() ?? "mina";
        }

        /// <summary>
        ///     Request identity, to track request sent from client.
        ///     Will generate request id(GUID).
        ///     If consumer doesn't provide or provides unexpected id(No GUID type) (header).
        /// </summary>
        [FromHeader(Name = RequestIdName)]
        public Guid? RequestId
        {
            get
            {
                if (_requestId == null)
                {
                    _requestId = Guid.NewGuid();
                    return _requestId;
                }

                return _requestId;
            }
            set => _requestId = value;
        }
    }
}
