using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Persistence.Contexts;
using WebApplicationCore.Persistence.Repositories;
using WebApplicationCore.Services;
using Xunit;

namespace XUnitInMemoryTesting
{
    public class InMemoryTest
    {
        private DbContextOptions<AppDbContext> GetOptions(string databaseName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .Options;

            AddMockData(options);
            return options;
        }

        private void AddMockData(DbContextOptions<AppDbContext> options)
        {
            using (var context = new AppDbContext(options))
            {
                context.AddRange(new List<Category>
                {
                    new Category { Id = 1, Name = "Fruits and Vegetables" }, // set manually due to in-memory provider
                    new Category { Id = 2, Name = "Dairy" }
                });
                context.AddRange(new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Apple",
                        QuantityInPackage = 1,
                        UnitOfMeasurement = EUnitOfMeasurement.Unity,
                        CategoryId = 100
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Milk",
                        QuantityInPackage = 2,
                        UnitOfMeasurement = EUnitOfMeasurement.Liter,
                        CategoryId = 101,
                    }
                });
                context.SaveChanges();
            }

        }

        [Fact]
        public async Task Should_Find_Category_By_Id()
        {
            using (var context = new AppDbContext(GetOptions("find_by_id")))
            {
                //arrange
                var unitOfWork = new UnitOfWork(context);
                var serviceRepository = new CategoryRepository(context);
                var serviceCategory = new CategoryService(serviceRepository, unitOfWork);
                //act
                var category = await serviceCategory.FindByIdAsync(2);//search for dairy

                //assert
                Assert.NotNull(category);
                Assert.Equal("Dairy", category.Category.Name);
                context.SaveChanges();
            }
        }
    }
}
