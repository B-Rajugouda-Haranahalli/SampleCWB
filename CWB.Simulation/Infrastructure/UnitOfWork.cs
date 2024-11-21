using CWB.Simulation.Repositories;
using System;
using System.Threading.Tasks;

namespace CWB.Simulation.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly SimulationDbContext _context;
        private IWorkDayMasterRepository _workDayMasterRepository;
        private IShopDepartmentRepository _shopDepartmentRepository;
        private IPlantRepository _plantRepository;
        private IMachineTypeRepository _machineTypeRepository;
        private IMachineRepository _machineRepository;
        private IVendorRepository _vendorRepository;
        private IMRBomRepository _mRBomRepository;
        private IMRBomGroupRepository _mRBomGroupRepository;
        private IItemMasterRepository _itemMasterRepository;

        public UnitOfWork(SimulationDbContext context)
        {
            this._context = context;
        }

        public IWorkDayMasterRepository WorkDayMaster
        {
            get
            {
                if (_workDayMasterRepository == null)
                {
                    _workDayMasterRepository = new WorkDayMasterRepository(_context);
                }
                return _workDayMasterRepository;
            }
        }

        public IShopDepartmentRepository ShopDepartmentRepository
        {
            get
            {
                if (_shopDepartmentRepository == null)
                {
                    _shopDepartmentRepository = new ShopDepartmentRepository(_context);
                }
                return _shopDepartmentRepository;
            }
        }

        public IPlantRepository PlantRepository
        {
            get
            {
                if (_plantRepository == null)
                {
                    _plantRepository = new PlantRepository(_context);
                }
                return _plantRepository;
            }
        }

        public IMachineTypeRepository MachineTypeRepository
        {
            get
            {
                if (_machineTypeRepository == null)
                {
                    _machineTypeRepository = new MachineTypeRepository(_context);
                }
                return _machineTypeRepository;
            }
        }

        public IMachineRepository MachineRepository
        {
            get
            {
                if (_machineRepository == null)
                {
                    _machineRepository = new MachineRepository(_context);
                }
                return _machineRepository;
            }
        }

        public IVendorRepository VendorRepository
        {
            get
            {
                if (_vendorRepository == null)
                {
                    _vendorRepository = new VendorRepository(_context);
                }
                return _vendorRepository;
            }
        }

        public IMRBomRepository MRBomRepository
        {
            get
            {
                if (_mRBomRepository == null)
                {
                    _mRBomRepository = new MRBomRepository(_context);
                }
                return _mRBomRepository;
            }
        }

        public IMRBomGroupRepository MRBomGroupRepository
        {
            get
            {
                if (_mRBomGroupRepository == null)
                {
                    _mRBomGroupRepository = new MRBomGroupRepository(_context);
                }
                return _mRBomGroupRepository;
            }
        }

        public IItemMasterRepository ItemMasterRepository
        {
            get
            {
                if (_itemMasterRepository == null)
                {
                    _itemMasterRepository = new ItemMasterRepository(_context);
                }
                return _itemMasterRepository;
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
