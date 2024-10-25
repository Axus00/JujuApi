using System.Text.Json.Serialization;

namespace ProjectApi.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; } = "Activo";
        public bool IsActive { get; set; } = true;
    }
}