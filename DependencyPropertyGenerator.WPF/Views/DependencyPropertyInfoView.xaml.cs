using CommunityToolkit.WinUI.Controls;

using DependencyPropertyGenerator.Common;
using DependencyPropertyGenerator.WPF.Models;
using DependencyPropertyGenerator.WPF.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;
using System.ComponentModel;

using Windows.ApplicationModel.DataTransfer;

namespace DependencyPropertyGenerator.WPF.Views;

public sealed partial class DependencyPropertyInfoView : UserControl
{
    public DependencyPropertyInfoView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
    }

    public double Spacing
    {
        get => (double)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public static readonly DependencyProperty SpacingProperty = DependencyProperty.Register(
        nameof(Spacing),
        typeof(double),
        typeof(DependencyPropertyInfoView),
        new PropertyMetadata(default(double))
    );

    public DependencyPropertyInfoViewModel ViewModel => field ??= InitializeViewModel();

    private DependencyPropertyInfoViewModel InitializeViewModel()
    {
        DependencyPropertyInfoViewModel viewModel = new();
        viewModel.PropertyChanged += OnViewModelPropertyChanged;
        return viewModel;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        DependencyPropertyInfoViewModel viewModel = (DependencyPropertyInfoViewModel)sender!;
        if (e.PropertyName == nameof(viewModel.GeneratedCode) && !string.IsNullOrWhiteSpace(viewModel.GeneratedCode))
        {
            VisualStateManager.GoToState(this, VisualStates.AfterGeneration, false);
        }
        else
        {
            VisualStateManager.GoToState(this, VisualStates.BeforeGeneration, false);
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        VisualStateManager.GoToState(this, VisualStates.BeforeGeneration, false);
    }

    private Visibility ToMetadataOptionsVisibility(PropertyMetadataKind kind) => kind == PropertyMetadataKind.FrameworkPropertyMetadata ? Visibility.Visible : Visibility.Collapsed;

    private bool ShouldEnabledCallbackSettings(PropertyChangedCallbackStyle style) => style != PropertyChangedCallbackStyle.None;

    private bool ShouldEnableCopyButton(string code) => !string.IsNullOrWhiteSpace(code);

    private double _maxItemWidth;

    private void UniformGrid_Loaded(object sender, RoutedEventArgs e)
    {
        UniformGrid uniformGrid = (UniformGrid)sender;
        foreach (UIElement child in uniformGrid.Children)
        {
            _maxItemWidth = Math.Max(_maxItemWidth, child.DesiredSize.Width);
        }
        AdjustUniformGridColumns(uniformGrid, uniformGrid.ActualWidth);
    }

    private void UniformGrid_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        AdjustUniformGridColumns((UniformGrid)sender, e.NewSize.Width);
    }

    private void AdjustUniformGridColumns(UniformGrid uniformGrid, double width)
    {
        int columns = Math.Max(1, (int)Math.Floor(width / _maxItemWidth));
        while (columns > 1 && uniformGrid.Children.Count % columns != 0)
        {
            columns--;
        }
        uniformGrid.Columns = columns;
    }

    private void OnCopyButtonClick(object sender, RoutedEventArgs e)
    {
        DataPackage package = new();
        package.SetText(ViewModel.GeneratedCode);
        Clipboard.SetContent(package);
    }

    private AccessModifier[] AccessModifiers => field ??= Enum.GetValues<AccessModifier>();
    private RegisteringAction[] RegisteringActions => field ?? Enum.GetValues<RegisteringAction>();
    private PropertyMetadataKind[] PropertyMetadataKinds => field ?? Enum.GetValues<PropertyMetadataKind>();
    private PropertyChangedCallbackStyle[] PropertyChangedCallbackStyles => field ?? Enum.GetValues<PropertyChangedCallbackStyle>();

    private static class VisualStates
    {
        public static readonly string BeforeGeneration = nameof(BeforeGeneration);
        public static readonly string AfterGeneration = nameof(AfterGeneration);
    }
}
