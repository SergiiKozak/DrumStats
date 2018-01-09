using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DrumStats.Controls
{
    public class FontAwesomeLabel : Label
    {
        public FontAwesomeLabel()
        {
            switch(Device.RuntimePlatform)
            {
                case Device.UWP:
                    FontFamily = "/Assets/Fonts/FontAwesome.ttf#FontAwesome";
                    break;
                case Device.Android:
                    FontFamily = "FontAwesome";
                    break;
                default:
                    FontFamily = null;
                    break;
            }
        }
    }

}
