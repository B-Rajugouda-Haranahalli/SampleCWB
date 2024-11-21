using CWB.CommonUtils.Common.Repositories;
using CWB.Masters.Domain.Routings;

namespace CWB.Masters.Repositories.Routings
{
    public interface ISubConWorkStepDetailsRepository : IRepository<SubConWorkStepDetails>
    {
        void DetachEntry(SubConWorkStepDetails subConWSDetails);
    }
}
