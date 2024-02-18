using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryAplication.Data.DeliveryAplicationContext _context;

        public IndexModel(DeliveryAplication.Data.DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IList<Facility> Facility { get; set; } = default!;

        public int FacilityID { get; set; }
        public int CategoryID { get; set; }
        public string RoomSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder, string searchString)
        {
            RoomSort = String.IsNullOrEmpty(sortOrder) ? "room_desc" : "";
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            CurrentFilter = searchString;

            IQueryable<Facility> facilityQuery = _context.Facility
                .Include(b => b.Location)
                .AsNoTracking();

            if (!String.IsNullOrEmpty(searchString))
            {
                facilityQuery = facilityQuery.Where(s => s.Location.LocationName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "room_desc":
                    facilityQuery = facilityQuery.OrderByDescending(s => s.Room);
                    break;
                case "price_desc":
                    facilityQuery = facilityQuery.OrderByDescending(s => s.Price);
                    break;
                    // You can add more sorting options here if needed
            }

            Facility = await facilityQuery.ToListAsync();

            if (id != null)
            {
                FacilityID = id.Value;
                Facility selectedFacility = Facility.FirstOrDefault(t => t.ID == id.Value);
                // Do something with selectedFacility if needed
            }
        }

    }
}
