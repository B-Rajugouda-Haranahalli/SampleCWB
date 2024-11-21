using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using Microsoft.EntityFrameworkCore;
using System;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class RawMaterialDetailRepository : Repository<RawMaterialDetail>, IRawMaterialDetailRepository
    {
        private readonly DbSet<RawMaterialDetail> _dbSet;
        public RawMaterialDetailRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<RawMaterialDetail>();
        }

        public void AddRawMaterial(RawMaterialDetail rawMaterial)
        {
            try
            {
                _dbSet.Add(rawMaterial);
                Context.SaveChanges();
            } catch(Exception ex)
            {
                throw ex;
            }
        }

        public void xxxxy(RawMaterialDetail rawMaterial)
        {
            try
            {
                _dbSet.Add(rawMaterial);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
