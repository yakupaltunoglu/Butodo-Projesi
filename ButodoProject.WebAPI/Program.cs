using ButodoProject.Core.Helper;
using ButodoProject.Core.Service.Dto;
using ButodoProject.Model.Domain;
using ButodoProject.Core.Validators;
using FluentNHibernate.Cfg;
using FluentValidation;
using FluentValidation.AspNetCore;
using NHibernate.Event;
using NHibernate.Tool.hbm2ddl;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var sessionFactory = Fluently.Configure()
            .Database(() => FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012.ShowSql()
                .ConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ButodoDB;Integrated Security=True;MultipleActiveResultSets=True"))
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

builder.Services.AddSingleton(factory => sessionFactory.OpenSession());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CompanyDto>, CompanyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CompanyValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
