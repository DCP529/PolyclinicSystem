using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using Services.Filters;
using System.Collections.Generic;

namespace Services
{
    public class DoctorService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private readonly string path;

        public DoctorService(IMapper mapper, IWebHostEnvironment webHostEvironment)
        {
            _dbContext = new PolyclinicDbContext();
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            path = _webHostEvironment.WebRootPath + "\\Images\\";
        }

        public async Task<List<Doctor>> GetDoctorsAsync(DoctorFilter filter)
        {
            var query = _dbContext.Doctors.AsQueryable();

            if (filter.FIO != null)
            {
                query = query.Where(x => x.FIO == filter.FIO);
            }

            if (filter.Specialization != null)
            {
                query = query.Where(x => x == x.Specializations.Where(x => x.Name == filter.Specialization));
            }

            return _mapper.Map<List<Doctor>>(await query.ToListAsync());
        }

        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            var mappedDoctor = _mapper.Map<DoctorDb>(doctor);

            await _dbContext.Doctors.AddAsync(mappedDoctor);
            await _dbContext.SaveChangesAsync();

            await FileManager.SaveImageAsync(doctor.Image, path);

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> ExistDoctorAsync(Doctor doctor)
        {
            var getDoctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.DoctorId == doctor.DoctorId);

            return getDoctor switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }

        public async Task<IActionResult> DeleteDoctorAsync(string doctorFIO)
        {
            var doctor = await _dbContext.Doctors.Where(x => x.FIO == doctorFIO).FirstOrDefaultAsync();

            var requestResult = await ExistDoctorAsync(_mapper.Map<Doctor>(doctor));

            if (requestResult is BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Doctors.Remove(doctor);

            FileManager.DeleteImage(doctor.ImagePath);

            await _dbContext.SaveChangesAsync();

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> UpdateDoctorAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var getDoctor = await _dbContext.Doctors.Where(x => x.DoctorId == doctor.DoctorId).FirstOrDefaultAsync();

            _dbContext.Entry(getDoctor).CurrentValues.SetValues(_mapper.Map<DoctorDb>(doctor));

            await _dbContext.SaveChangesAsync();

            FileManager.DeleteImage(getDoctor.ImagePath);

            await FileManager.SaveImageAsync(doctor.Image, path);

            return requestResult;
        }

        public async Task<IActionResult> GetImageDoctorAsync(Guid doctorId)
        {
            var getDoctor = await _dbContext.Doctors.Where(x => x.DoctorId == doctorId).FirstOrDefaultAsync();

            return await new FileManager().GetImageAsync(getDoctor.ImagePath);
        }
    }
}
