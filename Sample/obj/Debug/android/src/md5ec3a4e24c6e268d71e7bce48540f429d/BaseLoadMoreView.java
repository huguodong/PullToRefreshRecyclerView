package md5ec3a4e24c6e268d71e7bce48540f429d;


public class BaseLoadMoreView
	extends android.support.v7.widget.RecyclerView.ItemDecoration
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDrawOver:(Landroid/graphics/Canvas;Landroid/support/v7/widget/RecyclerView;Landroid/support/v7/widget/RecyclerView$State;)V:GetOnDrawOver_Landroid_graphics_Canvas_Landroid_support_v7_widget_RecyclerView_Landroid_support_v7_widget_RecyclerView_State_Handler\n" +
			"n_getItemOffsets:(Landroid/graphics/Rect;ILandroid/support/v7/widget/RecyclerView;)V:GetGetItemOffsets_Landroid_graphics_Rect_ILandroid_support_v7_widget_RecyclerView_Handler\n" +
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.Footer.BaseLoadMoreView, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseLoadMoreView.class, __md_methods);
	}


	public BaseLoadMoreView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseLoadMoreView.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Footer.BaseLoadMoreView, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BaseLoadMoreView (android.content.Context p0, android.support.v7.widget.RecyclerView p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseLoadMoreView.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Footer.BaseLoadMoreView, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Support.V7.Widget.RecyclerView, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
	}


	public void onDrawOver (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.State p2)
	{
		n_onDrawOver (p0, p1, p2);
	}

	private native void n_onDrawOver (android.graphics.Canvas p0, android.support.v7.widget.RecyclerView p1, android.support.v7.widget.RecyclerView.State p2);


	public void getItemOffsets (android.graphics.Rect p0, int p1, android.support.v7.widget.RecyclerView p2)
	{
		n_getItemOffsets (p0, p1, p2);
	}

	private native void n_getItemOffsets (android.graphics.Rect p0, int p1, android.support.v7.widget.RecyclerView p2);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
