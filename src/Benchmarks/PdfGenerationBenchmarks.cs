using System.Net.Http;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Toolchains.InProcess.Emit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Benchmarks
{
    [Config(typeof(Config))]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    //[InProcess]
    [MemoryDiagnoser]
    public class PdfGenerationBenchmarks
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.MediumRun
                    .WithLaunchCount(1)
                    .With(InProcessEmitToolchain.Instance)
                    .WithId("OutOfProc"));

                //Add(Job.MediumRun
                //    .WithLaunchCount(1)
                //    .With(InProcessEmitToolchain.Instance)
                //    .WithId("InProcess"));
            }
        }

        private HttpClient _client;
        private TestServer _server;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();

            _server = new TestServer(builder);

            _client = _server.CreateClient();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _client?.Dispose();
            _server?.Dispose();
        }

        [Benchmark]
        public async Task<HttpResponseMessage> GetLargePdf()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/getLarge");
            return await _client.SendAsync(request);
        }

        [Benchmark]
        public async Task<HttpResponseMessage> GetSmallPdf()
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, "/getSmall");
            return await _client.SendAsync(request);
        }
    }
}
