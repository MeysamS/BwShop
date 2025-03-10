using Bw.Persistence.Mongo;
using BwShop.Reviews.Application.Services;
using BwShop.Reviews.Application.Services.Contract;
using BwShop.Reviews.Domain.Models.Repositories;
using BwShop.Reviews.Domain.Services;
using BwShop.Reviews.Infrastructure.Data;
using BwShop.Reviews.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BwShop.Reviews.Infrastructure.RegisterServices.WebApplicationBuilderExtensions;
public static partial class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder)
    {
        builder.Services.AddMongoDbContext<ReviewMongoDbContext>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ReviewDomainService>();
        builder.Services.AddScoped<IReviewApplicationService,ReviewApplicationService>();

        return builder;
    }

}