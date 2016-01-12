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
using System;
using System.Collections.Generic;
using Android.Views.Animations;

namespace Sample
{
    [Activity(Label = "SwipeMenuRecyclerMode")]
    public class SwipeMenuRecyclerMode : AppCompatActivity, PagingableListener, SwipeRefreshLayout.IOnRefreshListener
    {
        private static SwipeMenuRecyclerView mPtrrv;
        private static MyAdapter mAdapter;
        private static int DEFAULT_ITEM_SIZE = 100;
        private static int MSG_CODE_REFRESH = 0;
        private static int MSG_CODE_LOADMORE = 1;
        private List<User> users;
        private static int TIME = 1000;
        public Handler mHandler;
        private static Context mContext;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_SwGridview);
            users = GetUsers();
            mContext = this;
            mHandler = new Handler(HandleMessage);
            FindViews();
            // Create your application here
        }
        private List<User> GetUsers()
        {
            List<User> userList = new List<User>();
            for (int i = 0; i < 50; i++)
            {
                User user = new User();
                user.userId = i + 1000;
                user.userName = "Pobi " + (i + 1);
                userList.Add(user);
            }
            return userList;
        }
        private void FindViews()
        {
            mPtrrv = FindViewById<SwipeMenuRecyclerView>(Resource.Id.ptrrv);
            mPtrrv.SetSwipeEnable(true);//open swipe
            mPtrrv.SetLayoutManager(new GridLayoutManager(this, 2));
            mPtrrv.SetPagingableListener(this);
            mPtrrv.SetOnRefreshListener(this);
            mPtrrv.SetOpenInterpolator(new BounceInterpolator());
            mPtrrv.SetCloseInterpolator(new BounceInterpolator());
            mAdapter = new MyAdapter(this, users);
            mPtrrv.SetAdapter(mAdapter);
            mPtrrv.SetLoadMoreCount(22);//如果小于输入的值则不显示加载更多
            mPtrrv.OnFinishLoading(true, false);
        }

        public class MyAdapter : RecyclerView.Adapter
        {
            List<User> users;
            public MyAdapter(Context context, List<User> users)
            {
                this.users = users;
            }
            public override int ItemCount
            {
                get
                {
                    return users.Count;
                }
            }
            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View view = LayoutInflater.From(mContext).Inflate(Resource.Layout.swptrrv_item, null);
                return new ViewHolder(view);
            }
            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                User user = users[position];
                ViewHolder myViewHolder = (ViewHolder)holder;
                SwipeMenuLayout itemView = (SwipeMenuLayout)myViewHolder.ItemView.JavaCast<SwipeMenuLayout>();
                itemView.Click += delegate
                {
                    Toast.MakeText(mContext, "Hi " + user.userName, ToastLength.Short).Show();
                };
                myViewHolder.TextView.Text = user.userName;
                myViewHolder.btDelete.Click += delegate
                {
                    try
                    {
                        users.RemoveAt(holder.AdapterPosition);
                        mAdapter.NotifyItemRemoved(holder.AdapterPosition);
                        mPtrrv.OnFinishLoading(true, false,true);
                    }
                    catch { }

                };
            }
        }
        public void Add()
        {
            for (int i = 0; i < 4; i++)
            {
                User newadd = new User();
                newadd.userId = i + 1000;
                newadd.userName = "New" + i;
                users.Add( newadd);
            }
        }
        public void HandleMessage(Message msg)
        {
            if (msg.What == MSG_CODE_REFRESH)
            {
                for (int i = 0; i < 4; i++)
                {
                    User newadd = new User();
                    newadd.userId = i + 1000;
                    newadd.userName = "New" + i;
                    users.Insert(0, newadd);
                }
                mAdapter.NotifyDataSetChanged();
                mPtrrv.SetOnRefreshComplete();
                mPtrrv.OnFinishLoading(true, false);
            }
            else if (msg.What == MSG_CODE_LOADMORE)
            {
                if (mAdapter.ItemCount >= DEFAULT_ITEM_SIZE)
                {
                    //加载完毕
                    Toast.MakeText(this, Resource.String.nomoredata, ToastLength.Short).Show();
                    mPtrrv.OnFinishLoading(false, false);
                }
                else
                {
                    Add();
                    Toast.MakeText(this, "正在加载中", ToastLength.Short).Show();
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
        //anctionbar菜单
        #region
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.action_left)
            {
                mPtrrv.SetSwipeDirection(SwipeMenuRecyclerView.DIRECTION_LEFT);
                return true;
            }
            if (id == Resource.Id.action_right)
            {
                mPtrrv.SetSwipeDirection(SwipeMenuRecyclerView.DIRECTION_RIGHT);
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        #endregion
        public class ViewHolder : RecyclerView.ViewHolder
        {
            public TextView TextView { get; set; }
            public View btOpen;
            public TextView btDelete;
            public ViewHolder(View itemView)
                : base(itemView)
            {
                TextView = itemView.FindViewById<TextView>(Resource.Id.tvContent);
                btOpen = itemView.FindViewById(Resource.Id.btOpen);
                btDelete = itemView.FindViewById<TextView>(Resource.Id.btDelete);
            }
        }
        public class User
        {
            public int userId;
            public String userName;
        }
    }
}