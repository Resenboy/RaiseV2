using Xamarin.Forms;

namespace Raise.CustomRenderers
{
    public class RoundedEntry : Entry
    {
        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }
        public static readonly BindableProperty BorderColorProperty =
            BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(RoundedEntry), defaultValue: Color.Gray);

        public int BorderWidth
        {
            get => (int)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(BorderWidth), typeof(int), typeof(RoundedEntry), defaultValue: 1);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(RoundedEntry), defaultValue: 0);

        public bool IsCurvedCornersEnabled
        {
            get => (bool)GetValue(IsCurvedCornersEnabledProperty);
            set => SetValue(IsCurvedCornersEnabledProperty, value);
        }
        public static readonly BindableProperty IsCurvedCornersEnabledProperty =
            BindableProperty.Create(nameof(IsCurvedCornersEnabled), typeof(bool), typeof(RoundedEntry), false);

        public Thickness CustomPadding
        {
            get => (Thickness)GetValue(CustomPaddingProperty);
            set => SetValue(CustomPaddingProperty, value);
        }
        public static readonly BindableProperty CustomPaddingProperty =
            BindableProperty.Create(nameof(CustomPadding), typeof(Thickness), typeof(RoundedEntry));
    }
}
