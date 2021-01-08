using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Raise.CustomControls
{
    public class ToolbarItem : BindableObject
    {
        //
        // Summary:
        //     Gets or sets toolbar text. It is a bindable property
        public static readonly BindableProperty TextProperty;
        //
        // Summary:
        //     Gets or sets toolbar text. It is a bindable property
        public static readonly BindableProperty IconProperty;
        //
        // Summary:
        //     Gets or sets Icon Height.
        public static readonly BindableProperty IconHeightProperty;
        //
        // Summary:
        //     Gets or sets Text Height.
        public static readonly BindableProperty TextHeightProperty;
        //
        // Summary:
        //     Gets or sets toolbar item name. It is a bindable property
        public static readonly BindableProperty NameProperty;

        //
        // Summary:
        //     Initializes a new instance of the Syncfusion.SfImageEditor.XForms.ToolbarItem
        //     class.
        public ToolbarItem()
        {

        }

        //
        // Summary:
        //     To get and set text value to toolbar item
        //
        // Value:
        //     This property takes the System.String as value
        public string Text { get; set; }
        //
        // Summary:
        //     To get and set Icon value to toolbar item
        //
        // Value:
        //     This property takes the Xamarin.Forms.ImageSource as value
        public ImageSource Icon { get; set; }
        //
        // Summary:
        //     To get and set IconHeight value for toolbar item
        //
        // Value:
        //     This property takes the System.Double as value
        public double IconHeight { get; set; }
        //
        // Summary:
        //     To get and set TextHeight value for toolbar item
        //
        // Value:
        //     This property takes the System.Double as value
        public double TextHeight { get; set; }
        //
        // Summary:
        //     To get and set Name value for toolbar item
        //
        // Value:
        //     This property takes the System.String as value
        public string Name { get; set; }
    }
}
