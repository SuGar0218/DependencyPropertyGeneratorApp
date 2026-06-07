using Microsoft.UI.Xaml;

using System;

using Windows.Foundation;
using Windows.Graphics;

namespace DependencyPropertyGenerator.WPF;

public static class WindowExtensions
{
    /// <summary>
    /// 以 XAML 尺寸调整窗口大小
    /// </summary>
    /// <remarks>
    /// 因为依赖 XAML 视觉树获取屏幕显示缩放以转换到屏幕物理像素，所以需要等待视觉树加载完成，通常是 <see cref="FrameworkElement.Loaded"/> 事件。
    /// </remarks>
    public static void Resize(this Window window, Size size)
    {
        if (window.ExtendsContentIntoTitleBar)
        {
            window.AppWindow.ResizeClient(new SizeInt32
            (
                _Width: IsValidLength(size.Width) ? DipToPixel(size.Width, window) : window.AppWindow.ClientSize.Width,
                _Height: IsValidLength(size.Height) ? DipToPixel(size.Height - 30, window) : window.AppWindow.ClientSize.Height
            ));
        }
        else
        {
            window.AppWindow.ResizeClient(new SizeInt32
            (
                _Width: IsValidLength(size.Width) ? DipToPixel(size.Width, window) : window.AppWindow.ClientSize.Width,
                _Height: IsValidLength(size.Height) ? DipToPixel(size.Height, window) : window.AppWindow.ClientSize.Height
            ));
        }
    }

    private static bool IsValidLength(double length) => double.IsNormal(length) && double.IsPositive(length);

    private static int DipToPixel(double dip, Window window) => (int)Math.Ceiling(dip * window.Content.XamlRoot.RasterizationScale);
}
