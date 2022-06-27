using Microsoft.OpenApi.Models;

namespace ApiClientes.Services
{
    public class SwaggerConfiguration
    {
        public static void Register(WebApplicationBuilder builder)
        {
            if (builder.Services == null) throw new ArgumentNullException
            (nameof(builder.Services));

            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",

                    Title = "API para gerenciamento de Cliente",
                    Description = "Projeto COTI Informática",
                    Contact = new OpenApiContact
                    {
                        Name = "Arthur Antunes",
                        Email = "controlp.arthur@gmail.com",
                    }

                });
            });
        }
        public static void Use(WebApplication app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCliente");
            });
        }
    }
}
