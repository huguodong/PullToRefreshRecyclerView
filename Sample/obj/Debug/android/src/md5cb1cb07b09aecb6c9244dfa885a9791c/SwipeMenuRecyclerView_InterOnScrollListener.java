package md5cb1cb07b09aecb6c9244dfa885a9791c;


public class SwipeMenuRecyclerView_InterOnScrollListener
	extends android.support.v7.widget.RecyclerView.OnScrollListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onScrollStateChanged:(Landroid/support/v7/widget/RecyclerView;I)V:GetOnScrollStateChanged_Landroid_support_v7_widget_RecyclerView_IHandler\n" +
			"n_onScrolled:(Landroid/support/v7/widget/RecyclerView;II)V:GetOnScrolled_Landroid_support_v7_widget_RecyclerView_IIHandler\n" +
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.SwipeMenuRecyclerView+InterOnScrollListener, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SwipeMenuRecyclerView_InterOnScrollListener.class, __md_methods);
	}


	public SwipeMenuRecyclerView_InterOnScrollListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SwipeMenuRecyclerView_InterOnScrollListener.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.SwipeMenuRecyclerView+InterOnScrollListener, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onScrollStateChanged (android.support.v7.widget.RecyclerView p0, int p1)
	{
		n_onScrollStateChanged (p0, p1);
	}

	private native void n_onScrollStateChanged (android.support.v7.widget.RecyclerView p0, int p1);


	public void onScrolled (android.support.v7.widget.RecyclerView p0, int p1, int p2)
	{
		n_onScrolled (p0, p1, p2);
	}

	private native void n_onScrolled (android.support.v7.widget.RecyclerView p0, int p1, int p2);

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
