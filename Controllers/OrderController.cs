using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly MyContext db;
        public OrderController(ILogger<OrderController> logger, MyContext db)
        {
            _logger = logger;
            this.db = db;
        }

        public ActionResult Details(int Id)
        {
            var order=db.Orders.Include(o=>o.Provider).Include(o => o.OrderItems).FirstOrDefault(o=>o.Id==Id);
            return View(order);
        }

        public ActionResult Create()
        {
            ViewBag.Providers = new SelectList(db.Providers.OrderBy(p => p.Name), "Id", "Name");
            var newOrder = new Order() { Date=DateTime.Today };
            return View(newOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var newOrd = new Order()
                {
                    Date = Convert.ToDateTime(collection["Date"].ToString()),
                    Number = collection["Number"].ToString(),
                    ProviderId = Convert.ToInt32(collection["ProviderId"].ToString()),
                    OrderItems = new List<OrderItem>()
                };
                db.Orders.Add(newOrd);
                db.SaveChanges();
            
                return RedirectToAction(nameof(Edit), new { newOrd.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Providers = new SelectList(db.Providers.OrderBy(p => p.Name), "Id", "Name");
            var order = db.Orders.Include(o => o.Provider).Include(o=>o.OrderItems).FirstOrDefault(o => o.Id == Id);
            return View(order);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, IFormCollection collection)
        {
            try
            {
                var num = collection["Number"].ToString();
                var date = collection["Date"].ToString();
                var pid = collection["ProviderId"].ToString();
                var order = db.Orders.First(o => o.Id == Convert.ToUInt32(Id));
                order.Number = num;
                order.Date = Convert.ToDateTime(date);
                order.ProviderId = Convert.ToInt32(pid);
                db.Orders.Update(order);
                db.SaveChanges();
                return RedirectToAction(nameof(Index),"Home");
            }
            catch
            {
                return View();
            }
        }

      
    }
}
