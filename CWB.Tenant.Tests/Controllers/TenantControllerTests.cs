using CWB.Constants.Tenant;
using CWB.Logging;
using CWB.Tenant.Controllers;
using CWB.Tenant.Services.Tenants;
using CWB.Tenant.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CWB.Tenant.Tests.Controllers
{
    [Trait("Category", "Tenant")]
    public class TenantControllerTests
    {
        private readonly Mock<ILoggerManager> _mocklogger;
        private readonly Mock<ITenantService> _mockTenantService;
        private readonly TenantController tenantController;
        public TenantControllerTests()
        {
            _mocklogger = new Mock<ILoggerManager>();
            _mockTenantService = new Mock<ITenantService>();
            tenantController = new TenantController(_mocklogger.Object, _mockTenantService.Object);
        }

        [Fact(DisplayName = "Get Tenants Returns Ok")]
        public async Task GetTenants_ReturnsOk()
        {
            //Arrange
            _mockTenantService.Setup(t => t.GetAllTenants()).ReturnsAsync(new List<TenantsListVM>());

            //Act
            var result = await tenantController.GetTenants();

            //Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<List<TenantsListVM>>();
        }

        [Fact(DisplayName = "Get Tenant Returns Ok")]
        public async Task GetTenant_ReturnsOk()
        {
            //Arrange
            _mockTenantService.Setup(t => t.GetTenantById(It.IsAny<long>()))
                .ReturnsAsync(new TenantsListVM());

            //Act
            var result = await tenantController.GetTenant(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<TenantsListVM>();
        }

        [Fact(DisplayName = "Get Tenant Returns Not Found")]
        public async Task GetTenant_ReturnsNotFound()
        {
            //Arrange
            _mockTenantService.Setup(t => t.GetTenantById(It.IsAny<long>()))
                .ReturnsAsync(null as TenantsListVM);

            //Act
            var result = await tenantController.GetTenant(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Tenant Status Returns Ok")]
        public async Task PostTenantStatus_ReturnsOk()
        {
            //Arrange
            _mockTenantService.Setup(t => t.UpdateTenantStatus(It.IsAny<TenantStatusVM>()))
                .Verifiable();

            //Act
            var result = await tenantController.TenantStatus(new TenantStatusVM { TenantId = 1, Status = true});

            //Assert
            result.Should().BeOfType<OkResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Tenant Status Validation BadRequest")]
        public async Task PostTenantStatus_Validation_BadRequest()
        {
            //Act
            var result = await tenantController.TenantStatus(new TenantStatusVM { TenantId = 0, Status = true });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Tenant Returns Ok")]
        public async Task PostTenant_ReturnsOk()
        {
            //Arrange
            _mockTenantService.Setup(t => t.CheckDuplicateTenantByEmail(It.IsAny<string>()))
                .Returns(false);
            _mockTenantService.Setup(t => t.CheckDuplicateTenantByCode(It.IsAny<string>()))
                .Returns(false);
            _mockTenantService.Setup(t => t.AddTenant(It.IsAny<TenantVM>(), It.IsAny<string>()))
                .Verifiable();

            //Act
            var result = await tenantController.Tenant(new TenantVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "test@test.com", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<OkResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Tenant Returns Validation Bad Request")]
        public async Task PostTenant_Returns_Validation_BadRequest()
        {
            //Act
            var result = await tenantController.Tenant(new TenantVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "Invalid", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Tenant Returns Bad Request")]
        public async Task PostTenant_Returns_BadRequest()
        {
            //Arrange
            _mockTenantService.Setup(t => t.CheckDuplicateTenantByEmail(It.IsAny<string>()))
                .Returns(true);

            //Act
            var result = await tenantController.Tenant(new TenantVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "test@test.com", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
