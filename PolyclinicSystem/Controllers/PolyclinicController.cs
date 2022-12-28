using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        public PolyclinicController(IMapper mapper, IWebHostEnvironment webHostEvironment, AbstractValidator<Polyclinic> polyclinicValidator)
        {
            _webHostEvironment = webHostEvironment;
            _mapper = mapper;
            _polyclinicService = new PolyclinicService(_mapper, _webHostEvironment);   
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

            return await _polyclinicService.AddPolyclinicAsync(polyclinic);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            return await _polyclinicService.UpdatePolyclinicAsync(polyclinic);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            if (polyclinic.Name != null)
            {
                return await _polyclinicService.DeletePolyclinicAsync(polyclinic);
            }

            return new BadRequestResult();
        }
    }
}
