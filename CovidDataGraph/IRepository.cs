using System.Collections.Generic;
using System.Threading.Tasks;

namespace CovidDataGraph
{
    public interface IRepository
    {
        Task<T> Get<T>();
    }
}
