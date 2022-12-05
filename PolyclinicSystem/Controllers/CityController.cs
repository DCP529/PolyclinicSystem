using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private CityService _cityService;
        private IMapper _mapper;

        public CityController(IMapper mapper)
        {
            _mapper = mapper;
            _cityService = new CityService(_mapper);
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
            return await _cityService.UpdateCityAsync(city);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCityAsync(string cityName)
        {
            return await _cityService.DeleteCityAsync(cityName);
        }
    }
}
