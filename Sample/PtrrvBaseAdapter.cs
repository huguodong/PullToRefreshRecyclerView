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

namespace Sample
{
    public class PtrrvBaseAdapter<T> : RecyclerView.Adapter where T : RecyclerView.ViewHolder
    {
        protected LayoutInflater mInflater;
        protected int mCount = 0;
        protected Context mContext = null;

        public static int TYPE_HEADER = 0;
        public static int TYPE_HISVIDEO = 1;
        public static int TYPE_MESSAGE = 2;
        public PtrrvBaseAdapter(Context context)
        {
            mContext = context;
            mInflater = LayoutInflater.From(context);
        }
        public void SetCount(int count)
        {
            mCount = count;
        }
        public override int ItemCount
        {
            get { return mCount; }
        }
        public Object GetItem(int position)
        {
            return null;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return null;
        }
    }
}