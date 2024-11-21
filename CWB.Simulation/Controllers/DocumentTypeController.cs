using CWB.Logging;
using Microsoft.AspNetCore.Mvc;

namespace CWB.Simulation.Controllers
{

    [ApiController]
    public class DocumentTypeController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public DocumentTypeController(ILoggerManager logger)
        {
            _logger = logger;
        }
    }
}
