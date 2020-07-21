using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcDemo.Database.Entities;
using GrpcDemo.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrpcDemo.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public BusinessController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost("add")]
        public IActionResult Add(BusinessCreationDto dto)
        {
            var entity = new Database.Entities.Business()
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.Name,
                Tel = dto.Tel,
                Address = dto.Address,
                Email = dto.Email,
                CreatedTime = DateTime.Now
            };

            _db.Businesses.Add(entity);
            var res = _db.SaveChanges() == 1;

            return Ok(res ? "Success" : "Fail");
        }

        [HttpPost("addlist")]
        public IActionResult AddList(List<BusinessCreationDto> dto)
        {
            var entities = new List<Database.Entities.Business>();
            foreach (var item in dto)
            {
                var entity = new Database.Entities.Business()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = item.Name,
                    Tel = item.Tel,
                    Address = item.Address,
                    Email = item.Email,
                    CreatedTime = DateTime.Now
                };
                entities.Add(entity);
            }

            _db.Businesses.AddRange(entities);
            var res = _db.SaveChanges() > 0 ;

            return Ok(res ? "Success" : "Fail");
        }
    }
}
