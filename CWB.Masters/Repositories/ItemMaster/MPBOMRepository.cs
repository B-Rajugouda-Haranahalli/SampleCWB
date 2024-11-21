using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using Microsoft.EntityFrameworkCore;
using System;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class MPBOMRepository : Repository<MPBOM>, IMPBOMRepository
    {
        private readonly DbSet<MPBOM> _dbSet;
        public MPBOMRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<MPBOM>();
        }

        public bool AddObj(MPBOM mPMakeFrom)
        {
            try
            {
                _dbSet.Add(mPMakeFrom);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
            return true;
        }

        public bool RemObj(MPBOM mPMakeFrom)
        {
            try
            {
                _dbSet.Remove(mPMakeFrom);
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

