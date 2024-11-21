using AutoMapper;
using CWB.Tenant.Domain.Tenants;
using CWB.Tenant.TenantExtensions;
using CWB.Tenant.ViewModels;

namespace CWB.Tenant.TenantUtils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<TenantRequestsVM, TenantRequest>();
            CreateMap<TenantRequest, TenantRequestVM>()
               .ForMember(s => s.TenantRequestId, s => s.MapFrom(src => src.Id));
            CreateMap<TenantRequest, TenantRequestsListVM>()
               .ForMember(s => s.TenantRequestId, s => s.MapFrom(src => src.Id))
               .ForMember(s => s.Status, s => s.MapFrom(src => src.RequestStatus.GetDescription()));

            CreateMap<TenantVM, Domain.Tenants.Tenant>();
            CreateMap<Domain.Tenants.Tenant, TenantsListVM>()
               .ForMember(s => s.TenantId, s => s.MapFrom(src => src.Id));

        }
    }
}
