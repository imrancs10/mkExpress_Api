using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MKExpress.API.Config;
using MKExpress.API.Dto;
using MKExpress.API.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace MKExpress.Web.API
{
    public class Startup
    {
        private readonly string _policyName = "CorsPolicy";
        public Startup(IConfiguration configuration)
        {
            //  var builder = new ConfigurationBuilder().AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "Properties",
            // "launchSettings.json"));
            //  Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.RegisterServices()
             .RegisterDataServices(Configuration)
             .RegisterAuthServices()
             .AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MKExpress.API", Version = "v1" }); })
             .AddControllers();
            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy(_policyName,
                    builder =>
                    {
                        builder.SetIsOriginAllowed(isOriginAllowed: _ => true).WithOrigins("http://MKExpress.com/", "http://greentowerae.com/","http://labeachdubai.com/", "http://abumansourae.com/","*","https://localhost:3000/", "https://MKExpress.com/", "https://greentowerae.com/", "https://labeachdubai.com/", "https://abumansourae.com/", "*", "http://localhost:3000/")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MKExpress.API v1");
                    c.DocExpansion(DocExpansion.None);
                });
            }

           
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCustomExceptionHandler();

            app.UseRouting();
            app.UseCors(_policyName);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run(async context =>
            {
                context.Response.Redirect("swagger/index.html");
            });

            Registrar.InitializeDatabase(app);
        }
    }
}
