using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validations;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class SpecializationController : ControllerBase
    {
        private SpecializationService _specializationService;
        private IMapper _mapper;
        private SpecializationValidator _specializationValidator;

        public SpecializationController(IMapper mapper, SpecializationValidator specializationValidator)
        {
            _mapper = mapper;
            _specializationService = new SpecializationService(_mapper);
            _specializationValidator = specializationValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Specialization>>> GetSpecialization()
        {
            return await _specializationService.GetSpecializationsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecializationAsync(Specialization specialization)
        {
            if (_specializationValidator.Validate(specialization).Errors.Count != 0)
            {
                throw new ValidationException(_specializationValidator.Validate(specialization).Errors);
            }

            return await _specializationService.AddSpecializationAsync(specialization);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecializationAsync(Specialization specialization)
        {
            if (_specializationValidator.Validate(specialization).Errors.Count != 0)
            {
                throw new ValidationException(_specializationValidator.Validate(specialization).Errors);
            }

            return await _specializationService.UpdateSpecializationAsync(specialization);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecializationAsync(string specializationName)
        {
            return await _specializationService.DeleteSpecializationAsync(specializationName);
        }
    }
}
