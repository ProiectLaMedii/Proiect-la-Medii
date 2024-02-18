using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace DeliveryAplication.Pages.Requests
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryAplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(DeliveryAplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Request> Request { get; set; }

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                Request = await _context.Request
                    .Include(r => r.Client)
                    .Include(r => r.RequestProducts)
                        .ThenInclude(rp => rp.Product)
                    .Include(r => r.DeliveryLocation) // Include DeliveryLocation
                    .Where(r => r.Client.Email == userEmail)
                    .ToListAsync();
                foreach (var request in Request)
                {
                    request.TotalPrice = request.CalculateTotalPrice();
                }
            }
            else
            {
                Request = new List<Request>();
            }
        }
    }
}