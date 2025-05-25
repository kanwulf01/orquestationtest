using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Messaging
{
 
        public class KafkaConsumer : BackgroundService
        {
            private readonly IConsumer<Ignore, string> _consumer;
            private readonly ILogger<KafkaConsumer> _logger;
            private readonly TopicPartition _topicPartition;
            private readonly ConsumerConfig _consumerConfig;

            public KafkaConsumer(IConfiguration config, ILogger<KafkaConsumer> logger)
            {
                _logger = logger;

                // 1. Configuración movida a campo de clase
                _consumerConfig = new ConsumerConfig
                {
                    BootstrapServers = "[kafka:BootstrapServers]",
                    GroupId = "my-permissions",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };

                _consumer = new ConsumerBuilder<Ignore, string>(_consumerConfig) 
                    .SetPartitionsAssignedHandler((_, partitions) =>
                        _logger.LogInformation($"Asignadas particiones: {string.Join(",", partitions)}"))
                    .Build();

                // Configurar partición específica
                var topic = "[kafka:TopicName]";
                var partition = new Partition(2);
                _topicPartition = new TopicPartition(topic, partition);

                // Obtener offset inicial

                var storedOffset = GetStoredOffset();

                _consumer.Seek(storedOffset);
            }

            protected override async Task ExecuteAsync(CancellationToken stoppingToken)
            {
                var consumeTask = Task.Run(() => StartConsuming(stoppingToken), stoppingToken);

                while (!stoppingToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, stoppingToken); 
                }

                await consumeTask;
            }

            private TopicPartitionOffset GetStoredOffset()
            {
                //_consumer.Subscribe("orders-events");
                _consumer.Assign(_topicPartition);
                var committed = _consumer.Committed(new List<TopicPartition> { _topicPartition }, TimeSpan.FromSeconds(10))
                                       .FirstOrDefault();

                if (committed.Offset != Offset.Unset)
                {
                    _logger.LogInformation($"Continuando desde offset commitado: {committed.Offset}");
                    return committed;
                }

                var offsets = _consumer.QueryWatermarkOffsets(_topicPartition, TimeSpan.FromSeconds(5));
                var initialOffset = _consumerConfig.AutoOffsetReset == AutoOffsetReset.Earliest // Corregido aquí
                    ? offsets.Low
                    : offsets.High;

                _logger.LogInformation($"Iniciando desde offset: {initialOffset}");
                return new TopicPartitionOffset(_topicPartition, initialOffset);
            }


            private async Task StartConsuming(CancellationToken ct)
            {
                try
                {
                    while (!ct.IsCancellationRequested)
                    {
                        try
                        {
                            var result = await Task.Run(() => _consumer.Consume(ct), ct);

                            if (result == null)
                            {
                                await Task.Delay(100, ct);
                                continue;
                            }

                            if (!result.IsPartitionEOF)
                            {
                                await ProcessMessageAsync(result.Message.Value);
                                await CommitOffsetAsync(result, ct);
                            }
                        }
                        catch (ConsumeException e)
                        {
                            _logger.LogError($"Error de consumo: {e.Error.Reason}");
                            if (e.Error.IsFatal) throw;
                        }
                        catch (OperationCanceledException)
                        {
                            _logger.LogInformation("Consumo cancelado");
                            break;
                        }
                    }
                }
                finally
                {
                    _consumer.Close();
                }
            }

            private async Task CommitOffsetAsync(ConsumeResult<Ignore, string> result, CancellationToken ct)
            {
                try
                {
                    // Commit asíncrono
                    await Task.Run(() => _consumer.Commit(result), ct);
                    _logger.LogDebug($"Commit exitoso para offset {result.Offset}");
                }
                catch (KafkaException e)
                {
                    _logger.LogError($"Error en commit: {e.Error.Reason}");
                }
            }

            private async Task ProcessMessageAsync(string message)
            {
                await Task.Delay(100);
                _logger.LogInformation($"Mensaje procesado: {message}");
            }

            public override void Dispose()
            {
                _consumer?.Close();
                _consumer?.Dispose();
                base.Dispose();
            }
        }
    }

