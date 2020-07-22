using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcDemo.Dtos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using GrpcDemo.Database.Entities;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace GrpcDemo.GrpcApi.Services
{
    public class BusinessService : Dtos.Business.BusinessBase
    {
        private readonly ILogger<BusinessService> _logger;
        private readonly ApplicationDbContext _db;
        public BusinessService(ILogger<BusinessService> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public override Task<BusinessListResult> GetList(QueryData data, ServerCallContext context)
        {
            var page = data.Page;
            var limit = data.Limit;
            var q = _db.Businesses.OrderByDescending(a => a.CreatedTime).Skip((page - 1) * limit).Take(limit);
            var list = q.Select(a => new BusinessItemResult()
            {
                Id = a.Id,
                Name = a.Name,
                Address = a.Address,
                Tel = a.Tel,
                Email = a.Email
            });
            var res = new BusinessListResult();
            res.List.AddRange(list.ToList());
            return Task.FromResult(res); 
        }


        public override Task<BusinessCreationResult> Add(BusinessCreationData data, ServerCallContext context)
        {
            var entity = new Database.Entities.Business()
            {
                Id = Guid.NewGuid().ToString(),
                Name = data.Name,
                Tel = data.Tel,
                Address = data.Address,
                Email = data.Email,
                CreatedTime = DateTime.Now
            };
            _db.Businesses.Add(entity);
            var res = _db.SaveChanges() == 1;

            return Task.FromResult(new BusinessCreationResult
            {
                Message = res ? "Success" : "Fail"
            });
        }


        public override Task<BusinessCreationResult> AddList(BusinessListCreationData list, ServerCallContext context)
        {
            var entities = new List<Database.Entities.Business>();
            foreach (var data in list.BusinessesCreationData)
            {
                var entity = new Database.Entities.Business()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = data.Name,
                    Tel = data.Tel,
                    Address = data.Address,
                    Email = data.Email,
                    CreatedTime = DateTime.Now
                };
                entities.Add(entity);
            }
            _db.Businesses.AddRange(entities);
            var res = _db.SaveChanges() > 0;

            return Task.FromResult(new BusinessCreationResult
            {
                Message = res ? "Success" : "Fail"
            });
        }

    }
}
