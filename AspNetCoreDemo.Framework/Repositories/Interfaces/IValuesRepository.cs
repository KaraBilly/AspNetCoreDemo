using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreDemo.Framework.Repositories.Entities;

namespace AspNetCoreDemo.Framework.Repositories.Interfaces
{
    public interface IValuesRepository
    {
        string GetValues(int value);
        Detail GetDetail();
    }
}
