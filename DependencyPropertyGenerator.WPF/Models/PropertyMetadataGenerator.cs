using System.Collections.Generic;

namespace DependencyPropertyGenerator.WPF.Models;

public class PropertyMetadataGenerator : IPropertyMetadataGenerator
{
    public string TypeName { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public string PropertyChangedCallback { get; set; } = string.Empty;

    public string Generate()
    {
        string propertyMetadata;
        string defaultValue = string.IsNullOrWhiteSpace(DefaultValue) ? $"default({TypeName})" : DefaultValue;
        List<string> propertyMetadataParams = [with(3), defaultValue];
        if (!string.IsNullOrWhiteSpace(PropertyChangedCallback))
        {
            propertyMetadataParams.Add(PropertyChangedCallback);
        }
        propertyMetadata = $@"new PropertyMetadata({string.Join(", ", propertyMetadataParams)})";
        return propertyMetadata;
    }
}
