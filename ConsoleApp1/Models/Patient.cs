using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    internal class Patient
    {
        private int _id;
        private string _cpr;
        private string _navn;

        public Patient(string cpr, string navn)
        {
            _id = 0;
            _cpr = cpr;
            _navn = navn;
        }

        public int Id { get => _id; set => _id = value; }
        public string Cpr { get => _cpr; set => _cpr = value; }
        public string Navn { get => _navn; set => _navn = value; }
    }
}
