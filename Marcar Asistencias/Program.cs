using Marcar_Asistencias.Data;
using Marcar_Asistencias.Repositories;
using MarcarAsistencias.Data;

namespace Marcar_Asistencias
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

			builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
			builder.Services.AddScoped<IEmpleadosRepository, EmpleadosRepository>();
            builder.Services.AddScoped<IAusenciasRepository, AusenciasRepository>();
            builder.Services.AddScoped<IHorariosRepository, HorariosRepository>();
            builder.Services.AddScoped<IVacacionesRepository, VacacionesRepository>();
            builder.Services.AddScoped<IComentariosRepository, ComentariosRepository>();


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
