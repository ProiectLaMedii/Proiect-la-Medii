using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DeliveryAplication.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
   policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    
    
    options.Conventions.AllowAnonymousToPage("/Locations/Index");
    options.Conventions.AllowAnonymousToPage("/Products/Index");
    options.Conventions.AuthorizeFolder("/Requests");
    options.Conventions.AuthorizeFolder("/Clients");
    options.Conventions.AuthorizeFolder("/Products");
    options.Conventions.AuthorizeFolder("/Deliveries");
    options.Conventions.AuthorizeFolder("/Drivers");

});
builder.Services.AddDbContext<DeliveryAplicationContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryAplicationContext") ??
throw new InvalidOperationException("Connectionstring 'DeliveryAplicationContext' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DeliveryAplicationContext") ??
throw new InvalidOperationException("Connectionstring 'DeliveryAplicationContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<LibraryIdentityContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        context.Response.Redirect("/Products/Index");
    });
    endpoints.MapRazorPages();
});


app.Run();
