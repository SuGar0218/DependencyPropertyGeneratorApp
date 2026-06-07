using DependencyPropertyGenerator.Common;

using System;
using System.Text;

namespace DependencyPropertyGenerator.CppWinRT.Models;

public class AttachedDependencyPropertyGenerator : ICppWinRTDependencyPropertyGenerator
{
    public AccessModifier AccessModifier { get; set; }

    public string IdlTypeName { get; set; } = string.Empty;

    public string TypeName { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string PropertyChangedCallback { get; set; } = string.Empty;

    public string GenerateIdl()
    {
        return new StringBuilder()
            .Append($"static Microsoft.UI.Xaml.DependencyProperty {PropertyName}Property {{ get; }};")
            .Append(Environment.NewLine)
            .Append($"{IdlTypeName} Get{PropertyName}({OwnerTypeName} target);")
            .Append(Environment.NewLine)
            .Append($"{IdlTypeName} Set{PropertyName}({OwnerTypeName} target, {TypeName} value);")
            .ToString();
    }

    public string GenerateH()
    {
        return new StringBuilder()
            .Append($@"{AccessModifier.ToCode()}:
    {TypeName} {PropertyName}();
    void {PropertyName}({TypeName} const& value);

    static DependencyProperty {PropertyName}Property();")
            .ToString();
    }

    public string GenerateCpp()
    {
        string propertyMetadata = new PropertyMetadataGenerator()
        {
            TypeName = TypeName,
            DefaultValue = DefaultValue,
            OwnerTypeName = OwnerTypeName,
            PropertyChangedCallback = PropertyChangedCallback
        }
        .Generate();

        return
$@"{TypeName} {OwnerTypeName}::{PropertyName}()
{{
    return winrt::unbox_value<{TypeName}>(GetValue({PropertyName}Property()));
}}

void {OwnerTypeName}::{PropertyName}({TypeName} value)
{{
    SetValue({PropertyName}Property(), winrt::box_value(value));
}}

DependencyProperty {OwnerTypeName}::{PropertyName}Property()
{{
    static DependencyProperty s_property = DependencyProperty::Register(
        L""{PropertyName}"",
        winrt::xaml_typename<{TypeName}>(),
        winrt::xaml_typename<{OwnerTypeName}>(),
        {propertyMetadata});
    return s_property;
}}";
    }
}
