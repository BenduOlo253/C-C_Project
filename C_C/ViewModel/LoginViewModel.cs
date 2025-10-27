using C_C.Model;
using C_C.View;
using System.Windows;
using System.Windows.Input;


using C_C.Services;


namespace C_C.ViewModel
{
    public class LoginViewModel : ObservableObject
    {

        private int _matricula;
        private string _contrasena;

        public int Matricula
        {
            get { return _matricula; }
            set
            {
                _matricula = value;
                OnPropertyChanged(nameof(Matricula));
            }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            set
            {
                _contrasena = value;
                OnPropertyChanged(nameof(Contrasena));
            }
        }


        public ICommand LoginCommand { get; }
        public ICommand IrARegistroCommand { get; }

        public LoginViewModel()
        {

            LoginCommand = new RelayCommand<Window>(Login);
            IrARegistroCommand = new RelayCommand<Window>(IrARegistro);
        }

       
        private void Login(Window window)
        {
            if (Matricula == 0 || string.IsNullOrEmpty(Contrasena))
            {
                MessageBox.Show("Por favor, ingresa tu matrícula y contraseña.", "Campos vacíos", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


          
            bool esValido = (Matricula == 12345 && Contrasena == "password");

            

            if (esValido)
            {
                
                UserSessionService.Instancia.Login(99);
                


                

                HomeView homeView = new HomeView();
                homeView.Show();
                window?.Close(); 
            }
            else
            {
               
                MessageBox.Show("Matrícula o contraseña incorrecta (Prueba: 12345 / password).", "Error de inicio de sesión", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void IrARegistro(Window window)
        {
            RegisterView registerView = new RegisterView();
            registerView.Show();
            window?.Close(); 
        }
    }
}