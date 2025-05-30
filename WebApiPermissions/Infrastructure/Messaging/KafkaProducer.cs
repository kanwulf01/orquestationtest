﻿using Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Messaging
{
    public class KafkaProducer : IDisposable, IKafkaServices
    {
        private readonly IProducer<Null, string> _producer;
        private readonly ILogger<KafkaProducer> _logger;

        public KafkaProducer(IConfiguration config, ILogger<KafkaProducer> logger)
        {
            _logger = logger;

            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config["Kafka:BootstrapServers"],
                MessageTimeoutMs = 30000,
                EnableDeliveryReports = true,
                Acks = Acks.Leader
            };

            _producer = new ProducerBuilder<Null, string>(producerConfig)
                .SetLogHandler((_, logMessage) =>
                    _logger.LogInformation($"Kafka Log: {logMessage.Message}"))
                .Build();
        }

        public async Task ProduceAsync<T>(string topic, T message)
        {
            try
            {
                var json = JsonSerializer.Serialize(message);
                var topicPartition = new TopicPartition(topic, new Partition(2));
                var deliveryResult = await _producer.ProduceAsync(topicPartition,
                    new Message<Null, string> { Value = json });

                _logger.LogInformation($"*** ENTREGADO A ***: {deliveryResult.TopicPartitionOffset}");
            }
            catch (ProduceException<Null, string> e)
            {
                _logger.LogError($"Error de entrega: {e.Error.Reason}");
                throw;
            }
        }

        public void Dispose()
        {
            _producer.Flush();
            _producer.Dispose();
        }
    }
}
