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

        private int mProgress = 30;//圆圈比例

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
            //����Ļ�ͼ��ֱҪ������(=.=#)
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
            paint.AntiAlias = true;// �����
            paint.Flags = PaintFlags.AntiAlias;// ��ǿ�������
            paint.Color = Color.Gray;// ����Ϊ��ɫ
            paint.StrokeWidth = 10;// ���ʿ��
            paint.SetStyle(Paint.Style.Stroke);// �п�
            c.DrawCircle((right - left) / 2 - mCircleOffset, bottom, mCircleSize, paint);//������Ϊ��(right - left)/2,bottom���ĵط������뾶ΪmCircleSize��Բ��
            paint.Color = Color.Green;// ���û���Ϊ��ɫ
            oval.Set((right - left) / 2 - mCircleOffset - mCircleSize, bottom - mCircleSize, (right - left) / 2 - mCircleOffset + mCircleSize, bottom + mCircleSize);// ��CircleС��ȦȦ��С�ĵط���Բ������Ҳ�ͱ�֤�˰뾶ΪmCircleSize
            c.DrawArc(oval, -90, ((float)mProgress / 100) * 360, false, paint);// Բ�����ڶ�������Ϊ����ʼ�Ƕȣ�������Ϊ��ĽǶȣ����ĸ�Ϊtrue��ʱ����ʵ�ģ�false��ʱ��Ϊ����
            paint.Reset();// ����������
            paint.StrokeWidth = 3;// �ٴ����û��ʵĿ��
            paint.TextSize = 40;// �������ֵĴ�С
            paint.Color = Color.Black;// ���û�����ɫ
            c.DrawText(mLoadMoreString, (right - left) / 2, bottom + 10, paint);
        }
    }
}