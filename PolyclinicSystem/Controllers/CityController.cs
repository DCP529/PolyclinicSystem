using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Models.Validations;
using Services;
using System.Security.Claims;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private CityService _cityService;
        private IMapper _mapper;
        private CityValidator _cityValidator;
        private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public CityController(IMapper mapper, AbstractValidator<City> cityValidator, PolyclinicDbContext dbContext)
        {
            _mapper = mapper;
            _cityService = new CityService(_mapper, dbContext);
            _cityValidator = (CityValidator)cityValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCitiesAsync()
        {
            return await _cityService.GetCitiesAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCityAsync(string cityName)
        {
            await _cityService.AddCityAsync(cityName);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCityAsync(string cityName, string updateCity)
        {
            await _cityService.UpdateCityAsync(cityName, updateCity);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCityAsync(string cityName)
        {
            await _cityService.DeleteCityAsync(cityName);

            return Ok();
        }
    }
}
