using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UniCredit.FX.Common.UI.Behaviours
{
    public class LoadBehaviour
    {
        public static DependencyProperty CommandProperty =
          DependencyProperty.RegisterAttached("Command",
          typeof(ICommand),
          typeof(LoadBehaviour),
          new FrameworkPropertyMetadata(CommandChanged));

        public static DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter",
                                                typeof(object),
                                                typeof(LoadBehaviour),
                                                new FrameworkPropertyMetadata(null));

        public static void SetCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(CommandProperty, value);
        }

        public static void SetCommandParameter(DependencyObject target, object value)
        {
            target.SetValue(CommandParameterProperty, value);
        }
        public static object GetCommandParameter(DependencyObject target)
        {
            return target.GetValue(CommandParameterProperty);
        }

        private static void CommandChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var control = target as Control;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.Loaded += OnLoad;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.Loaded -= OnLoad;
                }
            }
        }

        private static void OnLoad(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            var command = (ICommand)control.GetValue(CommandProperty);
            object commandParameter = control.GetValue(CommandParameterProperty);
            command.Execute(commandParameter);
        }
        
    }
}
