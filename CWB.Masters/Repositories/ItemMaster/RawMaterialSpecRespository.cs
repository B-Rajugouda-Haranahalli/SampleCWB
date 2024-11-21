using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class RawMaterialSpecRespository : Repository<RawMaterialSpec>, IRawMaterialSpecRepository
    {
        private readonly DbSet<RawMaterialSpec> _dbSet;
        public RawMaterialSpecRespository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<RawMaterialSpec>();
            //context.Database.ExecuteSqlRawAsync
          //  UOM var = _dbSet.First();
            //ManufacturedPartNoDetail var = _dbSet.Single();
        }

        public IEnumerable<RawMaterialSpec> GetRawMaterialSpecs()
        {
            return _dbSet.ToList();
        }

        public bool AddRMSpec(RawMaterialSpec spec)
        {
            try
            {
                _dbSet.Add(spec);
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
