using ApiClientes.Infra.Data.Contexts;
using ApiClientes.Infra.Data.Interfaces;
using ApiClientes.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Services
{
    public class EntityFrameworkConfiguration
    {
        public static void Register(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("ApiCliente");

            builder.Services.AddDbContext<SqlServerContext>
                            (map => map.UseSqlServer(connectionString));

            builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
        }
        
    }   
}
