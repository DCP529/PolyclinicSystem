using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class SpecializationController : ControllerBase
    {
        private SpecializationService _specializationService;
        private IMapper _mapper;

        public SpecializationController(IMapper mapper)
        {
            _mapper = mapper;
            _specializationService = new SpecializationService(_mapper);
        }

        [HttpGet]
        public async Task<ActionResult<List<Specialization>>> GetSpecialization()
        {
            return await _specializationService.GetSpecializationsAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddSpecializationAsync(Specialization specialization)
        {
            return await _specializationService.AddSpecializationAsync(specialization);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSpecializationAsync(Specialization specialization)
        {
            return await _specializationService.UpdateSpecializationAsync(specialization);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpecializationAsync(string specializationName)
        {
            return await _specializationService.DeleteSpecializationAsync(specializationName);
        }
    }
}
