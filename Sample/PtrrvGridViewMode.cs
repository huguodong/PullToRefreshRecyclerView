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
    public class PtrrvGridViewMode : AppCompatActivity, PullToRefreshRecyclerView.PagingableListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private PullToRefreshRecyclerView.PullToRefreshRecyclerView mPtrrv;
        private MyAdapter mAdapter;
        private static int DEFAULT_ITEM_SIZE = 100;
        private static int MSG_CODE_REFRESH = 0;
        private static int MSG_CODE_LOADMORE = 1;
        private List<String> list;
        private static int TIME = 1000;
        public Handler mHandler;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_gridview);
            mHandler = new Handler(HandleMessage);
            list = new List<string>();
            Add();
            FindViews();
        }
        private void FindViews()
        {
            mPtrrv = FindViewById<PullToRefreshRecyclerView.PullToRefreshRecyclerView>(Resource.Id.ptrrv);
            mPtrrv.SetSwipeEnable(true);//open swipe
            mPtrrv.SetLayoutManager(new GridLayoutManager(this, 2));
            mPtrrv.SetPagingableListener(this);
            mPtrrv.SetOnRefreshListener(this);
            mAdapter = new MyAdapter(list, this);
            mPtrrv.SetAdapter(mAdapter);
            mPtrrv.SetLoadMoreCount(22);//���С�������ֵ����ʾ���ظ���
            mPtrrv.OnFinishLoading(true, false);
        }

        public void Add()
        {
            for (int i = 0; i <= 4; i++)
            {
                list.Add("Brasil");
                list.Add("Mexico");
                list.Add("United States");
                list.Add("Canada");

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
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var m1 = menu.Add(0, 1, 0, "Add");
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case 1:
                    {
                        Add();
                        mAdapter.NotifyDataSetChanged();
                        mPtrrv.OnFinishLoading(true, false);
                    }
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }
        public void HandleMessage(Message msg)
        {
            if (msg.What == MSG_CODE_REFRESH)
            {
                for (int i = 0; i < 4; i++)
                { list.Insert(0, "�����"); }
                mAdapter.NotifyDataSetChanged();
                mPtrrv.SetOnRefreshComplete();
                mPtrrv.OnFinishLoading(true, false);
            }
            else if (msg.What == MSG_CODE_LOADMORE)
            {
                if (mAdapter.ItemCount >= DEFAULT_ITEM_SIZE)
                {
                    //�������
                    Toast.MakeText(this, Resource.String.nomoredata, ToastLength.Short).Show();
                    mPtrrv.OnFinishLoading(false, false);
                }
                else
                {
                    Toast.MakeText(this, "���ڼ�����", ToastLength.Short).Show();
                    Add();
                    mAdapter.NotifyDataSetChanged();
                    mPtrrv.OnFinishLoading(true, false);
                }
            }
        }
        public class MyAdapter : RecyclerView.Adapter
        {
            List<string> list;
            Context context;
            public MyAdapter(List<string> data, Context context)
            {
                list = data;
                this.context = context;
            }

            // Create new views (invoked by the layout manager)
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View view = LayoutInflater.From(context).Inflate(Resource.Layout.ptrrv_item, null);
                return new ViewHolder(view);
            }

            // Replace the contents of a view (invoked by the layout manager)
            public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
            {
                var item = list[position];
                // Replace the contents of the view with that element
                var holder = viewHolder as ViewHolder;
                holder.TextView.Text = item;


            }

            public override int ItemCount
            {
                get
                {
                    return list.Count; ;
                }
            }
            public class ViewHolder : RecyclerView.ViewHolder
            {
                public TextView TextView { get; set; }

                public ViewHolder(View itemView)
                    : base(itemView)
                {
                    TextView = itemView.FindViewById<TextView>(Resource.Id.tvContent);
                }
            }
        }
      
    }
}