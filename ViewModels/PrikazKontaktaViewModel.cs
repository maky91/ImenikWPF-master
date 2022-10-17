using Imenik.Model;
using Imenik.PomocneKlase;
using System.Windows;
using System.Windows.Input;

namespace Imenik.ViewModels
{
    public class PrikazKontaktaViewModel : ObservableObject
    {
        private Kontakt _izabraniKontakt;
        public Kontakt IzabraniKontakt
        {
            get { return _izabraniKontakt; }
            set
            {
                OnPropertyChanged(ref _izabraniKontakt, value);
            }
        }

        private readonly IImenikViewModel _sender;

        public ICommand IzmeniKomanda { get; private set; }
        public ICommand OmiljeniKomanda { get; private set; }
        public ICommand ObrisiKomanda { get; private set; }


        public PrikazKontaktaViewModel(Kontakt izabraniKontakt, IImenikViewModel sender)
        {
            IzabraniKontakt = izabraniKontakt;

            _sender = sender;

            OmiljeniKomanda = new RelayCommand(Omiljeni); 
            IzmeniKomanda = new RelayCommand(Izmeni);
            ObrisiKomanda = new RelayCommand(Obrisi);
        }

        // Marking selected contact as favorite
        private void Omiljeni()
        {
            if (IzabraniKontakt == null) return;
            IzabraniKontakt.JeOmiljeni = !IzabraniKontakt.JeOmiljeni;
            _sender.SacuvajKontakt(IzabraniKontakt);
        }

        // Setting IzmenaKontaktaView
        private void Izmeni()
        {
            _sender.IzmeniKontakt();
        }

        // Delete contact
        private void Obrisi()
        {
            if (IzabraniKontakt != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    $"Da li ste sigurni da zelite obrisati kontakt {IzabraniKontakt.PunoIme}?",
                      "Potvrdite", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    _sender.ObrisiIzabraniKontakt();
                }
            }
        }
    }
}
