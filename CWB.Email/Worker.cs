using Confluent.Kafka;
using CWB.CommonUtils.KafkaConfigs;
using CWB.CommonUtils.Notifications;
using CWB.Logging;
using CWB.Notification;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace CWB.Email
{
    internal class Worker
    {
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;
        private readonly EmailNotification _emailNotification;

        public Worker(IConfiguration configuration, ILoggerManager logger, EmailNotification emailNotification)
        {
            _configuration = configuration;
            _logger = logger;
            _emailNotification = emailNotification;
        }

        public async void DoWork()
        {
            try
            {


                var emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
                var kafkaConfig = _configuration.GetSection("KafkaTenantConfig").Get<KafkaConfig>();

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
                        var emailInfo = JsonConvert.DeserializeObject<EmailInfo>(cr.Message.Value);
                        await _emailNotification.SendAsync(emailInfo.EmailObject, emailInfo.ToAddress, emailInfo.Name, emailInfo.TemplateId, emailSettings);
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


                Console.WriteLine(emailSettings.ApiKey);
                _logger.LogInfo(emailSettings.ApiKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
