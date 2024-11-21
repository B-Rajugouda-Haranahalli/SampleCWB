using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain;
using CWB.Masters.ViewModels.ItemMaster;
using System.Collections.Generic;
using System.Threading.Tasks;
using CWB.Masters.Domain.ItemMaster;

namespace CWB.Masters.Repositories.ItemMaster
{
    public interface IUOMRepository : IRepository<UOM>
    {
        public IEnumerable<UOM> GetUOMs();
        public bool AddUOM(UOM uom);
    }
}
