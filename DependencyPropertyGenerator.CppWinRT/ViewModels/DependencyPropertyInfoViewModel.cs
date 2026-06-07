using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DependencyPropertyGenerator.Common;
using DependencyPropertyGenerator.CppWinRT.Models;

using System.ComponentModel;

namespace DependencyPropertyGenerator.CppWinRT.ViewModels;

public partial class DependencyPropertyInfoViewModel : ObservableObject
{
    [ObservableProperty]
    public partial AccessModifier AccessModifier { get; set; }

    [ObservableProperty]
    public partial string TypeName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string PropertyName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial RegisteringAction RegisteringAction { get; set; }

    [ObservableProperty]
    public partial string OwnerTypeName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string DefaultValue { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string CallbackName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial PropertyChangedCallbackStyle CallbackStyle { get; set; }

    [ObservableProperty]
    public partial string GeneratedIdl { get; private set; } = string.Empty;

    [ObservableProperty]
    public partial string GeneratedH { get; private set; } = string.Empty;

    [ObservableProperty]
    public partial string GeneratedCpp { get; private set; } = string.Empty;

    [RelayCommand]
    private void Generate()
    {
        ICppWinRTDependencyPropertyGenerator generator = RegisteringAction switch
        {
            RegisteringAction.Register => new NormalDependencyPropertyGenerator
            {
                AccessModifier = AccessModifier,
                TypeName = TypeName,
                PropertyName = PropertyName,
                OwnerTypeName = OwnerTypeName
            },
            RegisteringAction.RegisterAttached => new AttachedDependencyProperty
            {
                AccessModifier = AccessModifier,
                TypeName = TypeName,
                PropertyName = PropertyName,
                OwnerTypeName = OwnerTypeName
            },
            _ => throw new InvalidEnumArgumentException(),
        };
        GeneratedIdl = generator.GenerateIdl();
        GeneratedH = generator.GenerateH();
        GeneratedCpp = generator.GenerateCpp();
    }

    [RelayCommand]
    private void Clear()
    {
        TypeName = string.Empty;
        PropertyName = string.Empty;
        OwnerTypeName = string.Empty;
        DefaultValue = string.Empty;
        CallbackName = string.Empty;
        GeneratedIdl = string.Empty;
        GeneratedH = string.Empty;
        GeneratedCpp = string.Empty;
    }

    [RelayCommand]
    private void GenerateTypeDefaultValue() => DefaultValue = $"default({TypeName})";

    [RelayCommand]
    private void GenerateCallbackName()
    {
        if (string.IsNullOrWhiteSpace(PropertyName))
            return;

        CallbackName = $"On{PropertyName}Changed";
    }
}
