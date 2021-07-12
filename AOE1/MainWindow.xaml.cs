using OpenQA.Selenium.Interactions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.VisualBasic;
using System.Text;
using System.Collections.Generic;
/// <summary>
/// project AOE1.
/// this version is an upgrade of the final rock-paper-scissors game. there are now 4 choices, there is a history of end scores for each player 
/// if you keep playing the game. there is also a history in the game itself to see how many times you or the pc chose a specific character.
/// </summary>
namespace AOE1
{
    /// <summary>
    /// Project AOE: 10 punten -> winnaar.
    /// versies: BSS1 - BSS2 - AOE1
    /// </summary>
    /// 
    /// <remarks>
    /// OPLEIDINGSONDERDEEL: Werkplekleren 1
    /// AUTEUR: Juan Jacobs
    /// DATUM: 08/01/2021
    /// </remarks>
    public partial class MainWindow : Window
    {

        private string historiekEindwinnaars;
        private string naamSpeler;
        private string hergebruikteNaam;
        private int scoreSpeler = 0;
        private int scoreCpu = 0;
        private int keuzeSpeler;
        private int keuzeCpu;
        private int eindScore = 10;
        private int secondeTeller = 4;

        private DispatcherTimer timer;
        // historiek van keuzes
            // speler historiek
        private int spelerRidder = 0;
        private int spelerBoogschutter = 0;
        private int spelerZwaardvechter = 0;
        private int spelerSpeerwerper = 0;
            // Cpu historiek
        private int cpuRidder = 0;
        private int cpuBoogschutter = 0;
        private int cpuZwaardvechter = 0;
        private int cpuSpeerwerper = 0;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);

            

            timer.Tick += new EventHandler(DispatcherTimer_Tick);
            
            timer.Start();

        }
        
        

        private void DispatcherTimer_Tick(object sender, EventArgs e) // Aanmaken taken van aftellende timer (3sec). 
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
            string tempEindwinnaar;
            string titelHistoriek = "--- Historiek eindwinnaars ---";

            
            
            

            if (scoreCpu == eindScore || scoreSpeler == eindScore)
            {
                // naam opvragen speler als eindscore behaald is
                timer.Stop();
                naamSpeler = Microsoft.VisualBasic.Interaction.InputBox("Geef je naam in: ","Naam speler",hergebruikteNaam);
               
                if (naamSpeler == "")
                {
                    naamSpeler = "Anonieme speler";
                }
               
                hergebruikteNaam = naamSpeler;
                tempEindwinnaar = $"{naamSpeler, 15} - Computer {scoreSpeler, -3} - {scoreCpu, -3} ({DateTime.Now.ToString("HH:mm:ss")}) \n" ;

                historiekEindwinnaars = tempEindwinnaar  + historiekEindwinnaars;

                // checken wie gewonnen heeft
                if (scoreSpeler == eindScore)
                {
                    berichtWinnaar = "Proficiat jij wint!";
                }
                else
                {
                    berichtWinnaar = "Helaas, de computer wint!";
                }

                MessageBoxResult antwoord = MessageBox.Show($"Wil je opnieuw spelen?\n\n\n{titelHistoriek}\n\n{historiekEindwinnaars} ", $"{berichtWinnaar}", MessageBoxButton.YesNo);

                if (antwoord == MessageBoxResult.Yes)
                {
                    ResetGame();
                }
                else
                {
                    Application.Current.Shutdown();
                }
            }
        }

        private void ResetGame()
        {
            // score resetten
            scoreCpu = 0;
            scoreSpeler = 0;
            CpuScoreTxt.Text = "0";
            SpelerScoreTxt.Text = "0";

            // keuzevakken resetten

            CpuKeuzeTxt.Text = default;
            SpelerKeuzeTxt.Text = default;
            CpuKeuzeTxt.Background = Brushes.LightGray;
            SpelerKeuzeTxt.Background = Brushes.LightGray;
            CpuKeuzeImg.Source = default;
            SpelerKeuzeImg.Source = default;

            // Resultaat lbl + buttons resetten

            ResultaatTxt.Text = "Maak je keuze";
            
            ZwaardvechterBtn.BorderBrush = Brushes.Gray;
            RidderBtn.BorderBrush = Brushes.Gray;
            BoogschutterBtn.BorderBrush = Brushes.Gray;
            // historiek (keuzes) resetten
            // historiek speler
            spelerRidder = 0;
            spelerBoogschutter = 0;
            spelerZwaardvechter = 0;
            spelerSpeerwerper = 0;
            SpelerRidderLbl.Content = 0;
            SpelerBoogschutterLbl.Content = 0;
            SpelerZwaardvechterLbl.Content = 0;
            SpelerSpeerwerperLbl.Content = 0;

            // historiek Cpu
            cpuRidder = 0;
            cpuBoogschutter = 0;
            cpuZwaardvechter = 0;
            cpuSpeerwerper = 0;
            CpuRidderLbl.Content = 0;
            CpuBoogschutterLbl.Content = 0;
            CpuZwaardvechterLbl.Content = 0;
            CpuSpeerwerperLbl.Content = 0;
            timer.Start();
        } 
        private void GelijkSpel() // Veranderingen van layout bij gelijkspel. 
        {
            SpelerKeuzeTxt.Background = Brushes.LightGray;
            CpuKeuzeTxt.Background = Brushes.LightGray;
            ResultaatTxt.Text = "Gelijkspel!";
            ZetKeuzeVakEnHistoriek();
        }

        private void SpelerWint() // Veranderingen van layout wanneer speler wint. 
        {
            SpelerKeuzeTxt.Background = Brushes.LightGreen;
            CpuKeuzeTxt.Background = Brushes.OrangeRed;
            scoreSpeler++;
            SpelerScoreTxt.Text = scoreSpeler.ToString();
            ResultaatTxt.Text = "Speler wint!";
            ZetKeuzeVakEnHistoriek();
            CheckEindscore();
        }

        private void CpuWint() // Veranderingen van layout wanneer cpu wint. 
        {
            CpuKeuzeTxt.Background = Brushes.LightGreen;
            SpelerKeuzeTxt.Background = Brushes.OrangeRed;
            scoreCpu++;
            CpuScoreTxt.Text = scoreCpu.ToString();
            ResultaatTxt.Text = "Computer wint";
            ZetKeuzeVakEnHistoriek();
            CheckEindscore();

        }

        /// <summary>
        /// toont afbeelding van de keuze in het speelvak + update de historiek
        /// </summary>
        private void ZetKeuzeVakEnHistoriek() 
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
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\ridder.jpg", UriKind.RelativeOrAbsolute));
                    spelerRidder++;
                    SpelerRidderLbl.Content = spelerRidder;
                }
                else if (keuzeSpeler == 2)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\boogshutter.jpg", UriKind.RelativeOrAbsolute));
                    spelerBoogschutter++;
                    SpelerBoogschutterLbl.Content = spelerBoogschutter;

                }
                else if (keuzeSpeler == 3)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\zwaardvechter.jpg", UriKind.RelativeOrAbsolute));
                    spelerZwaardvechter++;
                    SpelerZwaardvechterLbl.Content = spelerZwaardvechter;
                }
                else if (keuzeSpeler == 4)
                {
                    SpelerKeuzeImg.Source = new BitmapImage(new Uri(@"\img\speerwerper.jpg", UriKind.RelativeOrAbsolute));
                    spelerSpeerwerper++;
                    SpelerSpeerwerperLbl.Content = spelerSpeerwerper;
                }

                if (keuzeCpu == 1)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\ridder.jpg", UriKind.RelativeOrAbsolute));
                    cpuRidder++;
                    CpuRidderLbl.Content = cpuRidder;
                }
                else if (keuzeCpu == 2)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\boogshutter.jpg", UriKind.RelativeOrAbsolute));
                    cpuBoogschutter++;
                    CpuBoogschutterLbl.Content = cpuBoogschutter;

                }
                else if (keuzeCpu == 3)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\zwaardvechter.jpg", UriKind.RelativeOrAbsolute));
                    cpuZwaardvechter++;
                    CpuZwaardvechterLbl.Content = cpuZwaardvechter;
                }
                else if (keuzeCpu == 4)
                {
                    CpuKeuzeImg.Source = new BitmapImage(new Uri(@"\img\speerwerper.jpg", UriKind.RelativeOrAbsolute));
                    cpuSpeerwerper++;
                    CpuSpeerwerperLbl.Content = cpuSpeerwerper;
                }
            }
            

        }

        /// <summary>
        /// checkt winnaar per ronde
        /// </summary>
        private void CheckWinnaar() 
        {
            // Random nr van 1tem3, staat gelijk aan de keuze van de cpu.
            // 1 = Ridder , 2 = Boogschutter , 3 = Zwaardvechter, 4 = Speerwerper.
            Random rnd = new Random();              
            keuzeCpu = rnd.Next(1, 5);           


            if ((keuzeSpeler + 2)==keuzeCpu||(keuzeCpu +2)== keuzeSpeler || keuzeSpeler == keuzeCpu)
            {
                GelijkSpel();              
            }
            else if (keuzeSpeler != 2 && keuzeCpu != 2 && keuzeSpeler != 3 && keuzeCpu != 3) // Check op *uitzonderlijke keuzes. (1-4 en 4-1) 
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
            else if (keuzeSpeler < keuzeCpu) // Check resterende keuzes. (1-2, 2-1, 2-3, 3-2, 3-4, 4-3)
            {
                SpelerWint();
            }
            else
            {
                CpuWint();
            }

          // * uitzonderlijke keuze omdat enkel hier het grootste getal wint van het kleinste.
        }

        /// <summary>
        /// aanpassen van de borders op de knoppen, afhankelijk van de geselecteerde knop.
        /// </summary>
        /// <param name="geselecteerdeButton"></param>
        private void ButtonBorders(Button geselecteerdeButton)
        {
            geselecteerdeButton.BorderBrush = Brushes.Black;

            if (geselecteerdeButton ==  RidderBtn)
            {
                ZwaardvechterBtn.BorderBrush = Brushes.Gray;
                BoogschutterBtn.BorderBrush = Brushes.Gray;
                SpeerwerperBtn.BorderBrush = Brushes.Gray;
            }
            else if (geselecteerdeButton == BoogschutterBtn)
            {
                ZwaardvechterBtn.BorderBrush = Brushes.Gray;
                RidderBtn.BorderBrush = Brushes.Gray;
                SpeerwerperBtn.BorderBrush = Brushes.Gray;
            }
            else if (geselecteerdeButton == ZwaardvechterBtn)
            {
                RidderBtn.BorderBrush = Brushes.Gray;
                BoogschutterBtn.BorderBrush = Brushes.Gray;
                SpeerwerperBtn.BorderBrush = Brushes.Gray;
            }
            else
            {
                ZwaardvechterBtn.BorderBrush = Brushes.Gray;
                BoogschutterBtn.BorderBrush = Brushes.Gray;
                RidderBtn.BorderBrush = Brushes.Gray;
            }
            
        }

        private void RidderBtn_Click(object sender, RoutedEventArgs e) // speler klikt op ridder
        {
            keuzeSpeler = 1;  // Ridder = 1
            ButtonBorders(RidderBtn);
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void BoogschutterBtn_Click(object sender, RoutedEventArgs e) // speler klikt op boogschutter
        {
            keuzeSpeler = 2; // Boogschutter = 2
            ButtonBorders(BoogschutterBtn);
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void ZwaardvechterBtn_Click(object sender, RoutedEventArgs e) // speler klikt op zwaardvechter
        {
            keuzeSpeler = 3;  // Zwaardvechter = 3

            ButtonBorders(ZwaardvechterBtn);
            CheckWinnaar();
            secondeTeller = 4;
        }

        private void SpeerwerperBtn_Click(object sender, RoutedEventArgs e) // speler klikt op speerwerper
        {
            keuzeSpeler = 4;  // Speerwerper = 4

            ButtonBorders(SpeerwerperBtn);
            CheckWinnaar();
            secondeTeller = 4;
        }

        // Hover effecten voor alle knoppen

        private void RidderBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            RidderBtnImg.Opacity = 0.2;        
        }

        private void RidderBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            RidderBtnImg.Opacity = 1;
        }

        private void BoogschutterBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            BoogschutterBtnImg.Opacity = 0.2;
        }

        private void BoogschutterBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            BoogschutterBtnImg.Opacity = 1;
        }

        private void ZwaardvechterBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            ZwaardvechterBtnImg.Opacity = 0.2;
        }

        private void ZwaardvechterBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            ZwaardvechterBtnImg.Opacity = 1;
        }

        private void SpeerwerperBtnImg_MouseEnter(object sender, MouseEventArgs e)
        {
            SpeerwerperBtnImg.Opacity = 0.2;
        }

        private void SpeerwerperBtnImg_MouseLeave(object sender, MouseEventArgs e)
        {
            SpeerwerperBtnImg.Opacity = 0.2;
        }

        private void SpeerwerperBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            SpeerwerperBtnImg.Opacity = 0.2;
        }

        private void SpeerwerperBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            SpeerwerperBtnImg.Opacity = 1;
        }
    }
}
