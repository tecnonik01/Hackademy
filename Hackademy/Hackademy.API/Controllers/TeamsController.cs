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
    public class TeamsController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }
        public TeamsController( HackademyContext _hackademyContext)
        {
            HackademyContext = _hackademyContext;
        }
        [HttpGet("GetAllTeams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var Teams = HackademyContext.Teams.Include(c=>c.Users)
                .Where(c=>!c.IsDeleted)
                .Select(c=>new TeamOutputMoldel
                {
                    Name=c.Name,
                    TeamId=c.TeamId,
                    TechLabId=c.TechLabId,
                    UserIds=c.Users.Select(c=>c.UserId).ToList(),
                })
                 .ToList();

            return Ok(Teams);
        }

        [HttpPost("CreateTeam")]
        public async Task<IActionResult> CreateTeam([FromBody]CreateTeamRequest TeamRequest)
        {
            if(TeamRequest.TechLabId!=0)
            {
                var Team = new Team
                {
                    TeamId = 0,
                    TechLabId = TeamRequest.TechLabId,
                    Name=TeamRequest.TeamName,
                    IsDeleted= false,
                };
                HackademyContext.Teams.Add(Team) ;
                HackademyContext.SaveChanges();
                return Ok(Team.TeamId);
            }
            return BadRequest(false);
        }

        [HttpDelete("DeleteTeam")]
        public async Task<IActionResult> DeleteTeam([FromBody] int Id)
        {
            var Team = HackademyContext.Teams.FirstOrDefault(c => c.TeamId == Id);
            if (Team == null) return BadRequest(false);
            Team.IsDeleted= true;
            HackademyContext.Teams.Update(Team);
            HackademyContext.SaveChanges();
            return Ok(true);
                    
        }

        [HttpPut("UpdateTeam")]

        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamRequest UpdateTeamRequest)
        {
            var Team = HackademyContext.Teams.FirstOrDefault(c => c.TeamId == UpdateTeamRequest.TeamId);
            if (Team == null) return BadRequest(false);
            Team.Name = UpdateTeamRequest.TeamName;
            HackademyContext.Teams.Update(Team);
            HackademyContext.SaveChanges(true);
            return Ok(true);
        }
        [HttpPut("InsertUserOnTeam")]

        public async Task<IActionResult> InsertUserOnTeam([FromBody] InsertUserOnTeamRequest InsertUserOnTeamRequest)
        {
            var Team = HackademyContext.Teams.Include(c=>c.Users).FirstOrDefault(c => c.TeamId == InsertUserOnTeamRequest.TeamId);
            var User=HackademyContext.Users.FirstOrDefault(c=>c.UserId== InsertUserOnTeamRequest.UserId);
           if(Team == null || User==null) return BadRequest(false);
            if (!Team.Users.Any(c => c.UserId == InsertUserOnTeamRequest.UserId))
                 Team.Users.Add(User);
            HackademyContext.SaveChanges(true);
            return Ok();
        }
        [HttpPut("RemoveUserOnTeam")]

        public async Task<IActionResult> RemoveUserOnTeam([FromBody] RemoveUserOnTeamRequest RemoveUserOnTeamRequest)
        {
            var Team = HackademyContext.Teams.Include(c => c.Users).FirstOrDefault(c => c.TeamId == RemoveUserOnTeamRequest.TeamId);
            var User = HackademyContext.Users.FirstOrDefault(c => c.UserId == RemoveUserOnTeamRequest.UserId);
            if (Team == null || User == null) return BadRequest(false);
            if (Team.Users.Any(c => c.UserId == RemoveUserOnTeamRequest.UserId))
                Team.Users.Remove(User);
            HackademyContext.SaveChanges(true);
            return Ok();
        }
    }
    public class CreateTeamRequest
    {
        public int TechLabId { get; set; }
        public string TeamName { get; set; }
    }
    public class UpdateTeamRequest
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
    public class InsertUserOnTeamRequest
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
    public class RemoveUserOnTeamRequest
    {
        public int TeamId { get; set; }
        public int UserId { get; set; }
    }
    public class TeamOutputMoldel
    {
        public string Name { get; set; }
        public int TeamId { get; set; }
        public IList<int> UserIds { get; set; }
        public int TechLabId { get; set; }
    }
}
