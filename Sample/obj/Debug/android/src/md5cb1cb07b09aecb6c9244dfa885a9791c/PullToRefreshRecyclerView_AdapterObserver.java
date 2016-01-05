package md5cb1cb07b09aecb6c9244dfa885a9791c;


public class PullToRefreshRecyclerView_AdapterObserver
	extends android.support.v7.widget.RecyclerView.AdapterDataObserver
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onChanged:()V:GetOnChangedHandler\n" +
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.PullToRefreshRecyclerView+AdapterObserver, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PullToRefreshRecyclerView_AdapterObserver.class, __md_methods);
	}


	public PullToRefreshRecyclerView_AdapterObserver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PullToRefreshRecyclerView_AdapterObserver.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.PullToRefreshRecyclerView+AdapterObserver, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onChanged ()
	{
		n_onChanged ();
	}

	private native void n_onChanged ();

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
