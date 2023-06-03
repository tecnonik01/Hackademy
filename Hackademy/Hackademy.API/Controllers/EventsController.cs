using Hackademy.Domain.Entity;
using Hackademy.Domain.Enum;
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
    public class EventsController : ControllerBase
    {
        private HackademyContext HackademyContext { get; set; }
        public EventsController( HackademyContext _hackademyContext)
        {
            HackademyContext = _hackademyContext;
        }
        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var Events = HackademyContext.Events.Where(c=>!c.IsDeleted).ToList();

            return Ok(Events);
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody]CreateEventRequest EventRequest)
        {
                var Event = new Event
                {
                    EventId = 0,
                    TeamId= EventRequest.TeamId,
                    EndDateTime=EventRequest.EndDateTime,
                    EventCity = EventRequest.EventCity,
                    EventStreet = EventRequest.EventStreet,
                    EventLink = EventRequest.EventLink,
                    EventTitle = EventRequest.EventTitle,
                    IsDeleted=false,
                    EventTypeEnum = EventRequest.EventTypeEnum,
                    StartDateTime=EventRequest.StartDateTime,

                };
                HackademyContext.Events.Add(Event) ;
                HackademyContext.SaveChanges();
                return Ok(Event.EventId);
        }

        [HttpDelete("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent([FromBody] int Id)
        {
            var Event = HackademyContext.Events.FirstOrDefault(c => c.EventId == Id);
            if (Event == null) return BadRequest(false);
            Event.IsDeleted = true;
            HackademyContext.Events.Update(Event);
            HackademyContext.SaveChanges();
            return Ok(true);
                    
        }

        [HttpPut("UpdateEvent")]

        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventRequest UpdateEventRequest)
        {
            var Event = HackademyContext.Events.FirstOrDefault(c => c.EventId == UpdateEventRequest.EventId);
            if (Event == null) return BadRequest(false);
            Event.EventLink = UpdateEventRequest.EventLink;
            Event.EventCity = UpdateEventRequest.EventCity;
            Event.EventStreet = UpdateEventRequest.EventStreet;
            Event.EventTitle = UpdateEventRequest.EventTitle;
            Event.EventTypeEnum = UpdateEventRequest.EventTypeEnum;
            Event.EndDateTime = UpdateEventRequest.EndDateTime;
            Event.StartDateTime = UpdateEventRequest.StartDateTime;
            HackademyContext.Events.Update(Event);
            HackademyContext.SaveChanges(true);
            return Ok(true);
        }
       

    }
    public class CreateEventRequest
    {
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public EventTypeEnum EventTypeEnum { get; set; }
        public string EventTitle { get; set; }
        public string? EventStreet { get; set; }
        public string? EventCity { get; set; }
        public string? EventLink { get; set; }
        public int TeamId { get; set; }
    }
    public class UpdateEventRequest
    {
        public int EventId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public EventTypeEnum EventTypeEnum { get; set; }
        public string EventTitle { get; set; }
        public string? EventStreet { get; set; }
        public string? EventCity { get; set; }
        public string? EventLink { get; set; }
    }
}
