using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS4_PKG_Tool
{
    public class MyCheckedListBox : CheckedListBox
    {
        private SolidBrush primaryColor = new SolidBrush(Color.FromArgb(69, 73, 74));
        private SolidBrush alternateColor = new SolidBrush(Color.FromArgb(60, 63, 65));
        private SolidBrush HighLightedRow = new SolidBrush(Color.FromArgb(57, 60, 62));

        public MyCheckedListBox()
        {

            this.SetStyle(ControlStyles.Selectable, false);
        }

        [Browsable(true)]
        public Color PrimaryColor
        {
            get { return primaryColor.Color; }
            set { primaryColor.Color = value; }
        }

        [Browsable(true)]
        public Color AlternateColor
        {
            get { return alternateColor.Color; }
            set { alternateColor.Color = value; }
        }


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            if (Items.Count <= 0)
                return;

            var contentRect = e.Bounds;
            contentRect.X = 16;
            e.Graphics.FillRectangle(e.Index % 2 == 0 ? primaryColor : HighLightedRow, contentRect);
            e.Graphics.DrawString(Convert.ToString(Items[e.Index]), e.Font, Brushes.Gainsboro, contentRect);
        }
    }
}
