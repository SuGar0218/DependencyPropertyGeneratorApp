namespace DependencyPropertyGenerator.Common;

public enum AccessModifier
{
    /// <summary>公共访问，不受限制</summary>
    Public = 0,

    /// <summary>受保护访问，仅限包含类或派生类</summary>
    Protected = 1,

    /// <summary>内部访问，仅限当前程序集</summary>
    Internal = 2,

    /// <summary>私有访问，仅限包含类内部</summary>
    Private = 3,

    /// <summary>受保护的内部访问（程序集内或派生类）</summary>
    ProtectedInternal = 4,

    /// <summary>私有受保护访问（C# 7.2+）</summary>
    PrivateProtected = 5
}
