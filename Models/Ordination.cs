using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Ordination
    {
        private int _id;
        private string _lægemiddel;
        private string _dosis;
        private int _antalUdleveringer;
        private int _antalForetagneUdleveringer;

        public Ordination(string lægemiddel, string dosis, int antalUdleveringer)
        {
            _id = 0;
            _lægemiddel = lægemiddel;
            _dosis = dosis;
            _antalUdleveringer = antalUdleveringer;
            _antalForetagneUdleveringer = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public string Lægemiddel { get => _lægemiddel; set => _lægemiddel = value; }
        public string Dosis { get => _dosis; set => _dosis = value; }
        public int AntalUdleveringer { get => _antalUdleveringer; set => _antalUdleveringer = value; }
        public int AntalForetagneUdleveringer { get => _antalForetagneUdleveringer; set => _antalForetagneUdleveringer = value; }
    }
}
