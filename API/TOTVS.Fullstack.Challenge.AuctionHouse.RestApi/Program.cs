using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi
{
    /// <summary>
    /// Classe principal da aplicação
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Método principal da aplicação
        /// </summary>
        /// <param name="args">Argumentos da aplicação</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria o construtor do host da aplicação
        /// </summary>
        /// <param name="args">Argumentos</param>
        /// <returns>Construtor de host da aplicação</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
