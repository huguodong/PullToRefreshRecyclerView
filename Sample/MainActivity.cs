using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

namespace Sample
{
    [Activity(Label = "Sample", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        int count = 1;
        private Button mBtnGridViewMode, mBtnListViewMode;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            FindViews();

        }
        private void FindViews(){
            mBtnGridViewMode=FindViewById<Button>(Resource.Id.btn_gv_mode);
            mBtnGridViewMode.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.btn_gv_mode :
                    this.StartActivity(new Intent(this,typeof(PtrrvGridViewMode)));
                    break;
            };

           
            
        }
    }
}

