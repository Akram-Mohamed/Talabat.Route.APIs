﻿using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Talabat.Core.Services.Contract.IResponseCasheService;

namespace Talabat.Services.CashService
{
    public class ResponseCacheService : IResponseCacheService
    {
        private IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task CacheResponseAsync(string key, object Response, TimeSpan timeToLive)
        {

            if (Response is null) return;

            var serializeOptions = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse = JsonSerializer.Serialize(Response, serializeOptions);

            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }

        public async Task<string?> GetCachedResponseAsync(string Key)
        {
            var response = await _database.StringGetAsync(Key);
            if (response.IsNullOrEmpty) return null;

            return response;
        }
    }
}
