using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Use this attribute to specify that a static method should be exposed as an AJAX PageMethod call
/// through the owning page.
/// </summary>
[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class UserControlScriptMethodAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserControlScriptMethodAttribute"/> class.
    /// </summary>
    public UserControlScriptMethodAttribute() { }
}