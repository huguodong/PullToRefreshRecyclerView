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

namespace PullToRefreshRecyclerView.Util
{
    public class PullToRefreshRecyclerViewUtil
    {
        public int FindLastVisibleItemPosition(RecyclerView.LayoutManager layoutManager)
        {
            if (layoutManager != null)
            {

                if (layoutManager is LinearLayoutManager)
                {
                    return ((LinearLayoutManager)layoutManager).FindLastVisibleItemPosition();
                }

                if (layoutManager is GridLayoutManager)
                {
                    return ((GridLayoutManager)layoutManager).FindLastVisibleItemPosition();
                }

            }
            return RecyclerView.NoPosition;
        }

        public int FindFirstCompletelyVisibleItemPosition(RecyclerView.LayoutManager layoutManager)
        {
            if (layoutManager != null)
            {

                if (layoutManager is LinearLayoutManager)
                {
                    return ((LinearLayoutManager)layoutManager).FindFirstCompletelyVisibleItemPosition();
                }

                if (layoutManager is GridLayoutManager)
                {
                    return ((GridLayoutManager)layoutManager).FindFirstCompletelyVisibleItemPosition();
                }

            }
            return RecyclerView.NoPosition;
        }

        public int FindFirstVisibleItemPosition(RecyclerView.LayoutManager layoutManager)
        {
            if (layoutManager != null)
            {

                if (layoutManager is LinearLayoutManager)
                {
                    return ((LinearLayoutManager)layoutManager).FindFirstVisibleItemPosition();
                }

                if (layoutManager is GridLayoutManager)
                {
                    return ((GridLayoutManager)layoutManager).FindFirstVisibleItemPosition();
                }

            }
            return RecyclerView.NoPosition;
        }
    }
}