using Ecommerce.DAL.Entities;
using Ecommerce.DAL.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Data.Context
{
    public class DataSeedContext
    {
        public static async Task DataSeeding(ILoggerFactory _log , ApiDbContext _dbContext)
        {
            

            if (!_dbContext.Set<ProductBrand>().Any())
            {
                try
                {
                    var productBrandInJson = File.ReadAllText("../Ecommerce.DAL/Data/DataSeed/brands.json");
                    var productBrand = JsonSerializer.Deserialize<List<ProductBrand>>(productBrandInJson);
                   // var productBrand= JsonConvert.DeserializeObject<List<ProductBrand>>(productBrandInJson);
                    foreach (var brand in productBrand)
                    {
                        await _dbContext.Set<ProductBrand>().AddAsync(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var log = _log.CreateLogger<DataSeedContext>();
                    log.LogError(ex.Message);
                }

            }

            if (!_dbContext.Set<ProductType>().Any())
            {
                try
                {
                    var productTypeInJson = File.ReadAllText("../Ecommerce.DAL/Data/DataSeed/types.json");
                    var productType = JsonSerializer.Deserialize<List<ProductType>>(productTypeInJson);
                   //var productType = JsonConvert.DeserializeObject<List<ProductType>>(productTypeInJson);
                    foreach (var type in productType)
                    {
                        await _dbContext.Set<ProductType>().AddAsync(type);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var log = _log.CreateLogger<DataSeedContext>();
                    log.LogError(ex.Message);
                }

            }
            if (!_dbContext.Set<Product>().Any())
            {
                try
                {
                    var productsInJson = File.ReadAllText("../Ecommerce.DAL/Data/DataSeed/products.json");
                    //var products = JsonConvert.DeserializeObject<List<Product>>(productsInJson);
                    var products = JsonSerializer.Deserialize<List<Product>>(productsInJson);
                    foreach (var product in products)
                    {
                        await _dbContext.Set<Product>().AddAsync(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var log = _log.CreateLogger<DataSeedContext>();
                    var x = ex.InnerException;
                    log.LogError(ex.Message);
                }


            }

            if (!_dbContext.Set<DeliveryMethod>().Any())
            {
                try
                {
                    var DeliveryJsonData = File.ReadAllText("../Ecommerce.DAL/Data/DataSeed/delivery.json");
                    var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryJsonData);

                    foreach (var item in deliveries)
                    {
                        await _dbContext.Set<DeliveryMethod>().AddAsync(item);
                    }
                    await _dbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var log = _log.CreateLogger<DataSeedContext>();
                    log.LogError(ex.Message);
                }
            }

        }
    }
}
