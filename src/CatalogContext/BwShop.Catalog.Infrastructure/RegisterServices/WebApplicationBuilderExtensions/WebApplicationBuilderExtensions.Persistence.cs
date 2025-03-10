// using Bw.Persistence.EfCore.Postgres;
// using BwShop.Media.Domain.Models.Aggregates;
// using BwShop.Media.Domain.Repositories;
// using BwShop.Media.Infrastructure.Data;
// using BwShop.Media.Infrastructure.Repositories;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;

// namespace BwShop.Media.Infrastructure.Registerservices.WebApplicationBuilderExtensions;
// public static partial class WebApplicationBuilderExtensions
// {
//     public static WebApplicationBuilder AddStorage(this WebApplicationBuilder builder)
//     {
//         builder.Services.AddPostgresDbContext<ProductMediaDbContext>();
//         builder.Services.AddUnitOfWork<ProductMediaDbContext>(ServiceLifetime.Scoped, true);
//         builder.Services.AddPostgresRepository<ProductMedia, Guid, ProductMediaRepository>();
//         builder.Services.AddScoped<IProductMediaRepository, ProductMediaRepository>();
//         return builder;
//     }

// }