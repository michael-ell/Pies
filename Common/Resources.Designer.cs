﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Codell.Pies.Common {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Codell.Pies.Common.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot has the object.  The object is not serializable, mark it with [Serializable]..
        /// </summary>
        public static string CannotHashObject {
            get {
                return ResourceManager.GetString("CannotHashObject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There is already a unit of work created for this context..
        /// </summary>
        public static string ExistingUnitOfWork {
            get {
                return ResourceManager.GetString("ExistingUnitOfWork", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sorry, our pies can only hold {0} ingredients..
        /// </summary>
        public static string MaxIngredientsReached {
            get {
                return ResourceManager.GetString("MaxIngredientsReached", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find {0} in appSettings..
        /// </summary>
        public static string MissingAppSetting {
            get {
                return ResourceManager.GetString("MissingAppSetting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The configuration section {0} is not configured..
        /// </summary>
        public static string MissingConfiguration {
            get {
                return ResourceManager.GetString("MissingConfiguration", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find connection string named {0}..
        /// </summary>
        public static string MissingConnectionString {
            get {
                return ResourceManager.GetString("MissingConnectionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to At least one of the following conditions must be met: &apos;{0}&apos; or &apos;{1}&apos;.
        /// </summary>
        public static string OrRuleMessage {
            get {
                return ResourceManager.GetString("OrRuleMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Oops, you must make room to increase this ingredient..
        /// </summary>
        public static string PercentRejected {
            get {
                return ResourceManager.GetString("PercentRejected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to load type through reflection: {0}..
        /// </summary>
        public static string ReflectionTypeLoaderError {
            get {
                return ResourceManager.GetString("ReflectionTypeLoaderError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -Select-.
        /// </summary>
        public static string SelectPrompt {
            get {
                return ResourceManager.GetString("SelectPrompt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ?.
        /// </summary>
        public static string Unknown {
            get {
                return ResourceManager.GetString("Unknown", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find a value for the description &apos;{0}&apos;..
        /// </summary>
        public static string UnknownDescription {
            get {
                return ResourceManager.GetString("UnknownDescription", resourceCulture);
            }
        }
    }
}
