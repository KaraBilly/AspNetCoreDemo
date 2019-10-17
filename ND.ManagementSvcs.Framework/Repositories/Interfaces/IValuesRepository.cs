using System;
using System.Collections.Generic;
using System.Text;
using ND.ManagementSvcs.Framework.Repositories.Entities;

namespace ND.ManagementSvcs.Framework.Repositories.Interfaces
{
    public interface IValuesRepository
    {
        string GetValues(int value);
        Detail GetDetail();
    }
}
