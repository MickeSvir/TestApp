using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System;
using System.Threading.Tasks;
using TestApp.Models;

namespace TestApp
{

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTestOrder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TestOrderMiddleware>();

        }
        public static IApplicationBuilder UseRemoveOrder(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RemoveOrderMiddleware>();

        }
        public static IApplicationBuilder UseAddOrderItem(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddOrderItemMiddleware>();

        }
        public static IApplicationBuilder UseRemoveOrderItem(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RemoveOrderItemMiddleware>();

        }

    }

    public class AddOrderItemMiddleware
    {
        public Task Invoke(HttpContext httpContext, MyContext db)
        {
            httpContext.Response.ContentType = "text/plain";
            try
            {
                var oid = httpContext.Request.Query["id"].ToString();
                var name = httpContext.Request.Query["name"].ToString();
                var quan = httpContext.Request.Query["quantity"].ToString();
                var unit = httpContext.Request.Query["unit"].ToString();
                var oi = new OrderItem()
                {
                    OrderId = Convert.ToInt32(oid),
                    Name = name,
                    Quantity = Convert.ToDecimal(quan),
                    Unit = unit
                };
                db.OrderItems.Add(oi);
                db.SaveChanges();
                return httpContext.Response.WriteAsync("true");
                
            }
            catch (Exception ex)
            {
                return httpContext.Response.WriteAsync(ex.Message);
            }

        }
    }

    public class RemoveOrderItemMiddleware
    {
 
        public Task Invoke(HttpContext httpContext, MyContext db)
        {
            httpContext.Response.ContentType = "text/plain";
            try
            {
                var oid = httpContext.Request.Query["id"].ToString();
                var oi= db.OrderItems.First(oi=>oi.Id==Convert.ToInt32(oid));
                db.OrderItems.Remove(oi);
                db.SaveChanges();
                return httpContext.Response.WriteAsync("true");
            
            }
            catch(Exception ex)
            {
                return httpContext.Response.WriteAsync(ex.Message);
            }

        }
    }

    public class TestOrderMiddleware
    {
 
        public Task Invoke(HttpContext httpContext, MyContext db)
        {
            
            var pid = httpContext.Request.Query["pid"].ToString();
            var num = httpContext.Request.Query["number"].ToString();
            var count = db.Orders.Where(o=>o.Number.Equals(num)).Where(o=>o.ProviderId.Equals(pid)).Count();
            httpContext.Response.ContentType = "text/plain";
            if (count == 0)
            {
                return httpContext.Response.WriteAsync("true");
            }
            else
            {
                return httpContext.Response.WriteAsync("false");
            }

        }
    }

    public class RemoveOrderMiddleware
    {
 
        public Task Invoke(HttpContext httpContext, MyContext db)
        {
            httpContext.Response.ContentType = "text/plain";
            try
            {
                var oid = httpContext.Request.Query["id"].ToString();
                var order = db.Orders.First(o=>o.Id.Equals(Convert.ToInt32(oid)));
                db.Orders.Remove(order);
                db.SaveChanges();
                return httpContext.Response.WriteAsync("true");
            }
            catch
            {
                return httpContext.Response.WriteAsync("false");
            }

        }
    }

}
