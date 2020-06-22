using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Projeto.Cryptography.Services;
using Projeto.Data.Contracts;
using Projeto.Data.Repositories;
using Projeto.Services.Authentication;

namespace Projeto.Services
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            var connectionString = Configuration.GetConnectionString("Aula");

            services.AddTransient(map => new FuncionarioRepository(connectionString));
            services.AddTransient(map => new UsuarioRepository(connectionString));
            services.AddTransient(map => new MD5Service());     

            #region Configuração do Swagger

            services.AddSwaggerGen(
                c => 
                {
                    c.SwaggerDoc("v1", new OpenApiInfo 
                    {
                        Title = "Sistema de Controle de Funcionários",
                        Description = "API REST para integração com serviços de funcionário.",
                        Version = "v1"
                    });
                });

            #endregion

            #region Confiuração do CORS

            services.AddCors(
                c => c.AddPolicy("DefaultPolicy",
                    builder => 
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    }));

            #endregion

            JwtConfiguration.Register(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            #region Configuração do Swagger

            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto API");
            });

            #endregion

            #region Configuração do CORS

            app.UseCors("DefaultPolicy");

            #endregion

            app.UseMvc();
        }
    }
}
