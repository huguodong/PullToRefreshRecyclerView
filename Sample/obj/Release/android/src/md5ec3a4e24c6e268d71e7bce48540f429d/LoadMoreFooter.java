package md5ec3a4e24c6e268d71e7bce48540f429d;


public class LoadMoreFooter
	extends md5ec3a4e24c6e268d71e7bce48540f429d.BaseFooter
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("PullToRefreshRecyclerView.Footer.LoadMoreFooter, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LoadMoreFooter.class, __md_methods);
	}


	public LoadMoreFooter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoadMoreFooter.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Footer.LoadMoreFooter, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public LoadMoreFooter (android.content.Context p0, android.support.v7.widget.RecyclerView p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == LoadMoreFooter.class)
			mono.android.TypeManager.Activate ("PullToRefreshRecyclerView.Footer.LoadMoreFooter, PullToRefreshRecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Support.V7.Widget.RecyclerView, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
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
