using System;
using System.Collections.Generic;
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
using System.IO;

namespace Лабораторная_работа_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> imagesFile = new List<BitmapImage>();
        int img = 0;//индекс для картинок
        
        public MainWindow()
        {
            InitializeComponent();
            SearchImages();//вывод картинок
            if (imagesFile.Count == 1)
            {
                img_1.Source = imagesFile[img];
                img_2.Source = imagesFile[img];
                img_3.Source = imagesFile[img];
            }
            else if (imagesFile.Count == 2)
            {
                img_1.Source = imagesFile[img];
                img_2.Source = imagesFile[img + 1];
                img_3.Source = imagesFile[img];
            }
            else if (imagesFile.Count == 0)
            {                
            }
            else
            {
                img_1.Source = imagesFile[img];
                img_2.Source = imagesFile[img + 1];
                img_3.Source = imagesFile[img + 2];
            }
        }

        private void SearchImages()//вывод изображений
        {
            int i = imagesFile.Count;
            string[] files = Directory.GetFiles("Pictures1","*.jpg", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                imagesFile.Add(Images_memory(File.ReadAllBytes(file)));
                i++;
            }
        }

        private BitmapImage Images_memory(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
            {
                return null;
            }

            BitmapImage image = new BitmapImage();

            try
            {
                MemoryStream image_memory = new MemoryStream(imageData); //создание потока, который хранит в себе картинки 
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;//кэширует все изоображения в памяти
                image.StreamSource = image_memory;//задаем поток
                image.EndInit();
                
            }
            catch { }

            return image;
        }

        private void RightBut(object sender, RoutedEventArgs e)
        {
            img++;
            if (imagesFile.Count <= 2)
                switch (imagesFile.Count)
                {
                    case 1:
                        img = 0;
                        break;
                    case 2:
                        if (img > imagesFile.Count - 1)
                            img = 0;
                        break;
                }
            else if (img > imagesFile.Count - 3)
            {
                img = 0;
            }
            ShiftImage();
        }

        private void LeftBut(object sender, RoutedEventArgs e)
        {
            img--;
            if (img < 0)
            {
                img = imagesFile.Count - 3;
            }
            switch (imagesFile.Count)
            {
                case 1:
                    img = 0;
                    break;
                case 2:
                    if (img < 0)
                        img = 1;
                    break;
            }
            ShiftImage();
        }

        private void ShiftImage()
        {
            if (imagesFile.Count <= 2)
                switch (imagesFile.Count)
                {
                    case 0:
                        break;
                    case 1:
                        img_3.Source = imagesFile[img];
                        img_2.Source = imagesFile[img];
                        img_1.Source = imagesFile[img];
                        break;
                    case 2:
                        img_3.Source = imagesFile[img];
                        if (img == 1)
                            img_2.Source = imagesFile[img - 1];
                        else
                            img_2.Source = imagesFile[img + 1];
                        img_1.Source = imagesFile[img];
                        break;
                }
            else
            {
                img_3.Source = imagesFile[img];
                img_2.Source = imagesFile[img + 1];
                img_1.Source = imagesFile[img + 2];
            }

        }
    }
}
