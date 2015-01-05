using System;
using Windows.UI.Xaml.Data;

namespace EveList8._1.Common.Converter
{
    class SexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return "Пол: " + (string) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
