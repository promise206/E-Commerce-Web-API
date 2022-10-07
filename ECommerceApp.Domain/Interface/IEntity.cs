using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.Domain.Interface
{
    public interface IEntity
    {
        public string Id { get; set; }
    }
}
