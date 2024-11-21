using CWB.Simulation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Simulation.Services
{
    public interface IVendorServices
    {
        IEnumerable<VendorVM> GetVendor(long TenantID);
        IEnumerable<VendorVM> GetVendorByType(string Type);
        Task AddVendor(VendorVM model);
        Task UpdateVendor(long VendorId, VendorVM model);
    }
}