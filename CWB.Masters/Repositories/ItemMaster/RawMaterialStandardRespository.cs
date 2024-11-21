using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using CWB.Masters.ViewModels.ItemMaster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class RawMaterialStandardRespository : Repository<RawMaterialStandard>, IRawMaterialStandardRepository
    {
        private readonly DbSet<RawMaterialStandard> _dbSet;
        public RawMaterialStandardRespository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<RawMaterialStandard>();
        //  UOM var = _dbSet.First();
            //ManufacturedPartNoDetail var = _dbSet.Single();
        }

        public IEnumerable<RawMaterialStandard> GetRawMaterialStandards()
        {
            return _dbSet.ToList();
        }

        public bool AddRMStandard(RawMaterialStandard standard)
        {
            try
            {
                _dbSet.Add(standard);
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
