using DependencyPropertyGenerator.WPF.Models;

using Microsoft.UI.Xaml.Data;

using System;

namespace DependencyPropertyGenerator.WPF.Views;

internal partial class PropertyChangedCallbackStyleToDisplayTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => Convert((PropertyChangedCallbackStyle)value);

    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();

    public static string Convert(PropertyChangedCallbackStyle style) => style switch
    {
        PropertyChangedCallbackStyle.None => "无回调",
        PropertyChangedCallbackStyle.Static => "静态回调",
        PropertyChangedCallbackStyle.Instance => "实例回调",
        _ => ((int)style).ToString(),
    };
}
