namespace LAB13_TINOCO_DAEA.Models.Response
{
    public class DetailResponseV1
    {
        public int DetailID { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float SubTotal { get; set; }

        public Customer Customer { get; set; }
        public Invoice Invoice { get; set; }
    }
}
