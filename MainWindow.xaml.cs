using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = null;
        public MainWindow()
        {
            InitializeComponent();
            debug();


            colorBrush.Color = Color.FromRgb(255, 180, 180);    //Изначальный цвет, не связано с анимацией.
        }

        

        bool currenttheme = false;

        SolidColorBrush colorBrush = new SolidColorBrush(); //Слой анимации, который позже может быть привязана к любому элементу имеющее свойство Background
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            background_color.Background = colorBrush;   //Тут слой и привязывается к самому элементу(Border'у)

            //Переменные для хранения цветов отдельных каналов (красный, зеленый, синий) к которым анимация будет стремиться
            byte to_r;
            byte to_g;
            byte to_b;

                //Условие, привязывает к переменным значения к которым анимация должна стремиться.
                if (!currenttheme)
                {
                    to_r = 50;
                    to_g = 50;
                    to_b = 50;
                    currenttheme = !currenttheme;
                }
                else
                {
                    to_r = 245;
                    to_g = 245;
                    to_b = 245;
                    currenttheme = !currenttheme;
                }


            BackEase backEase = new BackEase()  //Формула плавности с отскоком
            {
                Amplitude = 0.3     //Амплитуда, от этого значения зависит насколько сильно будет отскок.
            };
            backEase.EasingMode = EasingMode.EaseInOut; //Когда функция будет действовать, In - в начале, Out - в конце, InOut - в начале и в конце

            ColorAnimation colorAnimation = new ColorAnimation(Color.FromRgb(to_r, to_g, to_b), TimeSpan.FromSeconds(2))    // Собственно сама анимация
            {
                EasingFunction = backEase   //Привязывается функция плавности к анимации.
            };
            colorBrush.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);   //Запуск анимации
        }












        //Вывод информации

        private void debug()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Color color = (Color)ColorConverter.ConvertFromString(background_color.Background.ToString());
            contectcolor.Content = string.Format("Color: {0} {1} {2}", color.R, color.G, color.B);
        }
    }
}
