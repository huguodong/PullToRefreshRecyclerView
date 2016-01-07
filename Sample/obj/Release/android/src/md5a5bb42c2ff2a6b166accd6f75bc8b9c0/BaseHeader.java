package md5a5bb42c2ff2a6b166accd6f75bc8b9c0;


public class BaseHeader
	extends android.support.v7.widget.RecyclerView.ItemDecoration
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.Header.BaseHeader, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", BaseHeader.class, __md_methods);
	}


	public BaseHeader () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BaseHeader.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Header.BaseHeader, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

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
