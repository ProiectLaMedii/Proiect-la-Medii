using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeliveryAplication.Data;
using DeliveryAplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace DeliveryAplication.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly DeliveryAplication.Data.DeliveryAplicationContext _context;

        public CreateModel(DeliveryAplication.Data.DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            LocationSelectList = new SelectList(_context.Location, "ID", "LocationName");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public SelectList LocationSelectList { get; set; } // New property

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Product == null || Product == null)
            {
                return Page();
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}