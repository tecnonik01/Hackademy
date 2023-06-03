using Hackademy.Domain.Entity;
using Hackademy.Domain.Enum;
using Hackademy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Voltar.Common.Helper;
namespace Hackademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CareersController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }

        private CurrentUserService CurrentUserService { get; set; }
        public CareersController( HackademyContext _hackademyContext, CurrentUserService currentUserService)
        {
            HackademyContext = _hackademyContext;
            CurrentUserService = currentUserService;
        }
        [HttpGet("GetAllCareers")]
        public async Task<IActionResult> GetAllCareers()
        {
            var Careers = HackademyContext.Careers.Where(c=>!c.IsDeleted).ToList();

            return Ok(Careers);
        }

        [HttpPost("CreateCareer")]
        public async Task<IActionResult> CreateCareer([FromBody]CreateCareerRequest CareerRequest)
        {
                var Career = new Career
                {
                    CareerId = 0,
                    IsDeleted=false,
                    TechLabId= CareerRequest.TechLabId,
                    IsDone=false,
                    CareerStepNumber= CareerRequest.CareerStepNumber,
                    CareerDescription=CareerRequest.CareerDescription,
                    CareerTitle=CareerRequest.CareerTitle,

                };
                HackademyContext.Careers.Add(Career) ;
                HackademyContext.SaveChanges();
                return Ok(Career.CareerId);
        }

        [HttpDelete("DeleteCareer")]
        public async Task<IActionResult> DeleteCareer([FromBody] int Id)
        {
            var Career = HackademyContext.Careers.FirstOrDefault(c => c.CareerId == Id);
            if (Career == null) return BadRequest(false);
            Career.IsDeleted = true;
            HackademyContext.Careers.Update(Career);
            HackademyContext.SaveChanges();
            return Ok(true);
                    
        }

        [HttpPut("UpdateCareer")]

        public async Task<IActionResult> UpdateCareer([FromBody] UpdateCareerRequest UpdateCareerRequest)
        {
            var Career = HackademyContext.Careers.FirstOrDefault(c => c.CareerId == UpdateCareerRequest.CareerId);
            if (Career == null) return BadRequest(false);
            Career.CareerTitle = UpdateCareerRequest.CareerTitle;
            Career.CareerDescription = UpdateCareerRequest.CareerDescription;
            Career.CareerStepNumber = UpdateCareerRequest.CareerStepNumber;
            Career.IsDone = UpdateCareerRequest.IsDone;
            HackademyContext.Careers.Update(Career);
            HackademyContext.SaveChanges(true);
            return Ok(true);
        }
       

    }
    public class CreateCareerRequest
    {
        public int CareerStepNumber { get; set; }
        public int CareerTitle { get; set; }
        public int CareerDescription { get; set; }
        public int TechLabId { get; set; }

    }
    public class UpdateCareerRequest
    {
        public int CareerId { get; set; }
        public int CareerStepNumber { get; set; }
        public int CareerTitle { get; set; }
        public int CareerDescription { get; set; }
        public bool IsDone { get; set; }
    }
}
