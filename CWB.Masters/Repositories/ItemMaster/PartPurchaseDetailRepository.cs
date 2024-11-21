using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.Domain.ItemMaster;
using CWB.Masters.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace CWB.Masters.Repositories.ItemMaster
{
    public class PartPurchaseDetailRepository : Repository<Domain.ItemMaster.PartPurchaseDetails>, IPartPurchaseDetailRepository
    {

        private readonly DbSet<PartPurchaseDetails> _dbSet;
        
        public PartPurchaseDetailRepository(MastersDbContext context)
        : base(context)
        {
            _dbSet = context.Set<PartPurchaseDetails>();
        }

        public void AddPartPurchase(PartPurchaseDetails part)
        {
            try
            {
                _dbSet.Add(part);
                Context.SaveChanges();
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void RemovePartPurchase(PartPurchaseDetails part)
        {
            try
            {
                Remove(part);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
