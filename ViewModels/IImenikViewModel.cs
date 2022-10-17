using Imenik.Model;

namespace Imenik.ViewModels
{
    public interface IImenikViewModel
    {
       // Save contact
        void SacuvajKontakt(Kontakt kontakt);

        // Open File
        string OtvoriDatoteku(string filter);

        // Edit contact
        void IzmeniKontakt();

        // Delete selected contact
        void ObrisiIzabraniKontakt();

      
    }
}
