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
            FontFamily = Device.OnPlatform(null, "FontAwesome", "/Assets/Fonts/FontAwesome.ttf#FontAwesome");
        }
    }

}
