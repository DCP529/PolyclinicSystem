using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validations;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private CityService _cityService;
        private IMapper _mapper;
        private CityValidator _cityValidator;

        public CityController(IMapper mapper, CityValidator cityValidator)
        {
            _mapper = mapper;
            _cityService = new CityService(_mapper);
            _cityValidator = cityValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<City>>> GetCitiesAsync()
        {
            return await _cityService.GetCitiesAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddCityAsync(string cityName)
        {
            return await _cityService.AddCityAsync(cityName);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCityAsync(City city)
        {
            if (_cityValidator.Validate(city).Errors.Count != 0)
            {
                throw new ValidationException(_cityValidator.Validate(city).Errors);
            }

            return await _cityService.UpdateCityAsync(city);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCityAsync(string cityName)
        {
            return await _cityService.DeleteCityAsync(cityName);
        }
    }
}
