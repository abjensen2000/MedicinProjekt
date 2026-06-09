namespace Models
{
    internal class Apotek
    {
        private int _id;
        private string _navn;

        public Apotek(string navn)
        {
            _id = 0;
            _navn = navn;
        }

        public int Id { get => _id; set => _id = value; }
        public string Navn { get => _navn; set => _navn = value; }
    }
}
