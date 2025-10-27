using C_C.ViewModel;
using System.Windows;

namespace C_C.View
{
    
    public partial class ChatListView : Window
    {
        
        public ChatListView()
        {
            InitializeComponent();
            
            DataContext = new ChatListViewModel();
        }
       
    }
}

