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
using PullToRefreshRecyclerView.Footer;

namespace PullToRefreshRecyclerView.Impl
{
    public interface PrvInterface
    {
        void SetOnRefreshComplete();
        void SetOnLoadMoreComplete();//onFinishLoading,加载更多完成
        void SetPagingableListener(PagingableListener pagingableListener);
        void SetEmptyView(View emptyView);
        void SetAdapter(RecyclerView.Adapter adapter);
        void AddHeaderView(View view);
        void RemoveHeader();//绉婚櫎header
        void SetFooter(View view);
        void AddOnScrollListener(PullToRefreshRecyclerView.OnScrollListener onScrollLinstener);
        void SetLoadMoreFooter(BaseLoadMoreView loadMoreFooter);
        RecyclerView.LayoutManager GetLayoutManager();
        void OnFinishLoading(Boolean hasMoreItems, Boolean needSetSelection, bool delete = false);
        void SetSwipeEnable(Boolean enable);//设置是否可以下拉
        Boolean IsSwipeEnable();//返回当前组件是否可以下拉
        RecyclerView GetRecyclerView();
        void SetLayoutManager(RecyclerView.LayoutManager layoutManager);
        void SetLoadMoreCount(int count);//如果不达到count数量不让加载更多
        void Release();
    }
}