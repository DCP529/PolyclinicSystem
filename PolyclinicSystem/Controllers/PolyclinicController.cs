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
    public class PolyclinicController : ControllerBase
    {
        private PolyclinicService _polyclinicService;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private PolyclinicValidator _polyclinicValidator;
        private Guid _userId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public PolyclinicController(PolyclinicDbContext dbContext, IMapper mapper, IWebHostEnvironment webHostEvironment, AbstractValidator<Polyclinic> polyclinicValidator)
        {
            _webHostEvironment = webHostEvironment;
            _mapper = mapper;
            _polyclinicService = new PolyclinicService(_mapper, _webHostEvironment, dbContext);   
            _polyclinicValidator = (PolyclinicValidator)polyclinicValidator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Polyclinic>>> GetPolyclinicsAsync()
        {
            return await _polyclinicService.GetPolyclinicsAsync();
        }

        [HttpGet]
        [Route("GetPolyclinicImageAsync")]
        public async Task<IActionResult> GetPolyclinicImageAsync(Guid polyclinicId)
        {
            return await _polyclinicService.GetImagePolyclinicAsync(polyclinicId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddPolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            await _polyclinicService.AddPolyclinicAsync(polyclinic);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("AddDoctorForPolyclinic")]
        public async Task<IActionResult> AddDoctorForPolyclinic(Guid polyclinicId, Doctor doctor)
        {
            if(polyclinicId != Guid.Empty && doctor.DoctorId != Guid.Empty)
            {
                await _polyclinicService.AddDoctorForPolyclinicAsync(polyclinicId, doctor);

                return Ok();
            }

            return new BadRequestResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            await _polyclinicService.UpdatePolyclinicAsync(polyclinic);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            if (polyclinic.Name != null)
            {
                await _polyclinicService.DeletePolyclinicAsync(polyclinic);

                return Ok();
            }

            return new BadRequestResult();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("DeleteDoctorForPolyclinic")]
        public async Task<IActionResult> DeleteDoctorForPolyclinic(Guid polyclinicId, Guid doctorId)
        {
            await _polyclinicService.DeleteDoctorForPolyclinic(polyclinicId, doctorId);

            return Ok();
        }
    }
}
