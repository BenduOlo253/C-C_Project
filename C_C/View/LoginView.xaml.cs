

using System.Windows;
using System.Windows.Controls; 

namespace C_C.View
{
   
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            
            DataContext = new ViewModel.LoginViewModel();
        }

        
        private void BoxContrasena_PasswordChanged(object sender, RoutedEventArgs e)
        {
           
            if (DataContext is ViewModel.LoginViewModel viewModel)
            {
                
                if (sender is PasswordBox box)
                {
                    
                    viewModel.Contrasena = box.Password;
                }
            }
        }
    }
}