using CommunityToolkit.WinUI.Controls;

using DependencyPropertyGenerator.Common;
using DependencyPropertyGenerator.CppWinRT.Models;
using DependencyPropertyGenerator.CppWinRT.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using System;

using Windows.ApplicationModel.DataTransfer;

namespace DependencyPropertyGenerator.CppWinRT.Views;

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
        return viewModel;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
    }

    private bool ShouldEnabledCallbackSettings(PropertyChangedCallbackStyle style) => style != PropertyChangedCallbackStyle.None;

    private bool ShouldEnableCopyButton(string code) => !string.IsNullOrWhiteSpace(code);

    private string BuildFileName(string typename, string extension) => $"{typename[(typename.LastIndexOf(':') + 1)..]}.{extension}";

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

    private static void CopyText(string text)
    {
        DataPackage package = new();
        package.SetText(text);
        Clipboard.SetContent(package);
    }

    private void OnCopyIdlButtonClick(object sender, RoutedEventArgs e)
    {
        CopyText(ViewModel.GeneratedIdl);
    }

    private void OnCopyHButtonClick(object sender, RoutedEventArgs e)
    {
        CopyText(ViewModel.GeneratedH);
    }

    private void OnCopyCppButtonClick(object sender, RoutedEventArgs e)
    {
        CopyText(ViewModel.GeneratedCpp);
    }

    private AccessModifier[] AccessModifiers => field ??= Enum.GetValues<AccessModifier>();
    private RegisteringAction[] RegisteringActions => field ?? Enum.GetValues<RegisteringAction>();
    private PropertyChangedCallbackStyle[] PropertyChangedCallbackStyles => field ?? Enum.GetValues<PropertyChangedCallbackStyle>();
}
