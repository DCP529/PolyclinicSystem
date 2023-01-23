using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Models.Validations;
using Services;
using System.Security.Claims;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class SpecializationController : ControllerBase
    {
        private SpecializationService _specializationService;
        private IMapper _mapper;
        private SpecializationValidator _specializationValidator;
        private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public SpecializationController(PolyclinicDbContext dbContext, IMapper mapper, AbstractValidator<Specialization> specializationValidator)
        {
            _mapper = mapper;
            _specializationService = new SpecializationService(_mapper, dbContext);
            _specializationValidator = (SpecializationValidator)specializationValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Specialization>>> GetSpecialization()
        {
            return await _specializationService.GetSpecializationsAsync();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddSpecializationAsync(Specialization specialization)
        {
            if (_specializationValidator.Validate(specialization).Errors.Count != 0)
            {
                throw new ValidationException(_specializationValidator.Validate(specialization).Errors);
            }

            await _specializationService.AddSpecializationAsync(specialization);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateSpecializationAsync(string oldName, Specialization specialization)
        {
            if (_specializationValidator.Validate(specialization).Errors.Count != 0)
            {
                throw new ValidationException(_specializationValidator.Validate(specialization).Errors);
            }

            await _specializationService.UpdateSpecializationAsync(oldName, specialization);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSpecializationAsync(string specializationName)
        {
            await _specializationService.DeleteSpecializationAsync(specializationName);

            return Ok();
        }
    }
}
