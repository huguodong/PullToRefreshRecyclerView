package md5e44603b235f21ebfbb6b3d0f2dc433d5;


public class DemoLoadMoreView
	extends md5ec3a4e24c6e268d71e7bce48540f429d.BaseLoadMoreView
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Sample.DemoLoadMoreView, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DemoLoadMoreView.class, __md_methods);
	}


	public DemoLoadMoreView () throws java.lang.Throwable
	{
		super ();
		if (getClass () == DemoLoadMoreView.class)
			mono.android.TypeManager.Activate ("Sample.DemoLoadMoreView, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public DemoLoadMoreView (android.content.Context p0, android.support.v7.widget.RecyclerView p1) throws java.lang.Throwable
	{
		super ();
		if (getClass () == DemoLoadMoreView.class)
			mono.android.TypeManager.Activate ("Sample.DemoLoadMoreView, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Support.V7.Widget.RecyclerView, Xamarin.Android.Support.v7.RecyclerView, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0, p1 });
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
