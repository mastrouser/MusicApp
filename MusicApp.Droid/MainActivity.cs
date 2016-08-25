using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Widget;
using Com.Bumptech.Glide;
using MusicApp.Droid.Extensions.Adapters;
using MusicApp.Droid.Extensions.ItemDecorators;
using MusicApp.Droid.Models;
using System;
using System.Collections.Generic;
using static Android.Support.V7.Widget.RecyclerView;

namespace MusicApp.Droid
{
    [Activity(Label = "@string/applicationName", MainLauncher = true, Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity
    {

        private string TAG = "Main";
        private RecyclerView _recyclerView;
        AppBarLayout _appBarLayout;
        CollapsingToolbarLayout _collapsingToolbar;
        private AlbumsAdapter _adapter;
        private List<Album> _albums;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            SetupActionBar();
            SetupCollapsingToolbar();
            SetupRecyclerView();

            LoadBackdropCover();

        }

        public override bool OnCreateOptionsMenu(Android.Views.IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.MenuMain, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(Android.Views.IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.actionSettings:
                    Toast.MakeText(this, "Clicked on Settings", ToastLength.Short).Show();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }


        private void SetupRecyclerView()
        {
            _recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            _albums = new List<Album>();
            _adapter = new AlbumsAdapter(this, _albums);
            LayoutManager layoutManager = new GridLayoutManager(this, 2);
            _recyclerView.SetLayoutManager(layoutManager);
            _recyclerView.AddItemDecoration(new GridSpacingItemDecoration(2, dpToPx(10), true));
            _recyclerView.SetItemAnimator(new DefaultItemAnimator());
            _recyclerView.SetAdapter(_adapter);
            PrepareAlbums();
        }

        private void PrepareAlbums()
        {
            int[] covers = new int[]{
                Resource.Drawable.album1,
                Resource.Drawable.album2,
                Resource.Drawable.album3,
                Resource.Drawable.album4,
                Resource.Drawable.album5,
                Resource.Drawable.album6,
                Resource.Drawable.album7,
                Resource.Drawable.album8,
                Resource.Drawable.album9,
                Resource.Drawable.album10,
                Resource.Drawable.album11};

            _albums = new List<Album> {
                new Album { Name = "True Romance", NumberOfSongs = 13, Thumbnail = covers[0] },
                new Album { Name = "Xscpae", NumberOfSongs = 12, Thumbnail = covers[1] },
                new Album { Name = "Maroon 5", NumberOfSongs = 8, Thumbnail = covers[2] },
                new Album { Name = "Born to Die", NumberOfSongs = 11, Thumbnail = covers[3] },
                new Album { Name = "Honeymoon", NumberOfSongs = 12, Thumbnail = covers[4] },
                new Album { Name = "I Need a Doctor", NumberOfSongs = 1, Thumbnail = covers[5] },
                new Album { Name = "Loud", NumberOfSongs = 11, Thumbnail = covers[6] },
                new Album { Name = "Legend", NumberOfSongs = 14, Thumbnail = covers[7] },
                new Album { Name = "Hello", NumberOfSongs = 11, Thumbnail = covers[8] },
                new Album { Name = "Greatest Hits", NumberOfSongs = 17, Thumbnail = covers[9] },
            };
            _adapter.UpdateDataSource(_albums);
        }

        private void SetupCollapsingToolbar()
        {
            _collapsingToolbar =
                FindViewById<CollapsingToolbarLayout>(Resource.Id.collapsingToolbar);
            _collapsingToolbar.SetTitle(" ");

            this._appBarLayout =
                FindViewById<AppBarLayout>(Resource.Id.appBar);
            this._appBarLayout.SetExpanded(true);

            this._appBarLayout.OffsetChanged += AppBarLayout_OffsetChanged;


        }

        private void SetupActionBar()
        {
            Android.Support.V7.Widget.Toolbar toolbar
                = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }

        private void LoadBackdropCover()
        {
            try
            {
                Glide.With(this).Load(Resource.Drawable.cover1)
                    .Into((ImageView)FindViewById(Resource.Id.imageBackdrop));
            }
            catch (Exception e)
            {
                Log.Debug(TAG, e.Message);
            }
        }

        private int dpToPx(int dp)
        {
            Android.Util.DisplayMetrics displayMetrics = this.Resources.DisplayMetrics;
            int px = Convert.ToInt32(Math.Round(dp * (displayMetrics.Xdpi / (float)Android.Util.DisplayMetricsDensity.Default)));
            return px;
        }

        private void AppBarLayout_OffsetChanged(object sender, AppBarLayout.OffsetChangedEventArgs e)
        {
            bool isShow = false;
            int scrollRange = -1;
            if (scrollRange == -1)
                scrollRange = _appBarLayout.TotalScrollRange;
            if (scrollRange + e.VerticalOffset == 0)
            {
                this._collapsingToolbar.SetTitle(GetString(Resource.String.applicationName));
                isShow = true;
            }
            else
            {
                this._collapsingToolbar.SetTitle(" ");
                isShow = false;
            }
        }

    }
}

