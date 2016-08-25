using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using MusicApp.Droid.Models;
using Android.Content;
using Com.Bumptech.Glide;
using Android.Util;

namespace MusicApp.Droid.Extensions.Adapters
{

    public class AlbumViewHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; set; }
        public TextView Count { get; set; }
        public ImageView Thumbnail { get; set; }
        public ImageView Overflow { get; set; }

        public AlbumViewHolder(View view) : base(view)
        {
            this.Title = view.FindViewById<TextView>(Resource.Id.title);
            this.Count = view.FindViewById<TextView>(Resource.Id.count);
            this.Thumbnail = view.FindViewById<ImageView>(Resource.Id.thumbnail);
            this.Overflow = view.FindViewById<ImageView>(Resource.Id.overflow);
        }
    }

    public class AlbumsAdapter : RecyclerView.Adapter
    {
        private List<Album> _datasource;
        private Context _context;
        private string TAG = "AlbumsAdapter";

        public AlbumsAdapter(Context context, List<Album> datasource)
        {
            _datasource = datasource;
            _context = context;
        }

        public void UpdateDataSource(List<Album> datasource)
        {
            _datasource = datasource;
            NotifyDataSetChanged();
        }

        #region Overrides

        public override int ItemCount
        {
            get
            {
                return _datasource.Count;
            }
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Album album = _datasource[position];
            AlbumViewHolder viewHolder = holder as AlbumViewHolder;
            viewHolder.Title.Text = album.Name;
            viewHolder.Count.Text = string.Format("{0} songs", album.NumberOfSongs);
            viewHolder.Overflow.Click += (sender, e) => {
                ShowPopupMenu(viewHolder.Overflow);
            };

            try
            {
                Glide.With(_context).Load(album.Thumbnail).Into(viewHolder.Thumbnail);
            }
            catch (Exception e)
            {
                Log.Error(TAG, e.Message);
            }

        }

        private void ShowPopupMenu(View overflow)
        {
            Android.Support.V7.Widget.PopupMenu popup 
                = new Android.Support.V7.Widget.PopupMenu(_context, overflow);
            MenuInflater inflater = popup.MenuInflater;
            inflater.Inflate(Resource.Menu.MenuAlbum, popup.Menu);
            popup.MenuItemClick += Popup_MenuItemClick;
            popup.Show();
        }
       
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.AlbumCard, parent, false);
            return new AlbumViewHolder(itemView);
        }

        #endregion

        #region Events

        private void Popup_MenuItemClick(object sender, Android.Support.V7.Widget.PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.actionAddFavourite:
                    Toast.MakeText(_context, "Add to favourite",  ToastLength.Long).Show();
                    break;
                case Resource.Id.actionPlayNext:
                    Toast.MakeText(_context, "Play next", ToastLength.Long).Show();
                    break;
                default:
                    break;
            }
        }


        #endregion

    }

}