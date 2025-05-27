using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
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
        public ElasticService(IElasticClient client, IConfiguration configuration) { 
            _client = client;
            _indexName = configuration["Elasticsearch:DefaultIndex"] ?? "tu-indice";
        }

        public async Task EnsureIndexExistAsync()
        {
            var exists = await _client.Indices.ExistsAsync(_indexName);
            if (!exists.Exists)
            {
                await _client.Indices.CreateAsync(_indexName, c => c.Map<PermisosPostDtos>(m => m.AutoMap()));
            }
        }

        public async Task<IndexResponse?> IndexMessageCreatePermisosAsync(PermisosPostDtos permiso)
        {
            var response = await _client.IndexDocumentAsync(permiso);
            return response.IsValid ? response : null;
        }
    }
}
