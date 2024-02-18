using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Facilities
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;

        public EditModel(DeliveryAplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Facility Facility { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Facility = await _context.Facility.FindAsync(id);

            if (Facility == null)
            {
                return NotFound();
            }

            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "LocationName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var facilityToUpdate = await _context.Facility.FindAsync(id);

            if (facilityToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Facility>(
                facilityToUpdate,
                "Facility",
                s => s.Picture, s => s.Room, s => s.Price, s => s.AvailableDate, s => s.LocationID))
            {
                // Additional logic for handling the picture update if necessary
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }

            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "LocationName", facilityToUpdate.LocationID);
            return Page();
        }
    }
}
