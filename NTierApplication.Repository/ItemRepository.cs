using Microsoft.EntityFrameworkCore;
using NTierApplication.DataAccess;
using NTierApplication.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierApplication.Repository
{
    public class ItemRepository : IItemRepository
    {
        private MainContext DbContext;

        public ItemRepository(MainContext dbContext)
        {
            DbContext = dbContext;
        }

        public IQueryable<Item> GetAll() {
            return DbContext.Items;
        }

        public void Delete(Item item)
        {
            if (DbContext.Entry(item).State!= EntityState.Deleted)
            {
                DbContext.Items.Remove(item);
            }
        }

        public void Insert(Item item)
        {
            DbContext.Items.Add(item);
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public void Update(Item item)
        {
            if (DbContext.Entry(item).State != EntityState.Modified)
            {
                DbContext.Entry(item).State = EntityState.Modified;
            }
        }
    }
}
