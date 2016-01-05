package md5e44603b235f21ebfbb6b3d0f2dc433d5;


public class PtrrvGridViewMode_PtrrvAdapter
	extends md5e44603b235f21ebfbb6b3d0f2dc433d5.PtrrvBaseAdapter_1
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateViewHolder:(Landroid/view/ViewGroup;I)Landroid/support/v7/widget/RecyclerView$ViewHolder;:GetOnCreateViewHolder_Landroid_view_ViewGroup_IHandler\n" +
			"n_onBindViewHolder:(Landroid/support/v7/widget/RecyclerView$ViewHolder;I)V:GetOnBindViewHolder_Landroid_support_v7_widget_RecyclerView_ViewHolder_IHandler\n" +
			"";
		mono.android.Runtime.register ("Sample.PtrrvGridViewMode+PtrrvAdapter, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", PtrrvGridViewMode_PtrrvAdapter.class, __md_methods);
	}


	public PtrrvGridViewMode_PtrrvAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == PtrrvGridViewMode_PtrrvAdapter.class)
			mono.android.TypeManager.Activate ("Sample.PtrrvGridViewMode+PtrrvAdapter, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public PtrrvGridViewMode_PtrrvAdapter (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == PtrrvGridViewMode_PtrrvAdapter.class)
			mono.android.TypeManager.Activate ("Sample.PtrrvGridViewMode+PtrrvAdapter, Sample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public android.support.v7.widget.RecyclerView.ViewHolder onCreateViewHolder (android.view.ViewGroup p0, int p1)
	{
		return n_onCreateViewHolder (p0, p1);
	}

	private native android.support.v7.widget.RecyclerView.ViewHolder n_onCreateViewHolder (android.view.ViewGroup p0, int p1);


	public void onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1)
	{
		n_onBindViewHolder (p0, p1);
	}

	private native void n_onBindViewHolder (android.support.v7.widget.RecyclerView.ViewHolder p0, int p1);

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
