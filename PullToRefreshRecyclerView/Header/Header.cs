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

namespace PullToRefreshRecyclerView.Header
{
    public class Header : BaseHeader
    {

        public override void GetItemOffsets(Android.Graphics.Rect outRect, int itemPosition, Android.Support.V7.Widget.RecyclerView parent)
        {
            if (itemPosition == 0)
            {
                outRect.Set(0, mHeaderHeight, 0, 0);
            }
        }
    }
}