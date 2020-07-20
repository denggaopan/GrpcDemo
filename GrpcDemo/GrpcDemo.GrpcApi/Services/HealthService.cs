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
            return Task.FromResult(new HealthResult
            {
                Result = $"GrpcApi-{DateTime.Now}-{request.Id}"
            });
        }
    }
}
