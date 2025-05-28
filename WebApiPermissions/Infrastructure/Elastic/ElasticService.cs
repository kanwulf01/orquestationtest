using Application.DTOs;
using Application.Interfaces;
using Confluent.Kafka;
using Domain.Entities;
using Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Elastic
{
    public class ElasticService: IElasticService
    {
        private readonly IElasticClient _client;
        private readonly string _indexName;
        private readonly ILogger<ElasticService> _logger;
        public ElasticService(IElasticClient client, IConfiguration configuration, ILogger<ElasticService> logger) { 
            _client = client;
            _indexName = configuration["Elasticsearch:DefaultIndex"] ?? "tu-indice";
            _logger = logger;
        }

        public async Task EnsureIndexExistAsync()
        {
            var exists = await _client.Indices.ExistsAsync(_indexName);
            if (!exists.Exists)
            {
                _logger.LogInformation($"Existe Elastic?: {_indexName}");
                await _client.Indices.CreateAsync(_indexName, c => c.Map<PermisosPostDtos>(m => m.AutoMap()));
            }
        }

        public async Task<IndexResponse?> IndexMessageCreatePermisosAsync(PermisosPostDtos permiso)
        {
            _logger.LogInformation($"Enviado a Elastic: {permiso.NombreEmpleado}");
            var response = await _client.IndexDocumentAsync(permiso);
            _logger.LogInformation($"Respuesta de Elastic: {response.IsValid}");
            return response.IsValid ? response : null;
        }
    }
}
