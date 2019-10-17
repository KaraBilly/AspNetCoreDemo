using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.ManagementSvcs.Framework.Repositories.Entities.GroupShopping;
using ND.ManagementSvcs.Framework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ND.ManagementSvcs.Framework.Repositories
{
    public class GroupShoppingRepository : IGroupShoppingRepository
    {
        private readonly OrderContext _orderContext;

        public GroupShoppingRepository(OrderContext orderContext)
        {
            _orderContext = orderContext;
        }
        public async Task<List<GroupShoppingFound>> GetData()
        {
            return await _orderContext.GroupShoppingFound.ToListAsync();
        }

        public async Task<GroupShoppingFound> GetDataById(int id)
        {
            return await _orderContext.GroupShoppingFound.SingleOrDefaultAsync(x => x.GroupId == 1);
        }
    }
}
