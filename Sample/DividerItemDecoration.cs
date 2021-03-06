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
using Android.Graphics.Drawables;
using Android.Content.Res;
using Android.Graphics;

namespace Sample
{
    public class DividerItemDecoration : RecyclerView.ItemDecoration
    {
        private static int[] ATTRS = new int[] { Android.Resource.Attribute.ListDivider };
        public static int HORIZONTAL_LIST = LinearLayoutManager.Horizontal;
        public static int VERTICAL_LIST = LinearLayoutManager.Vertical;
        private Drawable mDivider;
        private int mOrientation;

        public DividerItemDecoration(Context context, int orientation)
        {
            TypedArray a = context.ObtainStyledAttributes(ATTRS);
            mDivider = a.GetDrawable(0);
            a.Recycle();
            SetOrientation(orientation);
        }
        public void SetOrientation(int orientation)
        {
            if (orientation != HORIZONTAL_LIST && orientation != VERTICAL_LIST)
            {
                throw new Java.Lang.IllegalArgumentException("invalid orientation");
            }
            mOrientation = orientation;
        }
        public override void OnDraw(Android.Graphics.Canvas c, RecyclerView parent)
        {
            if (mOrientation == VERTICAL_LIST)
            {
                DrawVertical(c, parent);
            }
            else
            {
                DrawHorizontal(c, parent);
            }
        }
        public void DrawVertical(Canvas c, RecyclerView parent)
        {
            int left = parent.PaddingLeft;
            int right = parent.Width - parent.PaddingRight;

            int childCount = parent.ChildCount;
            for (int i = 0; i < childCount; i++)
            {
                View child = parent.GetChildAt(i);
                RecyclerView v = new RecyclerView(parent.Context);
                RecyclerView.LayoutParams layoutpar = (RecyclerView.LayoutParams)child.LayoutParameters;
                int top = child.Bottom + layoutpar.BottomMargin;
                int bottom = top + mDivider.IntrinsicHeight;
                mDivider.SetBounds(left, top, right, bottom);
                mDivider.Draw(c);
            }
        }
        public void DrawHorizontal(Canvas c, RecyclerView parent)
        {
            int top = parent.PaddingTop;
            int bottom = parent.Height - parent.PaddingBottom;

            int childCount = parent.ChildCount;
            for (int i = 0; i < childCount; i++)
            {
                View child = parent.GetChildAt(i);
                RecyclerView.LayoutParams layoutpar = (RecyclerView.LayoutParams)child.LayoutParameters;
                int left = child.Right + layoutpar.RightMargin;
                int right = left + mDivider.IntrinsicHeight;
                mDivider.SetBounds(left, top, right, bottom);
                mDivider.Draw(c);
            }
        }
        public override void GetItemOffsets(Rect outRect, int itemPosition, RecyclerView parent)
        {
            if (mOrientation == VERTICAL_LIST)
            {
                outRect.Set(0, 0, 0, mDivider.IntrinsicHeight);
            }
            else
            {
                outRect.Set(0, 0, mDivider.IntrinsicWidth, 0);
            }
        }
    }
}