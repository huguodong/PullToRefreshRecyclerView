package md5e44603b235f21ebfbb6b3d0f2dc433d5;


public class PtrrvGridViewMode
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer,
		android.support.v4.widget.SwipeRefreshLayout.OnRefreshListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onRefresh:()V:GetOnRefreshHandler:Android.Support.V4.Widget.SwipeRefreshLayout/IOnRefreshListenerInvoker, Xamarin.Android.Support.v4\n" +
			"";
		mono.android.Runtime.register ("Sample.PtrrvGridViewMode, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PtrrvGridViewMode.class, __md_methods);
	}


	public PtrrvGridViewMode () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PtrrvGridViewMode.class)
			mono.android.TypeManager.Activate ("Sample.PtrrvGridViewMode, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onRefresh ()
	{
		n_onRefresh ();
	}

	private native void n_onRefresh ();

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
