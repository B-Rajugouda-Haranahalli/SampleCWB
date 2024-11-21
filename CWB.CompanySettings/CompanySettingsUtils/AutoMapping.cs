using AutoMapper;
using CWB.CompanySettings.Domain;
using CWB.CompanySettings.ViewModels.Designations;
using CWB.CompanySettings.ViewModels.DocType;
using CWB.CompanySettings.ViewModels.Location;

namespace CWB.CompanySettings.CompanySettingsUtils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<DocumentTypeVM, DocumentType>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.DocumentTypeId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.Description, m => m.MapFrom(src => src.Description)) 
                .ForMember(m => m.Extension, m => m.MapFrom(src => src.Extension))
                .ForMember(m => m.IsUploadedByUser, m => m.MapFrom(src => src.IsUploadedByUser));
            CreateMap<Domain.DocumentType, DocumentTypeVM>()
                 .ForMember(m => m.DocumentTypeId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.Description, m => m.MapFrom(src => src.Description))
                .ForMember(m => m.Extension, m => m.MapFrom(src => src.Extension))
                .ForMember(m => m.IsUploadedByUser, m => m.MapFrom(src => src.IsUploadedByUser));

            CreateMap<PlantVM, Plant>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.PlantId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.Address, m => m.MapFrom(src => src.Address))
                .ForMember(m => m.Notes, m => m.MapFrom(src => src.Notes))
                .ForMember(m => m.City, m => m.MapFrom(src => src.City))
                .ForMember(m => m.Pincode, m => m.MapFrom(src => src.Pincode))
                .ForMember(m => m.Country, m => m.MapFrom(src => src.Country))
                .ForMember(m => m.GstNo, m => m.MapFrom(src => src.GstNo))
                .ForMember(m => m.PanNo, m => m.MapFrom(src => src.PanNo))
                .ForMember(m => m.IsMainPlant, m => m.MapFrom(src => src.IsMainPlant))
                .ForMember(m => m.IsProductDesigned, m => m.MapFrom(src => src.IsProductDesigned));
            CreateMap<Plant, PlantVM>()
                 .ForMember(m => m.PlantId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.Address, m => m.MapFrom(src => src.Address))
                .ForMember(m => m.Notes, m => m.MapFrom(src => src.Notes))
                .ForMember(m => m.City, m => m.MapFrom(src => src.City))
                .ForMember(m => m.Pincode, m => m.MapFrom(src => src.Pincode))
                .ForMember(m => m.Country, m => m.MapFrom(src => src.Country))
                .ForMember(m => m.GstNo, m => m.MapFrom(src => src.GstNo))
                .ForMember(m => m.PanNo, m => m.MapFrom(src => src.PanNo))
                .ForMember(m => m.IsMainPlant, m => m.MapFrom(src => src.IsMainPlant))
                .ForMember(m => m.IsProductDesigned, m => m.MapFrom(src => src.IsProductDesigned));



            CreateMap<CityVM, City>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.CityId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));
            CreateMap<Domain.City, CityVM>()
             .ForMember(m => m.CityId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
             .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            CreateMap<CountryVM, Country>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.CountryId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));
            CreateMap<Domain.Country, CountryVM>()
             .ForMember(m => m.CountryId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
             .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));

            //Designation
            CreateMap<DesignationVM, Designation>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.DesignationId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));
            CreateMap<Domain.Designation, DesignationVM>()
             .ForMember(m => m.DesignationId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
             .ForMember(m => m.TenantId, m => m.MapFrom(src => src.TenantId));
            CreateMap<Designation, DesignationListVM>()
                .ForMember(m => m.DesignationId, m => m.MapFrom(src => src.Id));

            CreateMap<ShopDepartment, ShopDepartmentVM>()
                .ForMember(m => m.DepartmentId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
                .ForMember(m => m.Activity, m => m.MapFrom(src => src.Activity))
                .ForMember(m => m.PlantName, m => m.MapFrom(src => src.Plant.Name))
                .ForMember(m => m.Section, m => m.MapFrom(src => src.Section))
                .ForMember(m => m.ProdDept, m => m.MapFrom(src => src.ProdDept));
            CreateMap<ShopDepartmentVM, ShopDepartment>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.DepartmentId))
                .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
                .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
                .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
                .ForMember(m => m.Activity, m => m.MapFrom(src => src.Activity))
                .ForMember(m => m.Section, m => m.MapFrom(src => src.Section))
                .ForMember(m => m.ProdDept, m => m.MapFrom(src => src.ProdDept));

            CreateMap<HolidayVM, Holiday>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.HolidayId))
              .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
              .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
              .ForMember(m => m.HolidayDate, m => m.MapFrom(src => src.HolidayDate));

            CreateMap<PlantWorkingDetailsVM,PlantWorkingDetails>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.WDId))
               .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
               .ForMember(m => m.WeeklyOff1, m => m.MapFrom(src => src.WeeklyOff1))
               .ForMember(m => m.WeeklyOff2, m => m.MapFrom(src => src.WeeklyOff2))
               .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
               .ForMember(m => m.FirstShiftStartTime, m => m.MapFrom(src => src.FirstShiftStartTime))
               .ForMember(m => m.SecondShiftStartTime, m => m.MapFrom(src => src.SecondShiftStartTime))
               .ForMember(m => m.ThirdShiftStartTime, m => m.MapFrom(src => src.ThirdShiftStartTime))
               .ForMember(m => m.FirstShiftDuration, m => m.MapFrom(src => src.FirstShiftDuration))
               .ForMember(m => m.SecondShiftDuration, m => m.MapFrom(src => src.SecondShiftDuration))
               .ForMember(m => m.ThirdShiftDuration, m => m.MapFrom(src => src.ThirdShiftDuration));

            CreateMap<Holiday, HolidayVM>()
                .ForMember(m => m.HolidayId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
               .ForMember(m => m.Name, m => m.MapFrom(src => src.Name))
               .ForMember(m => m.HolidayDate, m => m.MapFrom(src => src.HolidayDate));

            CreateMap<PlantWorkingDetails,PlantWorkingDetailsVM>()
                .ForMember(m => m.WDId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.PlantId, m => m.MapFrom(src => src.PlantId))
               .ForMember(m => m.WeeklyOff1, m => m.MapFrom(src => src.WeeklyOff1))
               .ForMember(m => m.WeeklyOff2, m => m.MapFrom(src => src.WeeklyOff2))
               .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
               .ForMember(m => m.FirstShiftStartTime, m => m.MapFrom(src => src.FirstShiftStartTime))
               .ForMember(m => m.SecondShiftStartTime, m => m.MapFrom(src => src.SecondShiftStartTime))
               .ForMember(m => m.ThirdShiftStartTime, m => m.MapFrom(src => src.ThirdShiftStartTime))
               .ForMember(m => m.FirstShiftDuration, m => m.MapFrom(src => src.FirstShiftDuration))
               .ForMember(m => m.SecondShiftDuration, m => m.MapFrom(src => src.SecondShiftDuration))
               .ForMember(m => m.ThirdShiftDuration, m => m.MapFrom(src => src.ThirdShiftDuration));

            CreateMap<PlantWorkingDetails, PlantVM>()
          .ForMember(m => m.WDId, m => m.MapFrom(src => src.Id))
          .ForMember(m => m.PlantId, m => m.MapFrom(src => src.Plant.Id))
          .ForMember(m => m.Name, m => m.MapFrom(src => src.Plant.Name))
          .ForMember(m => m.IsMainPlant, m => m.MapFrom(src => src.Plant.IsMainPlant))
          .ForMember(m => m.IsProductDesigned, m => m.MapFrom(src => src.Plant.IsProductDesigned))
          .ForMember(m => m.Address, m => m.MapFrom(src => src.Plant.Address))
          .ForMember(m => m.Notes, m => m.MapFrom(src => src.Plant.Notes))
          .ForMember(m => m.WeeklyOff1, m => m.MapFrom(src => src.WeeklyOff1))
          .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
          .ForMember(m => m.FirstShiftStartTime, m => m.MapFrom(src => src.FirstShiftStartTime));

            CreateMap<PlantVM, PlantWorkingDetails>()
                 .ForMember(m => m.Id, m => m.MapFrom(src => src.WDId))
                 .ForMember(m=>m.PlantId,m=>m.MapFrom(src=>src.PlantId))
                 .ForMember(m => m.NoOfShifts, m => m.MapFrom(src => src.NoOfShifts))
                 .ForMember(m => m.WeeklyOff1, m => m.MapFrom(src => src.WeeklyOff1))
                 .ForMember(m => m.FirstShiftStartTime, m => m.MapFrom(src => src.FirstShiftStartTime));
        }
    }
}
