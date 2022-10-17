using Imenik.Model;
using Imenik.PomocneKlase;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Input;
using System.Diagnostics;
using System.Collections.Generic;
using Imenik.Views;
using System;

namespace Imenik.ViewModels
{

    public class ImenikViewModel : ObservableObject, IImenikViewModel
    {
        private readonly string _imeDatoteke = "Json/imenik.json";

        public ICommand UcitajKontakteKomanda { get; set; }
        public ICommand UcitajOmiljeneKomanda { get; set; }
        public ICommand DodajKomanda { get; set; }

        public ObservableCollection<Kontakt> Kontakti { get; private set; } = new ObservableCollection<Kontakt>();

        private Kontakt _izabraniKontakt;

        public Kontakt IzabraniKontakt
        {
            get { return _izabraniKontakt; }
            set
            {
                OnPropertyChanged(ref _izabraniKontakt, value);
                if (_trenutniPrikaz != null)
                {
                  
                 PrikaziDetalje();
                }
            }
        }
        private object _trenutniPrikaz;
        public object TrenutniPrikaz
        {
            get { return _trenutniPrikaz; }

            set { OnPropertyChanged(ref _trenutniPrikaz, value); }
        }

        public ImenikViewModel()
        {
            UcitajKontakteKomanda = new RelayCommand(UcitajKontakte);
            UcitajOmiljeneKomanda = new RelayCommand(UcitajOmiljene);
            DodajKomanda = new RelayCommand(DodajKontakt);
            UcitajKontakte();
            var kontakt = Kontakti.FirstOrDefault();
            IzabraniKontakt = kontakt;
            PrikaziDetalje();
        }
        // SRB - Ucitavanje kontakta 
        // ENG - Loading contacts 
        public void UcitajKontakte()
        {
            if (!File.Exists(_imeDatoteke))
            {
                File.Create(_imeDatoteke).Close();
            }
            var sKontakti = File.ReadAllText(_imeDatoteke);
            var rezKontakti = JsonSerializer.Deserialize<ObservableCollection<Kontakt>>(sKontakti);
            Kontakti.Clear();
            foreach (var kontakti in rezKontakti)
                Kontakti.Add(kontakti);

        }
        // SRB - Ucitavanje omiljenih 
        // ENG - Loading favorites
        private void UcitajOmiljene()
        {
            List<Kontakt> kontaktiTemp = Kontakti.Where(k => k.JeOmiljeni).ToList();
            for(int i = Kontakti.Count-1; i >=0; i--)
            {
                var kontakt = Kontakti[i];
                if (!kontaktiTemp.Contains(kontakt))
                {
                      Kontakti.Remove(kontakt);
                }
            }
            foreach(var kontakt in kontaktiTemp)
            {
                if (!Kontakti.Contains(kontakt))
                {
                    Kontakti.Add(kontakt);
                }
            }
        }

        // SRB - Dodavanje novog kontakta i postavljanje za izabrani
        // ENG - Adding new contact and selecting it
        private void DodajKontakt()
        {
            var noviKontakt = new Kontakt
            {
                Id = Guid.NewGuid(),
                Ime = " ",
                Prezime = " ",
                BrojTelefona = " ",
                EPosta = " ",
                Adresa = " ",
                JeOmiljeni = false,
                ImgPutanja = " "
            };
            Kontakti.Add(noviKontakt);
            IzabraniKontakt = noviKontakt;
            IzmeniKontakt();
        }

        /* SRB - Postavljanje View-a koji prikazije detalje o izabranom kontaktu
                 Poziva ga svojstvo IzabraniKontakt */

        /* ENG - Setting View which shows details abot selected contact 
                 Called by property IzabraniKontakt */

        public void PrikaziDetalje()
        {

            TrenutniPrikaz = new PrikazKontaktaView(IzabraniKontakt, this);
        }

        // SRB - Usnimavanje podataka novog ili izmenjenog kontakta
        // ENG - Saving data of new or edited contact

        public void SacuvajKontakt(Kontakt kontakt)
        {
            var sKontakti = JsonSerializer.Serialize(Kontakti);
            File.WriteAllText(_imeDatoteke, sKontakti);
            OnPropertyChanged(nameof(IzabraniKontakt));
            IzabraniKontakt = kontakt;
        }

        /*  SRB - Postavljanje View-a za izmenu podataka izabrang kontakta ili dodavanje novog 
                  Poziva se iz PrikazKontaktaView/ViewModel */

        /* ENG - Setting View for changing selected contact or adding a new one
                 Called from PrikazKontaktaView/ViewModel */
        public void IzmeniKontakt()
        {
            
            TrenutniPrikaz = new IzmenaKontaktaView(IzabraniKontakt, this);
        }

        //  SRB - Poziva se iz PrikazKontaktaView/ViewModel
        // ENG - Called from PrikazKontaktaView/ViewModel
        public void ObrisiIzabraniKontakt()
        {
            Kontakti.Remove(IzabraniKontakt);
            SacuvajKontakt(IzabraniKontakt);
            OnPropertyChanged(nameof(Kontakti));
            IzabraniKontakt = Kontakti.FirstOrDefault();

        }

        // SRB - Poziva se iz IzmenaKontaktaView/ViewModel
        // ENG - Called from IzmenaKontaktaView/ViewModel
        public string OtvoriDatoteku(string filter)
        {
            var dijalog = new OpenFileDialog();

            if (dijalog.ShowDialog() == true)
            {
                return dijalog.FileName;
            }
                return null;
        }

     
    }
}
