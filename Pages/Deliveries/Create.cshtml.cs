using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Deliveries
{
    public class CreateModel : PageModel
    {
        private readonly DeliveryAplication.Data.DeliveryAplicationContext _context;

        public CreateModel(DeliveryAplication.Data.DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ClientID"] = new SelectList(_context.Client, "ID", "ID");
        ViewData["DeliveryLocationID"] = new SelectList(_context.Location, "ID", "ID");
        ViewData["DriverID"] = new SelectList(_context.Driver, "ID", "ID");
        ViewData["PickupLocationID"] = new SelectList(_context.Location, "ID", "ID");
            return Page();
        }

        [BindProperty]
        public Delivery Delivery { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Delivery == null || Delivery == null)
            {
                return Page();
            }

            _context.Delivery.Add(Delivery);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
