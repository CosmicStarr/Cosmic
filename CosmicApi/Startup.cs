using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmicApi.Data;
using CosmicApi.Repository;
using CosmicApi.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using CosmicApi.Models.DTOs;
using CosmicApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using CosmicApi.SwaggerOptions;

namespace CosmicApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICosmicSpotRepository, CosmicSpotRepository>();
            services.AddScoped<IDirectionsRepository, DirectionsRepository>();
            services.AddAutoMapper(typeof(CosmicSpotDTO));
            services.AddApiVersioning(opt =>
            {
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ReportApiVersions = true;
            });
            services.AddVersionedApiExplorer(opt =>
            {
                opt.GroupNameFormat = "'v'VVV";
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();
            services.AddSwaggerGen();
            //services.AddAutoMapper(typeof(DirectionsDTO));
            //services.AddSwaggerGen(opt => 
            //{
            //    opt.SwaggerDoc("CosmicOpenApiSpec", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    {
            //        Title = "Cosmic Spot API",
            //        Version = "1",
            //        Description = "Cosmic Cooking Spot Api",
            //        Contact = new Microsoft.OpenApi.Models.OpenApiContact
            //        {
            //            Email = "NormandJ85@yahoo.com",
            //            Name = "Normand",
            //        },
            //    });
            //    //opt.SwaggerDoc("CosmicOpenApiSpecDirections", new Microsoft.OpenApi.Models.OpenApiInfo()
            //    //{
            //    //    Title = "Cosmic Spot API Directions",
            //    //    Version = "1",
            //    //    Description = "Cosmic Cooking Spot Directions",
            //    //    Contact = new Microsoft.OpenApi.Models.OpenApiContact
            //    //    {
            //    //        Email = "NormandJ85@yahoo.com",
            //    //        Name = "Normand",
            //    //    },
            //    //});
            //    var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlCommentsfullpath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            //    opt.IncludeXmlComments(xmlCommentsfullpath);
            //});
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(opt => 
            {
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    opt.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());
                    opt.RoutePrefix = "";
                }
            });
            //app.UseSwaggerUI(opt =>
            //{
            //    opt.SwaggerEndpoint("/swagger/CosmicOpenApiSpec/swagger.json", "Cosmic API");
            //    //opt.SwaggerEndpoint("/swagger/CosmicOpenApiSpecDirections/swagger.json", "Cosmic API Directions");
            //    opt.RoutePrefix = "";
            //});

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
