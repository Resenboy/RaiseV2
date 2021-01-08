using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Raise.CustomRenderers;
using Raise.Droid.CustomDroid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(DroidRoundedEntry))]
namespace Raise.Droid.CustomDroid
{
    public class DroidRoundedEntry : EntryRenderer
    {
        public RoundedEntry _Element => Element as RoundedEntry;
        public DroidRoundedEntry(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
        }

        protected override FormsEditText CreateNativeControl()
        {
            var control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null)
                return;

            var gd = new GradientDrawable();
            gd.SetShape(ShapeType.Rectangle);
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(_Element.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(_Element.BorderWidth), _Element.BorderColor.ToAndroid());
            control.SetBackground(gd);

            var padTop = (int)Context.ToPixels(_Element.CustomPadding.Top);
            var padBottom = (int)Context.ToPixels(_Element.CustomPadding.Bottom);
            var padLeft = (int)Context.ToPixels(_Element.CustomPadding.Left);
            var padRight = (int)Context.ToPixels(_Element.CustomPadding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == RoundedEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == RoundedEntry.BorderWidthProperty.PropertyName)
            {
                UpdateBackground();
            }
            else if (e.PropertyName == RoundedEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }

        protected void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}