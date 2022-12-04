using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;

namespace Services
{
    public class CityService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;

        public CityService(IMapper mapper)
        {
            _dbContext = new PolyclinicDbContext();
            _mapper = mapper;
        }
                
        public async Task<List<City>> GetCitiesAsync()
        {
            return _mapper.Map<List<City>>(await _dbContext.Cities.ToListAsync());
        }

        public async Task<IActionResult> AddCityAsync(string cityName)
        {
            var requestResult = await ExistCityAsync(cityName);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var city = await _dbContext.Cities.Where(x => x.Name == cityName).FirstOrDefaultAsync();

            var mappedCity = _mapper.Map<CityDb>(city);

            await _dbContext.Cities.AddAsync(mappedCity);
            await _dbContext.SaveChangesAsync();

            return requestResult;
        }

        public async Task<IActionResult> ExistCityAsync(string cityName)
        {
            var getCity = await _dbContext.Cities.FirstOrDefaultAsync(x => x.Name == cityName);

            return getCity switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }

        public async Task<IActionResult> UpdateAsync(City city)
        {
            var requestResult = await ExistCityAsync(city.Name);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var getCity = await _dbContext.Cities.Where(x => x.CityId == city.CityId).FirstOrDefaultAsync();

            _dbContext.Entry(getCity).CurrentValues.SetValues(_mapper.Map<CityDb>(city));

            await _dbContext.SaveChangesAsync();

            return requestResult;
        }

        public async Task<IActionResult> DeleteAsync(string cityName)
        {
            var requestResult = await ExistCityAsync(cityName);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Cities.Remove(await _dbContext.Cities.Where(x => x.Name == cityName).FirstOrDefaultAsync());

            await _dbContext.SaveChangesAsync();

            return new StatusCodeResult(200);
        }
    }
}