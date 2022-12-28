using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validations;
using Services;
using Services.Filters;
using System.Security.Claims;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private DoctorService _doctorService;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private DoctorValidator _doctorValidator;
        private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public DoctorController(IMapper mapper, IWebHostEnvironment webHostEvironment, AbstractValidator<Doctor> doctorValidator)
        {
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            _doctorService = new DoctorService(_mapper, _webHostEvironment);
            _doctorValidator = (DoctorValidator)doctorValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetDoctorsAsync(DoctorFilter filter)
        {
            return await _doctorService.GetDoctorsAsync(filter);
        }

        [HttpGet]
        [Route("GetDoctorImageAsync")]
        public async Task<IActionResult> GetDoctorImageAsync(Guid doctorId)
        {
            return await _doctorService.GetImageDoctorAsync(doctorId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            if (_doctorValidator.Validate(doctor).Errors.Count != 0)
            {
                throw new ValidationException(_doctorValidator.Validate(doctor).Errors);
            }

            return await _doctorService.AddDoctorAsync(doctor);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateDoctorAsync(Doctor doctor)
        {
            if (_doctorValidator.Validate(doctor).Errors.Count != 0)
            {
                throw new ValidationException(_doctorValidator.Validate(doctor).Errors);
            }

            return await _doctorService.UpdateDoctorAsync(doctor);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorAsync(string doctorFIO)
        {
            return await _doctorService.DeleteDoctorAsync(doctorFIO);
        }
    }
}
