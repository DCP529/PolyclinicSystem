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

        public CityService(IMapper mapper, PolyclinicDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
                
        public async Task<List<City>> GetCitiesAsync()
        {
            return _mapper.Map<List<City>>(await _dbContext.Cities.Where(x => x.Archived == false).ToListAsync());
        }

        public async Task AddCityAsync(string cityName)
        {
            var requestResult = await ExistCityAsync(cityName);

            if (requestResult is not BadRequestResult)
            {
                throw new Exception("Такой город уже существует!");
            }

            await _dbContext.Cities.AddAsync(new CityDb()
            {
                Name = cityName,
                CityId = Guid.NewGuid()
            });

            await _dbContext.SaveChangesAsync();
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

        public async Task UpdateCityAsync(string cityName, string updateCity)
        {
            var getCity = await _dbContext.Cities.Where(x => x.Name == cityName).FirstOrDefaultAsync();

            if (getCity == null)
            {
                throw new Exception("Такого города не существует!");
            }

            var city = new City()
            {
                CityId = getCity.CityId,
                Name = updateCity
            };

            _dbContext.Entry(getCity).CurrentValues.SetValues(_mapper.Map<CityDb>(city));

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(string cityName)
        {
            var requestResult = await ExistCityAsync(cityName);

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Нельзя удалить несуществующий город!");
            }

            var city = await _dbContext.Cities.Where(x => x.Name == cityName).FirstOrDefaultAsync();

            city.Archived = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}