using C_C.Model;
using C_C.View; 
using System.Windows;
using System.Windows.Input;
using System; 
using System.Collections.Generic; 

namespace C_C.ViewModel
{
    public class MiPerfilViewModel : ObservableObject
    {
        
        private Perfil _perfilActual;
        public Perfil PerfilActual
        {
            get { return _perfilActual; }
            set
            {
                _perfilActual = value;
                OnPropertyChanged(nameof(PerfilActual));
                (GuardarCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        
        public ICommand VolverCommand { get; }
        public ICommand GuardarCommand { get; }

        public MiPerfilViewModel()
        {
            
            CargarDatosPerfil(); 

            VolverCommand = new RelayCommand<Window>(VolverAHome);
            GuardarCommand = new RelayCommand(GuardarCambiosSimulado, CanGuardarCambios);
        }

        private void CargarDatosPerfil()
        {
            
            PerfilActual = new Perfil
            {
                IdPerfil = 99, 
                Matricula = 12345,
                Nombre = "Usuario de Prueba",
                Edad = 21,
                Genero = 'P', 
                Carrera = "Ingeniería de Software",
                Email = "test@uaz.edu.mx",
                NikName = "Tester",
                Biografia = "Esta es mi biografía de prueba. ¡Puedo editarla!",
                Fotos = new List<string> { "pack://application:,,,/Resources/Images/placeholder_profile.png" } 
            };
        }

       
        private bool CanGuardarCambios() => PerfilActual != null;

        private void GuardarCambiosSimulado()
        {
            if (!CanGuardarCambios()) return;

            
            MessageBox.Show("Perfil actualizado correctamente (Simulado). Los cambios se perderán al cerrar.", "Éxito (Simulado)", MessageBoxButton.OK, MessageBoxImage.Information);
           
        }


        
        private void VolverAHome(Window window)
        {
            HomeView homeView = new HomeView();
            homeView.Show();
            window?.Close();
        }
    }
}

