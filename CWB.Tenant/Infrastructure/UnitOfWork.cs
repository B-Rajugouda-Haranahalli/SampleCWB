using CWB.Tenant.Repositories.Tenants;
using System;
using System.Threading.Tasks;

namespace CWB.Tenant.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TenantDbContext _context;
        private TenantRequestRepository _tenantRequestRepository;
        private TenantRepository _tenantRepository;

        public UnitOfWork(TenantDbContext context)
        {
            this._context = context;
        }

        public ITenantRequestRepository TenantRequests
        {
            get
            {
                if (_tenantRequestRepository == null)
                {
                    _tenantRequestRepository = new TenantRequestRepository(_context);
                }
                return _tenantRequestRepository;
            }
        }
        public ITenantRepository Tenants
        {
            get
            {
                if (_tenantRepository == null)
                {
                    _tenantRepository = new TenantRepository(_context);
                }
                return _tenantRepository;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
