﻿using API.Entities;
using System.Text.Json;

namespace API.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var data = File.ReadAllText("Data/Seeds/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(data);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var data = File.ReadAllText("Data/Seeds/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(data);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var data = File.ReadAllText("Data/Seeds/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(data);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
