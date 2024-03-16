using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebCourierApi.BusinessLogic;
using WebCourierApi.Data;
using WebCourierApi.Data.Configuration;
using WebCourierApi.Data.Repositories;
using WebCourierApi.Data.Seeding;
using WebCourierApi.Model.POCO;
using WebCourierApi.Utils.ApiKey;
using WebCourierApi.Utils.ApiKey.Classifier;
using WebCourierApi.Utils.ApiKey.Filters;
using WebCourierApi.Utils.ApiKey.ResourceGuard;
using WebCourierApi.Utils.DynamicQuery.Parsers;
using WebCourierApi.Utils.DynamicQuery.Translators;
using WebCourierApi.Utils.Exceptions;
using WebCourierApi.Utils.MailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<IInquireRepository, PersistentInquireRepository>();
builder.Services.AddTransient<IDeliveryRepository, PersistentDeliveryRepository>();
builder.Services.AddTransient<INationalRepository, PersistentNationalRepository>();
builder.Services.AddScoped<INewsletterRepository, NewsletterRepository>();
builder.Services.AddSingleton<ISeeder<CountryPOCO>, CountriesSeeder>();
builder.Services.AddSingleton<ISeeder<CurrencyPOCO>, CurrenciesSeeder>();
builder.Services.AddSingleton<IEntityTypeConfiguration<InquirePOCO>, InquirePOCOConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<DeliveryPOCO>, DeliveryPOCOConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CountryPOCO>, CountryPOCOConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CurrencyPOCO>, CurrencyPOCOConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<NewsletterPOCO>, NewsletterPOCOConfiguration>();
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddHostedService<PeriodicMailService>();
builder.Services.AddControllers(options => options.Filters.Add<HttpResponseExceptionFilter>());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IApiKeyClassifier, ConfigurationApiKeyClassifier>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<ApiKeyHttpContextReader>();
builder.Services.AddSingleton<ExternalApiKeyAuthorizationFilter>();
builder.Services.AddSingleton<InternalApiKeyAuthorizationFilter>();
builder.Services.AddScoped<IResourceGuard<InquirePOCO, string>, ApiKeyResourceGuard<InquirePOCO>>();
builder.Services.AddScoped<IResourceGuard<DeliveryPOCO, string>, ApiKeyResourceGuard<DeliveryPOCO>>();

builder.Services.AddScoped<IOfferStrategy, PLBasicOfferStrategy>();
builder.Services.AddScoped<IOfferStrategy, EUBasicOfferStrategy>();

builder.Services.AddScoped<IQueryParser<InquirePOCO>, FilterSortPageQueryParser<InquirePOCO>>();
builder.Services.AddKeyedScoped<IQueryTranslator<InquirePOCO>, InquireFilteringQueryTranslator>("filtering");
builder.Services.AddKeyedScoped<IQueryTranslator<InquirePOCO>, InquireSortingQueryTranslator>("sorting");
builder.Services.AddScoped<IQueryParser<DeliveryPOCO>, FilterSortPageQueryParser<DeliveryPOCO>>();
builder.Services.AddKeyedScoped<IQueryTranslator<DeliveryPOCO>, DeliveryFilteringQueryTranslator>("filtering");
builder.Services.AddKeyedScoped<IQueryTranslator<DeliveryPOCO>, DeliverySortingQueryTranslator>("sorting");

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<WebCourierApiDbContext>(options => options
        .UseSqlite(builder.Configuration.GetConnectionString("Database"))
    );

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme()
        {
            Description = "WebCourier API KEY",
            Name = builder.Configuration["ApiKey:HeaderName"],
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "apiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<WebCourierApiDbContext>(options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("Database"))
    );

    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme()
        {
            Description = "WebCourier API KEY",
            Name = builder.Configuration["ApiKey:HeaderName"],
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "apiKey"
                    }
                },
                Array.Empty<string>()
            }
        });
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }