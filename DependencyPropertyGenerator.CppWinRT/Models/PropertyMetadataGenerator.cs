using System.Collections.Generic;

namespace DependencyPropertyGenerator.CppWinRT.Models;

public class PropertyMetadataGenerator : ICppWinRTPropertyMetadataGenerator
{
    public string TypeName { get; set; } = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public string PropertyChangedCallback { get; set; } = string.Empty;

    public string Generate()
    {
        List<string> propertyMetadataParams = [with(2)];
        string defaultValue = string.IsNullOrWhiteSpace(DefaultValue) && !string.IsNullOrWhiteSpace(PropertyChangedCallback) ?
            $"{TypeName}{{}}" :
            DefaultValue;
        if (defaultValue.StartsWith('\"') && defaultValue.EndsWith('\"'))
        {
            defaultValue = 'L' + defaultValue;
        }
        if (!string.IsNullOrWhiteSpace(defaultValue))
        {
            defaultValue = DefaultValue.StartsWith("winrt::box_value(") || DefaultValue.StartsWith("box_value(") ?
                defaultValue :
                $"box_value({defaultValue})";

            propertyMetadataParams.Add(defaultValue);
        }
        if (!string.IsNullOrWhiteSpace(PropertyChangedCallback))
        {
            string callback = PropertyChangedCallback.StartsWith("PropertyChangedCallback") ?
                PropertyChangedCallback :
                $"PropertyChangedCallback {{ &{OwnerTypeName}::{PropertyChangedCallback} }}";

            propertyMetadataParams.Add(callback);
        }
        return propertyMetadataParams.Count > 0 ?
            $"PropertyMetadata {{ {string.Join(", ", propertyMetadataParams)} }}" :
            "PropertyMetadata { nullptr }";
    }
}
