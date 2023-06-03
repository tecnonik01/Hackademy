using Hackademy.Domain.Entity;
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
    public class TechLabsController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }
        private CurrentUserService CurrentUserService { get; set; }
        public TechLabsController( HackademyContext _hackademyContext, CurrentUserService currentUserService)
        {
            HackademyContext = _hackademyContext;
            CurrentUserService = currentUserService;
        }
        [HttpGet("GetAllTechLabs")]
        public async Task<IActionResult> GetAllTechLabs()
        {
            var TechLabs = HackademyContext.TechLabs.Where(c=>!c.IsDeleted).ToList();

            return Ok(TechLabs);
        }

        [HttpPost("CreateTechLab")]
        public async Task<IActionResult> CreateTechLab([FromBody]CreateTechLabRequest TechLabRequest)
        {
                var TechLab = new TechLab
                {
                    TechLabId = 0,
                    Name=TechLabRequest.TechLabName
                };
                HackademyContext.TechLabs.Add(TechLab) ;
                HackademyContext.SaveChanges();
                return Ok(TechLab.TechLabId);
        }

        [HttpDelete("DeleteTechLab")]
        public async Task<IActionResult> DeleteTechLab([FromBody] int Id)
        {
            var TechLab = HackademyContext.TechLabs.FirstOrDefault(c => c.TechLabId == Id);
            if (TechLab == null) return BadRequest(false);
            TechLab.IsDeleted = true;
            HackademyContext.TechLabs.Update(TechLab);
            HackademyContext.SaveChanges();
            return Ok(true);
                    
        }

        [HttpPut("UpdateTechLab")]

        public async Task<IActionResult> UpdateTechLab([FromBody] UpdateTechLabRequest UpdateTechLabRequest)
        {
            var TechLab = HackademyContext.TechLabs.FirstOrDefault(c => c.TechLabId == UpdateTechLabRequest.TechLabId);
            if (TechLab == null) return BadRequest(false);
            TechLab.Name = UpdateTechLabRequest.TechLabName;
            HackademyContext.TechLabs.Update(TechLab);
            HackademyContext.SaveChanges(true);
            return Ok(true);
        }
       

    }
    public class CreateTechLabRequest
    {
        public string TechLabName { get; set; }
    }
    public class UpdateTechLabRequest
    {
        public int TechLabId { get; set; }
        public string TechLabName { get; set; }
    }
}
