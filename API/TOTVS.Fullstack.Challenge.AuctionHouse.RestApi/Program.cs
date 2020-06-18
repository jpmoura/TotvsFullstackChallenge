using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TOTVS.Fullstack.Challenge.AuctionHouse.RestApi
{
    /// <summary>
    /// Classe principal da aplica��o
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// M�todo principal da aplica��o
        /// </summary>
        /// <param name="args">Argumentos da aplica��o</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Cria o construtor do host da aplica��o
        /// </summary>
        /// <param name="args">Argumentos</param>
        /// <returns>Construtor de host da aplica��o</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
