using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters.Swagger
{
	/// <summary>
	/// Filtro de autenticação JWT para as operações listadas
	/// </summary>
	public class AuthenticationRequirementOperationFilter : IOperationFilter
	{
		/// <summary>
		/// Aplica o filtro de autenticação para aoperações
		/// </summary>
		/// <param name="operation">Operação</param>
		/// <param name="context">Contexto</param>
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			if (operation.Security == null)
			{
				operation.Security = new List<OpenApiSecurityRequirement>();
			}

			OpenApiSecurityScheme scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
			
			operation.Security.Add(new OpenApiSecurityRequirement
			{
				[scheme] = new List<string>()
			});
		}
	}
}
