﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JsonTransform.Tests.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("JsonTransform.Tests.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;channel&quot;: {
        ///    &quot;title&quot;: &quot;James Newton-King&quot;,
        ///    &quot;link&quot;: &quot;http://james.newtonking.com&quot;,
        ///    &quot;description&quot;: &quot;James Newton-King&apos;s blog.&quot;,
        ///    &quot;item&quot;: [
        ///      {
        ///        &quot;title&quot;: &quot;Json.NET 1.3 + New license + Now on CodePlex&quot;,
        ///        &quot;description&quot;: &quot;Announcing the release of Json.NET 1.3, the MIT license and the source on CodePlex&quot;,
        ///        &quot;link&quot;: &quot;http://james.newtonking.com/projects/json-net.aspx&quot;,
        ///        &quot;category&quot;: [
        ///          &quot;Json.NET&quot;,
        ///          &quot;CodePlex&quot;
        ///        ]
        ///      },
        ///      [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ComplexTransformation_Expected {
            get {
                return ResourceManager.GetString("ComplexTransformation_Expected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;link&quot;: &quot;http://james.newtonking.com&quot;,
        ///  &quot;channel&quot;: {
        ///    &quot;item&quot;: [
        ///      {
        ///        &quot;title&quot;: &quot;Json.NET 1.3 + New license + Now on CodePlex&quot;,
        ///        &quot;description&quot;: &quot;Announcing the release of Json.NET 1.3, the MIT license and the source on CodePlex&quot;,
        ///        &quot;odd&quot;: &quot;delete me&quot;,
        ///        &quot;category&quot;: [
        ///          &quot;Json.NET&quot;,
        ///          &quot;CodePlex&quot;
        ///        ]
        ///      },
        ///      {
        ///        &quot;channelDescription&quot;: &quot;James Newton-King&apos;s blog.&quot;,
        ///        &quot;title&quot;: &quot;LINQ to JSON beta&quot;,
        ///        &quot;odd&quot;: &quot;delete me [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ComplexTransformation_Source {
            get {
                return ResourceManager.GetString("ComplexTransformation_Source", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {
        ///  &quot;transform-remove-link&quot;: &quot;nevermind&quot;,
        ///  &quot;channel&quot;: {
        ///    &quot;title&quot;: &quot;James Newton-King&quot;,
        ///    &quot;transform-copy-link&quot;: &quot;link&quot;,
        ///    &quot;transform-copy-description&quot;: &quot;channel.item[1].channelDescription&quot;,
        ///    &quot;transform-foreach-item&quot;: {
        ///      &quot;transform-copy-link&quot;: &quot;channel.item[1].link&quot;,
        ///      &quot;transform-remove-odd&quot;: null,
        ///      &quot;transform-remove-channelDescription&quot;: null,
        ///      &quot;transform-union-category&quot;: [ &quot;Json.NET&quot; ]
        ///    }
        ///  }
        ///}.
        /// </summary>
        internal static string ComplexTransformation_Transformation {
            get {
                return ResourceManager.GetString("ComplexTransformation_Transformation", resourceCulture);
            }
        }
    }
}
