using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorCoordinator
{
    public class RoundRobin
    {
        private readonly List<string> _serviceUrls;
        private int _index = 0;

        public RoundRobin(IEnumerable<string> serviceUrls)
        {
            _serviceUrls = serviceUrls.ToList();
        }

        public HttpClient GetNextClient()
        {
            var url = _serviceUrls[_index];
            _index = (_index + 1) % _serviceUrls.Count;

            var client = new HttpClient { BaseAddress = new Uri(url) };

            return client;
        }
    }
}
