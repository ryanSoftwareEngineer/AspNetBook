using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new ProductModel[] {
                 new ProductModel {ProductId = 1, Name = "P1"},
                 new ProductModel {ProductId = 2, Name = "P2"},
                 new ProductModel {ProductId = 3, Name = "P3"},
                 new ProductModel {ProductId = 4, Name = "P4"},
                 new ProductModel {ProductId = 5, Name = "P5"}
            }).AsQueryable<ProductModel>());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            //act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;



            //assert
            ProductModel[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);

        }
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new ProductModel[] {
            new ProductModel {ProductId = 1, Name = "P1"},
             new ProductModel {ProductId = 2, Name = "P2"},
             new ProductModel {ProductId = 3, Name = "P3"},
             new ProductModel {ProductId = 4, Name = "P4"},
             new ProductModel {ProductId = 5, Name = "P5"}
            }).AsQueryable<ProductModel>());
            // Arrange
            ProductController controller =
            new ProductController(mock.Object) { PageSize = 3 };
            // Act
            ProductsListViewModel result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Assert
            PagingInfo pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }
        [Fact]
        public void Can_Filter_Products() {
            //arrange
            //create the mock repo
            Mock<IProductRepository> mock = new Mock<IProductRepository>(); 
            mock.Setup(product => product.Products).Returns((new ProductModel[] {
            new ProductModel {ProductId = 1, Name = "P1" , Category = "Cat1"},
             new ProductModel {ProductId = 2, Name = "P2", Category = "Cat2"},
             new ProductModel {ProductId = 3, Name = "P3", Category = "Cat1"},
             new ProductModel {ProductId = 4, Name = "P4", Category = "Cat2"},
             new ProductModel {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<ProductModel>());

            //arrange 
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;

            //action
            ProductModel[] result = (controller.List("Cat2").ViewData.Model as ProductsListViewModel).Products.ToArray();

            // test
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}