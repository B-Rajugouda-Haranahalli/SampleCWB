using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using CWB.Masters.Repositories.Company;
using Microsoft.EntityFrameworkCore;
using System;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class MPMakeFromRepository : Repository<MPMakeFrom>, IMPMakeFromRepository
    {
        private readonly DbSet<MPMakeFrom> _dbSet;
        public MPMakeFromRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<MPMakeFrom>();
        }

        public bool AddObj(MPMakeFrom mPMakeFrom)
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

        public bool RemObj(MPMakeFrom mPMakeFrom)
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
