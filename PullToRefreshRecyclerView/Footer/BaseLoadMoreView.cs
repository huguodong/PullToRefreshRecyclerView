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
using PullToRefreshRecyclerView.Util;
using Android.Graphics;

namespace PullToRefreshRecyclerView.Footer
{
   public class BaseLoadMoreView: RecyclerView.ItemDecoration
    {
        protected RecyclerView mRecyclerView;
        protected String mLoadMoreString;
        protected static int MSG_INVILIDATE = 1;
        protected long mUpdateTime = 150;
        protected PullToRefreshRecyclerViewUtil mPtrrvUtil;
        protected int mLoadMorePadding = 100;
        protected Handler mInvalidateHanlder;
        public BaseLoadMoreView(Context context, RecyclerView recyclerView)
        {
            mRecyclerView = recyclerView;
            mPtrrvUtil = new PullToRefreshRecyclerViewUtil();
            mInvalidateHanlder = new Handler(HandleMessage);
        }
        public void SetLoadmoreString(String str)
        {
            mLoadMoreString = str;
        }
      
        public int GetLoadMorePadding()
        {
            return mLoadMorePadding;
        }
        public void SetLoadMorePadding(int padding)
        {
            mLoadMorePadding = padding;
        }
        public void HandleMessage(Message msg)
        {
            if (mRecyclerView == null || mRecyclerView.GetAdapter() == null)
            {
                return;
            }
            int lastItemPosition = mRecyclerView.GetAdapter().ItemCount - 1;
            if (mPtrrvUtil.FindLastVisibleItemPosition(mRecyclerView.GetLayoutManager()) == lastItemPosition)
            {
                //when the item is visiable do this method
                //                    View view = mRecyclerView.getLayoutManager().findViewByPosition(lastItemPosition);
                //                    mInvilidateRect.set(0, 0, view.getRight() - view.getLeft(), view.getBottom() - view.getTop());
                mRecyclerView.Invalidate();

            }
        }

        public override void OnDrawOver(Android.Graphics.Canvas c, RecyclerView parent, RecyclerView.State state)
        {
            base.OnDrawOver(c, parent, state);
            mInvalidateHanlder.RemoveMessages(MSG_INVILIDATE);
            OnDrawLoadMore(c, parent);
            mInvalidateHanlder.SendEmptyMessageDelayed(MSG_INVILIDATE, mUpdateTime);
        }

        public override void GetItemOffsets(Rect outRect, int itemPosition, RecyclerView parent)
        {
            if (itemPosition == parent.GetAdapter().ItemCount - 1)
            {
                outRect.Set(0, 0, 0, GetLoadMorePadding());
            }
        }
        public virtual void OnDrawLoadMore(Canvas c, RecyclerView parent)
        { }
        public void Release()
        {
            mRecyclerView = null;
        }
    }
}