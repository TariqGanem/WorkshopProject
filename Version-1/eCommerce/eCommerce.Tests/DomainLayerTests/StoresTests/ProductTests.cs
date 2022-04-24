using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.src.DomainLayer.Store;
using Xunit;

namespace eCommerceTests.DomainLayerTests.StoresTests
{
    public class ProductTests
    {
        public Product Product { get;}

        public ProductTests()
        {
            Product = new Product("RTX3080", 5500.95, "GPU", 5);
        }

        [Theory()]
        [Trait("Category","UnitTesting")]
        [InlineData ("GPU")]
        [InlineData("Core")]
        [InlineData("Rog")]
        public void addKeyWord(String keyWord)
        {
            this.Product.AddKeyWord(keyWord);
            Assert.Contains(keyWord, Product.KeyWords);
        }

        [Theory()]
        [Trait("Category", "UnitTesting")]
        [InlineData(2.8 , 2.8)]
        [InlineData(0.8,0)]
        [InlineData(4,4)]
        [InlineData(5.1, 5.1)]
        public void addRating(double rate , double expected)
        {
            if (rate < 1 | rate > 5)
            {
                try
                {
                    this.Product.AddRating(rate);
                }
                catch(Exception e)
                {
                    Assert.Equal("Product RTX3080 could not be rated. Please use number between 1 to 5", e.Message);
                }
            }
            else
            {
                this.Product.AddRating(rate);
                Assert.Equal(this.Product.Rate, expected);
            }
            
        }
    }
}
