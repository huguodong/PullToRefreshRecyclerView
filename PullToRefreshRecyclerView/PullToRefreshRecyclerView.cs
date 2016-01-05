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
using Android.Support.V4.Widget;
using PullToRefreshRecyclerView.Impl;
using PullToRefreshRecyclerView.Footer;
using PullToRefreshRecyclerView.Util;
using Android.Util;
using Xamarin.NineOldAndroids.Views;

namespace PullToRefreshRecyclerView
{
    public class PullToRefreshRecyclerView : SwipeRefreshLayout, PrvInterface, ViewTreeObserver.IOnGlobalLayoutListener
    {
        #region

        private RecyclerView mRecyclerView;
        //root header
        private Header.Header mRootHeader;
        //main view,contain footer£¬header etc.
        private RelativeLayout mRootRelativeLayout;
        //header
        private View mHeader;
        private View mEmptyView;
        //default = 10
        private int mLoadMoreCount = 10;
        private static int mCurScroll;
        private Boolean mIsSwipeEnable = false;

        private Context mContext;

        private BaseFooter mLoadMoreFooter;

        private PagingableListener mPagingableListener;

        private AdapterObserver mAdapterObserver;

        private Boolean isLoading = false;
        private Boolean hasMoreItems = false;

        private static PullToRefreshRecyclerView.OnScrollListener mOnScrollLinstener;

        private InterOnScrollListener mInterOnScrollListener;
        public static PullToRefreshRecyclerView PULL;
        private PullToRefreshRecyclerViewUtil mPtrrvUtil;
        #endregion
        public interface PagingableListener
        {
            void OnLoadMoreItems();
        }
        public interface OnScrollListener
        {
            void OnScrollStateChanged(RecyclerView recyclerView, int newState);
            void OnScrolled(RecyclerView recyclerView, int dx, int dy);

            //old-method, like listview 's onScroll ,but it's no use ,right ? by linhonghong 2015.10.29
            void OnScroll(RecyclerView recyclerView, int firstVisibleItem, int visibleItemCount, int totalItemCount);
        }
        public PullToRefreshRecyclerView(Context context)
            : base(context)
        {
            this.Setup(context);
        }

        public PullToRefreshRecyclerView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Setup(context);

        }
        private void Setup(Context context)
        {
            PULL = this;
            SetupExtra(context);
            InitView();
            SetLinster();
        }
        private void InitView()
        {
            mRootRelativeLayout = (RelativeLayout)LayoutInflater.From(mContext).Inflate(Resource.Layout.ptrrv_root_view, null);

            this.AddView(mRootRelativeLayout);

            this.SetColorSchemeResources(Resource.Color.swap_holo_green_bright, Resource.Color.swap_holo_bule_bright,
                    Resource.Color.swap_holo_green_bright, Resource.Color.swap_holo_bule_bright);

            mRecyclerView = (RecyclerView)mRootRelativeLayout.FindViewById(Resource.Id.recycler_view);

            //        mLinearLayoutManager = new LinearLayoutManager(mContext);
            //        mRecyclerView.setLayoutManager(mLinearLayoutManager);
            mRecyclerView.HasFixedSize = true;

            if (!mIsSwipeEnable)
            {
                this.Enabled = false;
            }

        }
        private void SetupExtra(Context context)
        {
            mContext = context;
            isLoading = false;
            hasMoreItems = false;
            mPtrrvUtil = new PullToRefreshRecyclerViewUtil();
        }
        private void SetLinster()
        {
            mInterOnScrollListener = new InterOnScrollListener();
            mRecyclerView.AddOnScrollListener(mInterOnScrollListener);
        }

        public void SetOnRefreshComplete()
        {
            this.Refreshing = false;
        }

        public void SetOnLoadMoreComplete()
        {
            SetHasMoreItems(false);
        }

        public void SetPagingableListener(PullToRefreshRecyclerView.PagingableListener pagingableListener)
        {
            mPagingableListener = pagingableListener;
        }

        public void SetEmptyView(View emptyView)
        {
            mEmptyView = emptyView;
        }

        public void SetAdapter(RecyclerView.Adapter adapter)
        {
            mRecyclerView.SetAdapter(adapter);
            if (mAdapterObserver == null)
            {
                mAdapterObserver = new AdapterObserver();
            }
            if (adapter != null)
            {
                adapter.RegisterAdapterDataObserver(mAdapterObserver);
                mAdapterObserver.OnChanged();
            }
        }

        public void AddHeaderView(View view)
        {
            //2015.11.17 finish method
            if (mHeader != null)
            {
                mRootRelativeLayout.RemoveView(mHeader);
            }

            mHeader = view;

            if (mHeader == null)
            {
                return;
            }
            mHeader.ViewTreeObserver.AddOnGlobalLayoutListener(this);
            mRootRelativeLayout.AddView(mHeader);
        }

        public void SetFooter(View view)
        {

        }

        public void AddOnScrollListener(PullToRefreshRecyclerView.OnScrollListener onScrollLinstener)
        {
            mOnScrollLinstener = onScrollLinstener;
        }

        public void OnFinishLoading(bool hasMoreItems, bool needSetSelection)
        {
            if (GetLayoutManager() == null)
            {
                return;
            }
            if (!hasMoreItems && mLoadMoreFooter != null)
            {

                //if it's last line, minus the extra height of loadmore
                mCurScroll = mCurScroll - mLoadMoreFooter.GetLoadMorePadding();

            }

            // if items is too short, don't show loadingview
            if (GetLayoutManager().ItemCount < mLoadMoreCount)
            {

                hasMoreItems = false;

            }

            SetHasMoreItems(hasMoreItems);

            isLoading = false;

            if (needSetSelection)
            {
                int first = FindFirstVisibleItemPosition();
                mRecyclerView.ScrollToPosition(--first);
            }
        }
        public int FindFirstVisibleItemPosition()
        {
            return mPtrrvUtil.FindFirstVisibleItemPosition(GetLayoutManager());
        }

        public int FindLastVisibleItemPosition()
        {
            return mPtrrvUtil.FindLastVisibleItemPosition(GetLayoutManager());
        }

        public int FindFirstCompletelyVisibleItemPosition()
        {
            return mPtrrvUtil.FindFirstCompletelyVisibleItemPosition(GetLayoutManager());
        }

        public void SetSwipeEnable(bool enable)
        {
            mIsSwipeEnable = enable;
            this.Enabled = mIsSwipeEnable;
        }

        public bool IsSwipeEnable()
        {
            return mIsSwipeEnable;
        }

        public RecyclerView GetRecyclerView()
        {
            return this.mRecyclerView;
        }

        public void SetLayoutManager(RecyclerView.LayoutManager layoutManager)
        {
            if (mRecyclerView != null)
            {
                mRecyclerView.SetLayoutManager(layoutManager);
            }
        }

        public void SetLoadMoreCount(int count)
        {
            mLoadMoreCount = count;
        }

        public void Release()
        {

        }
        public void OnGlobalLayout()
        {
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.Build.VERSION_CODES.JellyBean)
            {
                mHeader.ViewTreeObserver.RemoveOnGlobalLayoutListener(this);
            }
            else
            {
                mHeader.ViewTreeObserver.RemoveGlobalOnLayoutListener(this);
            }

            if (GetRecyclerView() == null || mHeader == null)
            {
                return;
            }
            if (mRootHeader == null)
            {
                mRootHeader = new Header.Header();
            }
            mRootHeader.SetHeight(mHeader.MeasuredHeight);
            GetRecyclerView().RemoveItemDecoration(mRootHeader);
            GetRecyclerView().AddItemDecoration(mRootHeader);
            GetRecyclerView().GetAdapter().NotifyDataSetChanged();
        }
        public void SetLoadmoreString(String str)
        {
            if (mLoadMoreFooter != null)
            {
                mLoadMoreFooter.SetLoadmoreString(str);
            }
        }
        private void SetHasMoreItems(Boolean hasMoreItems)
        {
            this.hasMoreItems = hasMoreItems;
            if (mLoadMoreFooter == null)
            {
                mLoadMoreFooter = new LoadMoreFooter(mContext, GetRecyclerView());
            }
            if (!this.hasMoreItems)
            {
                //remove loadmore
                mRecyclerView.RemoveItemDecoration(mLoadMoreFooter);
            }
            else
            {
                //add loadmore
                mRecyclerView.RemoveItemDecoration(mLoadMoreFooter);
                mRecyclerView.AddItemDecoration(mLoadMoreFooter);
            }
            mRecyclerView.GetAdapter().NotifyDataSetChanged();
        }
        public RecyclerView.LayoutManager GetLayoutManager()
        {
            if (mRecyclerView != null)
            {
                return mRecyclerView.GetLayoutManager();
            }
            return null;
        }
        private class InterOnScrollListener : RecyclerView.OnScrollListener
        {
            public override void OnScrollStateChanged(RecyclerView recyclerView, int newState)
            {
                base.OnScrollStateChanged(recyclerView, newState);
                if (mOnScrollLinstener != null)
                {
                    mOnScrollLinstener.OnScrollStateChanged(recyclerView, newState);
                }
            }
            public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
            {
                base.OnScrolled(recyclerView, dx, dy);
                if (PULL.GetLayoutManager() == null)
                {
                    //here layoutManager is null
                    return;
                }

                mCurScroll = dy + mCurScroll;
                if (PULL.mHeader != null)
                {
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.Build.VERSION_CODES.Honeycomb)
                    {
                        PULL.mHeader.TranslationY = -mCurScroll;
                    }
                    else
                    {
                        ViewHelper.SetTranslationY(PULL.mHeader, -mCurScroll);
                    }
                }

                int firstVisibleItem, visibleItemCount, totalItemCount, lastVisibleItem;
                visibleItemCount = PULL.GetLayoutManager().ChildCount;
                totalItemCount = PULL.GetLayoutManager().ItemCount;
                firstVisibleItem = PULL.FindFirstVisibleItemPosition();
                //sometimes ,the last item is too big so as that the screen cannot show the item fully
                lastVisibleItem = PULL.FindLastVisibleItemPosition();
                //            lastVisibleItem = mLinearLayoutManager.findLastCompletelyVisibleItemPosition();

                if (PULL.mIsSwipeEnable)
                {
                    if (PULL.FindFirstCompletelyVisibleItemPosition() != 0)
                    {
                        //here has a bug, if the item is too big , use findFirstCompletelyVisibleItemPosition will cannot swipe
                        PULL.Enabled = false;
                    }
                    else
                    {
                        PULL.Enabled = true;
                    }
                }

                if (totalItemCount < PULL.mLoadMoreCount)
                {
                    PULL.SetHasMoreItems(false);
                    PULL.isLoading = false;
                }
                else if (!PULL.isLoading && PULL.hasMoreItems && ((lastVisibleItem + 1) == totalItemCount))
                {
                    if (PULL.mPagingableListener != null)
                    {
                        PULL.isLoading = true;
                        PULL.mPagingableListener.OnLoadMoreItems();
                    }

                }

                if (mOnScrollLinstener != null)
                {
                    mOnScrollLinstener.OnScrolled(recyclerView, dx, dy);
                    mOnScrollLinstener.OnScroll(recyclerView, firstVisibleItem, visibleItemCount, totalItemCount);
                }
            }
        }
        private class AdapterObserver : RecyclerView.AdapterDataObserver
        {
            public override void OnChanged()
            {
                base.OnChanged();
                if (PULL.mRecyclerView == null)
                {
                    //here must be wrong ,recyclerView is null????
                    return;
                }

                RecyclerView.Adapter adapter = PULL.mRecyclerView.GetAdapter();
                if (adapter != null && PULL.mEmptyView != null)
                {
                    if (adapter.ItemCount == 0)
                    {
                        if (PULL.mIsSwipeEnable)
                        {
                            PULL.Enabled = false;
                        }
                        PULL.mEmptyView.Visibility = ViewStates.Visible;
                        PULL.mRecyclerView.Visibility = ViewStates.Gone;
                    }
                    else
                    {
                        if (PULL.mIsSwipeEnable)
                        {
                            PULL.Enabled = true;
                        }
                        PULL.mEmptyView.Visibility = ViewStates.Gone;
                        PULL.mRecyclerView.Visibility = ViewStates.Visible;
                    }

                }
            }
        }


       
    }
}