using Confluent.Kafka;
using CWB.CommonUtils.Common;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.MessageBrokers;
using CWB.Constants.MessageBrokers;
using CWB.Constants.Tenant;
using CWB.Logging;
using CWB.TenantBroker.Models;
using CWB.TenantBroker.Services;
using CWB.TenantBroker.TenantBrokerUtils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CWB.TenantBroker
{
    internal class TenantBrokerWorker
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        private readonly IMessageBroker _messageBroker;
        private readonly ITenantBrokerService _tenantBrokerService;

        public TenantBrokerWorker(IConfiguration configuration, ILoggerManager logger, IMessageBroker messageBroker, ITenantBrokerService tenantBrokerService)
        {
            _configuration = configuration;
            _logger = logger;
            _messageBroker = messageBroker;
            _tenantBrokerService = tenantBrokerService;
        }


        public async Task DoWork()
        {
            try
            {

                var kafkaConfig = _configuration.GetSection("KafkaTenantConfig").Get<KafkaConfig>();
                var tenantConfig = _configuration.GetSection("TenantConfig").Get<TenantConfig>();
                await _messageBroker.CreateTopicAsync(kafkaConfig);

                var conf = new ConsumerConfig
                {
                    GroupId = kafkaConfig.GroupId,
                    BootstrapServers = kafkaConfig.Server,
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
                c.Subscribe(kafkaConfig.Topic);
                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true;
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {

                        var cr = c.Consume(cts.Token);
                        Console.WriteLine(cr.Message.Value);
                        var messageInfo = JsonConvert.DeserializeObject<MessageInfo>(cr.Message.Value);
                        await processMessage(messageInfo, tenantConfig);
                    }
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogError(ex.Message);
                }
                finally
                {
                    c.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task processMessage(MessageInfo messageInfo, TenantConfig tenantConfig)
        {
            switch (messageInfo.MessageType)
            {
                case MessageTypes.TenantRequest:
                    if (tenantConfig.AutoApproveRequest)
                    {
                        var request = JsonConvert.DeserializeObject<TenantRequests>(messageInfo.MessageObject.ToString());
                        var requestApprove = new TenantRequestApproveReject
                        {
                            Comments = "Auto Approval",
                            Status = TenantStatus.Approve,
                            TenantRequestId = request.TenantRequestId
                        };
                        await _tenantBrokerService.ApproveRejectTenantRequest(requestApprove);
                    }
                    else
                    {

                    }
                    break;
                case MessageTypes.TenantRequestApproved:
                    var requestApproved = JsonConvert.DeserializeObject<TenantRequests>(messageInfo.MessageObject.ToString());
                    var tenant = new TenantModel
                    {
                        CompanyInfo = requestApproved.CompanyInfo,
                        CompanyName = requestApproved.CompanyName,
                        Email = requestApproved.Email,
                        Phone = requestApproved.Phone
                    };
                    await _tenantBrokerService.CreateTenant(tenant);
                    break;
                case MessageTypes.TenantRequestRejected:
                    break;
                case MessageTypes.TenantCreated:
                    break;
            }
        }
    }
}
