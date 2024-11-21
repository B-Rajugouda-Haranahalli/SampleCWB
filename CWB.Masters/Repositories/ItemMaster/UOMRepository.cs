using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class UOMRepository : Repository<UOM>, IUOMRepository
    {
        private readonly DbSet<UOM> _dbSet;
        public UOMRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<UOM>();
          //  UOM var = _dbSet.First();
            //ManufacturedPartNoDetail var = _dbSet.Single();
        }

        public IEnumerable<UOM> GetUOMs()
        {
            return _dbSet.ToList();
        }


        public bool AddUOM(UOM uom)
        {
            try
            {
                _dbSet.Add(uom);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
            return true;
        }



    }
}
