using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Pets
{
    public class CreateModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;

        public CreateModel(DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            return Page();
        }

        [BindProperty]
        public Pet Pet { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Pet.Add(Pet);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error when saving pet: {ex.Message}");
                // For debugging: You can also output the exception to see what's going wrong
                ViewData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return Page();
            }
        }

    }
}
