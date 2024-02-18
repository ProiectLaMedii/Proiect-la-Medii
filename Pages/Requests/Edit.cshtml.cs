using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Requests
{
    public class EditModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;

        public EditModel(DeliveryAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }

        [BindProperty]
        public List<RequestProduct> RequestProducts { get; set; } = new List<RequestProduct>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(r => r.RequestProducts)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Request == null)
            {
                return NotFound();
            }

            RequestProducts = Request.RequestProducts.ToList();

            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Request).State = EntityState.Modified;

            foreach (var rp in RequestProducts)
            {
                var existingRp = _context.RequestProduct.FirstOrDefault(x => x.RequestID == Request.ID && x.ProductID == rp.ProductID);
                if (existingRp != null)
                {
                    existingRp.Quantity = rp.Quantity;
                }
                else
                {
                    rp.RequestID = Request.ID;
                    _context.RequestProduct.Add(rp);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(Request.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RequestExists(int id)
        {
            return _context.Request.Any(e => e.ID == id);
        }
    }
}
