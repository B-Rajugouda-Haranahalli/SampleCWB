using AutoMapper;
using CWB.Simulation.Domain;
using CWB.Simulation.ViewModels;

namespace CWB.Simulation.SimulationUtils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<WorkDayMaster, WorkDayMasterVM>()
                .ForMember(m => m.WorkDayMasterId, m => m.MapFrom(src => src.Id));
            CreateMap<Plant, PlantVM>()
                .ForMember(m => m.PlantId, m => m.MapFrom(src => src.Id));
            CreateMap<ShopDepartment, ShopDepartmentVM>()
                .ForMember(m => m.ShopDepartmentId, m => m.MapFrom(src => src.Id));
            CreateMap<MachineType, MachineTypeVM>()
                .ForMember(m => m.MachineTypeId, m => m.MapFrom(src => src.Id));
            CreateMap<Machine, MachineVM>()
                .ForMember(m => m.MachineId, m => m.MapFrom(src => src.Id));
            CreateMap<Vendor, VendorVM>()
                .ForMember(m => m.VendorId, m => m.MapFrom(src => src.Id));
            CreateMap<MRBom, MRBomVM>()
                .ForMember(m => m.MRBomId, m => m.MapFrom(src => src.Id));
            CreateMap<MRBomGroup, MRBomGroupVM>()
                .ForMember(m => m.MRBomGroupId, m => m.MapFrom(src => src.Id));
            CreateMap<ItemMaster, ItemMasterVM>()
                .ForMember(m => m.ItemMasterId, m => m.MapFrom(src => src.Id));
        }
    }
}