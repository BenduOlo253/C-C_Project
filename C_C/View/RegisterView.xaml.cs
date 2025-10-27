

using System.Windows;
using System.Windows.Controls; 

namespace C_C.View
{
    
    public partial class RegisterView : Window
    {
        public RegisterView()
        {
            InitializeComponent();
            
            DataContext = new ViewModel.RegisterViewModel();
        }

       
        private void BoxContrasena1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel.RegisterViewModel viewModel && sender is PasswordBox box)
            {
                viewModel.Contrasena = box.Password;
            }
        }

        private void BoxContrasena2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel.RegisterViewModel viewModel && sender is PasswordBox box)
            {
                viewModel.ContrasenaConfirmacion = box.Password;
            }
        }
    }
}