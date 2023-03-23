using System.ComponentModel.DataAnnotations;

namespace TestApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Display(Name="Номер заказа")]
        public string Number { get; set; } = null!;
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
        [Display(Name = "Поставщик")]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; } = null!;

        public List<OrderItem> OrderItems { get; set; }=new List<OrderItem>();  

    }

    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public string Name { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;
    }

    public class Provider
    {
        public int Id { get; set; }
        [Display(Name = "Поставщик")]
        public string Name { get; set; } = null!;

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
