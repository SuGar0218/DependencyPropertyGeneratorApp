using System;

namespace DependencyPropertyGenerator.WPF.Models;

/// <summary>
/// 描述 WPF 依赖属性的行为选项
/// </summary>
[Flags]
public enum FrameworkPropertyMetadataOptions
{
    /// <summary>
    /// 未指定任何选项；依赖属性使用 WPF 属性系统的默认行为。
    /// </summary>
    None = 0,

    /// <summary>
    /// 布局组合的度量传递受此依赖属性的值更改的影响。
    /// </summary>
    AffectsMeasure = 1,

    /// <summary>
    /// 布局组合的排列传递受此依赖属性的值更改的影响。
    /// </summary>
    AffectsArrange = 2,

    /// <summary>
    /// 父元素上的度量值传递受此依赖属性的值更改的影响。
    /// </summary>
    AffectsParentMeasure = 4,

    /// <summary>
    /// 父元素上的排列传递受此依赖属性的值更改的影响。
    /// </summary>
    AffectsParentArrange = 8,

    /// <summary>
    /// 呈现或布局组合的某些方面（除了度量值或排列）受此依赖属性的值更改的影响。
    /// </summary>
    AffectsRender = 16,

    /// <summary>
    /// 此依赖属性的值由子元素继承。
    /// </summary>
    Inherits = 32,

    /// <summary>
    /// 出于属性值继承的目的，此依赖属性的值跨越分隔树。
    /// </summary>
    OverridesInheritanceBehavior = 64,

    /// <summary>
    /// 不允许将数据绑定到此依赖属性。
    /// </summary>
    NotDataBindable = 128,

    /// <summary>
    /// 此依赖属性上的数据绑定默认为 TwoWay。
    /// </summary>
    BindsTwoWayByDefault = 256,

    /// <summary>
    /// 此依赖属性的值应通过日记进程保存或还原，或者在通过统一资源标识符（URI）导航时保存或还原。
    /// </summary>
    Journal = 1024,

    /// <summary>
    /// 此依赖属性的值的子属性不会影响呈现的任何方面。
    /// </summary>
    SubPropertiesDoNotAffectRender = 2048
}
