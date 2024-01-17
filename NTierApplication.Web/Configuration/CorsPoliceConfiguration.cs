namespace NTierApplication.Web.Configuration;

public static class CorsPoliceConfiguration
{
    public static void ConfigureCORSPolice(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(option =>
        {
            option.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });
    }
}
