using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Task.Core.Entities
{
    public class Product:BaseEntity
    {
        [MaxLength(50)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice {  get; set; }
        public string? UserId {  get; set; }
        public virtual user User { get; set; }
    }
}
