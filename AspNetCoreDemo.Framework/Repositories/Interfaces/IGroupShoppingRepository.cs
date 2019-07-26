using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreDemo.Framework.Repositories.Entities.GroupShopping;

namespace AspNetCoreDemo.Framework.Repositories.Interfaces
{
    public interface IGroupShoppingRepository
    {
        Task<List<GroupShoppingFound>> GetData();
        Task<GroupShoppingFound> GetDataById(int id);
    }
}
