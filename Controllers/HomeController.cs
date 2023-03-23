using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly MyContext db;
        public HomeController(MyContext db)
        {

            this.db = db;
        }

        public IActionResult Index(int providerId, string? numberOrder, DateTime MinDate, DateTime MaxDate, string? itemName, string? itemUnit)
        {
            List<Provider> providers = db.Providers.OrderBy(p => p.Name).ToList();

            IQueryable<Order> orders = db.Orders.Include(o => o.Provider).Include(o => o.OrderItems)
                .Where(o=>o.Date >= (MinDate == DateTime.MinValue ? DateTime.Today.AddMonths(-1).AddDays(-1) : MinDate))
                .Where(o=>o.Date < (MaxDate == DateTime.MinValue ? DateTime.Today.AddDays(1) : MaxDate.AddDays(1)));
            if (providerId != 0)
                orders = orders.Where(p => p.ProviderId == providerId);

            if(!string.IsNullOrEmpty(numberOrder?.Replace("Все",string.Empty)))
                orders=orders.Where(o => o.Number.Contains(numberOrder));

            if (!string.IsNullOrEmpty(itemName?.Replace("Все", string.Empty)))
            {
                var arrName = db.OrderItems.Where(i => i.Name.Equals(itemName)).Select(i => i.OrderId).ToArray();
                orders = orders.Where(o => arrName.Contains(o.Id));
            }
            if (!string.IsNullOrEmpty(itemUnit?.Replace("Все", string.Empty)))
            {
                var arrUnit = db.OrderItems.Where(i => i.Unit.Equals(itemUnit)).Select(i => i.OrderId).ToArray();
                orders = orders.Where(o => arrUnit.Contains(o.Id));
            }
            var orderNumbers = db.Orders.OrderBy(o => o.Number).Select(o => o.Number).Distinct().ToList();
            var itemNames = db.OrderItems.OrderBy(o => o.Name).Select(o => o.Name).Distinct().ToList();
            var itemUnits = db.OrderItems.OrderBy(o => o.Unit).Select(o => o.Unit).Distinct().ToList();
            FilterOrderViewModel fvm = new 
            (
                orders.OrderBy(o=>o.Number).ToList(), providers, orderNumbers, itemNames, itemUnits, providerId, numberOrder,itemName,itemUnit, MinDate, MaxDate

            );
            return View(fvm);
        }

    
    }
}