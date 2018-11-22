using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Find.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ambiente = BuildWebHost(args);
                using (var escopo = ambiente.Services.CreateScope())
                {
                    var servico = escopo.ServiceProvider;
                    try
                    {
                        var contexto = servico.GetRequiredService<APIContexto>();
                        InicializarBanco.Iniciar(contexto);
                    }
                    catch(Exception ex)
                    {
                        var logger = servico.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Ocorreu um erro ao enviar os dados");
                    }
                }
                ambiente.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
