using CWB.CommonUtils.Common;
using CWB.Constants.UserIdentity;
using CWB.Logging;
using CWB.Masters.Company.ViewModelValidators;
using CWB.Masters.MastersUtils;
using CWB.Masters.Services.Company;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModelValidators.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.Masters.Controllers
{
    [ApiController]
    [Authorize(Roles = Roles.ADMIN)]
    public class CompanyController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly ICompanyService _companyService;

        public CompanyController(ILoggerManager logger, ICompanyService companyService)
        {
            _logger = logger;
            _companyService = companyService;
        }

        /// <summary>
        /// Get All Company Types
        /// </summary>
        /// <returns>List of Company Types</returns>
        [HttpGet]
        [Route(ApiRoutes.Company.GetCompanyTypes)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<CompanyTypeVM>))]
        public IActionResult GetCompanyTypes()
        {
            var companyTypes = _companyService.GetCompanyTypes();
            return Ok(companyTypes);
        }


        /// <summary>
        /// Get All Companies by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Company.GetCompanies)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<CompaniesVM>))]
        public async Task<IActionResult> GetCompanies(long Id)
        {
            var companies = await _companyService.GetCompaniesByTenant(Id);
            return Ok(companies);
        }

        /// <summary>
        /// Get All Companies by tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(ApiRoutes.Company.GetDivisionsById)]
        [Produces(AppContentTypes.ContentType, Type = typeof(List<CompaniesVM>))]
        public async Task<IActionResult> GetCompanies(long Id, long tenantID)
        {
            var companies = await _companyService.GetCompaniesByCompanyNTenant(Id, tenantID);
            return Ok(companies);
        }

        /// <summary>
        /// Add/Edit company
        /// </summary>
        /// <param name="companyVM"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Company.PostCompany)]
        [Produces(AppContentTypes.ContentType, Type = typeof(CompanyVM))]
        public async Task<IActionResult> PostCompany([FromBody] CompanyVM companyVM)
        {
            //var validator = new CompanyVMValidator();
            //var validationResult = await validator.ValidateAsync(companyVM);
            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.Errors);
            var result = await _companyService.Company(companyVM);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Company.DeleteCompany)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteCompany(long companyID,long tenantId)
        {
           
            var result = await _companyService.DeleteCompany(companyID, tenantId);
            return Ok(result);
        }

        [HttpGet]
        [Route(ApiRoutes.Company.DeleteDivision)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> DeleteDivision(long divisionID, long tenantId)
        {

            var result = await _companyService.DeleteDivision(divisionID, tenantId);
            return Ok(result);
        }

        /// <summary>
        /// Check if company exist with company name
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Company.IsCompanyExist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckCompany([FromBody] CheckCompanyVM checkCompanyVM)
        {
            var validator = new CheckCompanyVMValidator();
            var validationResult = await validator.ValidateAsync(checkCompanyVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _companyService.CheckIfCompanyExisit(checkCompanyVM);
            return Ok(result);
        }

        /// <summary>
        /// Check if division exist with division name
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route(ApiRoutes.Company.IsDivisionExist)]
        [Produces(AppContentTypes.ContentType, Type = typeof(bool))]
        public async Task<IActionResult> CheckDivision([FromBody] CheckDivisionVM checkDivisionVM)
        {
            var validator = new CheckDivisionVMValidator();
            var validationResult = await validator.ValidateAsync(checkDivisionVM);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _companyService.CheckIfDivisionExisit(checkDivisionVM);
            return Ok(result);
        }
    }
}
