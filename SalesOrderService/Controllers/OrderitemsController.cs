using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SalesOrderService.Data;
using SalesOrderService.Models;

namespace SalesOrderService.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrderitemsController : ControllerBase {
        private readonly AppDbContext _context;

        public OrderitemsController(AppDbContext context) {
            _context = context;
        }

        // GET: api/Orderitems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orderitem>>> GetOrderitems() {
            return await _context.Orderitems.ToListAsync();
        }

        private void RecalculateTotel(int orderId) {
            var order = _context.Orders.Find(orderId);
            if(order == null) throw new Exception("Cannot find order for recalculation!");
            order.Total = (from oi in order.Orderitems
                           select new {
                               LineTotal = oi.Quantity * oi.Item.Price
                           }).Sum(x => x.LineTotal);
            _context.SaveChanges();
        }        

        // GET: api/Orderitems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orderitem>> GetOrderitem(int id) {
            var orderitem = await _context.Orderitems.FindAsync(id);

            if(orderitem == null) {
                return NotFound();
            }

            return orderitem;
        }

        // PUT: api/Orderitems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderitem(int id, Orderitem orderitem) {
            if(id != orderitem.Id) {
                return BadRequest();
            }

            _context.Entry(orderitem).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
                _context.Entry(orderitem).State = EntityState.Detached;
                _context.Orderitems.Find(orderitem.Id); // refresh the cache
                RecalculateTotel(orderitem.OrderId);
            } catch(DbUpdateConcurrencyException) {
                if(!OrderitemExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orderitems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Orderitem>> PostOrderitem(Orderitem orderitem) {
            _context.Orderitems.Add(orderitem);
            await _context.SaveChangesAsync();
            _context.Entry(orderitem).State = EntityState.Detached;
            _context.Orderitems.Find(orderitem.Id); // refresh the cache
            RecalculateTotel(orderitem.OrderId);

            return CreatedAtAction("GetOrderitem", new { id = orderitem.Id }, orderitem);
        }

        // DELETE: api/Orderitems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orderitem>> DeleteOrderitem(int id) {
            var orderitem = await _context.Orderitems.FindAsync(id);
            if(orderitem == null) {
                return NotFound();
            }

            _context.Orderitems.Remove(orderitem);
            await _context.SaveChangesAsync();

            RecalculateTotel(orderitem.OrderId);

            return orderitem;
        }

        private bool OrderitemExists(int id) {
            return _context.Orderitems.Any(e => e.Id == id);
        }
    }
}
