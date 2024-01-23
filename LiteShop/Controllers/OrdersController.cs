using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LiteShop.Entities;

namespace LiteShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApliactionDbContext _context;

        public OrdersController(ApliactionDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var apliactionDbContext = _context.Order.Include(o => o.Customer);
            return View(await apliactionDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer) 
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                    .ThenInclude(p => p.Category)

                .FirstOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/Create
        /*        public IActionResult Create()
                {
                    var productsWithCategories = _context.Products
                        .Include(p => p.Category) 
                        .ToList() 
                        .Select(p => new
                        {
                            p.Id,
                            p.Price,
                            ProductInfo = $"{p.Name} ({p.Category.Name})"
                        });

                    ViewBag.ProductId = new SelectList(productsWithCategories, "Id", "ProductInfo");
                    ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");
                    return View();
                }*/

        public IActionResult Create()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            ViewBag.Products = products;
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name");
            return View();
        }


        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataS,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.DataS = DateTime.Now;
                _context.Add(order);
                await _context.SaveChangesAsync(); // zapisanie orderu


                var productIds = Request.Form["ProductId"];
                int[] productIdsArray = productIds.Select(int.Parse).ToArray();

                foreach (var productId in productIdsArray)
                {
                    var existingOrderDetail = await _context.OrderDetail
                        .FirstOrDefaultAsync(od => od.OrderId == order.Id && od.ProductId == productId);

                    if (existingOrderDetail != null)
                    {
                        // zwiekszenie ilosci
                        existingOrderDetail.Amount += 1;
                    }
                    else
                    {
                        // dodanie nowego rekordu OrderDetail
                        var orderDetail = new OrderDetail
                        {
                            OrderId = order.Id,
                            ProductId = productId,
                            Amount = 1
                        };
                        _context.Add(orderDetail);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", order.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrderDetail(int orderDetailId, int orderId, int productId)
        {
        
            var orderDetail = await _context.OrderDetail
                .FirstOrDefaultAsync(od => od.OrderDetailId == orderDetailId &&
                                           od.OrderId == orderId &&
                                           od.ProductId == productId);

            if (orderDetail != null)
            {
                _context.OrderDetail.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Edit), new { id = orderId });
        }


        
        [HttpPost]
        public async Task<IActionResult> AddProductToOrder(int orderId, int newProductId)
        {
            Console.WriteLine("XXXXXXXXX " + orderId); ;
            var order = await _context.Order.FindAsync(orderId);
            if (order == null)
            {
                // gdy nie znaleziono zamowienia o podanym id
                return NotFound();
            }

            var orderDetail = new OrderDetail
            {
                OrderId = orderId,
                ProductId = newProductId,
                Amount = 1
            };

            _context.OrderDetail.Add(orderDetail);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { id = orderId });
        }


        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.OrderDetails)      // dolaczenie OrderDetails
                    .ThenInclude(od => od.Product) // dolaczenie informacji o produckie
                .Include(o => o.Customer)          // dolaczenie informacji o kliencie
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.AllProducts = new SelectList(_context.Product, "Id", "Name");
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", order.CustomerId);
            return View(order);
        }


        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataS,CustomerId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "Id", "Name", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
