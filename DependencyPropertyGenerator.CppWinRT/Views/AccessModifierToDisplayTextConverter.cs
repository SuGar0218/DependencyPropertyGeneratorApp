using DependencyPropertyGenerator.Common;

using Microsoft.UI.Xaml.Data;

using System;

namespace DependencyPropertyGenerator.CppWinRT.Views;

internal partial class AccessModifierToDisplayTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => Convert((AccessModifier)value);

    public object ConvertBack(object value, Type targetType, object parameter, string language) => throw new NotSupportedException();

    public static string Convert(AccessModifier accessModifier) => accessModifier.ToCode();
}
