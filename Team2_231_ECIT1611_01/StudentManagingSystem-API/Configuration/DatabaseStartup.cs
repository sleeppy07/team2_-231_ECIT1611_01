using BusinessObject.Model.SeedData;
using BusinessObject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace StudentManagingSystem_API.Configuration
{
    public static class DatabaseStartUp
    {
        public static IServiceCollection AddDatabaseModule<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {

/*            string connectionString = configuration.GetConnectionString("MyDbContext");
            services.AddDbContext<SmsDbContext>(context =>
            {
                context.UseNpgsql(connectionString);
            });*/
            services.Configure<DatabaseConfiguration>(configuration.GetSection(DatabaseConnection.DATABASE));
            services.AddSingleton(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseConfiguration>>().Value;
            });

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            DatabaseConfiguration databaseConfig = serviceProvider.GetRequiredService<DatabaseConfiguration>();
            switch (databaseConfig.DatabaseProvider)
            {
                case "postgres":
                    services.AddDbContext<T>(context =>
                    {
                        context.UseNpgsql(databaseConfig.DatabaseConnectionString);
                    });
                    break;
                default:
                    //Others Config
                    break;
            }

            services.AddScoped<DbContext>(provider => provider.GetService<T>() ?? throw new ArgumentNullException(nameof(T)));

            return services;
        }


        public static IApplicationBuilder UseApplicationDatabase<T>(this IApplicationBuilder app,
            IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                //var services = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<SmsDbContext>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                context.Database.OpenConnection();
                //context.Database.Migrate();
                context.Database.EnsureCreated();
                //IdentitySeedData.Seed(context, userMgr, roleMgr).Wait();
            }

            return app;
        }
    }
}
