using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters.Swagger;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Configurations
{
    /// <summary>
    /// Classe que aplica as configurações do Swagger
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Aplica as configurações do Swagger na camada de serviços
        /// </summary>
        /// <param name="serviceCollection">Container de serviços</param>
        public static void Apply(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Auction House API",
                    Description = "API para o desafio fullstack TOTVS",
                    Contact = new OpenApiContact
                    {
                        Name = "João Pedro Santos de Moura",
                        Email = "moura.joaopedro@gmail.com",
                        Url = new Uri("https://github.com/jpmoura"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://choosealicense.com/licenses/mit/"),
                    }
                });

                options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                options.EnableAnnotations();
                options.OperationFilter<AuthenticationRequirementOperationFilter>();
                options.OperationFilter<RemoveVersionFromParameterOperationFilter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPathDocumentFilter>();

                // Valida o versionamento de rotas
                options.DocInclusionPredicate((version, desc) =>
                {
                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo))
                    {
                        return false;
                    }

                    IEnumerable<ApiVersion> versions = methodInfo.DeclaringType.GetCustomAttributes(true).OfType<ApiVersionAttribute>().SelectMany(attr => attr.Versions);
                    ApiVersion[] maps = methodInfo.GetCustomAttributes(true).OfType<MapToApiVersionAttribute>().SelectMany(attr => attr.Versions).ToArray();

                    return versions.Any(apiVersion => $"v{apiVersion}" == version) && (!maps.Any() || maps.Any(apiVersion => $"v{apiVersion}" == version));
                });

                // Adiciona a documentação das classes pelo XML
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Aplica as configurações do Swagger na camada da aplicação
        /// </summary>
        /// <param name="applicationBuilder">Construtor da aplicação</param>
        public static void Apply(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseSwagger();

            applicationBuilder.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }
    }
}
