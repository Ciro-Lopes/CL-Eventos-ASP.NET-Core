using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using _net_core_api.model;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace _net_core_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        public DataContext Context { get; }
        public EventController(DataContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await this.Context.Events.ToListAsync();
                return Ok(results);
            }
            catch (System.Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await this.Context.Events.FirstOrDefaultAsync(x => x.EventId == id);
                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event model)
        {
            try
            {
                this.Context.Events.Add(model);
                await this.Context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Event model, int id)
        {
            try
            {
                var entity = await this.Context.Events.FirstOrDefaultAsync(x => x.EventId == id);

                if (entity != null)
                {
                    entity.ImageEvent = model.ImageEvent;
                    entity.NameEvent = model.NameEvent;
                    entity.AddresEvent = model.AddresEvent;
                    entity.DescriptionEvent = model.DescriptionEvent;
                    entity.TypeEvent = model.TypeEvent;

                    await this.Context.SaveChangesAsync();

                    return Ok(entity);
                }

                return Ok("Event not found");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entity = await this.Context.Events.FirstOrDefaultAsync(x => x.EventId == id);

                if (entity != null)
                {
                    var results = this.Context.Remove(entity);

                    await this.Context.SaveChangesAsync();

                    return Ok("Deleted with success");
                }

                return Ok("Event not found!");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }
    }
}
