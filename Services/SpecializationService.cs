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

        public async Task<List<Specialization>> GetSpecializationsAsync(string specializationName)
        {
            var query = await _dbContext.Specializations.ToListAsync();

            return _mapper.Map<List<Specialization>>(query);
        }

        public async Task<IActionResult> AddSpecializationAsync(Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is BadRequestResult)
            {
                return requestResult;
            }

            var mappedSpecialization = _mapper.Map<SpecializationDb>(specialization);

            await _dbContext.Specializations.AddAsync(mappedSpecialization);
            await _dbContext.SaveChangesAsync();

            return requestResult;
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

        public async Task<IActionResult> DeleteAsync(Specialization specialization)
        {
            var requestResult = await ExistSpecializationAsync(specialization);

            if (requestResult is not BadRequestResult)
            {
                return new BadRequestResult();
            }

            _dbContext.Specializations.Remove(_mapper.Map<SpecializationDb>(specialization));

            await _dbContext.SaveChangesAsync();

            return new StatusCodeResult(200);
        }

        public async Task<IActionResult> UpdateAsync(Specialization specialization)
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
