using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Requests
{
    public class DeleteModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;

        public DeleteModel(DeliveryAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Request Request { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.Request
            .Include(r => r.Client)
            .Include(r => r.RequestProducts)
                .ThenInclude(rp => rp.Product)
            .FirstOrDefaultAsync(m => m.ID == id);

            if (Request != null)
            {
                Request.RequestProducts = Request.RequestProducts.ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _context.Request
                .Include(r => r.RequestProducts)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Request != null)
            {
                // Remove all RequestProducts associated with the Request
                _context.RequestProduct.RemoveRange(Request.RequestProducts);

                _context.Request.Remove(Request);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
