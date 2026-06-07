using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using DependencyPropertyGenerator.Common;
using DependencyPropertyGenerator.WPF.Models;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DependencyPropertyGenerator.WPF.ViewModels;

public partial class DependencyPropertyInfoViewModel : ObservableObject
{
    public DependencyPropertyInfoViewModel()
    {
        foreach (FrameworkPropertyMetadataOptions option in Enum.GetValues<FrameworkPropertyMetadataOptions>())
        {
            PropertyMetadataOptionSetting setting = new()
            {
                Option = option,
                IsEnabled = option == FrameworkPropertyMetadataOptions.None,
            };
            setting.PropertyChanged += OnSettingPropertyChanged;
            MetadataOptionSettings.Add(setting);
        }
    }

    [ObservableProperty]
    public partial AccessModifier AccessModifier { get; set; }

    [ObservableProperty]
    public partial string TypeName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string PropertyName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string OwnerTypeName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial string DefaultValue { get; set; } = string.Empty;

    [ObservableProperty]
    public partial RegisteringAction RegisteringAction { get; set; }

    [ObservableProperty]
    public partial PropertyMetadataKind MetadataKind { get; set; } = PropertyMetadataKind.FrameworkPropertyMetadata;

    [ObservableProperty]
    public partial string CallbackName { get; set; } = string.Empty;

    [ObservableProperty]
    public partial PropertyChangedCallbackStyle CallbackStyle { get; set; }

    public ObservableCollection<PropertyMetadataOptionSetting> MetadataOptionSettings { get; } = [];

    [ObservableProperty]
    public partial string GeneratedCode { get; private set; } = string.Empty;

    [RelayCommand]
    private void Generate()
    {
        switch (RegisteringAction)
        {
            case RegisteringAction.Register:
                GeneratedCode = new NormalDependencyPropertyGenerator
                {
                    AccessModifier = AccessModifier,
                    TypeName = TypeName,
                    PropertyName = PropertyName,
                    OwnerTypeName = OwnerTypeName,
                    DefaultValue = DefaultValue,
                    MetadataKind = MetadataKind,
                    CallbackName = CallbackName,
                    CallbackStyle = CallbackStyle,
                    MetadataOptions = MetadataOptionSettings.Where(setting => setting.IsEnabled).Select(setting => setting.Option)
                }
                .Generate();
                break;

            case RegisteringAction.RegisterAttached:
                GeneratedCode = new AttachedDependencyPropertyGenerator
                {
                    AccessModifier = AccessModifier,
                    TypeName = TypeName,
                    PropertyName = PropertyName,
                    OwnerTypeName = OwnerTypeName,
                    DefaultValue = DefaultValue,
                    MetadataKind = MetadataKind,
                    CallbackName = CallbackName,
                    CallbackStyle = CallbackStyle,
                    MetadataOptions = MetadataOptionSettings.Where(setting => setting.IsEnabled).Select(setting => setting.Option)
                }
                .Generate();
                break;

            case RegisteringAction.RegisterReadOnly:
                GeneratedCode = new ReadOnlyDependencyPropertyGenerator
                {
                    AccessModifier = AccessModifier,
                    TypeName = TypeName,
                    PropertyName = PropertyName,
                    OwnerTypeName = OwnerTypeName,
                    DefaultValue = DefaultValue,
                    MetadataKind = MetadataKind,
                    CallbackName = CallbackName,
                    CallbackStyle = CallbackStyle,
                    MetadataOptions = MetadataOptionSettings.Where(setting => setting.IsEnabled).Select(setting => setting.Option)
                }
                .Generate();
                break;

            default:
                break;
        }
    }

    [RelayCommand]
    private void Clear()
    {
        TypeName = string.Empty;
        PropertyName = string.Empty;
        OwnerTypeName = string.Empty;
        DefaultValue = string.Empty;
        CallbackName = string.Empty;
        GeneratedCode = string.Empty;
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

    private void OnSettingPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        PropertyMetadataOptionSetting setting = (PropertyMetadataOptionSetting)sender!;
        switch (e.PropertyName)
        {
            case nameof(PropertyMetadataOptionSetting.IsEnabled):
                if (setting.IsEnabled)
                {
                    if (setting.Option == FrameworkPropertyMetadataOptions.None)
                    {
                        foreach (PropertyMetadataOptionSetting other in MetadataOptionSettings.Where(s => s != setting))
                        {
                            other.IsEnabled = false;
                        }
                    }
                    else
                    {
                        foreach (PropertyMetadataOptionSetting other in MetadataOptionSettings.Where(s => s.Option == FrameworkPropertyMetadataOptions.None))
                        {
                            other.IsEnabled = false;
                        }
                    }
                }
                break;

            default:
                break;
        }
    }
}
