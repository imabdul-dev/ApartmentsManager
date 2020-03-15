namespace ApartmentsManager.Models
{
    public class SampleApartment
    {
        public int UnitRef { get; set; }
        public char Block { get; set; }
        public double Plot { get; set; }
        public int Floor { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public int Apartment { get; set; }
        public int SqM { get; set; }
        public int SqFt { get; set; }
        public Status Status { get; set; }
        public int Price { get; set; }

        public override string ToString()
        {
            return $"{Floor} {Plot}";
        }

        public string ShortDescription => $"Appartment ID: {UnitRef}";
    }
}
