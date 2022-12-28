using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        public CityController(IMapper mapper, AbstractValidator<City> cityValidator)
        {
            _mapper = mapper;
            _cityService = new CityService(_mapper);
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
            return await _cityService.AddCityAsync(cityName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCityAsync(string cityName, string updateCity)
        {
            return await _cityService.UpdateCityAsync(cityName, updateCity);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCityAsync(string cityName)
        {
            return await _cityService.DeleteCityAsync(cityName);
        }
    }
}
