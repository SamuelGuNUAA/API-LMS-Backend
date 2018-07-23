using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using LMS.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace LMS
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
            services.AddMvc()
                    .AddJsonOptions(o =>
                {
                    if (o.SerializerSettings.ContractResolver != null)
                    {
                        var castedResolver = o.SerializerSettings.ContractResolver
                                        as DefaultContractResolver;
                        castedResolver.NamingStrategy = null;
                    }
                    o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    o.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                });

            //services.AddOptions();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            var setting = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidAudience = setting["Audience"],
                            ValidIssuer = setting["Issuer"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting["SecretKey"]))
                        };
                    });
            services.AddMvc();
            // swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My MLS API", Version = "v1" });
            });
            services.AddDbContext<LMSDBContext>();
            services.AddScoped<ILMSDataStore, LMSDataStore>();
            //services.AddTransient() create per each use
            //services.AddScoped(); create per each request
            //services.AddSingleton(); create from start up
        }

        //test
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
}
