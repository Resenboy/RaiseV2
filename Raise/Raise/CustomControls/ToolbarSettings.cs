using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Raise.CustomControls
{
    public class ToolbarSettings : Element
    {
        //
        // Summary:
        //     Gets or sets whether the toolbar needs to be visible or not. It is a bindable
        //     property
        public static readonly BindableProperty BackgroundColorProperty;
        //
        // Summary:
        //     Gets or sets whether the toolbar needs to be visible or not. It is a bindable
        //     property
        public static readonly BindableProperty TextColorProperty;
        //
        // Summary:
        //     Gets or sets whether the toolbar needs to be visible or not. It is a bindable
        //     property
        public static readonly BindableProperty IsVisibleProperty;
        //
        // Summary:
        //     Gets or sets the value for BackToolbarItem. It is a bindable property
        public static readonly BindableProperty BackToolbarItemProperty;
        //
        // Summary:
        //     Gets or sets height of the footer toolbar
        public static readonly BindableProperty FooterToolbarHeightProperty;
        //
        // Summary:
        //     Gets or sets height of the header toolbar.
        public static readonly BindableProperty HeaderToolbarHeightProperty;
        //
        // Summary:
        //     Gets or sets height of the SubItem toolbar.
        public static readonly BindableProperty SubItemToolbarHeightProperty;
        //
        // Summary:
        //     Gets/sets toolbaritems collection value. It is a bindable property
        public static readonly BindableProperty ToolbarItemsProperty;

        //
        // Summary:
        //     Initializes a new instance of the Syncfusion.SfImageEditor.XForms.ToolbarSettings
        //     class.
        public ToolbarSettings()
        {

        }

        //
        // Summary:
        //     Gets or sets the toolbar background color. It is a bindable property
        public Color BackgroundColor { get; set; }
        //
        // Summary:
        //     Gets or sets the background color of the toolbar. It is a bindable property
        public Color TextColor { get; set; }
        //
        // Summary:
        //     Gets or sets whether the toolbar needs to be visible or not. It is a bindable
        //     property
        public bool IsVisible { get; set; }
        //
        // Summary:
        //     Gets or sets BackButton of toolbar menu
        public ToolbarItem BackButton { get; set; }
        //
        // Summary:
        //     Gets or sets Height of the footer toolbar.
        public double FooterToolbarHeight { get; set; }
        //
        // Summary:
        //     Gets or sets Height of the header toolbar.
        public double HeaderToolbarHeight { get; set; }
        //
        // Summary:
        //     Gets or sets Height of the SubItem toolbar.
        public double SubItemToolbarHeight { get; set; }
        //
        // Summary:
        //     Gets or sets collection of the toolbarItem value
        public ObservableCollection<ToolbarItem> ToolbarItems { get; set; }

        //
        // Summary:
        //     Represents ToolbarItemSelected event
        //public event ToolbarItemSelectedEventHandler ToolbarItemSelected;
    }
}
