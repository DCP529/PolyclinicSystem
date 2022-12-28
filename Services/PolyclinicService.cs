using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;

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

        public async Task<List<Polyclinic>> GetPolyclinicsAsync()
        {
            return _mapper.Map<List<Polyclinic>>(_dbContext.Polyclinics.Select(x => x).Include(x => x.City).ToList());
        }
        
        public async Task<IActionResult> AddPolyclinicAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            var mappedPolyclinic = _mapper.Map<PolyclinicDb>(polyclinic);

            await _dbContext.Polyclinics.AddAsync(mappedPolyclinic);
            await _dbContext.SaveChangesAsync();

            await FileManager.SaveImageAsync(polyclinic.Image, path);

            return new StatusCodeResult(200);
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
        
        public async Task<IActionResult> DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            var polyclinicDb = await _dbContext.Polyclinics.Where(x => x.Name == polyclinic.Name)
                .FirstOrDefaultAsync();

            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Polyclinics.Remove(polyclinicDb);

            await _dbContext.SaveChangesAsync();

            FileManager.DeleteImage(polyclinicDb.ImagePath);

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> UpdatePolyclinicAsync(Polyclinic polyclinic)
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

            FileManager.DeleteImage(getPolyclinic.ImagePath);

            await FileManager.SaveImageAsync(polyclinic.Image, path);

            return requestResult;
        }

        public async Task<IActionResult> GetImagePolyclinicAsync(Guid polyclinicId)
        {
            var getPolyclinicId = await _dbContext.Polyclinics.Where(x => x.PolyclinicId == polyclinicId)
                .FirstOrDefaultAsync();

            return await new FileManager().GetImageAsync(getPolyclinicId.ImagePath);
        }
    }
}
