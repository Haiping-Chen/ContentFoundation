using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using ContentFoundation.RestApi;

namespace ContentFoundation.WebStarter
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment appEnv)
        {
            Configuration = configuration;
            CurrentEnvironment = appEnv;
        }

        public IConfiguration Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ApiExceptionFilter>();
            services.AddCors();
            services.AddJwtAuth(Configuration);

            services.AddMvc();
            
            // Add framework services.
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with Bearer into field", Name = "Authorization", Type = "apiKey" });

                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Content Foundation API",
                    Description = "Content Foundation API",
                    TermsOfService = "MIT",
                    Contact = new Contact { Name = "Haiping Chen", Email = "haiping008@gmail.com", Url = "" },
                    License = new License { Name = "", Url = "" }
                });

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "ContentFoundation.RestApi.xml");
                c.IncludeXmlComments(filePath);
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.EnabledValidator();
                c.SupportedSubmitMethods(new[] { "get", "post", "put", "patch", "delete" });
                c.ShowRequestHeaders();
                c.ShowJsonEditor();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = "api";
            });

            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            app.UseAuthentication();
            app.UseMvc();

            app.UseEntityDbContext(Configuration, env.ContentRootPath, new String[] { "CustomEntityFoundation", "Quickflow.Core", "Quickflow.ActivityRepository", "ContentFoundation.Core" });
            app.UseInitLoader();
        }

    }
}
