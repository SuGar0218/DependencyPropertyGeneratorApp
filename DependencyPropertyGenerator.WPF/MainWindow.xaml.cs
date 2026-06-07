using DependencyPropertyGenerator.WPF.ViewModels;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;

using Windows.Foundation;

namespace DependencyPropertyGenerator.WPF;

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
        viewModel.TypeName = "string";
        viewModel.PropertyName = "Username";
        viewModel.OwnerTypeName = "MyView";
    }
}
