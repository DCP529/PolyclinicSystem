using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;
using Services.Filters;

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
            var query = _dbContext.Specializations.Include(x => x).AsQueryable();

            if (filter.FIO != null)
            {
                var doctor = await _dbContext.Doctors
                .Where(x => x.FIO == filter.FIO)
                .FirstOrDefaultAsync();

                query = query.Where(x => x.DoctorId == doctor.DoctorId);
            }

            if (filter.Specialization != null)
            {
                query = query.Where(x => x.Name == filter.Specialization);
            }

            var docotors = await query.FirstOrDefaultAsync();

            return _mapper.Map<List<Doctor>>(docotors.Doctors);
        }

        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var mappedDoctor = _mapper.Map<DoctorDb>(doctor);

            await _dbContext.Doctors.AddAsync(mappedDoctor);
            await _dbContext.SaveChangesAsync();

            await new FileManager().SaveImageAsync(doctor.Image, path);

            return requestResult;
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

        public async Task<IActionResult> DeleteAsync(string doctorFIO)
        {
            var doctor = await _dbContext.Doctors.Where(x => x.FIO == doctorFIO).FirstOrDefaultAsync();

            var requestResult = await ExistDoctorAsync(_mapper.Map<Doctor>(doctor));

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Doctors.Remove(doctor);

            new FileManager().DeleteImage(_mapper.Map<Doctor>(doctor).Image, path);

            await _dbContext.SaveChangesAsync();

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> UpdateAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var getDoctor = await _dbContext.Doctors.Where(x => x.DoctorId == doctor.DoctorId).FirstOrDefaultAsync();

            _dbContext.Entry(getDoctor).CurrentValues.SetValues(_mapper.Map<DoctorDb>(doctor));

            await _dbContext.SaveChangesAsync();

            new FileManager().DeleteImage(_mapper.Map<Doctor>(getDoctor).Image, path);

            await new FileManager().SaveImageAsync(doctor.Image, path);

            return requestResult;
        }
    }
}
