using DependencyPropertyGenerator.Common;

using System;
using System.Text;

namespace DependencyPropertyGenerator.CppWinRT.Models;

public class NormalDependencyPropertyGenerator : ICppWinRTDependencyPropertyGenerator
{
    public AccessModifier AccessModifier { get; set; }

    public string TypeName { get; set; } = string.Empty;

    private string _lowerCamelCasePropertyName = string.Empty;

    public string PropertyName
    {
        get;
        set
        {
            if (field == value)
                return;

            field = value;
            _lowerCamelCasePropertyName = ToLowerCamelCase(value);
        }
    }
    = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string GenerateIdl()
    {
        return new StringBuilder()
            .Append($"static Microsoft.UI.Xaml.DependencyProperty {PropertyName}Property {{ get; }};")
            .Append(Environment.NewLine)
            .Append($"String {PropertyName};")
            .ToString();
    }

    public string GenerateH()
    {
        return new StringBuilder()
            .Append($@"public:
    static Microsoft::UI::Xaml::DependencyProperty {PropertyName}Property()
    {{
        return m_{_lowerCamelCasePropertyName}Property;
    }}")
            .AppendLine(Environment.NewLine)
            .Append($@"private:
    static Microsoft::UI::Xaml::DependencyProperty m_{_lowerCamelCasePropertyName}Property;")
            .ToString();
    }

    public string GenerateCpp()
    {
        return
$@"Microsoft::UI::Xaml::DependencyProperty {OwnerTypeName}::m_{_lowerCamelCasePropertyName}Property =
    Microsoft::UI::Xaml::DependencyProperty::Register(
        L""{PropertyName}"",
        winrt::xaml_typename<{TypeName}>(),
        winrt::xaml_typename<{OwnerTypeName}>(),
        Microsoft::UI::Xaml::PropertyMetadata {{ nullptr }}
);";
    }

    private static string ToLowerCamelCase(string upperCamelCase)
    {
        if (string.IsNullOrEmpty(upperCamelCase))
            return upperCamelCase;

        if (upperCamelCase[0] < 'A' || upperCamelCase[0] > 'Z')
            return upperCamelCase;

        char[] chars = upperCamelCase.ToCharArray();
        chars[0] += (char)('a' - 'A');
        return new string(chars);
    }
}
