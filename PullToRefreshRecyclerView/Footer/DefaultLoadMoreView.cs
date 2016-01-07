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
using Android.Graphics;

namespace PullToRefreshRecyclerView.Footer
{
  public  class DefaultLoadMoreView: BaseLoadMoreView
    {
        private Paint paint;
        private RectF oval;

        private int mCircleSize = 25;

        private int mProgress = 30;//鍦嗗湀姣斾緥

        private int mCircleOffset = 70;

        public DefaultLoadMoreView(Context context, RecyclerView recyclerView)
            : base(context, recyclerView)
        {
            paint = new Paint();
            oval = new RectF();
            mLoadMoreString = context.GetString(Resource.String.loading);
        }
        public override void OnDrawLoadMore(Canvas c, RecyclerView parent)
        {
            //这里的画图简直要画死人(=.=#)
            mProgress = mProgress + 5;
            if (mProgress == 100)
            {
                mProgress = 0;
            }
            int left = parent.PaddingLeft;
            int right = parent.MeasuredWidth - parent.PaddingRight;
            int childSize = parent.ChildCount;
            View child = parent.GetChildAt(childSize - 1);
            RecyclerView.LayoutParams layoutParams = (RecyclerView.LayoutParams)child.LayoutParameters;
            int top = child.Bottom + layoutParams.BottomMargin;
            int bottom = top + GetLoadMorePadding() / 2;
            paint.AntiAlias = true;// 抗锯齿
            paint.Flags = PaintFlags.AntiAlias;// 增强消除锯齿
            paint.Color = Color.Gray;// 画笔为灰色
            paint.StrokeWidth = 10;// 画笔宽度
            paint.SetStyle(Paint.Style.Stroke);// 中空
            c.DrawCircle((right - left) / 2 - mCircleOffset, bottom, mCircleSize, paint);//在中心为（(right - left)/2,bottom）的地方画个半径为mCircleSize的圆，
            paint.Color = Color.Green;// 设置画笔为绿色
            oval.Set((right - left) / 2 - mCircleOffset - mCircleSize, bottom - mCircleSize, (right - left) / 2 - mCircleOffset + mCircleSize, bottom + mCircleSize);// 在Circle小于圈圈大小的地方画圆，这样也就保证了半径为mCircleSize
            c.DrawArc(oval, -90, ((float)mProgress / 100) * 360, false, paint);// 圆弧，第二个参数为：起始角度，第三个为跨的角度，第四个为true的时候是实心，false的时候为空心
            paint.Reset();// 将画笔重置
            paint.StrokeWidth = 3;// 再次设置画笔的宽度
            paint.TextSize = 40;// 设置文字的大小
            paint.Color = Color.Black;// 设置画笔颜色
            c.DrawText(mLoadMoreString, (right - left) / 2, bottom + 10, paint);
        }
    }
}