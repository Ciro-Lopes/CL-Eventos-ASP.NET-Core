using System.Collections.Generic;

namespace _net_core_api.model
{
    public class Event
    {
        public int EventId { get; set; }
        public string ImageEvent { get; set; }
        public string NameEvent { get; set; }
        public string AddresEvent { get; set; }
        public string DescriptionEvent { get; set; }
        public string TypeEvent { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}