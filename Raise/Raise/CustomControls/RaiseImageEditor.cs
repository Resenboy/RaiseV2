using Raise.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Raise.CustomControls
{
    public class RaiseImageEditor : View
    {
        //
        // Summary:
        //     Gets or sets the Rotatable elements value. It is a bindable property
        public static readonly BindableProperty RotatableElementsProperty;
        //
        // Summary:
        //     Gets or sets the value for image effects. It is a bindable property
        public static readonly BindableProperty EffectValueProperty;
        //
        // Summary:
        //     Gets or sets the original image size value. It is a bindable property
        public static readonly BindableProperty OriginalImageSizeProperty;
        //
        // Summary:
        //     Gets or sets the ActualImageRendererBounds value. It is a bindable property
        public static readonly BindableProperty ActualImageRenderedBoundsProperty;
        //
        // Summary:
        //     Gets or sets the value for panning mode. It is a bindable property
        public static readonly BindableProperty PanningModeProperty;
        //
        // Summary:
        //     Gets or sets the ToolbarSetting value. It is a bindable property
        public static readonly BindableProperty ToolbarSettingsProperty;
        //
        // Summary:
        //     Gets or sets the type of image effects. It is a bindable property
        public static readonly BindableProperty ImageEffectProperty;
        //
        // Summary:
        //     Gets or sets the MaximumZoomLevel for Zooming. It is a bindable property
        public static readonly BindableProperty MaximumZoomLevelProperty;
        //
        // Summary:
        //     Gets or sets the value for whether edit text selection is enabled or not.
        public static readonly BindableProperty EnableAutoSelectTextProperty;
        //
        // Summary:
        //     Gets or sets the value for whether zooming is enabled or not. It is a bindable
        //     property
        public static readonly BindableProperty EnableZoomingProperty;
        //
        // Summary:
        //     Gets or sets the ColorPalette collection value. It is a bindable property
        public static readonly BindableProperty ColorPaletteProperty;
        //
        // Summary:
        //     Gets or sets the image source value. It is a bindable property
        public static readonly BindableProperty SourceProperty;
        //
        // Summary:
        //     Gets or sets the DefaultSelectedColorIndex value.
        public static readonly BindableProperty DefaultSelectedColorIndexProperty;

        //
        // Summary:
        //     Initializes a new instance of the Syncfusion.SfImageEditor.XForms class.
        public RaiseImageEditor()
        {

        }

        //
        // Summary:
        //     To check whether any annotations are selected. It is a read-only property
        //
        // Value:
        //     This property takes the System.Boolean as value
        public bool IsSelected { get; }
        //
        // Summary:
        //     Gets/sets ColorPalette collection value
        public ObservableCollection<Color> ColorPalette { get; set; }
        //
        // Summary:
        //     To define whether to enable image zooming or not
        //
        // Value:
        //     This property takes the System.Boolean as value
        public bool EnableZooming { get; set; }
        //
        // Summary:
        //     To define whether to enable selection of edit text in Text preview dialog
        //
        // Value:
        //     This property takes the System.Boolean as value
        public bool EnableAutoSelectText { get; set; }
        //
        // Summary:
        //     Gets or sets the maximumZoomLevel of the Zooming Image. It is a bindable property
        //
        // Value:
        //     This property takes the System.Single as value
        public float MaximumZoomLevel { get; set; }
        //
        // Summary:
        //     Gets or Sets the DefaultSelectedColorIndex value
        //
        // Value:
        //     This property takes the System.Int32 as value
        public int DefaultSelectedColorIndex { get; set; }
        //
        // Summary:
        //     To customize the toolbar appearance with the help of ToolbarSettings properties.
        //
        // Value:
        //     This property takes the Syncfusion.SfImageEditor.XForms.SfImageEditor.ToolbarSettings
        //     as value
        public ToolbarSettings ToolbarSettings { get; set; }
        //
        // Summary:
        //     To define whether the PanningMode is SingleFinger or TwoFinger
        //
        // Value:
        //     This property takes the Syncfusion.SfImageEditor.XForms.SfImageEditor.PanningMode
        //     as value
        public PanningMode PanningMode { get; set; }
        //
        // Summary:
        //     To get the Actual image rect. It is read-only property
        public Rectangle ActualImageRenderedBounds { get; }
        //
        // Summary:
        //     Gets or sets the Rotatable elements value
        //public ImageEditorElements RotatableElements { get; set; }
        //
        // Summary:
        //     To check whether any editing has been performed in the image. It is a read-only
        //     property
        //
        // Value:
        //     This property takes the System.Boolean as value
        public bool IsImageEdited { get; }
        //
        // Summary:
        //     Gets or sets the effects of the Image. The default effects are Hue,Saturation,Contrast,Brightness,Sharpen,Blur.
        //public ImageEffect ImageEffect { get; set; }
        //
        // Summary:
        //     Gets or sets the value of Image effects.
        public float EffectValue { get; set; }
        //
        // Summary:
        //     To get and set source of the image
        //
        // Value:
        //     This property takes the Xamarin.Forms.ImageSource as value
        public ImageSource Source { get; set; }
        //
        // Summary:
        //     To get the original image size. It is read-only property
        public Size OriginalImageSize { get; }

        //
        // Summary:
        //     Represents BeginReset event
        public event BeginResetEventHandler BeginReset;
        public delegate void BeginResetEventHandler(object sender, EventArgs e);

        //
        // Summary:
        //     Represent EndReset event
        //public event EndResetEventHandler EndReset;
        ////
        //// Summary:
        ////     Represent ImageSaved event
        //public event ImageSavedEventHandler ImageSaved;
        ////
        //// Summary:
        ////     Represent ItemSelected event
        //public event ItemSelectedEventHandler ItemSelected;
        ////
        //// Summary:
        ////     Represent ItemUnselected event
        //public event ItemUnselectedEventHandler ItemUnselected;
        ////
        //// Summary:
        ////     Represent ImageSaving event
        //public event ImageSavingEventHandler ImageSaving;
        ////
        //// Summary:
        ////     The event raised when the Image is Edited
        //public event EventHandler<ImageEditedEventArgs> ImageEdited;
        ////
        //// Summary:
        ////     Represents ImageLoaded event
        //public event ImageLoadedEventHandler ImageLoaded;

        ////
        //// Summary:
        ////     Adds custom view as annotations over the image.
        //public void AddCustomView(object customView, CustomViewSettings customViewSettings = null);
        ////
        //// Summary:
        ////     Adds shapes as annotations over the image.
        ////
        //// Parameters:
        ////   shapeType:
        ////     ShapeType is optional. Default ShapeType is Path
        ////
        ////   settings:
        ////     PenSettings is optional. If null, default PenSettings is applied
        //public void AddShape(ShapeType shapeType = ShapeType.Path, PenSettings settings = null);
        ////
        //// Summary:
        ////     Adds text as annotation on top of the image
        ////
        //// Parameters:
        ////   text:
        ////     text is optional. If empty, empty string will be added
        ////
        ////   settings:
        ////     TextSettings is optional. If null, default TextSettings is applied
        //public void AddText(string text = "", TextSettings settings = null);
        ////
        //// Summary:
        ////     To apply the image effects without using the toolbar.
        //public void ApplyImageEffect(ImageEffect imageEffects, int effectValue);
        ////
        //// Summary:
        ////     To bring the selected object to one step front of a group of elements on image
        //public void BringForward();
        ////
        //// Summary:
        ////     To bring the selected object to the front of a group of elements on image.
        //public void BringToFront();
        ////
        //// Summary:
        ////     Crops the image to the selected/specified rectangle
        ////
        //// Parameters:
        ////   rect:
        ////     Rectangle is an optional parameter
        //public void Crop(Rectangle rect = default);
        ////
        //// Summary:
        ////     Deletes the selected shape on the image
        //public void Delete();
        ////
        //// Summary:
        ////     Flips the image horizontally/ vertically
        ////
        //// Parameters:
        ////   direction:
        ////     Specfies the FlipDirection
        //public void Flip(FlipDirection direction);
        ////
        //// Summary:
        ////     Gets the current image stream. Obtained only after saving.
        ////
        //// Returns:
        ////     A Stream
        //public Task<Stream> GetStream();
        ////
        //// Summary:
        ////     LoadEdits() method used to deserialize the shapes.
        //public void LoadEdits(Stream str);
        ////
        //// Summary:
        ////     Reverts the latest Undo operation
        //public void Redo();
        ////
        //// Summary:
        ////     Resets the changes and initial image is loaded
        //public void Reset();
        ////
        //// Summary:
        ////     Rotates the image to 90 degrees
        //public void Rotate();
        ////
        //// Summary:
        ////     Saves the image to a location
        //public void Save(string format = null, Size size = default);
        ////
        //// Summary:
        ////     SaveEdits() method used to serialize the current edits of shapes. Serialized
        ////     object will be return in the form of JSON stream.
        //public Stream SaveEdits();
        ////
        //// Summary:
        ////     To send the selected object to one step backward of a group of elements on image.
        //public void SendBackward();
        ////
        //// Summary:
        ////     To send the selected object to the Back of a group of elements on image.
        //public void SendToBack();
        ////
        //// Summary:
        ////     To set the toolbar icon visibility based on the icon name
        ////
        //// Parameters:
        ////   name:
        ////     The icon name to be hidden
        ////
        ////   isVisible:
        ////     Set the visibility
        //public void SetToolbarItemVisibility(string name, bool isVisible);
        ////
        //// Summary:
        ////     To crop the image based on Custom Rectangle value
        //public void ToggleCropping(Rectangle rectangle);
        ////
        //// Summary:
        ////     To crop the image based on X and Y aspect ratio
        //public void ToggleCropping(float xRatio, float yRatio);
        ////
        //// Summary:
        ////     Enables / disables the cropping view which helps to choose the area to crop
        //public void ToggleCropping();
        ////
        //// Summary:
        ////     Reverts the latest change done in the image
        //public void Undo();
        ////
        //// Summary:
        ////     override method
        //protected override void OnBindingContextChanged();
        ////
        //// Summary:
        ////     Method used to measure the ImageEditor.
        ////
        //// Parameters:
        ////   widthConstraint:
        ////     Width of the ImageEditor.
        ////
        ////   heightConstraint:
        ////     Height of the ImageEditor.
        ////
        //// Returns:
        ////     Actual size of the ImageEditor.
        //protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint);
    }
}
