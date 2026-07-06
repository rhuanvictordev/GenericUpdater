using AtualizadorGenerico.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;

namespace AtualizadorGenerico
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ciarPastaProgramas();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 1L * 1024 * 1024 * 1024; });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseExceptionHandler("/Home/Erro");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllerRoute(name: "default", pattern:"{controller=Home}/{action=Index}/{id?}");
            app.Run();


            static void ciarPastaProgramas()
            {
                var pasta = Path.Combine(AppContext.BaseDirectory, "Programas");

                if (!Directory.Exists(pasta))
                {
                    try
                    {
                        Directory.CreateDirectory(pasta);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erro ao tentar criar a pasta Programas no Program.cs");
                    }
                }
            }

        }
    }
}
