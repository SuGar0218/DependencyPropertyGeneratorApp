using DependencyPropertyGenerator.Common;

namespace DependencyPropertyGenerator.CppWinRT.Models;

public interface ICppWinRTDependencyPropertyGenerator
{
    AccessModifier AccessModifier { get; set; }
    string TypeName { get; set; }
    string PropertyName { get; set; }
    string DefaultValue { get; set; }
    string OwnerTypeName { get; set; }
    string PropertyChangedCallback { get; set; }

    string GenerateIdl();
    string GenerateH();
    string GenerateCpp();
}
