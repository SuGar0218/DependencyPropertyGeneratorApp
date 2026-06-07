namespace DependencyPropertyGenerator.CppWinRT.Models;

public interface ICppWinRTPropertyMetadataGenerator
{
    string TypeName { get; set; }
    string OwnerTypeName { get; set; }
    string DefaultValue { get; set; }
    string PropertyChangedCallback { get; set; }
    string Generate();
}
