using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Find.Data;
using Find.Models;
using Microsoft.AspNetCore.DataProtection;

namespace API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        IConfiguration Configuration{get;}
        
        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<APIContexto>(opt =>
                opt.UseMySql(Configuration.GetConnectionString("bancodados"))
                );
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          app.UseMvc();
        }
    }
}
