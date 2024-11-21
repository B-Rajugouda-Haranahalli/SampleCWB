using AutoMapper;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Constants.MessageBrokers;
using CWB.Logging;
using CWB.Tenant.Infrastructure;
using CWB.Tenant.Repositories.Tenants;
using CWB.Tenant.TenantUtils;
using CWB.Tenant.ViewModels;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Tenant.Services.Tenants
{
    public class TenantService : ITenantService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IOptions<KafkaConfig> _kafkaConfigOptions;
        private readonly IOptions<AppConfig> _appConfig;

        public TenantService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, ITenantRepository tenantRepository,
            IMessageBroker messageBroker, IOptions<KafkaConfig> kafkaConfigOptions, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantRepository = tenantRepository;
            _messageBroker = messageBroker;
            _kafkaConfigOptions = kafkaConfigOptions;
            _appConfig = appConfig;
        }

        public async Task AddTenant(TenantVM tenantVM, string tenantCode)
        {
            var tenant = _mapper.Map<Domain.Tenants.Tenant>(tenantVM);
            //initially set to true
            tenant.IsActive = true;
            tenant.TenantCode = tenantCode;
            await _tenantRepository.AddAsync(tenant);
            await _unitOfWork.CommitAsync();
            if (_appConfig.Value.Notify)
            {
                //add to message broker on tenant request
                var messageInfo = new MessageInfo
                {
                    MessageObject = tenant,
                    MessageType = MessageTypes.TenantCreated
                };

                await _messageBroker.SendAsync(_kafkaConfigOptions.Value, messageInfo);
            }
        }

        public bool CheckDuplicateTenantByCode(string code)
        {
            return _tenantRepository.GetRangeAsync(r => r.TenantCode == code).Any();
        }

        public bool CheckDuplicateTenantByEmail(string email)
        {
            return _tenantRepository.GetRangeAsync(r => r.Email == email).Any();
        }

        public async Task<IEnumerable<TenantsListVM>> GetAllTenants()
        {
            var tenants = await _tenantRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TenantsListVM>>(tenants);
        }

        public async Task<TenantsListVM> GetTenantById(long id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
            return tenant == null ? null : _mapper.Map<TenantsListVM>(tenant);
        }

        public async Task UpdateTenantStatus(TenantStatusVM tenantStatusVM)
        {
            var tenant = await _tenantRepository.GetByIdAsync(tenantStatusVM.TenantId);
            tenant.IsActive = tenantStatusVM.Status;
            await _unitOfWork.CommitAsync();
        }
    }
}
