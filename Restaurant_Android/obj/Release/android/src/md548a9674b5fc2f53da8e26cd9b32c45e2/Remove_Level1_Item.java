package md548a9674b5fc2f53da8e26cd9b32c45e2;


public class Remove_Level1_Item
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Restaurant_Android.Remove_Level1_Item, Restaurant_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Remove_Level1_Item.class, __md_methods);
	}


	public Remove_Level1_Item ()
	{
		super ();
		if (getClass () == Remove_Level1_Item.class)
			mono.android.TypeManager.Activate ("Restaurant_Android.Remove_Level1_Item, Restaurant_Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
