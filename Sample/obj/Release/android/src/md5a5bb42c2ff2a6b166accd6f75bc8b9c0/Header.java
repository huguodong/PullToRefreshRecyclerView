package md5a5bb42c2ff2a6b166accd6f75bc8b9c0;


public class Header
	extends md5a5bb42c2ff2a6b166accd6f75bc8b9c0.BaseHeader
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_getItemOffsets:(Landroid/graphics/Rect;ILandroid/support/v7/widget/RecyclerView;)V:GetGetItemOffsets_Landroid_graphics_Rect_ILandroid_support_v7_widget_RecyclerView_Handler\n" +
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.Header.Header, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Header.class, __md_methods);
	}


	public Header () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Header.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Header.Header, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


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
