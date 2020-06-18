using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters.Swagger
{
    /// <summary>
    /// Filtro de versão do documentação da API para o Swagger
    /// </summary>
    public class ReplaceVersionWithExactValueInPathDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Aplica o filtro de documentação de API para o swagger
        /// </summary>
        /// <param name="swaggerDoc">Documentação</param>
        /// <param name="context">Contexto</param>
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            OpenApiPaths toReplaceWith = new OpenApiPaths();

            foreach ((string key, OpenApiPathItem value) in swaggerDoc.Paths)
            {
                toReplaceWith.Add(key.Replace("v{version}", swaggerDoc.Info.Version, StringComparison.InvariantCulture), value);
            }

            swaggerDoc.Paths = toReplaceWith;
        }
    }
}
