using C_C.Model;
using C_C.View; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.Linq; 
using System.Windows;
using System.Windows.Input;

namespace C_C.ViewModel
{
    public class HomeViewModel : ObservableObject
    {
        
        private List<Perfil> _perfilesSugeridos;
        private int _indicePerfilActual = -1; 
        private Perfil _perfilActual;
        public Perfil PerfilActual
        {
            get { return _perfilActual; }
            set
            {
                _perfilActual = value;
                OnPropertyChanged(nameof(PerfilActual));
                
                IndiceFotoActual = 0;
                
                (SiguienteFotoCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (AnteriorFotoCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

       
        private int _indiceFotoActual = 0;
        public int IndiceFotoActual
        {
            get { return _indiceFotoActual; }
            set
            {
                if (_perfilActual != null && value >= 0 && value < _perfilActual.Fotos.Count)
                {
                    _indiceFotoActual = value;
                    OnPropertyChanged(nameof(IndiceFotoActual));
                    OnPropertyChanged(nameof(FotoActual)); 
                    
                    (SiguienteFotoCommand as RelayCommand)?.RaiseCanExecuteChanged();
                    (AnteriorFotoCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        
        public string FotoActual => _perfilActual?.Fotos?.ElementAtOrDefault(IndiceFotoActual);

        
        public ICommand LikeCommand { get; }
        public ICommand DislikeCommand { get; }
        public ICommand SiguienteFotoCommand { get; }
        public ICommand AnteriorFotoCommand { get; }
        public ICommand VerMiPerfilCommand { get; } 
        public ICommand IrAMiPerfilCommand { get; } 
        public ICommand IrAChatsCommand { get; }
        public ICommand IrAConfiguracionCommand { get; }
        public ICommand CerrarSesionCommand { get; }


        public HomeViewModel()
        {
            
            CargarPerfiles();
            CargarSiguientePerfil(); 
           
            LikeCommand = new RelayCommand(Like, CanLikeDislike);
            DislikeCommand = new RelayCommand(Dislike, CanLikeDislike);
            SiguienteFotoCommand = new RelayCommand(SiguienteFoto, CanSiguienteFoto);
            AnteriorFotoCommand = new RelayCommand(AnteriorFoto, CanAnteriorFoto);
            VerMiPerfilCommand = new RelayCommand<Window>(IrAMiPerfil);
            IrAMiPerfilCommand = new RelayCommand<Window>(IrAMiPerfil);
            IrAChatsCommand = new RelayCommand<Window>(IrAChats);
            IrAConfiguracionCommand = new RelayCommand<Window>(IrAConfiguracion);
            CerrarSesionCommand = new RelayCommand<Window>(CerrarSesion);
        }

        private void CargarPerfiles()
        {
           
            _perfilesSugeridos = new List<Perfil>
            {
                new Perfil { IdPerfil = 1, Matricula = 20001, Nombre = "Hipo Horrendo Abadejo III", Edad = 24, Genero = 'M', Carrera = "Entrenador de Dragones", Email="hiccup@berk.com", NikName="Hipo", Biografia="Jefe de Berk, amigo de Chimuelo.", Fotos = new List<string> { "pack://application:,,,/Resources/Images/hiccup1.jpg", "pack://application:,,,/Resources/Images/hiccup2.jpg" } },
                new Perfil { IdPerfil = 2, Matricula = 20002, Nombre = "Andrew Garfield", Edad = 22, Genero = 'M', Carrera = "Fotografía / Vigilante", Email="spidey@dailybugle.com", NikName="Peter Parker?", Biografia="Solo un chico amigable del vecindario.", Fotos = new List<string> { "pack://application:,,,/Resources/Images/andrew1.jpg", "pack://application:,,,/Resources/Images/andrew2.jpg" } },
                new Perfil { IdPerfil = 3, Matricula = 20003, Nombre = "Astrid Hofferson", Edad = 23, Genero = 'F', Carrera = "Guerrera Vikinga", Email="astrid@berk.com", NikName="Astrid", Biografia="La mejor guerrera de Berk. Jinete de Tormenta.", Fotos = new List<string> { "pack://application:,,,/Resources/Images/astrid1.jpg" } },
                
            };
            _indicePerfilActual = -1; 
        }

        private void CargarSiguientePerfil()
        {
            _indicePerfilActual++;
            if (_indicePerfilActual < _perfilesSugeridos.Count)
            {
                PerfilActual = _perfilesSugeridos[_indicePerfilActual];
            }
            else
            {
                PerfilActual = null; 
                MessageBox.Show("¡Ya viste todos los perfiles por hoy!", "Fin de la lista", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
             (LikeCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DislikeCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        
        private bool CanLikeDislike() => PerfilActual != null; 

        private void Like()
        {
            if (!CanLikeDislike()) return;

           
            bool esMatch = new Random().Next(0, 3) == 0; 
            if (esMatch)
            {
                MessageBox.Show($"¡Es un Match con {PerfilActual.Nombre}!", "¡Match!", MessageBoxButton.OK, MessageBoxImage.Information);
                
            }

            
            CargarSiguientePerfil();
        }

        private void Dislike()
        {
            if (!CanLikeDislike()) return;
            
            CargarSiguientePerfil();
        }

        
        private bool CanSiguienteFoto() => PerfilActual != null && IndiceFotoActual < PerfilActual.Fotos.Count - 1;
        private void SiguienteFoto()
        {
            if (CanSiguienteFoto()) IndiceFotoActual++;
        }

        private bool CanAnteriorFoto() => PerfilActual != null && IndiceFotoActual > 0;
        private void AnteriorFoto()
        {
            if (CanAnteriorFoto()) IndiceFotoActual--;
        }


        
        private void IrAMiPerfil(Window window)
        {
            MiPerfilView perfilView = new MiPerfilView();
            perfilView.Show();
            window?.Close();
        }

        private void IrAChats(Window window)
        {
            ChatListView chatListView = new ChatListView();
            chatListView.Show();
            window?.Close();
        }

        private void IrAConfiguracion(Window window)
        {
            ConfiguracionView configView = new ConfiguracionView();
            configView.Show();
            window?.Close();
        }

        private void CerrarSesion(Window window)
        {
            
            LoginView loginView = new LoginView();
            loginView.Show();
            window?.Close();
        }
    }
}

