using Hackademy.Domain.Entity;
using Hackademy.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Voltar.Common.Helper;
namespace Hackademy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }
        public CoursesController( HackademyContext _hackademyContext)
        {
            HackademyContext = _hackademyContext;
        }
        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var Courses = HackademyContext.Courses.ToList();

            return Ok(Courses);
        }

        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody]CreateCourseRequest CourseRequest)
        {
            if(CourseRequest.EndDate> CourseRequest.StartDate)
            {
                var course = new Course
                {
                    StartDate = CourseRequest.StartDate,
                    EndDate = CourseRequest.EndDate,
                    CourseId = 0,
                    CourseName = CourseRequest.CourseName,
                    Description = CourseRequest.Description,
                    MaxCapacity = CourseRequest.MaxCapacity,

                };
                HackademyContext.Courses.Add(course) ;
                HackademyContext.SaveChanges();
                return Ok(course.CourseId);
            }
            return BadRequest(false);
        }

        [HttpDelete("DeleteCourse")]
        public async Task<IActionResult> DeleteCourse([FromBody] int Id)
        {
            var Course = HackademyContext.Courses.FirstOrDefault(c => c.CourseId == Id);
            if (Course == null) return BadRequest(false);
            HackademyContext.Courses.Remove(Course);
            HackademyContext.SaveChanges();
            return Ok(true);
                    
        }

        [HttpPut("UpdateCourse")]

        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseRequest UpdateCourseRequest)
        {
            var Course = HackademyContext.Courses.FirstOrDefault(c => c.CourseId == UpdateCourseRequest.CourseId);
            if (Course == null) return BadRequest(false);
            Course.StartDate = UpdateCourseRequest.StartDate;
            Course.EndDate = UpdateCourseRequest.EndDate;
            Course.MaxCapacity = UpdateCourseRequest.MaxCapacity;
            Course.Description = UpdateCourseRequest.Description;
            Course.CourseName = UpdateCourseRequest.CourseName;
            HackademyContext.Courses.Update(Course);
            HackademyContext.SaveChanges(true);
            return Ok(true);
        }
        [HttpPut("InsertUserOnCourse")]

        public async Task<IActionResult> InsertUserOnCourse([FromBody] InsertUserOnCourseRequest InsertUserOnCourseRequest)
        {
            var course = HackademyContext.Courses.Include(c=>c.UserCourses).FirstOrDefault(c => c.CourseId == InsertUserOnCourseRequest.CourseId);
           if(course == null) return BadRequest(false);
            if (course.UserCourses == null)
            {
                course.UserCourses = new List<UserCourse>();
            }
            course.UserCourses.Add(new UserCourse()
            {
                CourseId=InsertUserOnCourseRequest.CourseId,
                UserId=InsertUserOnCourseRequest.UserId
            });
            HackademyContext.SaveChanges(true);
            return Ok();
        }

    }
    public class CreateCourseRequest
    {
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
    }
    public class UpdateCourseRequest
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxCapacity { get; set; }
    }
    public class InsertUserOnCourseRequest
    {
        public int CourseId { get; set; }
        public int UserId { get; set; }
    }
}
