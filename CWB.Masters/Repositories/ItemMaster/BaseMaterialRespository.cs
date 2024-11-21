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
    public class BaseMaterialRespository : Repository<BaseRawMaterial>, IBaseRawMaterialRepository
    {
        private readonly DbSet<BaseRawMaterial> _dbSet;
        public BaseMaterialRespository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<BaseRawMaterial>();
          //  UOM var = _dbSet.First();
            //ManufacturedPartNoDetail var = _dbSet.Single();
        }

        public IEnumerable<BaseRawMaterial> GetBaseRawMaterials()
        {
            return _dbSet.ToList();
        }


        public bool AddBaseRM(BaseRawMaterial baseRM)
        {
            try
            {
                _dbSet.Add(baseRM);
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
