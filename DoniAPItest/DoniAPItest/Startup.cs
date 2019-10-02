using System.Collections.Generic;
using System.Linq;
using DoniAPItest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DoniAPItest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Creamos persistencia de datos en Memoria
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("Donicus"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // En caso de no existir ningun tour en memoria cargamos una lista de Tours de ejemplo
            if (!context.Tours.Any())
            {
                var ToursDeEjemplo = new List<Tour>()
                {
                    new Tour()
                    {
                        Nombre = "Tour por la ciudad de Las Palmas de GC",
                        Descripcion = "Las Palmas es la capital de Gran Canaria, una de las islas que forman el archipiélago español de Canarias, frente al noroeste de África. La ciudad es un puerto importante de cruceros y es conocida por sus zonas de compras libres de impuestos y sus playas de arena.",
                        Observaciones = "<span><b>Observaciones</b> ejemplo 1 <s>de Las Palmas de Gran Canaria</s></span>",
                        UrlImagen = "https://holidayillnessclaim.co.uk/wp-content/uploads/2016/11/Vista_de_Las_Palmas_de_Gran_Canaria-Copy-600x200.jpg"
                    },
                    new Tour()
                    {
                        Nombre = "Tour de Toledo",
                        Descripcion = "El Museo y Parque Arqueológico Cueva Pintada se encuentra en la ciudad de Gáldar, al noroeste de la isla de Gran Canaria, islas Canarias.",
                        Observaciones = "<span><b>Observaciones</b> ejemplo 2 <s>de Tour de Toledo</s></span>",
                        UrlImagen = "https://www.citytoursinmadrid.es/wp-content/uploads/2018/10/Toledo-600x200.jpg",

                    },
                    new Tour()
                    {
                        Nombre = "Tour por Roma",
                        Descripcion = "Roma es una ciudad italiana de 2 857 321 habitantes, ​ capital de la región del Lacio y de Italia. Es el municipio más poblado de Italia y la cuarta ciudad más poblada de la Unión Europea.​ Por antonomasia, se la conoce desde la Antigüedad como la Urbe. También es llamada La Ciudad Eterna.",
                        Observaciones = "<span><b>Observaciones</b> ejemplo 3 <s>de Tour por Roma</s></span>",
                        UrlImagen = "https://t-ec.bstatic.com/xdata/images/xphoto/600x200/49345771.jpg?k=eef1ebc043e9c90e1338efe31127f3a0a75ce7399e29a372f648bd53311fd240&o="
                    },
                    new Tour()
                    {
                        Nombre = "Tour de Paris y la Torre Eiffel",
                        Descripcion = "La torre Eiffel​, inicialmente llamada la tour de 300 mètres, es una estructura de hierro pudelado diseñada por los ingenieros Maurice Koechlin y Émile Nouguier.",
                        Observaciones = "<span><b>Observaciones</b> ejemplo 4 <s>de Tour de Paris y la Torre Eiffel</s></span>",
                        UrlImagen = "https://q-cf.bstatic.com/xdata/images/xphoto/600x200/49345752.jpg?k=50900e5723e39b2ff17c86794601fc70e29919eedd114663ba00526d8c597674&o=",

                    }

                };
                context.Tours.AddRange(ToursDeEjemplo);
                context.SaveChanges();
            }
        }
    }
}
