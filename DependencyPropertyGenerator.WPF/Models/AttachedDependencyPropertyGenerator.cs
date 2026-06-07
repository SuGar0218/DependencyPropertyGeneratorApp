using DependencyPropertyGenerator.Common;

using System.Collections.Generic;
using System.ComponentModel;

namespace DependencyPropertyGenerator.WPF.Models;

public class AttachedDependencyPropertyGenerator : IWpfDependencyPropertyGenerator
{
    public AccessModifier AccessModifier { get; set; }

    public string TypeName { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public PropertyMetadataKind MetadataKind { get; set; }

    public string CallbackName { get; set; } = string.Empty;

    public PropertyChangedCallbackStyle CallbackStyle { get; set; }

    public IEnumerable<FrameworkPropertyMetadataOptions> MetadataOptions { get; set; } = [];

    public string Generate()
    {
        string defaultValue = string.IsNullOrWhiteSpace(DefaultValue) ? $"default({TypeName})" : DefaultValue;
        string callback = string.IsNullOrWhiteSpace(CallbackName) ? string.Empty : CallbackStyle switch
        {
            PropertyChangedCallbackStyle.None => string.Empty,
            PropertyChangedCallbackStyle.Static => CallbackName,
            PropertyChangedCallbackStyle.Instance => $"(d, e) => (({OwnerTypeName})d).{CallbackName}(e)",
            _ => string.Empty
        };

        string propertyMetadata = MetadataKind switch
        {
            PropertyMetadataKind.PropertyMetadata => new PropertyMetadataGenerator
            {
                TypeName = TypeName,
                PropertyName = PropertyName,
                DefaultValue = defaultValue,
                OwnerTypeName = OwnerTypeName,
                PropertyChangedCallback = callback
            }
            .Generate(),

            PropertyMetadataKind.FrameworkPropertyMetadata => new FrameworkPropertyMetadataGenerator
            {
                TypeName = TypeName,
                PropertyName = PropertyName,
                DefaultValue = defaultValue,
                OwnerTypeName = OwnerTypeName,
                PropertyChangedCallback = callback,
                MetadataOptions = MetadataOptions
            }
            .Generate(),

            _ => throw new InvalidEnumArgumentException(),
        };

        return
$@"{AccessModifier.ToCode()} static {TypeName} Get{PropertyName}({OwnerTypeName} target) => ({TypeName})GetValue({PropertyName}Property);
{AccessModifier.ToCode()} static {TypeName} Set{PropertyName}({OwnerTypeName} target, {TypeName} value) => SetValue({PropertyName}Property, value);

{AccessModifier.ToCode()} static readonly DependencyProperty {PropertyName}Property = DependencyProperty.RegisterAttached(
    nameof({PropertyName}),
    typeof({TypeName}),
    typeof({OwnerTypeName}),
    {propertyMetadata}
);";
    }
}
