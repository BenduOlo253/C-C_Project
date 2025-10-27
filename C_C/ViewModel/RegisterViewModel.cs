using C_C.Model;
using C_C.Repositories;
using C_C.Utils;
using C_C.View;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System;

namespace C_C.ViewModel
{
    public class RegisterViewModel : ObservableObject
    {
       
        private readonly ICuentaRepository _cuentaRepository;

        
        private Alumno _alumnoNuevo;
        private string _contrasena;
        private string _contrasenaConfirmacion;
        private DateTime? _fechaNacimiento = DateTime.Now; 

        public Alumno AlumnoNuevo
        {
            get { return _alumnoNuevo; }
            set
            {
                _alumnoNuevo = value;
                OnPropertyChanged(nameof(AlumnoNuevo));
               
                (RegistrarseCommand as RelayCommand<Window>)?.RaiseCanExecuteChanged();
            }
        }

        public string Contrasena
        {
            get { return _contrasena; }
            set
            {
                _contrasena = value;
                OnPropertyChanged(nameof(Contrasena));
               
                (RegistrarseCommand as RelayCommand<Window>)?.RaiseCanExecuteChanged();
            }
        }

        public string ContrasenaConfirmacion
        {
            get { return _contrasenaConfirmacion; }
            set
            {
                _contrasenaConfirmacion = value;
                OnPropertyChanged(nameof(ContrasenaConfirmacion));
               
                (RegistrarseCommand as RelayCommand<Window>)?.RaiseCanExecuteChanged();
            }
        }

        public DateTime? FechaNacimiento 
        {
            get { return _fechaNacimiento; }
            set
            {
                _fechaNacimiento = value;
                OnPropertyChanged(nameof(FechaNacimiento));
               
                (RegistrarseCommand as RelayCommand<Window>)?.RaiseCanExecuteChanged();
            }
        }

      
        public ICommand RegistrarseCommand { get; }
        public ICommand VolverCommand { get; }

        public RegisterViewModel()
        {
           
            _cuentaRepository = new CuentaRepository();

           
            AlumnoNuevo = new Alumno(); 

           
            RegistrarseCommand = new RelayCommand<Window>(Registrarse, CanRegistrarse); 
            VolverCommand = new RelayCommand<Window>(Volver);
        }

       
        private bool CanRegistrarse(Window window)
        {
            
            return AlumnoNuevo != null &&
                   AlumnoNuevo.Matricula != 0 &&
                   !string.IsNullOrEmpty(AlumnoNuevo.Nombre) &&
                   !string.IsNullOrEmpty(AlumnoNuevo.Carrera) && 
                   AlumnoNuevo.Genero != '\0' && 
                   FechaNacimiento.HasValue &&
                   !string.IsNullOrEmpty(AlumnoNuevo.Email) &&
                   !string.IsNullOrEmpty(Contrasena) &&
                   !string.IsNullOrEmpty(ContrasenaConfirmacion) &&
                   Contrasena == ContrasenaConfirmacion;
        }


        private void Registrarse(Window window)
        {
            

            try
            {
               
                int edad = DateTime.Now.Year - FechaNacimiento.Value.Year;
                if (FechaNacimiento.Value.Date > DateTime.Now.AddYears(-edad))
                {
                    edad--; 
                }
                AlumnoNuevo.Edad = edad;

               
                string hashContrasena = HashUtil.ComputeSha256Hash(Contrasena);

                
                _cuentaRepository.AddCuenta(AlumnoNuevo, hashContrasena);

                
                MessageBox.Show("¡Registro exitoso! Ahora puedes iniciar sesión.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                Volver(window); 
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"Error al registrar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Volver(Window window)
        {
            LoginView loginView = new LoginView();
            loginView.Show();

            
            window?.Close();
        }
    }
}

