using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CosmicApi.SwaggerOptions
{
    public class ConfigureSwagger : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        public ConfigureSwagger(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var item in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    item.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = $"Cosmic Spot API {item.ApiVersion}",
                        Version = item.ApiVersion.ToString()
                    });
            }
            var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlCommentsfullpath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
            options.IncludeXmlComments(xmlCommentsfullpath);
        }
    }
}
