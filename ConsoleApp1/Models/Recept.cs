using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    internal class Recept
    {
        private int _id;
        private string _ydernummer;
        private string _cpr;
        private List<int> _ordinationerId;
        private bool _lukket;

        public Recept(string ydernummer, string cpr)
        {
            _id = 0;
            _ydernummer = ydernummer;
            _cpr = cpr;
            _ordinationerId = new List<int>();
            _lukket = false;
        }

        public int Id { get => _id; set => _id = value; }
        public string Ydernummer { get => _ydernummer; set => _ydernummer = value; }
        public string Cpr { get => _cpr; set => _cpr = value; }
        public List<int> OrdinationerId { get => _ordinationerId; set => _ordinationerId = value; }
        public bool Lukket { get => _lukket; set => _lukket = value; }
    }
}
