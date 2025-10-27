

using C_C.Model;
using C_C.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System;
using C_C.Repositories;
using C_C.Services;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace C_C.ViewModel
{
    public class ChatListViewModel : ObservableObject
    {
        private readonly IChatRepository _chatRepository;
        private readonly UserSessionService _sessionService;

        
        private ObservableCollection<ChatPreview> _chatList;
        public ObservableCollection<ChatPreview> ChatList
        {
            get { return _chatList; }
            set
            {
                _chatList = value;
                OnPropertyChanged(nameof(ChatList)); 
            }
        }

        private bool _estaCargando;
        public bool EstaCargando
        {
            get { return _estaCargando; }
            set { _estaCargando = value; OnPropertyChanged(nameof(EstaCargando)); }
        }

        public ICommand VolverCommand { get; }
        public ICommand AbrirChatCommand { get; }

        public ChatListViewModel()
        {
            _chatRepository = new ChatRepository();
            _sessionService = UserSessionService.Instancia;

            CargarDatosInicialesAsync();

            VolverCommand = new RelayCommand<Window>(VolverAHome);
            AbrirChatCommand = new RelayCommand<ChatPreview>(AbrirChat);
        }

        private async void CargarDatosInicialesAsync()
        {
            EstaCargando = true;
            await CargarChatsRealesAsync();
            EstaCargando = false;
        }

        private async Task CargarChatsRealesAsync()
        {
            try
            {
                int miIdPerfil = _sessionService.IdPerfilLogueado.GetValueOrDefault(0);

                if (miIdPerfil == 0)
                {
                    MessageBox.Show("Error: No se pudo obtener la sesión del usuario. (Usando ID 99 de prueba)", "Error de Sesión", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                    miIdPerfil = 99;
                }

                List<ChatPreview> chats = await _chatRepository.ObtenerChatsAsync(miIdPerfil);

                
                ChatList = new ObservableCollection<ChatPreview>(chats);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los chats: {ex.Message}", "Error de Base de Datos", MessageBoxButton.OK, MessageBoxImage.Error);
                ChatList = new ObservableCollection<ChatPreview>();
            }
        }

        private void AbrirChat(ChatPreview chatSeleccionado)
        {
            if (chatSeleccionado == null) return;

            try
            {
                
                ChatView chatView = new ChatView(chatSeleccionado.IdOtroPerfil);
                chatView.Show();
            }
            catch (MissingMethodException)
            {
                MessageBox.Show($"Error: La ventana ChatView no tiene el constructor correcto.", "Error de Navegación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el chat: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VolverAHome(Window window)
        {
            HomeView homeView = new HomeView();
            homeView.Show();
            window?.Close();
        }
    }
}