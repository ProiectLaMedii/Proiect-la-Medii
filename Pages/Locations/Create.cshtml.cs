using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Locations
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
            return Page();
        }

        [BindProperty]
        public Location Location { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Location.Picture))
                {
                    string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images");
                    string imagePath = Path.Combine(imagesFolder, Location.Picture);
                    if (!System.IO.File.Exists(imagePath))
                    {
                        ModelState.AddModelError("Location.Picture", "The specified image does not exist.");
                    }
                }
                return Page();
            }

            // Save the location
            _context.Location.Add(Location);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}