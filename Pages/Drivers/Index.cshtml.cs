﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DeliveryAplication.Data;
using DeliveryAplication.Models;

namespace DeliveryAplication.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        private readonly DeliveryAplication.Data.DeliveryAplicationContext _context;

        public IndexModel(DeliveryAplication.Data.DeliveryAplicationContext context)
        {
            _context = context;
        }

        public IList<Driver> Driver { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Driver != null)
            {
                Driver = await _context.Driver.ToListAsync();
            }
        }
    }
}
