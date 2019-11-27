using System;
using Xunit;
using EntegyAPI.Interface;
using EntegyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EntegyAPI_Tests
{
    public class ChequeControllerTest
    {
        ChequeController _controller;
        IChequeService _service;

        public ChequeControllerTest()
        {
            _service = new ChequeServiceFake();
            _controller = new ChequeController(_service);
        }
        
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get("2.52");

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_DecimalWithinBoundCases()
        {
            // Act
            var okResult = _controller.Get("2.52");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("TWO DOLLARS AND FIFTY-TWO CENTS", result.Value);
        }

        [Fact]
        public void Get_DecimalWithSingleDigitCent()
        {
            // Act
            var okResult = _controller.Get("2.5");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("TWO DOLLARS AND FIFTY CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithZero()
        {
            // Act
            var okResult = _controller.Get("0");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("The input is zero", result.Value);

        }

        [Fact]
        public void Get_DecimalWithNegative()
        {
            // Act
            var okResult = _controller.Get("-0.52");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("The input is negative", result.Value);

        }

        [Fact]
        public void Get_DecimalWithAlphabets()
        {
            // Act
            var okResult = _controller.Get("aw0.52");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("The input is not a number", result.Value);

        }

        [Fact]
        public void Get_DecimalWithTensValueInDollarAndCent()
        {
            // Act
            var okResult = _controller.Get("1234.56");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("ONE THOUSAND TWO HUNDRED THIRTY-FOUR DOLLARS AND FIFTY-SIX CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithOnlyCent()
        {
            // Act
            var okResult = _controller.Get("0.22");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("TWENTY-TWO CENTS ONLY", result.Value);

        }

        [Fact]
        public void Get_DecimalWithDollarAndSingleDigitCent()
        {
            // Act
            var okResult = _controller.Get("102.03");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("ONE HUNDRED TWO DOLLARS AND THREE CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithDollarAndSingleDigitCentRounding()
        {
            // Act
            var okResult = _controller.Get("1.021");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("ONE DOLLAR AND TWO CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithSingleDigitDollarAndCentRounding()
        {
            // Act
            var okResult = _controller.Get("1.23987");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("ONE DOLLAR AND TWENTY-THREE CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithMaxDigitDollarAndCentRounding()
        {
            // Act
            var okResult = _controller.Get("999000000000000.23987");

            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("NINE HUNDRED NINTY-NINE TRILLION DOLLARS AND TWENTY-THREE CENTS", result.Value);

        }

        [Fact]
        public void Get_DecimalWithMaxDigitDollarError()
        {
            // Act
            var okResult = _controller.Get("1000000000000000.23987");
            // Assert
            var result = Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.Equal("Sorry the maximum value that can be processed is 999999999999999.99", result.Value);

        }
    }
}
