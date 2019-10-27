using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для NewsLoaderControl.xaml
    /// </summary>
    public partial class NewsLoaderControl
    {
        private readonly string _tempPath = Path.GetTempFileName();
        private readonly LinkedList<News> _newsList = new LinkedList<News>();
        private readonly DispatcherTimer _itemChanger = new DispatcherTimer();
        private LinkedListNode<News> _node;

        private class News
        {
            public ImageSource Image { get; }
            public string Head { get; }
            public string Body { get; }
            public string Link { get; }

            public News(string head, string body, ImageSource image, string link)
            {
                Head = head;
                Body = body;
                Image = image;
                Link = link;
            }
        }

        public NewsLoaderControl()
        {
            InitializeComponent();
        }

        public ImageSource NewsImage
        {
            get => NewsImageControl.Source;
            set => NewsImageControl.Source = value;
        }

        public string NewsHead
        {
            get => NewsHeadControl.Text;
            set => NewsHeadControl.Text = value;
        }

        public string NewsBody
        {
            get => NewsBodyControl.Text;
            set => NewsBodyControl.Text = value;
        }

        public string NewsLink { get; set; }

        private void NewsControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_newsList.Count != 0)
                Process.Start(NewsLink);
        }

        /// <summary>
        /// Смена отображаемой новости
        /// </summary>
        /// <param name="next">Если значение true то устанавливает следующим элементом LinkedListNode.Next</param>
        private void ChangeNewsItem(bool next)
        {
            if (next)
            {
                _node = _node.Next ?? _newsList.First;
            }
            else
            {
                _node = _node.Previous ?? _newsList.Last;
            }

            var sb = FindResource("ChangeItemsBegin") as Storyboard;
            Storyboard.SetTarget(sb ?? throw new InvalidOperationException("Не найден ресурс \"ChangeItemsBegin\""), MainGrid);
            sb.Completed += StoryBoard_Completed;
            sb.Begin();


        }

        private void StoryBoard_Completed(object sender, EventArgs e)
        {
            var sb = FindResource("ChangeItemsEnd") as Storyboard;
            Storyboard.SetTarget(sb ?? throw new InvalidOperationException("Не найден ресурс \"ChangeItemsEnd\""), MainGrid);
            sb.Begin();

            SetNewsValues();
        }

        public void SetNews(string url)
        {
            var client = new WebClient();

            client.DownloadFileCompleted += Client_DownloadFileCompleted;

            client.DownloadFileAsync(new Uri(url), _tempPath);
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            using (var reader = new StreamReader(_tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    var ex = line.Split(char.Parse("#"));
                    var imageUri = new Uri(ex[2]);
                    var imageBitmap = new BitmapImage(imageUri);

                    var news = new News(ex[0], ex[1], imageBitmap, ex[3]);

                    _newsList.AddLast(news);
                }
            }

            if (_newsList.Count != 0)
            {
                MainGrid.Visibility = Visibility.Visible;
                NewsIndacatorLabel.Visibility = Visibility.Hidden;

                _node = _newsList.First;
                SetNewsValues();

                _itemChanger.Tick += ItemChanger_Tick;
                _itemChanger.Interval = new TimeSpan(0, 0, 5);
                _itemChanger.Start();
            }
            else
            {
                NewsIndicatorText.Text = "Список новостей пуст";
                ButtonArrowLeft.Visibility = Visibility.Hidden;
                ButtonArrowRight.Visibility = Visibility.Hidden;
            }
        }

        private void ItemChanger_Tick(object sender, EventArgs e)
        {
            ChangeNewsItem(true);
        }

        private void SetNewsValues()
        {
            NewsImage = _node.Value.Image;
            NewsHead = _node.Value.Head;
            NewsBody = _node.Value.Body;
            NewsLink = _node.Value.Link;
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            ChangeNewsItem(true);
            _itemChanger.Interval = new TimeSpan(0, 0, 5);
            _itemChanger.Stop();
            _itemChanger.Start();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            ChangeNewsItem(false);
            _itemChanger.Interval = new TimeSpan(0, 0, 5);
            _itemChanger.Stop();
            _itemChanger.Start();
        }
    }
}
