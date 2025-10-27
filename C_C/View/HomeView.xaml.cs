using C_C.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace C_C.View
{
    public partial class HomeView : Window
    {
        public HomeView()
        {
            InitializeComponent();

            
            DataContext = new HomeViewModel();

            
            this.MouseLeftButtonDown += MoverVentana;
        }

       

        private void MoverVentana(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void BotonCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("HideMenu") as Storyboard;
            sb.Begin();
        }

        private void Overlay_Click(object sender, MouseButtonEventArgs e)
        {
            Storyboard sb = FindResource("HideMenu") as Storyboard;
            sb.Begin();
        }

        private void BotonMostrarMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuLateral.Visibility = Visibility.Visible;
            Overlay.Visibility = Visibility.Visible;
            Storyboard sb = FindResource("ShowMenu") as Storyboard;
            sb.Begin();
        }

        private void AnimacionOcultarMenu_Completed(object sender, EventArgs e)
        {
            MenuLateral.Visibility = Visibility.Collapsed;
            Overlay.Visibility = Visibility.Collapsed;
        }

        
    }
}

