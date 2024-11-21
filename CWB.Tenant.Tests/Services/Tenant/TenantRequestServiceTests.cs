using AutoMapper;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Constants.Tenant;
using CWB.Logging;
using CWB.Tenant.Domain.Tenants;
using CWB.Tenant.Infrastructure;
using CWB.Tenant.Repositories.Tenants;
using CWB.Tenant.Services.Tenants;
using CWB.Tenant.TenantUtils;
using CWB.Tenant.ViewModels;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CWB.Tenant.Tests.Services.Tenant
{
    [Trait("Category", "Tenant Request Service")]
    public class TenantRequestServiceTests
    {
        private readonly Mock<ILoggerManager> _mocklogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITenantRequestRepository> _mockTenantRequestRepository;
        private readonly Mock<IMessageBroker> _mockMessageBroker;
        private readonly Mock<IOptions<KafkaConfig>> _mockkafkaConfigOptions;
        private readonly Mock<IOptions<AppConfig>> _mockAppConfig;
        private readonly ITenantRequestService _tenantRequestRepository;

        public TenantRequestServiceTests()
        {
            _mocklogger = new Mock<ILoggerManager>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTenantRequestRepository = new Mock<ITenantRequestRepository>();
            _mockMessageBroker = new Mock<IMessageBroker>();
            _mockkafkaConfigOptions = new Mock<IOptions<KafkaConfig>>();
            _mockAppConfig = new Mock<IOptions<AppConfig>>();
            _tenantRequestRepository = new TenantRequestService(_mocklogger.Object, _mockMapper.Object, _mockUnitOfWork.Object, _mockTenantRequestRepository.Object,
                _mockMessageBroker.Object, _mockkafkaConfigOptions.Object, _mockAppConfig.Object);
        }

        [Fact(DisplayName = "Add Request Success")]
        public async Task AddRequest_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.AddAsync(It.IsAny<TenantRequest>()))
                .Verifiable();
            _mockUnitOfWork.Setup(u => u.CommitAsync())
                .Verifiable();
            _mockMapper.Setup(m => m.Map<TenantRequest>(It.IsAny<TenantRequestsVM>()))
                .Returns(new TenantRequest());
            _mockMessageBroker.Setup(m => m.SendAsync(It.IsAny<KafkaConfig>(), It.IsAny<MessageInfo>()))
                .ReturnsAsync(true);
            _mockAppConfig.Setup(m => m.Value)
                .Returns(new AppConfig());

            //Act
            await _tenantRequestRepository.AddRequest(It.IsAny<TenantRequestsVM>());

        }

        [Fact(DisplayName = "Check Duplicate Request By Email Success")]
        public void CheckDuplicateRequestByEmail_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetRangeAsync(e => e.Email == It.IsAny<string>()))
                .Returns(new List<TenantRequest>());

            //Act
            var result = _tenantRequestRepository.CheckDuplicateRequestByEmail(It.IsAny<string>());

            //Assert
            result.Should().BeFalse();

        }

        [Fact(DisplayName = "Check Request Status By Id Success")]
        public void CheckRequestStatusById_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetRangeAsync(e => e.Id == It.IsAny<int>() && e.RequestStatus == It.IsAny<TenantRequestStatus>()))
                .Returns(new List<TenantRequest>());

            //Act
            var result = _tenantRequestRepository.CheckRequestStatusById(It.IsAny<int>(), TenantStatus.Approve);

            //Assert
            result.Should().BeFalse();

        }

        [Fact(DisplayName = "Check Request Status By Id Exception")]
        public void CheckRequestStatusById_Exception()
        {
            try
            {
                //Act
                var result = _tenantRequestRepository.CheckRequestStatusById(It.IsAny<int>(), It.IsAny<string>());
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Equal("Invalid Status: ", ex.Message);
            }
        }

        [Fact(DisplayName = "Get All Requests Success")]
        public async Task GetAllRequests_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<TenantRequest>());

            //Act
            var result = await _tenantRequestRepository.GetAllRequests();

            //Assert
            result.Should().BeOfType<TenantRequestsListVM[]>();
        }

        [Fact(DisplayName = "Get All Requests By Status Success")]
        public void GetAllRequestsByStatus_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetRangeAsync(e => e.RequestStatus == It.IsAny<TenantRequestStatus>()))
                .Returns(new List<TenantRequest>());

            //Act
            var result = _tenantRequestRepository.GetAllRequestsByStatus(TenantStatus.Approve);

            //Assert
            result.Should().BeOfType<TenantRequestsListVM[]>();

        }

        [Fact(DisplayName = "Get All Requests By Status Exception")]
        public void GetAllRequestsByStatus_Exception()
        {
            try
            {
                //Act
                var result = _tenantRequestRepository.GetAllRequestsByStatus(It.IsAny<string>());
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Equal("Invalid Status: ", ex.Message);
            }
        }

        [Fact(DisplayName = "Get Request By Id Success")]
        public async Task GetRequestById_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new TenantRequest());
            _mockMapper.Setup(m => m.Map<TenantRequestsListVM>(It.IsAny<TenantRequest>()))
                .Returns(new TenantRequestsListVM());

            //Act
            var result = await _tenantRequestRepository.GetRequestById(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<TenantRequestsListVM>();

        }

        [Fact(DisplayName = "Update Request Status Success")]
        public async Task UpdateRequestStatus_Success()
        {
            //Arrange
            _mockTenantRequestRepository.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new TenantRequest());
            _mockUnitOfWork.Setup(u => u.CommitAsync())
                .Verifiable();
            _mockMessageBroker.Setup(m => m.SendAsync(It.IsAny<KafkaConfig>(), It.IsAny<MessageInfo>()))
                .ReturnsAsync(true);
            _mockAppConfig.Setup(m => m.Value)
                .Returns(new AppConfig());
            //Act
            await _tenantRequestRepository.UpdateRequestStatus(It.IsAny<long>(), TenantStatus.Approve, "Comments");

        }

        [Fact(DisplayName = "Update Request Status Exception")]
        public async Task UpdateRequestStatus_Exception()
        {
            try
            {
                //Act
                await _tenantRequestRepository.UpdateRequestStatus(It.IsAny<long>(), null, It.IsAny<string>());
            }
            catch (Exception ex)
            {
                //Assert
                Assert.Equal("Invalid Status: ", ex.Message);
            }
        }
    }
}