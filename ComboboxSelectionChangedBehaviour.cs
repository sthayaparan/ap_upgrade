using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Editors;

namespace UniCredit.FX.Common.UI.Behaviours
{
    public class ComboboxSelectionChangedBehaviour
    {
        public static DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
                                                                                               typeof(ICommand),
                                                                                               typeof(ComboboxSelectionChangedBehaviour),
                                                                                               new FrameworkPropertyMetadata(CommandChanged));

        public static DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter",
                                                                                                        typeof(object),
                                                                                                        typeof(ComboboxSelectionChangedBehaviour),
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
            var control = target as ComboBoxEdit;
            if (control != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    control.SelectedIndexChanged += OnSelectionChanged;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    control.SelectedIndexChanged -= OnSelectionChanged;
                }
            }
        }

        private static void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            var control = sender as FrameworkElement;
            if (control == null) return;
            var command = (ICommand)control.GetValue(CommandProperty);
            object commandParameter = control.GetValue(CommandParameterProperty);
            command.Execute(commandParameter);
        }
    }
}
