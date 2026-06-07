using DependencyPropertyGenerator.CppWinRT.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

using Windows.Foundation;

namespace DependencyPropertyGenerator.CppWinRT;

public sealed partial class MainWindow : Window
{
    public MainWindow()
    {
        ExtendsContentIntoTitleBar = true;
        InitializeComponent();
    }

    private void OnRootLoaded(object sender, RoutedEventArgs e)
    {
        this.Resize(new Size(800, 600));
        Activate();
    }

    private void OnTitleTextBlockDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
    {
        DependencyPropertyInfoViewModel viewModel = PART_MainView.ViewModel;
        viewModel.TypeName = "winrt::hstring";
        viewModel.PropertyName = "Label";
        viewModel.OwnerTypeName = "MyControl";
    }
}
