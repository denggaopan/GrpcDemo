using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using Grpc.Core;
using GrpcDemo.Dtos;
using Newtonsoft.Json;
using static GrpcDemo.Dtos.Business;
using static GrpcDemo.Dtos.Healther;

namespace GrpcDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            for (var i = 0; i < 10; i++)
            {
                //checkHealth();
                //addBusiness();
                addBusinessList();
            }


            Console.ReadKey();
        }

        private static void checkHealth()
        {
            var tw = new Stopwatch();

            Console.WriteLine($"============{DateTime.Now}============");
            tw.Start();
            using (HttpClient http = new HttpClient())
            {
                var res = http.GetStringAsync("http://localhost:49001/api/health").Result;
                Console.WriteLine($"call restapi:{res}");
            }
            tw.Stop();
            Console.WriteLine(DateTime.Now + "killtime:" + tw.ElapsedMilliseconds / 1000 + "s");

            Console.WriteLine($"============{DateTime.Now}============");
            tw.Start();
            var channel = new Channel("localhost:49002", ChannelCredentials.Insecure);
            var client = new HealtherClient(channel);
            var result = client.Check(new HealthRequest { Id = "1" }, new CallOptions());
            Console.WriteLine($"call grpcapi:{result}");
            tw.Stop();
            Console.WriteLine(DateTime.Now + "killtime:" + tw.ElapsedMilliseconds / 1000 + "s");
        }

        private static void addBusiness()
        {
            var tw = new Stopwatch();

            Console.WriteLine($"============{DateTime.Now}============");
            tw.Start();
            using (HttpClient http = new HttpClient())
            {
                //var json = "{\"name\":\"金康3\",\"address\":\"中国武汉\",\"tel\":\"400888999\",\"email\":\"contact@jk.com\"}";
                var dto = new BusinessCreationDto();
                dto.Name = "jk";
                dto.Address = "中国武汉";
                dto.Tel = "888888";
                dto.Email = "a@a.com";
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json,Encoding.UTF8, "application/json");
                var res = http.PostAsync("http://localhost:49001/api/business/add",content).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"call restapi:{res}");
            }
            tw.Stop();
            Console.WriteLine(DateTime.Now + "==>use time:" + tw.ElapsedMilliseconds + "ms");

            Console.WriteLine($"============{DateTime.Now}============");
            tw.Restart();
            var channel = new Channel("localhost:49002", ChannelCredentials.Insecure);
            var client = new BusinessClient(channel);
            var data = new BusinessCreationData();
            data.Name = "金康3g";
            data.Address = "中国武汉";
            data.Tel = "888888";
            data.Email = "a@a.com";
            var result = client.Add(data);
            Console.WriteLine($"call grpcapi:{result.Message}");
            tw.Stop();
            Console.WriteLine(DateTime.Now + "==>use time::" + tw.ElapsedMilliseconds + "ms");
        }

        private static void addBusinessList()
        {
            var loop = 10000;

            var tw = new Stopwatch();
            Console.WriteLine($"============{DateTime.Now}============");
            tw.Start();
            using (HttpClient http = new HttpClient())
            {
                var dto = new List<BusinessCreationDto>();
                for (var i = 0; i < loop; i++)
                {
                    var item = new BusinessCreationDto();
                    item.Name = "金康4";
                    item.Address = "中国武汉";
                    item.Tel = "888888";
                    item.Email = "a@a.com";
                    dto.Add(item);
                }
                var json = JsonConvert.SerializeObject(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var res = http.PostAsync("http://localhost:49001/api/business/addlist", content).Result.Content.ReadAsStringAsync().Result;
                Console.WriteLine($"call restapi:{res}");
            }
            tw.Stop();
            Console.WriteLine(DateTime.Now + "==>use time:" + tw.ElapsedMilliseconds + "ms");

            Console.WriteLine($"============{DateTime.Now}============");
            tw.Restart();
            var channel = new Channel("localhost:49002", ChannelCredentials.Insecure);
            var client = new BusinessClient(channel);
            var data = new BusinessListCreationData();
            for (var i = 0; i < loop; i++)
            {
                var item = new BusinessCreationData();
                item.Name = "金康4g";
                item.Address = "中国武汉";
                item.Tel = "888888";
                item.Email = "a@a.com";
                data.BusinessesCreationData.Add(item);
            }
            var result = client.AddList(data);
            Console.WriteLine($"call grpcapi:{result.Message}");
            tw.Stop();
            Console.WriteLine(DateTime.Now + "==>use time::" + tw.ElapsedMilliseconds + "ms");
        }
    }
}
