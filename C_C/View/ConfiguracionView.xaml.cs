using System.Windows;

namespace C_C.View
{
    
    public partial class ConfiguracionView : Window
    {
        public ConfiguracionView()
        {
            InitializeComponent();
        }

        private void BotonVolver_Click(object sender, RoutedEventArgs e)
        {
            HomeView home = new HomeView();
            home.Show();
            this.Close();
        }
    }
}
