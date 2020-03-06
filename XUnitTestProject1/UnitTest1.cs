using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationCore.Domain.Models;
using WebApplicationCore.Domain.Repositories;
using WebApplicationCore.Domain.Services;
using WebApplicationCore.Persistence.Contexts;
using WebApplicationCore.Persistence.Repositories;
using WebApplicationCore.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        //arrange
        private Mock<ICategoryRepository> _categoryRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private ICategoryService _categoryService;

        public UnitTest1()
        {
            SetupMocks();
            _categoryService = new CategoryService(_categoryRepository.Object, _unitOfWork.Object);
        }

        private void SetupMocks()
        {
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoryRepository.Setup(r => r.FindByIdAsync(1))
                .ReturnsAsync(new Category { Id = 1, Name = "Dairy", Products = new Collection<Product>() });

            _categoryRepository.Setup(r => r.FindByIdAsync(2))
                .Returns(Task.FromResult<Category>(null));

            _categoryRepository.Setup(r => r.AddAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.CompleteAsync()).Returns(Task.CompletedTask);
        }


        [Fact]
        public async Task Should_Find_Category_By_Id()
        {
            //act
            var category = await _categoryService.FindByIdAsync(1);

            //assert
            Assert.NotNull(category);
            Assert.Equal("Dairy", category.Category.Name);
        }

        [Fact]
        public async Task Should_Return_Null_When_Not_Found()
        {
            //act
            var category = await _categoryService.FindByIdAsync(2);

            //assert
            Assert.Null(category);

        }
    }
}
