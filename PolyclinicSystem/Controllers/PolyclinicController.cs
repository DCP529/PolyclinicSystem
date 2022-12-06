using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Validations;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class PolyclinicController : ControllerBase
    {
        private PolyclinicService _polyclinicService;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;
        private PolyclinicValidator _polyclinicValidator;

        public PolyclinicController(IMapper mapper, IWebHostEnvironment webHostEvironment, PolyclinicValidator polyclinicValidator)
        {
            _webHostEvironment = webHostEvironment;
            _mapper = mapper;
            _polyclinicService = new PolyclinicService(_mapper, _webHostEvironment);   
            _polyclinicValidator = polyclinicValidator;
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

        [HttpPost]
        public async Task<IActionResult> AddPolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            return await _polyclinicService.AddPolyclinicAsync(polyclinic);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            return await _polyclinicService.UpdatePolyclinicAsync(polyclinic);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            if (_polyclinicValidator.Validate(polyclinic).Errors.Count != 0)
            {
                throw new ValidationException(_polyclinicValidator.Validate(polyclinic).Errors);
            }

            return await _polyclinicService.DeletePolyclinicAsync(polyclinic);
        }
    }
}
