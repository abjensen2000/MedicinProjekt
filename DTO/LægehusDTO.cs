using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class LægehusDTO
    {
        private int _id;
        private string _ydernummer;
        private string _navn;
        private List<int> _patienterCPR;

        public LægehusDTO(string ydernummer, string navn)
        {
            _id = 0;
            _ydernummer = ydernummer;
            _navn = navn;
            _patienterCPR = new List<int>();
        }

        public int Id { get => _id; set => _id = value; }
        public string Ydernummer { get => _ydernummer; set => _ydernummer = value; }
        public string Navn { get => _navn; set => _navn = value; }
        public List<int> PatientCPR { get => _patienterCPR; set => _patienterCPR = value; }
    }
}
