using System.Windows;
using C_C.ViewModel; 

namespace C_C.View
{
    
    public partial class MiPerfilView : Window
    {
        public MiPerfilView()
        {
            InitializeComponent();

           
            DataContext = new MiPerfilViewModel();
        }

}

