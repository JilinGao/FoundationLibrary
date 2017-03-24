using System.Threading.Tasks;
using ServiceClients;
using Xunit;
using ServiceClient = ServiceClients.ServiceClient;

namespace ServiceClientTest
{
    public class ServiceClientUnitTest
    {
        /// <summary>
        ///     �������Get����
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Concurrence()
        {
            var serviceClient = new ServiceClient();
            var taskA = serviceClient.RequestAsync("http://www.bing.com", HttpVerb.Get, new { A = "a" });
            var taskB = serviceClient.RequestAsync("http://www.bing.com", HttpVerb.Get, new { A = "a" });
            var taskC = serviceClient.RequestAsync("http://www.bing.com", HttpVerb.Get, new { B = "b" });
            var taskD = serviceClient.RequestAsync("http://www.bing.com", HttpVerb.Get, new TestClassA { A = "a" });
            var taskE = serviceClient.RequestAsync("http://www.bing.com", HttpVerb.Get, new TestClassA { A = "a" });

            await Task.WhenAll(taskA, taskB, taskC, taskD, taskE);
        }

        /// <summary>
        ///     ����תQueryString�Ĳ�������
        /// </summary>
        [Fact]
        public void MuilitTask_FormUrl_SimpleClass()
        {
            var testA = new TestClassA { A = "a" };

            Parallel.For(1, 100, (i, s) =>
            {
                var result = ServiceClient.FormatUrl("abc.com", testA, HttpVerb.Get);
                Assert.Equal("abc.com?A=a", result);
            });


        }


        public class TestClassA
        {
            public string A { get; set; }
        }

        public class TestClassB
        {
            public string B { get; set; }
        }

    }
}
