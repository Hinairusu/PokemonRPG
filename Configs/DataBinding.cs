using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Xceed.Wpf.Toolkit;

namespace PokemonRPG.Configs
{
    internal class DataBinding
    {
        public static bool BindThis<T, U>(T Item, U Source, string PropertyName, string SecondPropertyName = null)
        {
            try
            {
                if (Item is TextBox)
                {
                    BindingOperations.SetBinding(Item as TextBox, TextBox.TextProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is TextBlock)
                {
                    BindingOperations.SetBinding(Item as TextBlock, TextBlock.TextProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is WatermarkTextBox)
                {
                    BindingOperations.SetBinding(Item as WatermarkTextBox, TextBox.TextProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is WatermarkPasswordBox)
                {
                    BindingOperations.SetBinding(Item as WatermarkPasswordBox, TextBox.TextProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is WatermarkComboBox)
                {
                    if (!string.IsNullOrWhiteSpace(PropertyName))
                        BindingOperations.SetBinding(Item as WatermarkComboBox, ComboBox.TextProperty,
                            MakeBinding(Source, PropertyName));
                    if (!string.IsNullOrWhiteSpace(SecondPropertyName))
                        BindingOperations.SetBinding(Item as WatermarkComboBox, ItemsControl.ItemsSourceProperty,
                            MakeBinding(Source, SecondPropertyName));
                }
                else if (Item is Label)
                {
                    BindingOperations.SetBinding(Item as Label, ContentControl.ContentProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is UIntegerUpDown)
                {
                    BindingOperations.SetBinding(Item as UIntegerUpDown, UIntegerUpDown.ValueProperty,
                        MakeBinding(Source, PropertyName));
                    (Item as UIntegerUpDown).Value = (Item as UIntegerUpDown).Value;
                }
                else if (Item is IntegerUpDown)
                {
                    BindingOperations.SetBinding(Item as IntegerUpDown, IntegerUpDown.ValueProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is CheckBox)
                {
                    BindingOperations.SetBinding(Item as CheckBox, ToggleButton.IsCheckedProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is RadioButton)
                {
                    BindingOperations.SetBinding(Item as RadioButton, ToggleButton.IsCheckedProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is ListView)
                {
                    BindingOperations.SetBinding(Item as ListView, ItemsControl.ItemsSourceProperty,
                        MakeBinding(Source, PropertyName));
                }
                else if (Item is ComboBox)
                {
                    if (!string.IsNullOrWhiteSpace(PropertyName))
                        BindingOperations.SetBinding(Item as ComboBox, ItemsControl.ItemsSourceProperty,
                            MakeBinding(Source, PropertyName));
                    if (!string.IsNullOrWhiteSpace(SecondPropertyName))
                        BindingOperations.SetBinding(Item as ComboBox, ComboBox.TextProperty,
                            MakeBinding(Source, SecondPropertyName));
                }
                else if (Item is ListBox)
                {
                    BindingOperations.SetBinding(Item as ListBox, ItemsControl.ItemsSourceProperty,
                            MakeBinding(Source, PropertyName));
                }
                else if (Item is DecimalUpDown)
                {
                    BindingOperations.SetBinding(Item as DecimalUpDown, DecimalUpDown.ValueProperty,
                        MakeBinding(Source, PropertyName));
                    (Item as DecimalUpDown).Increment = 0.01M;
                }
                else
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static Binding MakeBinding<T>(T source, string PropertyPath, BindingMode Bind = BindingMode.TwoWay,
            UpdateSourceTrigger Trigger = UpdateSourceTrigger.PropertyChanged)
        {
            var bind = new Binding
            {
                Source = source,
                Path = new PropertyPath(PropertyPath),
                Mode = Bind,
                UpdateSourceTrigger = Trigger
            };

            return bind;
        }
    }
}