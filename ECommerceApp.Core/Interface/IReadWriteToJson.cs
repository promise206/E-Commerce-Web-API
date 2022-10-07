using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp.Core.Interface
{
    public interface IReadWriteToJson
    {
        Task<List<T>> ReadAllFromJson<T>(string jsonFile);
    }
}
