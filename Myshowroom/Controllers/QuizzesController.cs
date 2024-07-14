using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myshowroom.DataContext;
using Myshowroom.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TechLearn.Models.Domain_Models;

namespace TechLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        private readonly dataContext _context;
        private readonly HttpClient _httpClient;
        public QuizzesController(dataContext context , HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        [HttpPost]
        public async Task<IActionResult> Compile([FromBody] CodeRequest request)
        {
            var apiUrl = "https://api.jdoodle.com/v1/execute";
            var apiRequest = new
            {
                script = request.Code,
                language = request.Language,
                versionIndex = "3", // Adjust as needed for the API
                clientId = "55724e24e8c6b08841e1a00c68edc89", // Replace with your JDoodle client ID
                clientSecret = "12636a27109ac14acb1db96416753f4fd7ef893b9e2843d7f2c861593fa3beb0" // Replace with your JDoodle client secret
            };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(apiRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            return Ok(new { output = responseContent });
        }
    }
}
