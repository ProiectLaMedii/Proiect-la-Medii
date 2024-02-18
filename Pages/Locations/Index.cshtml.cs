using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;
using DeliveryAplication.Models.ViewModels;

namespace DeliveryAplication.Pages.Locations
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryAplication.Data.DeliveryAplicationContext _context;

        public IndexModel(DeliveryAplication.Data.DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IList<Location> Location { get; set; } = default!;

        public LocationIndexData LocationData { get; set; }
        public int LocationID { get; set; }
        
        public async Task OnGetAsync(int? id, int? bookID)
        {
            LocationData = new LocationIndexData();
            LocationData.Locations = await _context.Location
            .Include(i => i.Delivery) // Include Delivery property

            .OrderBy(i => i.LocationName)
            .ToListAsync();
            if (id != null)
            {
                LocationID = id.Value;
                Location location = LocationData.Locations
                .Where(i => i.ID == id.Value).Single();
                LocationData.Delivery = location.Delivery; // Assign Delivery property
            }
        }
    }
}