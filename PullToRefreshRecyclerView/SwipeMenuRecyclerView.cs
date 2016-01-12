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
using Android.Views.Animations;
using Android.Support.V4.View;

namespace PullToRefreshRecyclerView
{
    public class SwipeMenuRecyclerView : SwipeRefreshLayout, PrvInterface, ViewTreeObserver.IOnGlobalLayoutListener
    {
        #region

        private RecyclerView mRecyclerView;
        //root header
        private Header.Header mRootHeader;
        //main view,contain footer，header etc.
        private RelativeLayout mRootRelativeLayout;
        //header
        private View mHeader;
        private View mEmptyView;
        //default = 10
        private int mLoadMoreCount = 10;
        private static int mCurScroll;
        private Boolean mIsSwipeEnable = false;

        private Context mContext;

        private BaseLoadMoreView mLoadMoreFooter;

        private PagingableListener mPagingableListener;

        private AdapterObserver mAdapterObserver;

        private Boolean isLoading = false;
        private Boolean hasMoreItems = false;

        private static PullToRefreshRecyclerView.OnScrollListener mOnScrollLinstener;

        private InterOnScrollListener mInterOnScrollListener;
        public static SwipeMenuRecyclerView PULL;
        private PullToRefreshRecyclerViewUtil mPtrrvUtil;
        #endregion
        #region
        public static int TOUCH_STATE_NONE = 0;
        public static int TOUCH_STATE_X = 1;
        public static int TOUCH_STATE_Y = 2;

        public static int DIRECTION_LEFT = 1;
        public static int DIRECTION_RIGHT = -1;
        protected int mDirection = DIRECTION_LEFT; // swipe from right to left by default

        protected float mDownX;
        protected float mDownY;
        protected int mTouchState;
        protected int mTouchPosition;
        protected SwipeMenuLayout mTouchView;
        protected OnSwipeListener mOnSwipeListener;

        protected IInterpolator mCloseInterpolator;
        protected IInterpolator mOpenInterpolator;

        protected RecyclerView.LayoutManager mLlm;
        protected ViewConfiguration mViewConfiguration;
        protected long startClickTime;
        protected float dx;
        protected float dy;

        #endregion

        public interface OnScrollListener
        {
            void OnScrollStateChanged(RecyclerView recyclerView, int newState);
            void OnScrolled(RecyclerView recyclerView, int dx, int dy);

            //old-method, like listview 's onScroll ,but it's no use ,right ? by linhonghong 2015.10.29
            void OnScroll(RecyclerView recyclerView, int firstVisibleItem, int visibleItemCount, int totalItemCount);
        }
        public SwipeMenuRecyclerView(Context context)
            : base(context)
        {
            this.Setup(context);
            Init();
        }

        public SwipeMenuRecyclerView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Setup(context);
            Init();
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
            SetHasMoreItems(false,false);
        }

        public void SetPagingableListener(PagingableListener pagingableListener)
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
        
        /// <param name="delete">是否删除操作</param>
        public void OnFinishLoading(bool hasMoreItems, bool needSetSelection, bool delete = false)
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
            if (delete)//判断是否删除操作
            {
                SetHasMoreItems(hasMoreItems,true);
            }
            else
            {
                SetHasMoreItems(hasMoreItems,false);
            }


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
                mLlm = layoutManager;
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
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.JellyBean)
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
        private void SetHasMoreItems(Boolean hasMoreItems, bool delete)
        {
            this.hasMoreItems = hasMoreItems;
            if (mLoadMoreFooter == null)
            {
                mLoadMoreFooter = new DefaultLoadMoreView(mContext, GetRecyclerView());
            }
            if (!this.hasMoreItems)
            {
                //remove loadmore
                mRecyclerView.RemoveItemDecoration(mLoadMoreFooter);
            }
            else
            {
                if (delete)//如果是删除操作就移除加载更多动画
                {
                    mRecyclerView.RemoveItemDecoration(mLoadMoreFooter);
                }
                else
                {
                    //add loadmore
                    mRecyclerView.RemoveItemDecoration(mLoadMoreFooter);
                    mRecyclerView.AddItemDecoration(mLoadMoreFooter);
                }  
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

        public void SetLoadMoreFooter(BaseLoadMoreView loadMoreFooter)
        {
            mLoadMoreFooter = loadMoreFooter;
        }

        public void RemoveHeader()
        {
            if (mRootHeader != null)
            {
                GetRecyclerView().RemoveItemDecoration(mRootHeader);
                mRootHeader = null;
            }
            if (mHeader != null)
            {
                mRootRelativeLayout.RemoveView(mHeader);
                mHeader = null;
            }
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
                    PULL.SetHasMoreItems(false,false);
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
        #region
        protected void Init()
        {
            mTouchState = TOUCH_STATE_NONE;
            mViewConfiguration = ViewConfiguration.Get(Context);
        }
        public void SetCloseInterpolator(IInterpolator interpolator)
        {
            mCloseInterpolator = interpolator;
        }
        public void SetOpenInterpolator(IInterpolator interpolator)
        {
            mOpenInterpolator = interpolator;
        }
        public IInterpolator GetOpenInterpolator()
        {
            return mOpenInterpolator;
        }
        public IInterpolator GetCloseInterpolator()
        {
            return mCloseInterpolator;
        }

        public override bool OnInterceptTouchEvent(MotionEvent ev)
        {
            if (ev.Action != MotionEventActions.Down && mTouchView == null)
            {
                return base.OnInterceptTouchEvent(ev);
            }

            int action = (int)ev.Action;
            switch (action & MotionEventCompat.ActionMask)
            {
                case (int)MotionEventActions.Down:
                    dx = 0.0f; // reset
                    dy = 0.0f; // reset
                    startClickTime = Java.Lang.JavaSystem.CurrentTimeMillis(); // reset
                    int oldPos = mTouchPosition;
                    mDownX = ev.GetX();
                    mDownY = ev.GetY();
                    mTouchState = TOUCH_STATE_NONE;
                    mTouchPosition = mRecyclerView.GetChildAdapterPosition(mRecyclerView.FindChildViewUnder((int)ev.GetX(), (int)ev.GetY()));
                    if (mTouchPosition == oldPos && mTouchView != null
                            && mTouchView.IsOpen())
                    {
                        mTouchState = TOUCH_STATE_X;
                        mTouchView.OnSwipe(ev);
                    }
                    // find the touched child view
                    View view = null;
                    RecyclerView.ViewHolder vh = mRecyclerView.FindViewHolderForAdapterPosition(mTouchPosition);
                    if (vh != null)
                    {
                        view = vh.ItemView;
                    }
                    // is not touched the opened menu view, so we intercept this touch event
                    if (mTouchPosition != oldPos && mTouchView != null && mTouchView.IsOpen())
                    {
                        mTouchView.SmoothCloseMenu();
                        mTouchView = null;
                        // try to cancel the touch event
                        MotionEvent cancelEvent = MotionEvent.Obtain(ev);
                        cancelEvent.Action = MotionEventActions.Cancel;
                        base.OnTouchEvent(cancelEvent);
                        return true;
                    }
                    if (view is SwipeMenuLayout)
                    {
                        mTouchView = (SwipeMenuLayout)view;
                        mTouchView.SetSwipeDirection(mDirection);
                    }
                    if (mTouchView != null)
                    {
                        mTouchView.OnSwipe(ev);
                    }
                    break;
                case (int)MotionEventActions.Move:
                    dy = Math.Abs((ev.GetY() - mDownY));
                    dx = Math.Abs((ev.GetX() - mDownX));
                    if (mTouchState == TOUCH_STATE_X && mTouchView.IsSwipeEnable())
                    {
                        mTouchView.OnSwipe(ev);
                        ev.Action = MotionEventActions.Cancel;
                        base.OnTouchEvent(ev);
                    }
                    else if (mTouchState == TOUCH_STATE_NONE && mTouchView.IsSwipeEnable())
                    {
                        if (Math.Abs(dy) > mViewConfiguration.ScaledTouchSlop)
                        {
                            mTouchState = TOUCH_STATE_Y;
                        }
                        else if (dx > mViewConfiguration.ScaledTouchSlop)
                        {
                            mTouchState = TOUCH_STATE_X;
                            if (mOnSwipeListener != null)
                            {
                                mOnSwipeListener.onSwipeStart(mTouchPosition);
                            }
                        }
                    }
                    break;
                case (int)MotionEventActions.Up:
                    Boolean isCloseOnUpEvent = false;
                    if (mTouchState == TOUCH_STATE_X && mTouchView.IsSwipeEnable())
                    {
                        isCloseOnUpEvent = !mTouchView.OnSwipe(ev);
                        if (mOnSwipeListener != null)
                        {
                            mOnSwipeListener.onSwipeEnd(mTouchPosition);
                        }
                        if (!mTouchView.IsOpen())
                        {
                            mTouchPosition = -1;
                            mTouchView = null;
                        }
                        ev.Action = MotionEventActions.Cancel;
                        base.OnTouchEvent(ev);
                    }
                    long clickDuration = Java.Lang.JavaSystem.CurrentTimeMillis() - startClickTime;
                    Boolean isOutDuration = clickDuration > ViewConfiguration.LongPressTimeout;
                    Boolean isOutX = dx > mViewConfiguration.ScaledTouchSlop;
                    Boolean isOutY = dy > mViewConfiguration.ScaledTouchSlop;
                    // long pressed or scaled touch, we just intercept up touch event
                    if (isOutDuration || isOutX || isOutY)
                    {
                        return true;
                    }
                    else {
                        float eX = ev.GetX();
                        float eY = ev.GetY();
                        View upView = mRecyclerView.FindChildViewUnder(eX, eY);
                        if (upView is SwipeMenuLayout)
                        {
                            SwipeMenuLayout smView = (SwipeMenuLayout)upView;
                            int x = (int)eX - smView.Left;
                            int y = (int)eY - smView.Top;
                            View menuView = smView.GetMenuView();
                            float translationX = ViewCompat.GetTranslationX(menuView);
                            float translationY = ViewCompat.GetTranslationY(menuView);
                            // intercept the up event when touched on the contentView of the opened SwipeMenuLayout
                            if (!(x >= menuView.Left + translationX &&
                                    x <= menuView.Right + translationX &&
                                    y >= menuView.Top + translationY &&
                                    y <= menuView.Bottom + translationY) &&
                                    isCloseOnUpEvent)
                            {
                                return true;
                            }
                        }
                    }
                    break;
                case (int)MotionEventActions.Cancel:
                    if (mTouchView != null && mTouchView.IsSwipeEnable())
                    {
                        // when event has canceled, we just consider as up event
                        ev.Action = MotionEventActions.Up;
                        mTouchView.OnSwipe(ev);
                    }
                    break;
            }
            return base.OnInterceptTouchEvent(ev);
        }
        public void SmoothOpenMenu(int position)
        {
            View view = mLlm.FindViewByPosition(position);
            if (view is SwipeMenuLayout)
            {
                mTouchPosition = position;
                // close pre opened swipe menu
                if (mTouchView != null && mTouchView.IsOpen())
                {
                    mTouchView.SmoothCloseMenu();
                }
                mTouchView = (SwipeMenuLayout)view;
                mTouchView.SetSwipeDirection(mDirection);
                mTouchView.SmoothOpenMenu();
            }
        }

        /**
    * close the opened menu manually
    */
        public void SmoothCloseMenu()
        {
            // close the opened swipe menu
            if (mTouchView != null && mTouchView.IsOpen())
            {
                mTouchView.SmoothCloseMenu();
            }
        }
        public void SetOnSwipeListener(OnSwipeListener onSwipeListener)
        {
            this.mOnSwipeListener = onSwipeListener;
        }

        /**
         * get current touched view
         * @return touched view, maybe null
         */
        public SwipeMenuLayout GetTouchView()
        {
            return mTouchView;
        }


        /**
         * set the swipe direction
         * @param direction swipe direction (left or right)
         */
        public void SetSwipeDirection(int direction)
        {
            mDirection = direction;


        }



        #endregion


    }
}