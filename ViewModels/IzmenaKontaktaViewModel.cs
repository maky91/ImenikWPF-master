using Imenik.Model;
using Imenik.PomocneKlase;
using System.Windows.Input;

namespace Imenik.ViewModels
{
    public class IzmenaKontaktaViewModel : ObservableObject
    {
        private Kontakt _izabraniKontakt;
        public Kontakt IzabraniKontakt
        {
            get { return _izabraniKontakt; }
            set { OnPropertyChanged(ref _izabraniKontakt, value);
                   }
        }

        private readonly IImenikViewModel _sender;

        public ICommand OmiljeniKomanda { get; private set; }
        public ICommand ImgKomanda { get; private set; }
        public ICommand SacuvajKomanda { get; private set; }
        
       
        public object TrenutniPrikaz { get; private set; }

        public IzmenaKontaktaViewModel(Kontakt kontaktZaIzmenu, IImenikViewModel sender)
        {
            IzabraniKontakt = kontaktZaIzmenu;
            _sender = sender;

            OmiljeniKomanda = new RelayCommand(Omiljeni);
            ImgKomanda = new RelayCommand(PretraziSliku); // ove komande su povezane sa sa korisnickim interfejsom
            SacuvajKomanda = new RelayCommand(Sacuvaj);
        }

        // Marking contact as favorite
        private void Omiljeni()
        {
            IzabraniKontakt.JeOmiljeni = !IzabraniKontakt.JeOmiljeni;
        }

        // Adding contact picture
        private void PretraziSliku()
        {
            var putanjaDatoteke = _sender.OtvoriDatoteku("Image files|*.bmp;*.jpg;*.jpeg;*.png|All files");
            IzabraniKontakt.ImgPutanja = putanjaDatoteke;
        }

        // Saving contact data
        private void Sacuvaj()
        {
            _sender.SacuvajKontakt(IzabraniKontakt);
        }
    }
}