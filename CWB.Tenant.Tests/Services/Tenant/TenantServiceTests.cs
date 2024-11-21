using AutoMapper;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Logging;
using CWB.Tenant.Infrastructure;
using CWB.Tenant.Repositories.Tenants;
using CWB.Tenant.Services.Tenants;
using CWB.Tenant.TenantUtils;
using CWB.Tenant.ViewModels;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CWB.Tenant.Tests.Services.Tenant
{
    [Trait("Category", "Tenant Service")]
    public class TenantServiceTests
    {

        private readonly Mock<ILoggerManager> _mocklogger;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<ITenantRepository> _mockTenantRepository;
        private readonly Mock<IMessageBroker> _mockMessageBroker;
        private readonly Mock<IOptions<KafkaConfig>> _mockkafkaConfigOptions;
        private readonly Mock<IOptions<AppConfig>> _mockAppConfig;
        private readonly ITenantService _tenantRepository;

        public TenantServiceTests()
        {
            _mocklogger = new Mock<ILoggerManager>();
            _mockMapper = new Mock<IMapper>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockTenantRepository = new Mock<ITenantRepository>();
            _mockMessageBroker = new Mock<IMessageBroker>();
            _mockkafkaConfigOptions = new Mock<IOptions<KafkaConfig>>();
            _mockAppConfig = new Mock<IOptions<AppConfig>>();
            _tenantRepository = new TenantService(_mocklogger.Object, _mockMapper.Object, _mockUnitOfWork.Object, _mockTenantRepository.Object,
                _mockMessageBroker.Object, _mockkafkaConfigOptions.Object, _mockAppConfig.Object);
        }

        [Fact(DisplayName = "Add Tenant Success")]
        public async Task AddTenant_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Tenants.Tenant>()))
                .Verifiable();
            _mockUnitOfWork.Setup(u => u.CommitAsync())
                .Verifiable();
            _mockMessageBroker.Setup(m => m.SendAsync(It.IsAny<KafkaConfig>(), It.IsAny<MessageInfo>()))
                .ReturnsAsync(true);
            _mockMapper.Setup(m => m.Map<Domain.Tenants.Tenant>(It.IsAny<TenantVM>()))
                .Returns(new Domain.Tenants.Tenant());
            _mockAppConfig.Setup(m => m.Value)
                .Returns(new AppConfig());
            //Act
            await _tenantRepository.AddTenant(It.IsAny<TenantVM>(), It.IsAny<string>());

        }

        [Fact(DisplayName = "Check Duplicate Tenant By Code Success")]
        public void CheckDuplicateTenantByCode_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetRangeAsync(t => t.TenantCode == It.IsAny<string>()))
                .Returns(new List<Domain.Tenants.Tenant>());

            //Act
            var result = _tenantRepository.CheckDuplicateTenantByCode(It.IsAny<string>());

            //Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Check Duplicate Tenant By Email Success")]
        public void CheckDuplicateTenantByEmail_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetRangeAsync(t => t.Email == It.IsAny<string>()))
                .Returns(new List<Domain.Tenants.Tenant>());

            //Act
            var result = _tenantRepository.CheckDuplicateTenantByEmail(It.IsAny<string>());

            //Assert
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Get All Tenants Success")]
        public async Task GetAllTenants_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Domain.Tenants.Tenant>());
            _mockMapper.Setup(m => m.Map<List<TenantsListVM>>(It.IsAny<List<Domain.Tenants.Tenant>>()))
             .Returns(new List<TenantsListVM>());

            //Act
            var result = await _tenantRepository.GetAllTenants();

            //Assert
            result.Should().BeOfType<TenantsListVM[]>();
        }

        [Fact(DisplayName = "Get Tenant By Id Success")]
        public async Task GetTenantById_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Domain.Tenants.Tenant());
            _mockMapper.Setup(m => m.Map<TenantsListVM>(It.IsAny<Domain.Tenants.Tenant>()))
             .Returns(new TenantsListVM());

            //Act
            var result = await _tenantRepository.GetTenantById(It.IsAny<long>());

            //Assert
            result.Should().BeOfType<TenantsListVM>();
        }

        [Fact(DisplayName = "Get Tenant By Id null")]
        public async Task GetTenantById_Null()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(null as Domain.Tenants.Tenant);

            //Act
            var result = await _tenantRepository.GetTenantById(It.IsAny<long>());

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Update Tenant Status Success")]
        public async Task UpdateTenantStatus_Success()
        {
            //Arrange
            _mockTenantRepository.Setup(r => r.GetByIdAsync(It.IsAny<long>()))
                .ReturnsAsync(new Domain.Tenants.Tenant());
            _mockUnitOfWork.Setup(u => u.CommitAsync())
                .Verifiable();

            //Act
            await _tenantRepository.UpdateTenantStatus(new TenantStatusVM { Status = false, TenantId = 0 });
        }
    }
}
