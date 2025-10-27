using System.Windows;
using C_C.Model;
using System.Windows.Controls;

namespace C_C.ViewModel
{
    
    public class BurbujaChatTemplateSelector : DataTemplateSelector
    {
        
        public DataTemplate PlantillaMia { get; set; }
        public DataTemplate PlantillaDeOtro { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            
            if (item is Mensaje mensaje)
            {
               
                return mensaje.EsEnviadoPorMi ? PlantillaMia : PlantillaDeOtro;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
