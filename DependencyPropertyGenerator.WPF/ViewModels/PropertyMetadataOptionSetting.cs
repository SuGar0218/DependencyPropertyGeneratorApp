using CommunityToolkit.Mvvm.ComponentModel;

using DependencyPropertyGenerator.WPF.Models;

namespace DependencyPropertyGenerator.WPF.ViewModels;

public partial class PropertyMetadataOptionSetting : ObservableObject
{
    [ObservableProperty]
    public partial FrameworkPropertyMetadataOptions Option { get; set; }

    [ObservableProperty]
    public partial bool IsEnabled { get; set; }
}
