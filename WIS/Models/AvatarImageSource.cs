using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;

namespace WIS.Models
{
    public class AvatarImageSource : StreamImageSource
    {
        public AvatarImageSource()
        {

        }
        public AvatarImageSource(string name,Color background,Color foreground,int size)
        {
            Name = name;
            Background = background;
            Foreground = foreground;
            Size = size;
        }

        public override bool IsEmpty => string.IsNullOrEmpty(Name);


        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == NameProperty.PropertyName ||
                propertyName == BackgroundProperty.PropertyName ||
                propertyName == SizeProperty.PropertyName ||
                propertyName == ForegroundProperty.PropertyName)
            {
                OnSourceChanged();
            }
        }

        public override Func<CancellationToken, Task<Stream>> Stream => GetStreamAsync;

        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            "Name", typeof(string), typeof(AvatarImageSource));

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }

        public static readonly BindableProperty BackgroundProperty = BindableProperty.Create(
            "Background", typeof(Color), typeof(AvatarImageSource));

        public Color Background
        {
            get => (Color)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly BindableProperty ForegroundProperty = BindableProperty.Create(
            "Foreground", typeof(Color), typeof(AvatarImageSource));

        public Color Foreground
        {
            get => (Color)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        public static readonly BindableProperty SizeProperty = BindableProperty.Create(
            "Size", typeof(int), typeof(AvatarImageSource));

        public int Size
        {
            get => (int)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        public Task<Stream> GetStreamAsync(CancellationToken userToken = new CancellationToken())
        {
            base.OnLoadingStarted();
            userToken.Register(CancellationTokenSource.Cancel);
            var result = Draw();
            OnLoadingCompleted(CancellationTokenSource.IsCancellationRequested);
            return Task.FromResult(result);

        }



        private Stream Draw()
        {
            var bitmap = new SKBitmap(Size * 2, Size * 2, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            var canvas = new SKCanvas(bitmap);
            canvas.Clear(SKColors.Transparent);

            var midy = canvas.LocalClipBounds.Size.ToSizeI().Height / 2;
            var midx = canvas.LocalClipBounds.Size.ToSizeI().Width / 2;
            var radius = midx - midx / 5;

            var circleFill = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                StrokeJoin = SKStrokeJoin.Miter,
                Color = SKColor.Parse(Background.ToHex())
            };
            canvas.DrawCircle(midx, midy, radius, circleFill);


            var family = SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Normal, SKFontStyleWidth.Normal,
                SKFontStyleSlant.Upright);
            var textSize = midx / 1.5f;
            var paint = new SKPaint
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                Color = SKColor.Parse(Foreground.ToHex()),
                TextSize = textSize,
                TextAlign = SKTextAlign.Center,
                Typeface = family
            };
            var rect = new SKRect();
            paint.MeasureText(Name, ref rect);
            canvas.DrawText(Name, radius + rect.Height / 2, radius + rect.Width / 2, paint);
            var skImage = SKImage.FromBitmap(bitmap);
            var result = (skImage.Encode(SKEncodedImageFormat.Png, 100)).AsStream();
            return result;
        }
    }
}
