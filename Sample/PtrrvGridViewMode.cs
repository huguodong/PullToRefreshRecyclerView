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
using Android.Support.V7.App;
using PullToRefreshRecyclerView;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;
namespace Sample
{
    [Activity(Label = "PtrrvGridViewMode")]
    public class PtrrvGridViewMode : AppCompatActivity, PullToRefreshRecyclerView.PullToRefreshRecyclerView.PagingableListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private PullToRefreshRecyclerView.PullToRefreshRecyclerView mPtrrv;
        public PtrrvAdapter mAdapter;
        private static int DEFAULT_ITEM_SIZE = 60;
        private static int ITEM_SIZE_OFFSET = 40;
        private static int MSG_CODE_REFRESH = 0;
        private static int MSG_CODE_LOADMORE = 1;

        private static int TIME = 1000;
        public Handler mHandler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_gridview);
            mHandler = new Handler(HandleMessage);
            FindViews();
            // Create your application here
        }
        private void FindViews()
        {
            mPtrrv = FindViewById<PullToRefreshRecyclerView.PullToRefreshRecyclerView>(Resource.Id.ptrrv);
            mPtrrv.SetSwipeEnable(true);//open swipe
            mPtrrv.SetLayoutManager(new GridLayoutManager(this, 4));
            mPtrrv.SetPagingableListener(this);
            mPtrrv.SetOnRefreshListener(this);
            mAdapter = new PtrrvAdapter(this);
            mAdapter.SetCount(DEFAULT_ITEM_SIZE);
            mPtrrv.SetAdapter(mAdapter);
            mPtrrv.SetLoadmoreString("12312312312");
            mPtrrv.OnFinishLoading(true, false);
        }

        public void OnLoadMoreItems()
        {
            mHandler.SendEmptyMessageDelayed(MSG_CODE_LOADMORE, TIME);
        }

        public void OnRefresh()
        {
            mHandler.SendEmptyMessageDelayed(MSG_CODE_REFRESH, TIME);
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
                    Toast.MakeText(this, Resource.String.nomoredata, ToastLength.Short).Show();
                    mPtrrv.OnFinishLoading(false, false);
                }
                else
                {
                    mAdapter.SetCount(DEFAULT_ITEM_SIZE + ITEM_SIZE_OFFSET);
                    mAdapter.NotifyDataSetChanged();
                    mPtrrv.OnFinishLoading(true, false);
                }
            }
        }
        public class PtrrvAdapter : PtrrvBaseAdapter<PtrrvAdapter.ViewHolder>
        {
            public PtrrvAdapter(Context context)
                : base(context)
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

                public ViewHolder(View itemView)
                    : base(itemView)
                {
                }
            }
        }
    }
}