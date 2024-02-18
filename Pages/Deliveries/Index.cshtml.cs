using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;
using Microsoft.AspNetCore.Identity;

namespace DeliveryAplication.Pages.Deliveries
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

        public IList<Delivery> Delivery { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var userEmail = user.Email;

                Delivery = await _context.Delivery
                    .Include(d => d.Client)
                    .Include(d => d.DeliveryLocation)
                    .Include(d => d.Driver)
                    .Include(d => d.PickupLocation)
                    .Where(d => d.Client.Email == userEmail)
                    .ToListAsync();
            }
            else
            {
                Delivery = new List<Delivery>();
            }
        }
    }
}
    

