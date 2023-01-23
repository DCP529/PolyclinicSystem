using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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

        public DoctorService(IMapper mapper, IWebHostEnvironment webHostEvironment, PolyclinicDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            path = _webHostEvironment.WebRootPath + "\\Images\\";
        }

        public async Task<List<Doctor>> GetDoctorsAsync(DoctorFilter filter)
        {
            var query = _dbContext.Doctors.Where(x => x.Archived == false).AsQueryable();

            if (filter.DoctorId != Guid.Empty)
            {
                query = query.Where(x => x.DoctorId == filter.DoctorId);

                return _mapper.Map<List<Doctor>>(await query.ToListAsync());
            }

            if (filter.FIO != null)
            {
                query = query.Where(x => x.FIO == filter.FIO);

                return _mapper.Map<List<Doctor>>(await query.ToListAsync());
            }

            if (filter.Specialization != null)
            {
                var listSpecialization = _dbContext.Specializations.Where(x => x.Name == filter.Specialization && x.Archived == false);

                var listDoctorsId = new List<Guid>();

                foreach (var item in listSpecialization)
                {
                    listDoctorsId.Add(item.DoctorId);
                }

                var listDoctor = new List<DoctorDb>();

                foreach (var item in listDoctorsId)
                {
                    listDoctor.Add(_dbContext.Doctors.FirstOrDefault(x => x.DoctorId == item));
                }

                return _mapper.Map<List<Doctor>>(listDoctor);
            }

            return _mapper.Map<List<Doctor>>(query.ToList());
        }

        public async Task AddDoctorAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is not BadRequestResult)
            {
                throw new Exception("Такой доктор уже существует!");
            }

            var mappedDoctor = _mapper.Map<DoctorDb>(doctor);

            await _dbContext.Doctors.AddAsync(mappedDoctor);
            await _dbContext.SaveChangesAsync();

            await FileManager.SaveImageAsync(doctor.Image, path);
        }

        public async Task<IActionResult> ExistDoctorAsync(Doctor doctor)
        {
            var getDoctor = await _dbContext.Doctors.FirstOrDefaultAsync(x => x.DoctorId == doctor.DoctorId
            || x.FIO == doctor.FIO);

            return getDoctor switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }

        public async Task DeleteDoctorAsync(string doctorFIO)
        {
            var doctor = await _dbContext.Doctors.Where(x => x.FIO == doctorFIO).FirstOrDefaultAsync();

            var requestResult = await ExistDoctorAsync(_mapper.Map<Doctor>(doctor));

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Нельзя удалить не существующего доктора!");
            }

            var specializations = await _dbContext.Specializations.Where(x => x.DoctorId == doctor.DoctorId).ToListAsync();

            if (specializations != null)
            {
                specializations.ForEach(x => x.Archived = true);
            }

            doctor.Archived = true;

            FileManager.DeleteImage(doctor.ImagePath);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            var requestResult = await ExistDoctorAsync(doctor);

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Такого доктора не существует!");
            }

            var getDoctor = await _dbContext.Doctors.Where(x => x.FIO == doctor.FIO).FirstOrDefaultAsync();

            _dbContext.Entry(getDoctor).CurrentValues.SetValues(_mapper.Map<DoctorDb>(doctor));

            await _dbContext.SaveChangesAsync();

            FileManager.DeleteImage(getDoctor.ImagePath);

            await FileManager.SaveImageAsync(doctor.Image, path);
        }

        public async Task<IActionResult> GetImageDoctorAsync(Guid doctorId)
        {
            var getDoctor = await _dbContext.Doctors.Where(x => x.DoctorId == doctorId).FirstOrDefaultAsync();

            getDoctor.ImagePath.Replace("//", "/");

            return await new FileManager().GetImageAsync(getDoctor.ImagePath);
        }
    }
}
