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
        public void Test1()
        {

        }
    }
}
