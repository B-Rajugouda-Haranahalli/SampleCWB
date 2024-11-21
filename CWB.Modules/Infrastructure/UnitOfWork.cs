using CWB.Modules.Repositories;
using System;
using System.Threading.Tasks;

namespace CWB.Modules.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ModuleDbContext _context;
        private IModuleTypeRepository _moduleTypeRepository;
        private IModuleRepository _moduleRepository;
        private IModuleTenantConfigRepository _moduleTenantConfigRepository;

        public UnitOfWork(ModuleDbContext context)
        {
            this._context = context;
        }
        public IModuleTenantConfigRepository ModuleTenantConfigs
        {
            get
            {
                if (_moduleTenantConfigRepository == null)
                {
                    _moduleTenantConfigRepository = new ModuleTenantConfigRepository(_context);
                }
                return _moduleTenantConfigRepository;
            }
        }



        public IModuleRepository Modules
        {
            get
            {
                if (_moduleRepository == null)
                {
                    _moduleRepository = new ModuleRepository(_context);
                }
                return _moduleRepository;
            }
        }

        public IModuleTypeRepository ModuleTypes

        {
            get
            {
                if (_moduleTypeRepository == null)
                {
                    _moduleTypeRepository = new ModuleTypeRepository(_context);
                }
                return _moduleTypeRepository;
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
