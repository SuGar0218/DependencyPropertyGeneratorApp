namespace DependencyPropertyGenerator.WPF.Models;

public interface IPropertyMetadataGenerator
{
    string TypeName { get; set; }
    string PropertyName { get; set; }
    string OwnerTypeName { get; set; }
    string DefaultValue { get; set; }
    string PropertyChangedCallback { get; set; }
    string Generate();
}
