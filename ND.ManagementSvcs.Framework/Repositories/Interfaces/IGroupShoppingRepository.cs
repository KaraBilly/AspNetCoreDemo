using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ND.ManagementSvcs.Framework.Repositories.Entities.GroupShopping;

namespace ND.ManagementSvcs.Framework.Repositories.Interfaces
{
    public interface IGroupShoppingRepository
    {
        Task<List<GroupShoppingFound>> GetData();
        Task<GroupShoppingFound> GetDataById(int id);
    }
}
