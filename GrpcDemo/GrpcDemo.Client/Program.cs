using System;
using System.Net.Http;
using Grpc.Core;
using GrpcDemo.Dtos;
using static GrpcDemo.Dtos.Healther;

namespace GrpcDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            
            using(HttpClient http = new HttpClient())
            {
                var res = http.GetStringAsync("http://localhost:49001/api/health").Result;
                Console.WriteLine($"call restapi:{res}");
            }


            var channel = new Channel("localhost:49002", ChannelCredentials.Insecure);
            var client = new HealtherClient(channel);
            var result = client.Check(new HealthRequest { Id = "1" },new CallOptions());
            Console.WriteLine($"call grpcapi:{result}");

            Console.ReadKey();
        }
    }
}
