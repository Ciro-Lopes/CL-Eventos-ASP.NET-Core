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
                var results = await this.Context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);
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
            try
            {
                this.Context.Tickets.Add(model);
                await this.Context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Failed to request on database");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Ticket model, int id)
        {
            try
            {
                var entity = await this.Context.Tickets.FirstOrDefaultAsync(x => x.TicketId == id);

                if (entity != null)
                {
                    entity.PriceTicket = model.PriceTicket;
                    entity.amountTicket = model.amountTicket;

                    await this.Context.SaveChangesAsync();

                    return Ok(entity);
                }

                return Ok("Ticket not found!");
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

                if (entity != null)
                {
                    var results = this.Context.Remove(entity);
                    await this.Context.SaveChangesAsync();
                    return Ok("Deleted with success!");
                }

                return Ok("Ticket not found!");
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}