using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UI.ValueConverters
{
    /// <summary>
    /// Converts a <see cref="bool"/> to a <see cref="Visibility"/> (eaither Visisble if true, or Collapsed, if false).
    /// If the <see cref="ControlBoolToVisibilityConverterParameter"/> is passed in and set to InverseBool
    /// it will return Visisble if false and Collapsed if true
    /// </summary>
    public class ControlBoolToVisibilityConverter : BaseValueConverter<ControlBoolToVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool inputValue = (bool)value;

            // if the parameter has been set to ControlBoolToVisibilityConverterParameter.InverseBool
            // we want to swop the inputValue around.
            if (parameter != null && parameter is ControlBoolToVisibilityConverterParameter)
                if (((ControlBoolToVisibilityConverterParameter)parameter) == ControlBoolToVisibilityConverterParameter.InverseBool)
                    inputValue = !inputValue;


            if ((bool)inputValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
    public enum ControlBoolToVisibilityConverterParameter { InverseBool, test }


    public class NullVisiblityConverter : BaseValueConverter<NullVisiblityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
