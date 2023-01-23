using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using Services.Filters;
using System.Drawing;

namespace Services
{
    public class PolyclinicService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private readonly string path;

        public PolyclinicService(IMapper mapper, IWebHostEnvironment webHostEvironment, PolyclinicDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            path = _webHostEvironment.WebRootPath + "\\Images\\";
        }

        public async Task<List<Polyclinic>> GetPolyclinicsAsync()
        {
            return _mapper.Map<List<Polyclinic>>(_dbContext.Polyclinics.Where(x => x.Archived == false).Include(x => x.City).Include(x => x.Doctors).ToList());
        }
        
        public async Task AddPolyclinicAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is not BadRequestResult)
            {
                throw new Exception("Такая поликлиника уже существует!");
            }

            var mappedPolyclinic = _mapper.Map<PolyclinicDb>(polyclinic);

            await _dbContext.Polyclinics.AddAsync(mappedPolyclinic);
            await _dbContext.SaveChangesAsync();

            await FileManager.SaveImageAsync(polyclinic.Image, path);
        }

        public async Task AddDoctorForPolyclinicAsync(Guid polyclinicId, Doctor doctor)
        {
            var polyclinic = await _dbContext.Polyclinics.FirstOrDefaultAsync(x => x.PolyclinicId == polyclinicId);

            var doctorDb = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.DoctorId == doctor.DoctorId);

            polyclinic.Doctors.Add(doctorDb);

            await _dbContext.SaveChangesAsync();
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
        
        public async Task DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            var polyclinicDb = await _dbContext.Polyclinics.Where(x => x.Name == polyclinic.Name)
                .FirstOrDefaultAsync();

            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is not BadRequestResult)
            {
                throw new Exception("Такой поликлиники не существует!");
            }

            polyclinicDb.Archived = true;

            await _dbContext.SaveChangesAsync();

            FileManager.DeleteImage(polyclinicDb.ImagePath);
        }

        public async Task DeleteDoctorForPolyclinic(Guid polyclinicId, Guid doctorId)
        {
            var polyclinicDb = await _dbContext.Polyclinics.Where(x => x.PolyclinicId == polyclinicId)
                .Include(x => x.Doctors)
                .FirstOrDefaultAsync();

            var doctorDb = await _dbContext.Doctors.Where(x => x.DoctorId == doctorId).FirstOrDefaultAsync();

            polyclinicDb.Doctors.Remove(doctorDb);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePolyclinicAsync(Polyclinic polyclinic)
        {
            var requestResult = await ExistPolyclinicAsync(polyclinic);

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Такой поликлиник не существует!");
            }

            var getPolyclinic = await _dbContext.Polyclinics.Where(x => x.PolyclinicId == polyclinic.PolyclinicId)
                .FirstOrDefaultAsync();

            _dbContext.Entry(getPolyclinic).CurrentValues.SetValues(_mapper.Map<PolyclinicDb>(polyclinic));

            await _dbContext.SaveChangesAsync();

            FileManager.DeleteImage(getPolyclinic.ImagePath);

            await FileManager.SaveImageAsync(polyclinic.Image, path);
        }

        public async Task<IActionResult> GetImagePolyclinicAsync(Guid polyclinicId)
        {
            var getPolyclinicId = await _dbContext.Polyclinics.Where(x => x.PolyclinicId == polyclinicId)
                .FirstOrDefaultAsync();

            getPolyclinicId.ImagePath.Replace("//", "/");

            return await new FileManager().GetImageAsync(getPolyclinicId.ImagePath);
        }
    }
}
