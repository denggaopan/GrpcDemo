using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcDemo.Dtos;
using Microsoft.Extensions.Logging;

namespace GrpcDemo.GrpcApi
{
    public class HealthService : Healther.HealtherBase
    {
        private readonly ILogger<HealthService> _logger;
        public HealthService(ILogger<HealthService> logger)
        {
            _logger = logger;
        }

        public override Task<HealthResult> Check(HealthRequest request, ServerCallContext context)
        {
            _logger.LogTrace("do check.");
            return Task.FromResult(new HealthResult
            {
                Result = $"GrpcApi is healthful! -{DateTime.Now}-{request.Id}"
            });
        }
    }
}
