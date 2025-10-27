

using C_C.Model;
using C_C.View;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System;
using C_C.Services; 

namespace C_C.ViewModel
{
    public class ChatViewModel : ObservableObject
    {
       
        private readonly UserSessionService _sessionService;
        private int _miIdPerfil;
        private int _idOtroPerfil;

        private ObservableCollection<Mensaje> _listaMensajes;
        public ObservableCollection<Mensaje> ListaMensajes
        {
            get { return _listaMensajes; }
            set { _listaMensajes = value; OnPropertyChanged(nameof(ListaMensajes)); }
        }


        private string _nuevoMensajeTexto;
        public string NuevoMensajeTexto
        {
            get { return _nuevoMensajeTexto; }
            set
            {
                _nuevoMensajeTexto = value;
                OnPropertyChanged(nameof(NuevoMensajeTexto));
                (EnviarMensajeCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }


        private string _nombreOtroUsuario;
        public string NombreOtroUsuario
        {
            get { return _nombreOtroUsuario; }
            set { _nombreOtroUsuario = value; OnPropertyChanged(nameof(NombreOtroUsuario)); }
        }

       
        private string _fotoOtroUsuario;
        public string FotoOtroUsuario
        {
            get { return _fotoOtroUsuario; }
            set { _fotoOtroUsuario = value; OnPropertyChanged(nameof(FotoOtroUsuario)); }
        }

        public ICommand EnviarMensajeCommand { get; }
        public ICommand VolverCommand { get; }


        public ChatViewModel(int idOtroPerfil)
        {
            _sessionService = UserSessionService.Instancia;
            _miIdPerfil = _sessionService.IdPerfilLogueado.GetValueOrDefault(99); 
            _idOtroPerfil = idOtroPerfil;

            
            CargarMensajesQuemados();

            EnviarMensajeCommand = new RelayCommand(EnviarMensaje, CanEnviarMensaje);
            VolverCommand = new RelayCommand<Window>(VolverAListaChats);
        }

       
        private void CargarMensajesQuemados()
        {
            
            switch (_idOtroPerfil)
            {
                case 10: 
                    NombreOtroUsuario = "Armin Arlert";
                    FotoOtroUsuario = "https://placehold.co/100x100/A0A/FFF?text=Armin";
                    ListaMensajes = new ObservableCollection<Mensaje>
                     {
                         new Mensaje { IdMensaje = 1, IdChat = 1, IdPerfilRemitente = 10, Texto = "¡Hola! ¿Listo para el plan?", Fecha = DateTime.Now.AddMinutes(-5) },
                         new Mensaje { IdMensaje = 2, IdChat = 1, IdPerfilRemitente = _miIdPerfil, Texto = "¡Nací listo! ¿Dónde nos vemos?", Fecha = DateTime.Now.AddMinutes(-4) },
                         new Mensaje { IdMensaje = 3, IdChat = 1, IdPerfilRemitente = 10, Texto = "En el muro María, cerca de la brecha.", Fecha = DateTime.Now.AddMinutes(-3) }
                     };
                    break;
                case 11: 
                    NombreOtroUsuario = "Eren Jaeger";
                    FotoOtroUsuario = "https://placehold.co/100x100/F0A/FFF?text=Eren";
                    ListaMensajes = new ObservableCollection<Mensaje>
                     {
                         new Mensaje { IdMensaje = 4, IdChat = 2, IdPerfilRemitente = _miIdPerfil, Texto = "¿Todo bien, Eren?", Fecha = DateTime.Now.AddMinutes(-10) },
                         new Mensaje { IdMensaje = 5, IdChat = 2, IdPerfilRemitente = 11, Texto = "TATAKAE!!!!", Fecha = DateTime.Now.AddMinutes(-9) }
                     };
                    break;
                default:
                    NombreOtroUsuario = "Chat Desconocido";
                    FotoOtroUsuario = "https://placehold.co/100x100/CCC/FFF?text=?";
                    ListaMensajes = new ObservableCollection<Mensaje>
                    {
                         new Mensaje { IdMensaje = 6, IdChat = 3, IdPerfilRemitente = _idOtroPerfil, Texto = "Este es un chat de prueba por defecto.", Fecha = DateTime.Now.AddHours(-2) },
                    };
                    break;
            }
        }

        private bool CanEnviarMensaje() => !string.IsNullOrWhiteSpace(NuevoMensajeTexto);

        private void EnviarMensaje()
        {
            if (!CanEnviarMensaje()) return;

            var nuevoMsg = new Mensaje
            {
                IdMensaje = ListaMensajes.Count + 100,
                IdChat = 1, 
                IdPerfilRemitente = _miIdPerfil,
                Texto = NuevoMensajeTexto,
                Fecha = DateTime.Now,
                ConfirmacionLectura = false
            };

            ListaMensajes.Add(nuevoMsg);
            NuevoMensajeTexto = "";
        }


        private void VolverAListaChats(Window window)
        {
           
            window?.Close();
        }
    }
}