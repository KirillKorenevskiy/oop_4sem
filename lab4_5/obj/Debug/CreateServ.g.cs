﻿#pragma checksum "..\..\CreateServ.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4C66DE90CE5043BC859A4596B0057C525CCC581D97787FB94E99D914D7942A2B"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using lab4_5;


namespace lab4_5 {
    
    
    /// <summary>
    /// CreateServ
    /// </summary>
    public partial class CreateServ : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox name;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cost;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox description;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPicture;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addService;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox doctor;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox cabinet;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox category;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\CreateServ.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image d;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/lab4_5;component/createserv.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CreateServ.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            
            #line 30 "..\..\CreateServ.xaml"
            ((System.Windows.Controls.TextBlock)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBlock_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 3:
            this.cost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.description = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.AddPicture = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\CreateServ.xaml"
            this.AddPicture.Click += new System.Windows.RoutedEventHandler(this.AddPicture_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.addService = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\CreateServ.xaml"
            this.addService.Click += new System.Windows.RoutedEventHandler(this.addService_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 43 "..\..\CreateServ.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.doctor = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.cabinet = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.category = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 11:
            this.d = ((System.Windows.Controls.Image)(target));
            return;
            case 12:
            
            #line 54 "..\..\CreateServ.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

