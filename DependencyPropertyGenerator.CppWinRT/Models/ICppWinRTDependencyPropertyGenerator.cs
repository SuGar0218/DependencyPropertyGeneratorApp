using DependencyPropertyGenerator.Common;

namespace DependencyPropertyGenerator.CppWinRT.Models;

public interface ICppWinRTDependencyPropertyGenerator
{
    AccessModifier AccessModifier { get; set; }
    string TypeName { get; set; }
    string PropertyName { get; set; }

    string GenerateIdl();
    string GenerateH();
    string GenerateCpp();
}
