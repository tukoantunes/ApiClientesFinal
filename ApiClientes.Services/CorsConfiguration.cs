namespace ApiClientes.Services
{
    public class CorsConfiguration
    {
        private static string _CORS_POLICY = "DefaultPolicy";

        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
            .AddCors(s => s.AddPolicy(_CORS_POLICY,
              builder => {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();

               }));
        }
        public static void Use(WebApplication app)
        {
            app.UseCors(_CORS_POLICY);
        }
    }
}
