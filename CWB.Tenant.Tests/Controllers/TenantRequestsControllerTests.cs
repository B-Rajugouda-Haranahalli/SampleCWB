using CWB.Constants.Tenant;
using CWB.Logging;
using CWB.Tenant.Controllers;
using CWB.Tenant.Services.Tenants;
using CWB.Tenant.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace CWB.Tenant.Tests.Controllers
{
    [Trait("Category", "Tenant Requests")]
    public class TenantRequestsControllerTests
    {
        private readonly Mock<ILoggerManager> _mocklogger;
        public readonly Mock<ITenantRequestService> _mockTenantRequestService;
        private readonly TenantRequestsController tenantRequestsController;

        public TenantRequestsControllerTests()
        {
            _mocklogger = new Mock<ILoggerManager>();
            _mockTenantRequestService = new Mock<ITenantRequestService>();
            tenantRequestsController = new TenantRequestsController(_mocklogger.Object, _mockTenantRequestService.Object);
        }


        [Fact(DisplayName = "Tenant Requests returns Ok")]
        public async Task PostTenantRequests_ReturnOk()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.CheckDuplicateRequestByEmail(It.IsAny<string>()))
                .Returns(false);
            _mockTenantRequestService.Setup(t => t.AddRequest(It.IsAny<TenantRequestsVM>()))
                .Verifiable();

            //Act
            var result = await tenantRequestsController.TenantRequests(new TenantRequestsVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "test@test.com", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<OkResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Tenant Requests returns BadRequest")]
        public async Task PostTenantRequests_BadRequest()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.CheckDuplicateRequestByEmail(It.IsAny<string>()))
                .Returns(true);

            //Act
            var result = await tenantRequestsController.TenantRequests(new TenantRequestsVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "test@test.com", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Tenant Requests returns Validation BadRequest")]
        public async Task PostTenantRequests_ValidationBadRequest()
        {
            //Act
            var result = await tenantRequestsController.TenantRequests(new TenantRequestsVM { CompanyInfo = "Info", CompanyName = "Company Name", Email = "Invalid", Phone = "00000000" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Get Tenant Requests by status returns Ok")]
        public void GetTenantRequestByStatus_ReturnOk()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.GetAllRequestsByStatus(It.IsAny<string>()))
                .Returns(new List<TenantRequestsListVM>());

            //Act
            var result = tenantRequestsController.GetTenantRequestByStatus(It.IsAny<string>());

            //Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<List<TenantRequestsListVM>>();
        }

        [Fact(DisplayName ="Get Tenant Request by id returns Ok")]
        public async Task GetTenantRequestById_ReturnOk()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.GetRequestById(It.IsAny<long>()))
                .ReturnsAsync(new TenantRequestsListVM());

            //Act
            var result = await tenantRequestsController.GetTenantRequestById(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<TenantRequestsListVM>();
        }

        [Fact(DisplayName = "Get Tenant Request by id returns BadRequest")]
        public async Task GetTenantRequestById_BadRequest()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.GetRequestById(It.IsAny<long>()))
                .ReturnsAsync(null as TenantRequestsListVM);

            //Act
            var result = await tenantRequestsController.GetTenantRequestById(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<NotFoundResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Tenant Request status returns Ok")]
        public async Task PostTenantRequestStatus_ReturnOk()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.CheckRequestStatusById(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(true);
            _mockTenantRequestService.Setup(t => t.UpdateRequestStatus(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
                .Verifiable();

            //Act
            var result = await tenantRequestsController.TenantRequestStatus(new TenantRequestApproveRejectVM {TenantRequestId = 1, Status = TenantStatus.Approve, Comments="Status updated" });

            //Assert
            result.Should().BeOfType<OkResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Tenant Requests status returns Validation BadRequest")]
        public async Task PostTenantRequestStatus_ValidationBadRequest()
        {
            //Act
            var result = await tenantRequestsController.TenantRequestStatus(new TenantRequestApproveRejectVM { TenantRequestId = 1, Status = TenantStatus.Pending, Comments = "Status updated" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Tenant Request status returns BadRequest")]
        public async Task PostTenantRequestStatus_ReturnBadRequest()
        {
            //Arrange
            _mockTenantRequestService.Setup(t => t.CheckRequestStatusById(It.IsAny<long>(), It.IsAny<string>()))
                .Returns(false);

            //Act
            var result = await tenantRequestsController.TenantRequestStatus(new TenantRequestApproveRejectVM { TenantRequestId = 1, Status = TenantStatus.Approve, Comments = "Status updated" });

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
        }
    }
}
