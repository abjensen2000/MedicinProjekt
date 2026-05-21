using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Lægehus
    {
        private int _id;
        private string _ydernummer;
        private string _navn;

        public Lægehus(string ydernummer, string navn)
        {
            _id = 0;
            _ydernummer = ydernummer;
            _navn = navn;
        }

        public int Id { get => _id; set => _id = value; }
        public string Ydernummer { get => _ydernummer; set => _ydernummer = value; }
        public string Navn { get => _navn; set => _navn = value; }
    }
}
