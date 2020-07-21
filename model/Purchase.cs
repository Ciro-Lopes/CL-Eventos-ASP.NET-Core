namespace model
{
    public class Purchase
    {
        public string NameEvent { get; set; }
        public float PriceTicket { get; set; }
        public int amountTicket { get; set; }

        public Purchase(string nameEvent, float priceTicket, int amountTicket)
        {
            this.NameEvent = nameEvent;
            this.PriceTicket = priceTicket;
            this.amountTicket = amountTicket;
        }
    }
}