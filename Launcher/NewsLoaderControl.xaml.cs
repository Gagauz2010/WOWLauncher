using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Launcher
{
    /// <summary>
    /// Логика взаимодействия для NewsLoaderControl.xaml
    /// </summary>
    public partial class NewsLoaderControl : UserControl
    {
        private string _tempPath = Path.GetTempFileName();
        private LinkedList<News> newsList = new LinkedList<News>();
        private LinkedListNode<News> node;
        private DispatcherTimer itemChanger = new DispatcherTimer();

        private class News
        {
            public ImageSource image { get; set; }
            public string head { get; set; }
            public string body { get; set; }
            public string link { get; set; }

            public News(string head, string body, ImageSource image, string link)
            {
                this.head = head;
                this.body = body;
                this.image = image;
                this.link = link;
            }
        }

        public NewsLoaderControl()
        {
            this.InitializeComponent();
        }

        public ImageSource NewsImage
        {
            get { return news_image.Source; }
            set { news_image.Source = value; }
        }

        public string NewsHead
        {
            get { return news_head.Text; }
            set { news_head.Text = value; }
        }

        public string NewsBody
        {
            get { return news_body.Text; }
            set { news_body.Text = value; }
        }

        public string NewsLink { get; set; }

        private void NewsControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (newsList.Count != 0)
                Process.Start(NewsLink);
        }

        /// <summary>
        /// Смена отображаемой новости
        /// </summary>
        /// <param name="next">Если значение true то устанавливает следующим элементом LinkedListNode.Next</param>
        private void changeNewsItem(bool next)
        {
            if (next)
            {
                node = node.Next;
                if (node == null)
                    node = newsList.First;
            }
            else
            {
                node = node.Previous;
                if (node == null)
                    node = newsList.Last;
            }

            Storyboard sb = FindResource("ChangeItemsBegin") as Storyboard;
            Storyboard.SetTarget(sb, MainGrid);
            sb.Completed += sb_Completed;
            sb.Begin();


        }

        private void sb_Completed(object sender, EventArgs e)
        {
            Storyboard sb = FindResource("ChangeItemsEnd") as Storyboard;
            Storyboard.SetTarget(sb, MainGrid);
            sb.Begin();

            SetNewsValues();
        }

        public void SetNews(string Url)
        {
            var client = new WebClient();

            client.DownloadFileCompleted += client_DownloadFileCompleted;

            client.DownloadFileAsync(new Uri(Url), _tempPath);
        }

        private void client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            using (StreamReader reader = new StreamReader(_tempPath))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] ex = line.Split(char.Parse("#"));
                    Uri imageUri = new Uri(ex[2]);
                    BitmapImage imageBitmap = new BitmapImage(imageUri);

                    News news = new News(ex[0], ex[1], imageBitmap, ex[3]);

                    newsList.AddLast(news);
                }
            }

            if (newsList.Count != 0)
            {
                MainGrid.Visibility = Visibility.Visible;
                news_indacator_label.Visibility = Visibility.Hidden;

                node = newsList.First;
                SetNewsValues();

                itemChanger.Tick += new EventHandler(itemChanger_Tick);
                itemChanger.Interval = new TimeSpan(0, 0, 5);
                itemChanger.Start();
            }
            else
            {
                news_indicator_text.Text = "Спосок новостей пуст";
                btn_left.Visibility = Visibility.Hidden;
                btn_right.Visibility = Visibility.Hidden;
            }
        }

        private void itemChanger_Tick(object sender, EventArgs e)
        {
            changeNewsItem(true);
        }

        private void SetNewsValues()
        {
            NewsImage = node.Value.image;
            NewsHead = node.Value.head;
            NewsBody = node.Value.body;
            NewsLink = node.Value.link;
        }

        private void btn_right_Click(object sender, RoutedEventArgs e)
        {
            changeNewsItem(true);
            itemChanger.Interval = new TimeSpan(0, 0, 5);
            itemChanger.Stop();
            itemChanger.Start();
        }

        private void btn_left_Click(object sender, RoutedEventArgs e)
        {
            changeNewsItem(false);
            itemChanger.Interval = new TimeSpan(0, 0, 5);
            itemChanger.Stop();
            itemChanger.Start();
        }
    }
}
