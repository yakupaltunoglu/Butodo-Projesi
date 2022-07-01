using ButodoProject.Core.Helper;
using ButodoProject.Core.Model.Domain;
using ButodoProject.Core.Service.Dto;
using FluentNHibernate.Cfg;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ButodoProject.Model.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using FluentValidation.AspNetCore;
using ButodoProject.Core.Validators.Personals;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using NHibernate.Tool.hbm2ddl;

namespace ButodoProject.Web
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
            services.AddMvc();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddControllers();

            //options => options.Filters.Add<ValidationFilter>())
            //    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<PersonalValidator>().DisableDataAnnotationsValidation = true
            //    ).ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true

            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<PersonalDto>, PersonalValidator>();
            services.AddValidatorsFromAssemblyContaining<PersonalValidator>();




            services.AddSingleton(Configuration);
            //services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "SemerkandBookshelf.Api", Version = "v1" }); });

            //services.Configure<MyConfigDto>(Configuration.GetSection("MyConfig"));

            var sessionFactory = Fluently.Configure()
                .Database(() => FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012.ShowSql()
                    .ConnectionString(Configuration.GetConnectionString("DefaultConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<EntityBase>())
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .ExposeConfiguration(cfg =>
                {
                    cfg.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { new NHibernateListener() };
                    cfg.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { new NHibernateListener() };
                    cfg.EventListeners.PreDeleteEventListeners = new IPreDeleteEventListener[] { new NHibernateListener() };
                    //cfg.EventListeners.DeleteEventListeners = new IDeleteEventListener[0];
                })
                .BuildSessionFactory();

            services.AddSingleton(factory => sessionFactory.OpenSession());


          
            //services.AddScoped(factory => factory.GetServices<NHibernate.ISessionFactory>().First().OpenSession());

            //services.AddIdentityServer()


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x => x.LoginPath = "/account/login");

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);//You can set Time   
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
