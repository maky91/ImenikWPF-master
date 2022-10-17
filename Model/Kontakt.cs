using Imenik.PomocneKlase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imenik.Model
{
    public class Kontakt : ObservableObject
    {
        public Guid Id { get; set; }    
        
        // First Name
        private string _ime;
        public string Ime
        {
            get { return _ime; }
            set { OnPropertyChanged(ref _ime, value); }
        }

        // Last Name
        private string _prezime;
        public string Prezime
        {
            get { return _prezime; }
            set { OnPropertyChanged(ref _prezime, value); }
        }

        // Full Name
        public string PunoIme
        {
            get { return $"{Ime} {Prezime}"; }
        }

        // Phone number
        private string _brojTelefona;
        public string BrojTelefona
        {
            get { return _brojTelefona; }
            set { OnPropertyChanged(ref _brojTelefona, value); }
        }

        // Email
        private string _eposta;
        public string EPosta
        {
            get { return _eposta; }
            set { OnPropertyChanged(ref _eposta, value); }
        }

        // Adress
        private string _adresa;
        public string Adresa
        {
            get { return _adresa; }
            set { OnPropertyChanged(ref _adresa, value); }
        }

        // isFavorite
        private bool _jeOmiljeni;
        public bool JeOmiljeni
        {
            get { return _jeOmiljeni; }
            set { OnPropertyChanged(ref _jeOmiljeni, value); }
        }

        // Img path
        private string _imgPutanja;
        public string ImgPutanja
        {
            get { return _imgPutanja; }
            set { OnPropertyChanged(ref _imgPutanja, value); }
        }
    }
}
