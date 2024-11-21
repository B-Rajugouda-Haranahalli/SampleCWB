using AutoMapper;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.Infrastructure;
using CWB.CompanySettings.Repositories.Location;
using CWB.CompanySettings.ViewModels.Location;
using CWB.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.CompanySettings.Services.Location
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _departmentRepository = departmentRepository;

        }

        public bool CheckDepartmentExisit(CheckDepartmentVM checkDepartmentVM)
        {
            var departments = _departmentRepository.GetRangeAsync(d => d.Name == checkDepartmentVM.Name &&
            d.TenantId == checkDepartmentVM.TenantId && d.PlantId == checkDepartmentVM.PlantId);
            if (!departments.Any())
            {
                return false;
            }
            return (departments.First().Id != checkDepartmentVM.DepartmentId);
        }

        public async Task<ShopDepartmentVM> Department(ShopDepartmentVM shopDepartmentVM)
        {
            var department = _mapper.Map<ShopDepartment>(shopDepartmentVM);
            if (department.Id == 0)
            {
                await _departmentRepository.AddAsync(department);
            }
            else
            {
                department = await _departmentRepository.UpdateAsync(department.Id, department);
            }
            await _unitOfWork.CommitAsync();
            shopDepartmentVM.DepartmentId = department.Id;
            return shopDepartmentVM;
        }

        public async Task<IEnumerable<ShopDepartmentVM>> GetAllDepartments(long PlantId, long TenantId)
        {
            var departments = await _departmentRepository.GetAllDepartmentsByTenantAsync(TenantId);
            return _mapper.Map<IEnumerable<ShopDepartmentVM>>(departments);
        }

        

        public IEnumerable<ShopDepartmentVM> GetDepartmentListWithPlants(List<long> DepartmentIds, long TenantId)
        {
            var departments = _departmentRepository.GetDepartmentsWithPlant(DepartmentIds, TenantId);
            return _mapper.Map<IEnumerable<ShopDepartmentVM>>(departments);
        }

        public async Task<bool> DelDepartment(long departmentId)
        {
            try
            {
                var dept = await _departmentRepository.SingleOrDefaultAsync(d => d.Id == departmentId);
                if (dept != null)
                {
                    if (dept.Id > 0)
                    {
                        _departmentRepository.Remove(dept);
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
