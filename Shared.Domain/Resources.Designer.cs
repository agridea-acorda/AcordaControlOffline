﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Agridea.Acorda.AcordaControlOffline.Shared.Domain.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Contacté le {0}.
        /// </summary>
        internal static string AppointmentContactDateString {
            get {
                return ResourceManager.GetString("AppointmentContactDateString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Rendez-vous le {0}.
        /// </summary>
        internal static string AppointmentDateString {
            get {
                return ResourceManager.GetString("AppointmentDateString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (Non agendé).
        /// </summary>
        internal static string ApppointmentNotSet {
            get {
                return ResourceManager.GetString("ApppointmentNotSet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Annoncé.
        /// </summary>
        internal static string InspectionModeScheduled {
            get {
                return ResourceManager.GetString("InspectionModeScheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Inopiné.
        /// </summary>
        internal static string InspectionModeUnscheduled {
            get {
                return ResourceManager.GetString("InspectionModeUnscheduled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non applicable.
        /// </summary>
        internal static string InspectionOutcomeNotApplicable {
            get {
                return ResourceManager.GetString("InspectionOutcomeNotApplicable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non contrôlé.
        /// </summary>
        internal static string InspectionOutcomeNotInspected {
            get {
                return ResourceManager.GetString("InspectionOutcomeNotInspected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Non respecté.
        /// </summary>
        internal static string InspectionOutcomeNotOk {
            get {
                return ResourceManager.GetString("InspectionOutcomeNotOk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Respecté.
        /// </summary>
        internal static string InspectionOutcomeOk {
            get {
                return ResourceManager.GetString("InspectionOutcomeOk", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Partiellement respecté.
        /// </summary>
        internal static string InspectionOutcomePartiallyOk {
            get {
                return ResourceManager.GetString("InspectionOutcomePartiallyOk", resourceCulture);
            }
        }
    }
}
