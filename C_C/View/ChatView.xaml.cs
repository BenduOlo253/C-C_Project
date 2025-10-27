

using System.Windows;
using C_C.ViewModel; 

namespace C_C.View
{
    
    public partial class ChatView : Window
    {
       
        public ChatView()
        {
            InitializeComponent();
        }

      
        public ChatView(int idOtroPerfil)
        {
            InitializeComponent();

            
            DataContext = new ChatViewModel(idOtroPerfil);
        }
    }
}