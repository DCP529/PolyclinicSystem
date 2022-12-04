using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using System.Numerics;

namespace Services
{
    public class PolyclinicService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private readonly string path;

        public PolyclinicService(IMapper mapper, IWebHostEnvironment webHostEvironment)
        {
            _dbContext = new PolyclinicDbContext();
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            path = _webHostEvironment.WebRootPath + "\\Images\\";
        }

        public async Task<List<Polyclinic>> GetPolyclinicsAsync(string cityName)
        {
            var query = await _dbContext.Cities.Include(x => x.Name == cityName).FirstOrDefaultAsync();

            return _mapper.Map<List<Polyclinic>>(query.Polyclinics);
        }
        
        public async Task<IActionResult> AddPolyclinicAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var mappedPolyclinic = _mapper.Map<PolyclinicDb>(polyclinic);

            await _dbContext.Polyclinics.AddAsync(mappedPolyclinic);
            await _dbContext.SaveChangesAsync();

            await new FileManager().SaveImageAsync(polyclinic.Image, path);

            return requestResult;
        }

        public async Task<IActionResult> ExistPolyclinicAsync(Polyclinic polyclinic)
        {
            var getPolyclinic = await _dbContext.Polyclinics.FirstOrDefaultAsync(x => x.PolyclinicId == polyclinic.PolyclinicId);

            return getPolyclinic switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }
        
        public async Task<IActionResult> DeleteAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Polyclinics.Remove(_mapper.Map<PolyclinicDb>(polyclinic));

            await _dbContext.SaveChangesAsync();

            new FileManager().DeleteImage(polyclinic.Image, path);

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> UpdateAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var getPolyclinic = await _dbContext.Polyclinics.Where(x => x.PolyclinicId == polyclinic.PolyclinicId)
                .FirstOrDefaultAsync();

            _dbContext.Entry(getPolyclinic).CurrentValues.SetValues(_mapper.Map<PolyclinicDb>(polyclinic));

            await _dbContext.SaveChangesAsync();

            new FileManager().DeleteImage(_mapper.Map<Polyclinic>(getPolyclinic).Image, path);

            await new FileManager().SaveImageAsync(polyclinic.Image, path);

            return requestResult;
        }
    }
}
