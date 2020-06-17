using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters.Swagger
{
    /// <summary>
    /// Remove o parâmetro obrigatório de versão da API da query string
    /// </summary>
    public class RemoveVersionFromParameterOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Aplica o filtro de operação de remoção de parâmetro de versão da API da query string
        /// </summary>
        /// <param name="operation">Operação</param>
        /// <param name="context">Contexto</param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            OpenApiParameter versionParameter = operation.Parameters.Single(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
}
