using CWB.CommonUtils.Common;
using CWB.Logging;
using CWB.Simulation.SimulationUtils;
using CWB.Simulation.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CWB.Simulation.Controllers
{
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public SectionController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route(ApiRoutes.Section.SectionsByTenant)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SectionVM))]
        public IActionResult GetSections(long tenantID)
        {
            return Ok();
        }


        [HttpGet]
        [Route(ApiRoutes.Section.SectionsByDeparment)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SectionVM))]
        public IActionResult GetSectionsByDepartment(long departmentID)
        {
            return Ok();
        }


        [HttpGet]
        [Route(ApiRoutes.Section.SectionsBySection)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SectionVM))]
        public IActionResult GetSectionsBySection(long parentSectionID)
        {
            return Ok();
        }

        [HttpGet]
        [Route(ApiRoutes.Section.SectionsById)]
        [Produces(AppContentTypes.ContentType, Type = typeof(SectionVM))]
        public IActionResult GetSection(long sectionID)
        {
            return Ok();
        }
    }
}
