using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ModelsDb;


namespace Services
{
    public class SpecializationService
    {
        private PolyclinicDbContext _dbContext;
        private IMapper _mapper;

        public SpecializationService(IMapper mapper, PolyclinicDbContext dbContext)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Specialization>> GetSpecializationsAsync()
        {
            return _mapper.Map<List<Specialization>>(await _dbContext.Specializations.Where(x => x.Archived == false).ToListAsync());
        }

        public async Task AddSpecializationAsync(Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is not BadRequestResult)
            {
                throw new Exception("Такая специализация уже существует!");
            }

            var mappedSpecialization = _mapper.Map<SpecializationDb>(specialization);

            await _dbContext.Specializations.AddAsync(mappedSpecialization);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IActionResult> ExistSpecializationAsync(Specialization specialization)
        {
            SpecializationDb getSpecialization = new();

            if (specialization.DoctorId != Guid.Empty)
            {
                getSpecialization = await _dbContext.Specializations
                    .FirstOrDefaultAsync(x => x.DoctorId == specialization.DoctorId);
            }
            else
            {
                getSpecialization = await _dbContext.Specializations
                        .FirstOrDefaultAsync(x => x.Name == specialization.Name);
            }
            return getSpecialization switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }

        public async Task DeleteSpecializationAsync(string specializationName)
        {
            var getSpecialization = await _dbContext.Specializations.Where(x => x.Name == specializationName).FirstOrDefaultAsync();

            var requestResult = await ExistSpecializationAsync(_mapper.Map<Specialization>(getSpecialization));

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Такой специализации не существует!");
            }

            getSpecialization.Archived = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateSpecializationAsync(string oldName, Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is BadRequestResult)
            {
                throw new Exception("Такой специализации не существует!");
            }

            var getSpecialization = await _dbContext.Specializations.Where(x => x.DoctorId == specialization.DoctorId && x.Name == oldName)
                .FirstOrDefaultAsync();

            _dbContext.Entry(getSpecialization).CurrentValues.SetValues(_mapper.Map<SpecializationDb>(specialization));

            await _dbContext.SaveChangesAsync();
        }
    }
}
