using SortApi.Dtos;
using SortApi.Dtos.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SortApi.Repository
{
    public interface IRepository
    {
        public Task Sort(SortRequest sortRequest);
        public Task<ICollection<SortResponse>> Get();
        public Task<SortResponse> Get(Guid RequestId);
    }
}
