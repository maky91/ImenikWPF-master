using Imenik.Model;
using Imenik.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Imenik.Views
{
    /// <summary>
    /// Interaction logic for PrikazKontaktaView.xaml
    /// </summary>
    public partial class PrikazKontaktaView : UserControl
    {
        private PrikazKontaktaViewModel _viewModel;
       
        public PrikazKontaktaView(Kontakt izabraniKontakt, IImenikViewModel sender)
        {
            InitializeComponent();
            _viewModel = new PrikazKontaktaViewModel(izabraniKontakt, sender);
            this.DataContext = _viewModel;
        }

    }
}
