using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _net_core_api.model;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace _net_core_api.Controllers
{
    [ApiController]
    [Route("Event")]
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

                if (results.Count == 0)
                {
                    return Ok("Events not found!");
                }

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
            if (model.ImageEvent == "" || model.NameEvent == "" || model.AddresEvent == "" ||
               model.DescriptionEvent == "" || model.TypeEvent != "Empresa" && model.TypeEvent != "Universidade")
            {
                return Ok("the data was not filled in correctly");
            }

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var results = await this.Context.Events.FirstOrDefaultAsync(x => x.EventId == id);

                if (results == null)
                {
                    return Ok("Event not found!");
                }

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Event model, int id)
        {
            if (model.ImageEvent == "" || model.NameEvent == "" || model.AddresEvent == "" ||
               model.DescriptionEvent == "" || model.TypeEvent != "Empresa" && model.TypeEvent != "Universidade")
            {
                return Ok("the data was not filled in correctly");
            }

            try
            {
                var entity = await this.Context.Events.FirstOrDefaultAsync(x => x.EventId == id);

                if (entity == null)
                {
                    return Ok("Event not found!");
                }

                entity.ImageEvent = model.ImageEvent;
                entity.NameEvent = model.NameEvent;
                entity.AddresEvent = model.AddresEvent;
                entity.DescriptionEvent = model.DescriptionEvent;
                entity.TypeEvent = model.TypeEvent;

                await this.Context.SaveChangesAsync();

                return Ok(entity);
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

                if (entity == null)
                {
                    return Ok("Event not found!");
                }

                var results = this.Context.Remove(entity);
                await this.Context.SaveChangesAsync();
                return Ok(entity);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }
    }
}
