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

namespace Sample
{
    [Activity(Label = "PtrrvListViewMode")]
    public class PtrrvListViewMode : Activity, PullToRefreshRecyclerView.PagingableListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private PullToRefreshRecyclerView.PullToRefreshRecyclerView mPtrrv;
        private PtrrvAdapter mAdapter;
        private static int DEFAULT_ITEM_SIZE = 20;
        private static int ITEM_SIZE_OFFSET = 20;

        private static int MSG_CODE_REFRESH = 0;
        private static int MSG_CODE_LOADMORE = 1;
        public Handler mHandler;
        private static int TIME = 1000;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_listview);
            mHandler = new Handler(HandleMessage);
            FindViews();
            // Create your application here
        }
        private void FindViews()
        {
            mPtrrv = FindViewById<PullToRefreshRecyclerView.PullToRefreshRecyclerView>(Resource.Id.ptrrv);
            mPtrrv.SetSwipeEnable(true);
            DemoLoadMoreView loadMoreView = new DemoLoadMoreView(this, mPtrrv.GetRecyclerView());
            loadMoreView.SetLoadmoreString("等等等");
            loadMoreView.SetLoadMorePadding(100);
            mPtrrv.SetLoadMoreFooter(loadMoreView);
            mPtrrv.SetLayoutManager(new LinearLayoutManager(this));
            mPtrrv.SetPagingableListener(this);
            mPtrrv.SetOnRefreshListener(this);
            mPtrrv.GetRecyclerView().AddItemDecoration(new DividerItemDecoration(this,
               DividerItemDecoration.VERTICAL_LIST));
            //mPtrrv.AddHeaderView(View.Inflate(this, Resource.Layout.header, null));
            mPtrrv.RemoveHeader();
  
            mAdapter = new PtrrvAdapter(this);
            mAdapter.SetCount(DEFAULT_ITEM_SIZE);
            mPtrrv.SetAdapter(mAdapter);
            mPtrrv.OnFinishLoading(true, false);
        }
        public void HandleMessage(Message msg)
        {
            if (msg.What == MSG_CODE_REFRESH)
            {
                mAdapter.SetCount(DEFAULT_ITEM_SIZE);
                mAdapter.NotifyDataSetChanged();
                mPtrrv.SetOnRefreshComplete();
                mPtrrv.OnFinishLoading(true, false);
            }
            else if (msg.What == MSG_CODE_LOADMORE)
            {
                if (mAdapter.ItemCount == DEFAULT_ITEM_SIZE + ITEM_SIZE_OFFSET)
                {
                    //over
                    Toast.MakeText(this, "没了", ToastLength.Short).Show();
                    mPtrrv.OnFinishLoading(false, false);
                }
                else {
                    mAdapter.SetCount(DEFAULT_ITEM_SIZE + ITEM_SIZE_OFFSET);
                    mAdapter.NotifyDataSetChanged();
                    mPtrrv.OnFinishLoading(true, false);
                }
            }
        }

        public void OnLoadMoreItems()
        {
            mHandler.SendEmptyMessageDelayed(MSG_CODE_LOADMORE, TIME);
        }

        public void OnRefresh()
        {
            mHandler.SendEmptyMessageDelayed(MSG_CODE_REFRESH, TIME);
        }
        private class PtrrvAdapter : PtrrvBaseAdapter<PtrrvAdapter.ViewHolder>
        {
            public PtrrvAdapter(Context context) : base(context)
            {

            }
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View view = mInflater.Inflate(Resource.Layout.ptrrv_item, null);
                return new ViewHolder(view);
            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {

            }
            public class ViewHolder : RecyclerView.ViewHolder
            {

                public ViewHolder(View itemView) : base(itemView)
                {

                }
            }
        }

    }
}