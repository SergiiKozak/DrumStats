using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using DrumStats.Controls;
using DrumStats.Droid.Renderers;

[assembly: ExportRenderer(typeof(FontAwesomeLabel), typeof(FontAwesomeLabelRenderer))]
namespace DrumStats.Droid.Renderers
{
    class FontAwesomeLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            var label = Control;
            Typeface font;
            try
            {
                font = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/FontAwesome.ttf");
                label.Typeface = font;
            }
            catch (Exception)
            {
                System.Diagnostics.Debug.WriteLine("TTF file not found. Make sure the Android project contains it at '/Assets/Fonts/FontAwesome.ttfs'.");
            }

        }

    }
}