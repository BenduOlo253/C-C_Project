using System;
using System.Windows;
using System.Windows.Controls;


namespace C_C.Utils
{
    public static class PasswordBoxHelper
    {
        private static bool _isUpdating; 

        
        public static readonly DependencyProperty BoundPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BoundPassword",
                typeof(string),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static string GetBoundPassword(DependencyObject d)
        {
            return (string)d.GetValue(BoundPasswordProperty);
        }

        public static void SetBoundPassword(DependencyObject d, string value)
        {
            d.SetValue(BoundPasswordProperty, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBox box)
            {
                
                box.PasswordChanged -= HandlePasswordChanged;

                if (!_isUpdating && box.Password != (string)e.NewValue)
                {
                    box.Password = (string)e.NewValue ?? string.Empty;
                }

                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        
        public static readonly DependencyProperty BindPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BindPassword",
                typeof(bool),
                typeof(PasswordBoxHelper),
                new PropertyMetadata(false, OnBindPasswordChanged));

        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPasswordProperty);
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPasswordProperty, value);
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            if (dp is PasswordBox box)
            {
                if ((bool)e.NewValue)
                    box.PasswordChanged += HandlePasswordChanged;
                else
                    box.PasswordChanged -= HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox box)
            {
                _isUpdating = true;
                SetBoundPassword(box, box.Password);
                _isUpdating = false;
            }
        }
    }
}