namespace _net_core_api.model
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public float PriceTicket { get; set; }
        public int amountTicket { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}