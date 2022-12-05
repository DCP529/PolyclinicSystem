using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Models;
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

        public DoctorController(IMapper mapper, IWebHostEnvironment webHostEvironment)
        {
            _mapper = mapper;
            _webHostEvironment = webHostEvironment;
            _doctorService = new DoctorService(_mapper, _webHostEvironment);
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
            return await _doctorService.AddDoctorAsync(doctor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDoctorAsync(Doctor doctor)
        {
            return await _doctorService.UpdateDoctorAsync(doctor);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDoctorAsync(string doctorFIO)
        {
            return await _doctorService.DeleteDoctorAsync(doctorFIO);
        }
    }
}
