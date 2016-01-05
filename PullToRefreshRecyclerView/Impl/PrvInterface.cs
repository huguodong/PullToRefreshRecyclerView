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

namespace PullToRefreshRecyclerView.Impl
{
    public interface PrvInterface
    {
        void SetOnRefreshComplete();
        void SetOnLoadMoreComplete();//onFinishLoading,加载更多完成
        void SetPagingableListener(PullToRefreshRecyclerView.PagingableListener pagingableListener);
        void SetEmptyView(View emptyView);
        void SetAdapter(RecyclerView.Adapter adapter);
        void AddHeaderView(View view);
        //    public void removeHeader();//绉婚櫎header
        void SetFooter(View view);
        void AddOnScrollListener(PullToRefreshRecyclerView.OnScrollListener onScrollLinstener);
        RecyclerView.LayoutManager GetLayoutManager();
        void OnFinishLoading(Boolean hasMoreItems, Boolean needSetSelection);
        void SetSwipeEnable(Boolean enable);//设置是否可以下拉
        Boolean IsSwipeEnable();//返回当前组件是否可以下拉
        RecyclerView GetRecyclerView();
        void SetLayoutManager(RecyclerView.LayoutManager layoutManager);
        void SetLoadMoreCount(int count);//如果不达到count数量不让加载更多
        void Release();
    }
}