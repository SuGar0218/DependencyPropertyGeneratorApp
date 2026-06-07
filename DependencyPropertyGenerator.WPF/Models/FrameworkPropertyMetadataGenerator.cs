using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependencyPropertyGenerator.WPF.Models;

public class FrameworkPropertyMetadataGenerator : IWpfPropertyMetadataGenerator
{
    public string TypeName { get; set; } = string.Empty;

    public string PropertyName { get; set; } = string.Empty;

    public string OwnerTypeName { get; set; } = string.Empty;

    public string DefaultValue { get; set; } = string.Empty;

    public string PropertyChangedCallback { get; set; } = string.Empty;

    public IEnumerable<FrameworkPropertyMetadataOptions>? MetadataOptions { get; set; }

    public string Generate()
    {
        string defaultValue = string.IsNullOrWhiteSpace(DefaultValue) ? $"default({TypeName})" : DefaultValue;
        string propertyMetadata;
        List<string> propertyMetadataParams = [with(3), defaultValue];
        string optionsParam = MetadataOptions is null ? string.Empty : string.Join(" | ", MetadataOptions
            .SkipWhile(option => option == FrameworkPropertyMetadataOptions.None)
            .Select(option => $"FrameworkPropertyMetadataOptions.{Enum.GetName(option)}"));
        if (!string.IsNullOrWhiteSpace(optionsParam))
        {
            propertyMetadataParams.Add(optionsParam);
        }
        if (!string.IsNullOrWhiteSpace(PropertyChangedCallback))
        {
            propertyMetadataParams.Add(PropertyChangedCallback);
        }
        string indentInNewLine = $"{Environment.NewLine}{TabToSpace()}{TabToSpace()}";
        string paramsText = $"{string.Join($",{indentInNewLine}", propertyMetadataParams)}";
        if (propertyMetadataParams.Count > 1)
        {
            paramsText = indentInNewLine + paramsText;
        }
        propertyMetadata = $@"new FrameworkPropertyMetadata({paramsText})";
        return propertyMetadata;
    }

    private static string TabToSpace(int space = 4)
    {
        StringBuilder stringBuilder = new(space);
        for (int i = 0; i < space; i++)
        {
            stringBuilder.Append(' ');
        }
        return stringBuilder.ToString();
    }
}
