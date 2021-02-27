using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace test.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        public List<stats> l = new List<stats>();
        private readonly TestServices _testService;
        public TestController(TestServices testService){
            _testService = testService;
        }
        [HttpGet]
        public string Hi(){
            return _testService.GetTestData();
        }

        }
    }
