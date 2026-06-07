using DependencyPropertyGenerator.Common;

namespace DependencyPropertyGenerator.WPF.Models;

public interface IWpfDependencyPropertyGenerator
{
    AccessModifier AccessModifier { get; set; }
    string TypeName { get; set; }
    string PropertyName { get; set; }
    string Generate();
}
