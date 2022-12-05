using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ModelsDb;
using Services;

namespace PolyclinicSystem.Controllers
{
    [Route("[controller]")]
    public class PolyclinicController : ControllerBase
    {
        private PolyclinicService _polyclinicService;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEvironment;

        public PolyclinicController(IMapper mapper, IWebHostEnvironment webHostEvironment)
        {
            _webHostEvironment = webHostEvironment;
            _mapper = mapper;
            _polyclinicService = new PolyclinicService(_mapper, _webHostEvironment);            
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
            return await _polyclinicService.AddPolyclinicAsync(polyclinic);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePolyclinicAsync(Polyclinic polyclinic)
        {
            return await _polyclinicService.UpdatePolyclinicAsync(polyclinic);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePolyclinicAsync(Polyclinic polyclinic)
        {
            return await _polyclinicService.DeletePolyclinicAsync(polyclinic);
        }
    }
}
