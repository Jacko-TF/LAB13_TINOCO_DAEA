using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LAB13_TINOCO_DAEA.Models;
using LAB13_TINOCO_DAEA.Models.Response;

namespace LAB13_TINOCO_DAEA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetailsAPIController : ControllerBase
    {
        private readonly MarketContext _context;

        public DetailsAPIController(MarketContext context)
        {
            _context = context;
        }

        // GET: api/DetailsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Detail>>> GetDetails()
        {
          if (_context.Details == null)
          {
              return NotFound();
          }
            return await _context.Details.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<DetailResponseV1>>> GetDetailsByInvoiceNumber(string invoiceNumber)
        {
            if (_context.Details == null)
            {
                return NotFound();
            }

            List<DetailResponseV1> detailsResponse = new();

            var details = await _context.Details.Where(x => x.Invoice.InvoiceNumber == invoiceNumber).ToListAsync();

            foreach (var item in details)
            {
                DetailResponseV1 detailResponseV1 = new DetailResponseV1();

                detailResponseV1.Amount = item.Amount;
                detailResponseV1.Price = item.Price;
                detailResponseV1.DetailID = item.DetailID;
                detailResponseV1.Price = item.Price;

                detailResponseV1.Invoice = item.Invoice;

                detailResponseV1.Customer = item.Invoice.Customer;
            }

            return detailsResponse.OrderBy(x => x.Customer.FirstName).OrderBy(x => x.Invoice.InvoiceNumber).ToList();
        }

        // GET: api/DetailsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Detail>> GetDetail(int id)
        {
          if (_context.Details == null)
          {
              return NotFound();
          }
            var detail = await _context.Details.FindAsync(id);

            if (detail == null)
            {
                return NotFound();
            }

            return detail;
        }

        // PUT: api/DetailsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetail(int id, Detail detail)
        {
            if (id != detail.DetailID)
            {
                return BadRequest();
            }

            _context.Entry(detail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DetailsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Detail>> PostDetail(Detail detail)
        {
          if (_context.Details == null)
          {
              return Problem("Entity set 'MarketContext.Details'  is null.");
          }
            _context.Details.Add(detail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDetail", new { id = detail.DetailID }, detail);
        }

        // DELETE: api/DetailsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetail(int id)
        {
            if (_context.Details == null)
            {
                return NotFound();
            }
            var detail = await _context.Details.FindAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            _context.Details.Remove(detail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetailExists(int id)
        {
            return (_context.Details?.Any(e => e.DetailID == id)).GetValueOrDefault();
        }
    }
}
