using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QDFeedParser;

namespace HNTiles
{
    public partial class MainWindow : Window
    {
        TilesCollection _tiles = new TilesCollection();

        public MainWindow()
        {
            InitializeComponent();

            this.TilesList.ItemsSource = _tiles;
        }

        private void GetTiles(object sender, RoutedEventArgs e)
        {
            btnGetTiles.IsEnabled = false;

            Uri feeduri = new Uri("http://www.hnsearch.com/bigrss");
            IFeedFactory factory = new HttpFeedFactory();
            IFeed feed = factory.CreateFeed(feeduri);
            foreach (var item in feed.Items)
            {
                Tile t = new Tile();
                t.Title = item.Title;
                t.Link = item.Link;
                _tiles.Add(t);
            }

            btnGetTiles.IsEnabled = true;
        }
    }

    class Tile
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }

    class TilesCollection :  ObservableCollection<Tile>
    {
        public void CopyFrom(IEnumerable<Tile> tiles)
        {
            this.Items.Clear();
            foreach (var t in tiles)
            {
                this.Items.Add(t);
            }

            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
