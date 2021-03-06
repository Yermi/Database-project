﻿#pragma checksum "..\..\FlightLineWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9E2E73A211814C088843EB10B4496239"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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


namespace WpfApplication2 {
    
    
    /// <summary>
    /// FlightLineWindow
    /// </summary>
    public partial class FlightLineWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\FlightLineWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox flightLineId;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FlightLineWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sourceCMB;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\FlightLineWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox destinationCMB;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\FlightLineWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox totalKM_TXB;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\FlightLineWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button flightLinesDoneButton;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApplication2;component/flightlinewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FlightLineWindow.xaml"
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
            
            #line 4 "..\..\FlightLineWindow.xaml"
            ((WpfApplication2.FlightLineWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.flight_line_Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.flightLineId = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.sourceCMB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 12 "..\..\FlightLineWindow.xaml"
            this.sourceCMB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.source_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.destinationCMB = ((System.Windows.Controls.ComboBox)(target));
            
            #line 13 "..\..\FlightLineWindow.xaml"
            this.destinationCMB.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.destination_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.totalKM_TXB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.flightLinesDoneButton = ((System.Windows.Controls.Button)(target));
            
            #line 16 "..\..\FlightLineWindow.xaml"
            this.flightLinesDoneButton.Click += new System.Windows.RoutedEventHandler(this.flightLinesDoneButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

