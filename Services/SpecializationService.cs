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

        public SpecializationService(IMapper mapper)
        {
            _dbContext = new PolyclinicDbContext();
            _mapper = mapper;
        }

        public async Task<List<Specialization>> GetSpecializationsAsync()
        {
            return _mapper.Map<List<Specialization>>(await _dbContext.Specializations.ToListAsync());
        }

        public async Task<IActionResult> AddSpecializationAsync(Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            var mappedSpecialization = _mapper.Map<SpecializationDb>(specialization);

            await _dbContext.Specializations.AddAsync(mappedSpecialization);
            await _dbContext.SaveChangesAsync();

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> ExistSpecializationAsync(Specialization specialization)
        {
            var getSpecialization = await _dbContext.Specializations
                .FirstOrDefaultAsync(x => x.SpecializationId == specialization.SpecializationId);

            return getSpecialization switch
            {
                null => new BadRequestResult(),
                _ => new StatusCodeResult(200)
            };
        }

        public async Task<IActionResult> DeleteSpecializationAsync(string specializationName)
        {
            var getSpecialization = await _dbContext.Specializations.Where(x => x.Name == specializationName).FirstOrDefaultAsync();

            var requestResult = await ExistSpecializationAsync(_mapper.Map<Specialization>(getSpecialization));

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            _dbContext.Specializations.Remove(getSpecialization);

            await _dbContext.SaveChangesAsync();

            return requestResult;
        }

        public async Task<IActionResult> UpdateSpecializationAsync(Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var getSpecialization = await _dbContext.Specializations.Where(x => x.SpecializationId == specialization.SpecializationId)
                .FirstOrDefaultAsync();

            _dbContext.Entry(getSpecialization).CurrentValues.SetValues(_mapper.Map<SpecializationDb>(specialization));

            await _dbContext.SaveChangesAsync();

            return requestResult;
        }
    }
}
