using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestApp.Models
{
    public class FilterOrderViewModel
    {
        public FilterOrderViewModel(List<Order> orders, List<Provider> providers, List<string> orderNumbers, List<string> orderItemNames, List<string> orderItemUnit,
            int providerId, string? numberOrder, string? itemName, string? itemUnit, DateTime minDateOrder, DateTime maxDateOrder )
        {

            providers.Insert(0, new Provider { Name = "Все", Id = 0 });
            Providers = new SelectList(providers, "Id", "Name", providerId);
            orderNumbers.Insert(0, "Все");
            OrderNumbers = new SelectList(orderNumbers, numberOrder);
            orderItemNames.Insert(0, "Все");
            ItemOrderNames = new SelectList(orderItemNames, itemName);
            orderItemUnit.Insert(0, "Все");
            ItemOrderUnit = new SelectList(orderItemUnit, itemUnit);
            SelectedProviderId = providerId;
            SelectedNumber = numberOrder;
            MinDate = minDateOrder==DateTime.MinValue?DateTime.Today.AddMonths(-1): minDateOrder;
            MaxDate = maxDateOrder == DateTime.MinValue ? DateTime.Today : maxDateOrder; ;
            Orders = orders;
        }

        public SelectList OrderNumbers { get; }
        public SelectList Providers { get; }
        public SelectList ItemOrderNames { get; }
        public SelectList ItemOrderUnit { get; }
        public int SelectedProviderId { get; } 
        public string? SelectedNumber { get; } 
        public DateTime MinDate { get; }
        public DateTime MaxDate { get; }
        public List<Order> Orders { get; }
    }
}
