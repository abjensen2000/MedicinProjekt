namespace DTO
{
    public class ApotekDTO
    {
        private int _id;
        private string _navn;

        public ApotekDTO(string navn)
        {
            _id = 0;
            _navn = navn;
        }

        public ApotekDTO() { 
        
        }

        public int Id { get => _id; set => _id = value; }
        public string Navn { get => _navn; set => _navn = value; }
    }
}

