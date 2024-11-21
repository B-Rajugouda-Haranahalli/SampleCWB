using CWB.App.Models.Designation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.App.Services.CompanySettings
{
    public interface IDesignationService
    {
        Task<IEnumerable<DesignationVM>> GetDesignations();
        Task<bool> CheckIfDesignationExisit(long DesignationId, string DesignationName);
        Task<bool> DelDesignation(long designationId);
        Task<DesignationVM> Designation(DesignationVM designationVM);
    }
}
