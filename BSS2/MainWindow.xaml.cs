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
using System.Windows.Threading;

/// <summary>
/// project BSS2.
/// this is the final version of the rock-paper-scissors game. We added a timer, a max score and images to the game.
/// </summary>
namespace AOE1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int scoreSpeler = 0;
        private int scoreCPU = 0;
        private int keuzeSpeler;
        private int keuzeCpu;
        private int eindScore = 10;
        private int secondeTeller = 4;
        private DispatcherTimer timer;
        
        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);

            

            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            
            timer.Start();

        }

        private void DispatcherTimer_Tick(object sender, EventArgs e) // Aanmaken van aftellendetimer (3sec). 
        {
            secondeTeller --;

            if (secondeTeller == 0)
            {
                timer.Stop();
                CpuWint();

                secondeTeller = 3;
                timer.Start();
            }

            TimerLbl.Content = $"{secondeTeller}";

        }

        private void CheckEindscore()  // Controle of iemand de eindscore behaald heeft. 
        {
            string berichtWinnaar;

            if (scoreCPU == eindScore || scoreSpeler == eindScore)
            {
                if (scoreSpeler == eindScore)
                {
                    berichtWinnaar = "Proficiat jij wint!";
                }
                else
                {
                    berichtWinnaar = "Helaas, de computer wint!";
                }
                timer.Stop();
                MessageBoxResult antwoord = MessageBox.Show("Wil je opnieuw spelen?", $"{berichtWinnaar}", MessageBoxButton.YesNo);

                if (antwoord == MessageBoxResult.Yes)
                {
                    scoreCPU = 0;
                    scoreSpeler = 0;
                    CpuKeuzeTxt.Text = default;
                    SpelerKeuzeTxt.Text = default;
                    CpuKeuzeTxt.Background = Brushes.LightGray;
                    SpelerKeuzeTxt.Background = Brushes.LightGray;
                    CpuScoreTxt.Text = "0";
                    SpelerScoreTxt.Text = "0";
                    ResultaatTxt.Text = "Maak je keuze";
                    CpuKeuzeImg.Source = default;
                    SpelerKeuzeImg.Source = default;
                    SchaarBtn.BorderBrush = Brushes.Gray;
                    BladBtn.BorderBrush = Brushes.Gray;
                    SteenBtn.BorderBrush = Brushes.Gray;
                    timer.Start();


                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }
        private void GelijkSpel() // Veranderingen van layout bij gelijkspel. 
        {
            SpelerKeuzeTxt.Background = Brushes.LightGray;
            CpuKeuzeTxt.Background = Brushes.LightGray;
            ResultaatTxt.Text = "Gelijkspel!";
            ZetKeuzeImg();
        }

        private void SpelerWint() // Veranderingen van layout wanneer speler wint. 
        {
            SpelerKeuzeTxt.Background = Brushes.LightGreen;
            CpuKeuzeTxt.Background = Brushes.OrangeRed;
            scoreSpeler++;
            SpelerScoreTxt.Text = scoreSpeler.ToString();
            ResultaatTxt.Text = "Speler wint!";
            ZetKeuzeImg();
            CheckEindscore();
        }

        private void CpuWint() // Veranderingen van layout wanneer cpu wint. 
        {
            CpuKeuzeTxt.Background = Brushes.LightGreen;
            SpelerKeuzeTxt.Background = Brushes.OrangeRed;
            scoreCPU++;
            CpuScoreTxt.Text = scoreCPU.ToString();
            ResultaatTxt.Text = "Computer wint";
            ZetKeuzeImg();
            CheckEindscore();

        }

        private void ZetKeuzeImg() // Getoonde afbeelding in het keuzevak. 
        {

            if (secondeTeller == 0) // Check eerst of de timer is verstreken. (ja = klok afbeeldingen)
            {
                ResultaatTxt.Text = "De tijd is op, computer wint.";
                CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\timer.jpg", UriKind.RelativeOrAbsolute));
                SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\timer.jpg", UriKind.RelativeOrAbsolute));
            }
            else
            {
                if (keuzeSpeler == 1)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\blad.jpg", UriKind.RelativeOrAbsolute));
                }
                else if (keuzeSpeler == 2)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\steen.jpg", UriKind.RelativeOrAbsolute));

                }
                else if (keuzeSpeler == 3)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\schaar2.png", UriKind.RelativeOrAbsolute));
                }

                if (keuzeCpu == 1)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\blad.jpg", UriKind.RelativeOrAbsolute));
                }
                else if (keuzeCpu == 2)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\steen.jpg", UriKind.RelativeOrAbsolute));

                }
                else if (keuzeCpu == 3)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\schaar2.png", UriKind.RelativeOrAbsolute));
                }
            }
            

        }

        private void CheckWinnaar()// Checken wie de ronde wint. 
        {

            Random rnd = new Random();              // Random nr van 1tem3, staat gelijk aan de keuze van de cpu.
            keuzeCpu = rnd.Next(1, 4);           // 1 = Blad , 2 = Steen , 3 = Schaar.


            if (keuzeSpeler == keuzeCpu)
            {
                GelijkSpel();
              
                
            }
            else if (keuzeSpeler != 2 && keuzeCpu != 2) // Check op uitzonderlijke keuzes. (1/3 en 3/1)
            {
                if (keuzeSpeler < keuzeCpu)
                {
                
                    CpuWint();

                }
                else
                {
                    SpelerWint();


                }
            }
            else if (keuzeSpeler < keuzeCpu) // Check resterende keuzes. (1/2, 2/1, 2/3, 3/2)
            {
                SpelerWint();
            }
            else
            {
                CpuWint();
            }
         
        }


        private void BladBtn_Click(object sender, RoutedEventArgs e) // speler klikt op blad
        {
            keuzeSpeler = 1;  // blad = 1
            BladBtn.BorderBrush = Brushes.Black;
            SteenBtn.BorderBrush = Brushes.Gray;
            SchaarBtn.BorderBrush = Brushes.Gray;
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void SteenBtn_Click(object sender, RoutedEventArgs e) // speler klikt op steen
        {
            keuzeSpeler = 2; // steen = 2
            
            SteenBtn.BorderBrush = Brushes.Black;
            BladBtn.BorderBrush = Brushes.Gray;
            SchaarBtn.BorderBrush = Brushes.Gray;
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void SchaarBtn_Click(object sender, RoutedEventArgs e) // speler klikt op schaar
        {
            keuzeSpeler = 3;  // schaar = 3
            
            SchaarBtn.BorderBrush = Brushes.Black;
            BladBtn.BorderBrush = Brushes.Gray;
            SteenBtn.BorderBrush = Brushes.Gray;
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void BladBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            BladBtnImg.Opacity = 0.2;
            
        }

        private void BladBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            BladBtnImg.Opacity = 1;
        }

        private void SchaarBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SchaarBtnImg.Opacity = 0.2;
        }

        private void SchaarBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            SchaarBtnImg.Opacity = 1;
        }

        private void SteenBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SteenBtnImg.Opacity = 0.2;
        }

        private void SteenBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            SteenBtnImg.Opacity = 1;
        }
    }
}
