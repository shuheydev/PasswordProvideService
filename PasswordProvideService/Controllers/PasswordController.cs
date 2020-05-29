using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PasswordGenerator;

namespace PasswordProvideService.Controllers
{
    public class Requirement
    {
        public int Length { get; set; } = 6;
        public bool DigitOn { get; set; } = true;
        public bool LowerCaseOn { get; set; } = true;
        public bool UpperCaseOn { get; set; } = true;
        public bool NonAlphaNumericOn { get; set; } = true;
        public int Count { get; set; } = 1;
    }


    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordGeneratorBuilder builder;

        public PasswordController(IPasswordGeneratorBuilder builder)
        {
            this.builder = builder;
            Generate();
        }

        [HttpGet]
        [Route("Generate")]
        public IActionResult Generate(
            [FromQuery]int length = 6,
            [FromQuery]bool digitOn = true,
            [FromQuery]bool lowerCaseOn = true,
            [FromQuery]bool upperCaseOn = true,
            [FromQuery]bool nonAlphaNumericOn = true,
            [FromQuery]int count = 1)
        {
            builder.SetRequiredLength(length)
                .SetRequireDigit(digitOn)
                .SetRequireLowercase(lowerCaseOn)
                .SetRequireUppercase(upperCaseOn)
                .SetRequireNonAlphanumeric(nonAlphaNumericOn);

            var generator = builder.Build();

            var result = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string password = generator.Generate();

                result.Add(password);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("GeneratePost")]
        public IActionResult GeneratePost(
            [FromBody]Requirement requirement)
        {
            builder.SetRequiredLength(requirement.Length)
                .SetRequireDigit(requirement.DigitOn)
                .SetRequireLowercase(requirement.LowerCaseOn)
                .SetRequireUppercase(requirement.UpperCaseOn)
                .SetRequireNonAlphanumeric(requirement.NonAlphaNumericOn);

            var generator = builder.Build();

            var result = new List<string>();
            for (int i = 0; i < requirement.Count; i++)
            {
                string password = generator.Generate();

                result.Add(password);
            }

            return Ok(result);
        }
    }
}
