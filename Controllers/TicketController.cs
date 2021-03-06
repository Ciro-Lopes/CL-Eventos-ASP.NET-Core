using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _net_core_api.model;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("Ticket")]
    public class TicketController : ControllerBase
    {
        public DataContext Context { get; }

        public TicketController(DataContext context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await this.Context.Tickets.ToListAsync();

                if (results.Count == 0)
                {
                    return Ok("Tickets not found!");
                }

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ticket model)
        {
            var eventValidation = await this.Context.Events.FirstOrDefaultAsync(e => e.EventId == model.EventId);

            if (eventValidation == null)
            {
                return Ok("Event not found!");
            }

            if (model.PriceTicket < 0 || model.amountTicket < 0)
            {
                return Ok("the data was not filled in correctly");
            }

            try
            {
                this.Context.Tickets.Add(model);
                await this.Context.SaveChangesAsync();
                return Ok("Ticket created witch success!");
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
                var results = await this.Context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

                if (results == null)
                {
                    return Ok("Ticket not found!");
                }

                return Ok(results);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Ticket model, int id)
        {
            if (model.PriceTicket < 0 || model.amountTicket < 0)
            {
                return Ok("the data was not filled in correctly");
            }

            try
            {
                var entity = await this.Context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

                if (entity == null)
                {
                    return Ok("Ticket not found!");
                }

                entity.PriceTicket = model.PriceTicket;
                entity.amountTicket = model.amountTicket;

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
                var entity = await this.Context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

                if (entity == null)
                {
                    return Ok("Ticket not found!");
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

        [HttpPut("Purchase/{id}")]
        public async Task<IActionResult> GetEventTicket(int id)
        {
            try
            {
                var buyTicket = await this.Context.Tickets.FirstOrDefaultAsync(t => t.EventId == id);

                if (buyTicket == null)
                {
                    return Ok("Ticket not found!");
                }

                if (buyTicket.amountTicket > 0)
                {
                    buyTicket.amountTicket = buyTicket.amountTicket - 1;
                    await this.Context.SaveChangesAsync();
                    return Ok("successful purchase!");
                }

                return Ok("Tickets Sold Out");
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }
    }
}