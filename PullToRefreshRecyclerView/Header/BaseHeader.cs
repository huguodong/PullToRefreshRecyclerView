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
using Android.Support.V7.Widget;

namespace PullToRefreshRecyclerView.Header
{
    public class BaseHeader : RecyclerView.ItemDecoration
    {
        protected int mHeaderHeight;

        public void SetHeight(int height)
        {
            mHeaderHeight = height;
        }

        public int GetHeight()
        {
            return mHeaderHeight;
        }
    }
}