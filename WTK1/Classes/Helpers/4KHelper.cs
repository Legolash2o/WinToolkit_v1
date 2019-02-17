using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinToolkit.Classes.Helpers
{
    public static class _4KHelper
    {
        public static int currentDPI;
        public static void Scale4K(this SplitContainer container, Panel panel)
        {
            float scale = 1f;
            if (cOptions.ScaleOptions == 0)
            {
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                if (screenHeight > 1080)
                    scale = (float)screenHeight / 1080;
            }
            else if (cOptions.ScaleOptions == 1)
            {
                scale = 1;
            }
            else
            {
                scale = cOptions.ScaleOptions;
            }

            if (panel == Panel.Pan1)
                container.Panel1MinSize = container.Panel1MinSize * (int)scale;

            if (panel == Panel.Pan2)
                container.Panel2MinSize = container.Panel2MinSize * (int)scale;
        }

        public static void TestScale4K(this SplitContainer container, int testScale)
        {
            float scale = 1f;
            if (testScale == 0)
            {
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                if (screenHeight > 1080)
                    scale = (float)screenHeight / 1080;
            }
            else if (testScale == 1)
            {
                scale = 1;
            }
            else
            {
                scale = testScale;
            }

         
            if (container.FixedPanel == FixedPanel.Panel1)
                container.Panel1MinSize = container.Panel1MinSize * (int)scale;

            if (container.FixedPanel == FixedPanel.Panel2)
                container.Panel2MinSize = container.Panel2MinSize * (int)scale;
        }


        public enum Panel
        {
            Pan1,
            Pan2
        }

        public static void AutoSize(this SplitterPanel panel)
        {
            panel.AutoSizeMode = AutoSizeMode.GrowOnly;
            panel.AutoSize = true;

        }
    }
}
