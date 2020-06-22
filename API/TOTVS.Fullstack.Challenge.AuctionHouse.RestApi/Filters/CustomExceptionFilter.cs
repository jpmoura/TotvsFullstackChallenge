using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Constants;
using TOTVS.Fullstack.Challenge.AuctionHouse.Domain.Exceptions;
using TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Dtos.Errors;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi.Filters
{
    /// <summary>
    /// Filtro de exceções
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        /// <summary>
        /// Serviço de log
        /// </summary>
        private readonly ILogger<CustomExceptionFilter> logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="logger">Serviço de log</param>
        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Executor do filtro ao capturar uma exceção
        /// </summary>
        /// <param name="context">Contexto da exceção</param>
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode statusCode;
            string errorMessage = context.Exception.Message;

            switch (context.Exception)
            {
                case ValidationException _:
                case InvalidParameterException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case ResourceNotFoundException _:
                    statusCode = HttpStatusCode.NotFound;
                    break;
                case DisabledUserException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                default:
                    errorMessage = ErrorMessage.UnhandledException;
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            LogException(context);
            BuildResponse(context, statusCode, errorMessage);
        }

        /// <summary>
        /// Realiza o log da exceção
        /// </summary>
        /// <param name="exceptionContext">Contexto da exceção</param>
        private void LogException(ExceptionContext exceptionContext)
        {
            logger.LogError(exceptionContext.HttpContext.TraceIdentifier, exceptionContext.Exception);
        }

        /// <summary>
        /// Constrói a resposta 
        /// </summary>
        /// <param name="context">Contexto da exceção</param>
        /// <param name="statusCode">Código do status da resposta</param>
        /// <param name="errorMessage">Mensagem de erro da resposta</param>
        private void BuildResponse(ExceptionContext context, HttpStatusCode statusCode, string errorMessage)
        {
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";

            context.Result = new ObjectResult(new ErrorMessageDto { Message = errorMessage });
        }
    }
}
