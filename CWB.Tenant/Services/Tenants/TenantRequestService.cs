using AutoMapper;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Constants.MessageBrokers;
using CWB.Constants.Tenant;
using CWB.Logging;
using CWB.Tenant.Domain.Tenants;
using CWB.Tenant.Infrastructure;
using CWB.Tenant.Repositories.Tenants;
using CWB.Tenant.TenantUtils;
using CWB.Tenant.ViewModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.Tenant.Services.Tenants
{
    public class TenantRequestService : ITenantRequestService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantRequestRepository _tenantRequestRepository;
        private readonly IMessageBroker _messageBroker;
        private readonly IOptions<KafkaConfig> _kafkaConfigOptions;
        private readonly IOptions<AppConfig> _appConfig;
        public TenantRequestService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork, ITenantRequestRepository tenantRequestRepository,
            IMessageBroker messageBroker, IOptions<KafkaConfig> kafkaConfigOptions, IOptions<AppConfig> appConfig)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tenantRequestRepository = tenantRequestRepository;
            _messageBroker = messageBroker;
            _kafkaConfigOptions = kafkaConfigOptions;
            _appConfig = appConfig;
        }
        public async Task AddRequest(TenantRequestsVM request)
        {
            var tenantRequest = _mapper.Map<TenantRequest>(request);
            //initially set to pending
            tenantRequest.RequestStatus = TenantRequestStatus.Pending;
            await _tenantRequestRepository.AddAsync(tenantRequest);
            await _unitOfWork.CommitAsync();
            if (_appConfig.Value.Notify)
            {
                //add to message broker on tenant request
                var messageInfo = new MessageInfo
                {
                    MessageObject = _mapper.Map<TenantRequestsListVM>(tenantRequest),
                    MessageType = MessageTypes.TenantRequest
                };

                await _messageBroker.SendAsync(_kafkaConfigOptions.Value, messageInfo);
            }

        }

        public bool CheckDuplicateRequestByEmail(string email)
        {
            return _tenantRequestRepository.GetRangeAsync(r => r.Email == email).Any();
        }

        public bool CheckRequestStatusById(long id, string status)
        {
            TenantRequestStatus requestStatus;
            if (Enum.TryParse(status, out requestStatus))
            {
                return _tenantRequestRepository.GetRangeAsync(r => r.Id == id && r.RequestStatus == requestStatus).Any();
            }
            else
            {
                _logger.LogError($"Invalid Status: {status}");
                throw new Exception($"Invalid Status: {status}");
            }

        }

        public async Task<IEnumerable<TenantRequestsListVM>> GetAllRequests()
        {
            var requests = await _tenantRequestRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TenantRequestsListVM>>(requests);
        }

        public IEnumerable<TenantRequestsListVM> GetAllRequestsByStatus(string status)
        {
            TenantRequestStatus requestStatus;
            if (Enum.TryParse(status, out requestStatus))
            {
                var requests = _tenantRequestRepository.GetRangeAsync(r => r.RequestStatus == requestStatus);
                return _mapper.Map<IEnumerable<TenantRequestsListVM>>(requests);
            }
            else
            {
                _logger.LogError($"Invalid Status: {status}");
                throw new Exception($"Invalid Status: {status}");
            }

        }

        public async Task<TenantRequestsListVM> GetRequestById(long id)
        {
            var request = await _tenantRequestRepository.GetByIdAsync(id);

            return request == null ? null : _mapper.Map<TenantRequestsListVM>(request);
        }

        public async Task UpdateRequestStatus(long id, string status, string comments)
        {
            TenantRequestStatus requestStatus;
            if (Enum.TryParse(status, out requestStatus))
            {
                var request = await _tenantRequestRepository.GetByIdAsync(id);
                request.RequestStatus = requestStatus;
                if (!string.IsNullOrEmpty(comments))
                {
                    request.Comments = comments;
                }
                await _unitOfWork.CommitAsync();
                if (_appConfig.Value.Notify)
                {
                    //add to message broker on tenant request
                    var messageInfo = new MessageInfo
                    {
                        MessageObject = _mapper.Map<TenantRequestsListVM>(request),
                        MessageType = (status == TenantStatus.Approve) ? MessageTypes.TenantRequestApproved : MessageTypes.TenantRequestRejected
                    };
                    await _messageBroker.SendAsync(_kafkaConfigOptions.Value, messageInfo);
                }

            }
            else
            {
                _logger.LogError($"Invalid Status: {status}");
                throw new Exception($"Invalid Status: {status}");
            }
        }
    }
}
