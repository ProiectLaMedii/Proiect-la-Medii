using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Requests
{
    public class CreateModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(DeliveryAplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ID", "FullName");
            ViewData["ProductID"] = new SelectList(_context.Product, "ID", "Name");
            ViewData["DeliveryLocationID"] = new SelectList(_context.Location, "ID", "LocationName");

           
            RequestProducts.Add(new RequestProduct());

            return Page();
        }

        private async Task<int> GetCurrentClientIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                var client = await _context.Client
                    .Where(c => c.Email == userEmail)
                    .FirstOrDefaultAsync();

                if (client != null)
                {
                    return client.ID;
                }
            }

            return 0;
        }

        [BindProperty]
        public Request Request { get; set; }

        [BindProperty]
        public List<RequestProduct> RequestProducts { get; set; } = new List<RequestProduct>();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Request.Add(Request);
            await _context.SaveChangesAsync();
            

            foreach (var rp in RequestProducts)
            {
                rp.RequestID = Request.ID;
                _context.RequestProduct.Add(rp);
            }

            await _context.SaveChangesAsync();

            

            // Create a new Delivery object for each product in the request
            foreach (var rp in RequestProducts)
            {
                var product = await _context.Product.FindAsync(rp.ProductID);
                if (product != null)
                {
                    var delivery = new Delivery
                    {
                        OrderNumber = Request.ID.ToString(), // You need to implement this method
                        DispatchTime = DateTime.Now,
                        ArrivalTime = DateTime.Now.AddHours(100), // Adjust this as needed
                        DeliveryStatus = "Pending",
                        ClientID = Request.ClientID,
                        DriverID = await GetAvailableDriverID(), // You need to implement this method
                        PickupLocationID = product.LocationID, // Check if the nullable int has a value
                        DeliveryLocationID = Request.DeliveryLocationID.Value
                    };

                    _context.Delivery.Add(delivery);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        private async Task<int> GetAvailableDriverID()
        {
            // Fetch all driver IDs
            var driverIDs = await _context.Driver.Select(d => d.ID).ToListAsync();

            // Check if there are any drivers
            if (driverIDs.Count == 0)
            {
                throw new Exception("No drivers available");
            }

            // Select a random driver ID
            var random = new Random();
            var randomDriverID = driverIDs[random.Next(driverIDs.Count)];

            return randomDriverID;
        }
    }
}
