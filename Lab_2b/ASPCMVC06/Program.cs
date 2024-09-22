internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapControllerRoute(
            name: "M01",
            pattern: "/{MResearch?}/{action}/{id?}",
            defaults: new { controller = "TMResearch", action = "M01" },
            constraints: new { MResearch = "MResearch", action = "M01|M02", id = new ASPCMVC06.CustomValid() }
        );

        app.MapControllerRoute(
            name: "V2",
            pattern: "V2/{MResearch?}/{action}",
            defaults: new { controller = "TMResearch", action = "M02" },
            constraints: new { action = "M01|M02", MResearch = "MResearch" }
        );

        app.MapControllerRoute(
            name: "V3",
            pattern: "V3/{MResearch?}/{str}/{action}",
            defaults: new { controller = "TMResearch", action = "M03", str = "" },
            constraints: new { V3 = new ASPCMVC06.CustomValidTwo(), action = "M01|M02|M03", MResearch = "MResearch" }
        );

        app.MapControllerRoute(
            name: "MXX",
            pattern: "{*uri}",
            defaults: new { controller = "TMResearch", action = "MXX" });

        app.Run();
    }
}