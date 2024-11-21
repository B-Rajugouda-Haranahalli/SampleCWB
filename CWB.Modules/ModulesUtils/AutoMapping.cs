using AutoMapper;
using CWB.Modules.Domain;
using CWB.Modules.ViewModels;

namespace CWB.Modules.ModulesUtils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ModuleType, ModuleTypesVM>()
                .ForMember(m => m.ModuleTypeId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.Modules, m => m.MapFrom(src => src.Modules));
            CreateMap<Module, ModulesVM>()
                .ForMember(m => m.ModuleId, m => m.MapFrom(src => src.Id));

        }
    }
}
