using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validations;
using Services;
using Services.Filters;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private DoctorService _doctorService;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private DoctorValidator _doctorValidator;

        public DoctorController(IMapper mapper, IWebHostEnvironment webHostEvironment, DoctorValidator doctorValidator)
        {
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            _doctorService = new DoctorService(_mapper, _webHostEvironment);
            _doctorValidator = doctorValidator;
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

        [HttpPost]
        public async Task<IActionResult> AddDoctorAsync(Doctor doctor)
        {
            if (_doctorValidator.Validate(doctor).Errors.Count != 0)
            {
                throw new ValidationException(_doctorValidator.Validate(doctor).Errors);
            }

            return await _doctorService.AddDoctorAsync(doctor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctorAsync(Doctor doctor)
        {
            if (_doctorValidator.Validate(doctor).Errors.Count != 0)
            {
                throw new ValidationException(_doctorValidator.Validate(doctor).Errors);
            }

            return await _doctorService.UpdateDoctorAsync(doctor);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorAsync(string doctorFIO)
        {
            return await _doctorService.DeleteDoctorAsync(doctorFIO);
        }
    }
}
