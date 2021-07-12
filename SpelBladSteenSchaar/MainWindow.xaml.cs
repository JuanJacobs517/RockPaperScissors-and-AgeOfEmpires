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

/// <summary>
/// project BSS1.
/// BSS1 is the first version of the rock-paper-scissors game. there is no timer or final score, just the 3 choices. you vs the computer.
/// </summary>
namespace BSS1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int scoreUser = 0;
        private int scoreCPU = 0;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void GelijkSpel()
        {
            SpelerKeuzeTxt.Background = Brushes.LightGray;
            CpuKeuzeTxt.Background = Brushes.LightGray;
            
        }

        private void SpelerWint() // veranderingen layout wanneer speler wint.
        {
            SpelerKeuzeTxt.Background = Brushes.LightGreen;
            CpuKeuzeTxt.Background = Brushes.OrangeRed;
            scoreUser++;
            SpelerScoreTxt.Text = scoreUser.ToString();

        }

        private void CpuWint() //veranderingen layout wanneer cpu wint
        {
            CpuKeuzeTxt.Background = Brushes.LightGreen;
            SpelerKeuzeTxt.Background = Brushes.OrangeRed;
            scoreCPU++;
            CpuScoreTxt.Text = scoreCPU.ToString();
        }

        private void VergelijkKeuze(string keuze)
        {
            
            Random rnd = new Random();              // random nr van 1tem3 staat gelijk aan de keuze van de cpu
            int keuzeCpu = rnd.Next(1,4);           // 1 = Blad , 2 = Steen , 3 = Schaar

            SpelerKeuzeTxt.Background = default;   //reset van de kleur in vorige ronde
            CpuKeuzeTxt.Background = default;

            

            switch (keuze)
            {
                case "Blad":
                    if (keuzeCpu == 1) // keuzeCpu = blad
                    {
                        SpelerKeuzeTxt.Text = "Blad";
                        CpuKeuzeTxt.Text = "Blad";
                        ResultaatTxt.Text = "Gelijkspel! Jullie kozen beide voor blad.";
                        GelijkSpel();
                    }
                    else if (keuzeCpu == 2) // keuzeCpu steen
                    {
                        SpelerKeuzeTxt.Text = "Blad";
                        CpuKeuzeTxt.Text = "Steen";
                        ResultaatTxt.Text = "Jij wint! Blad wint van steen.";
                        SpelerWint();
                    }
                    else  // keuzeCPu schaar
                    {
                        SpelerKeuzeTxt.Text = "Blad";
                        CpuKeuzeTxt.Text = "Schaar";
                        ResultaatTxt.Text = "Jij verliest! Blad verliest van schaar.";
                        CpuWint();
                    }
                    break;
                case "Steen":
                    if (keuzeCpu == 2) // keuzeCpu = steen
                    {
                        SpelerKeuzeTxt.Text = "Steen";
                        CpuKeuzeTxt.Text = "Steen";
                        ResultaatTxt.Text = "Gelijkspel! Jullie kozen beide voor steen.";
                        GelijkSpel();
                    }
                    else if (keuzeCpu == 1) //keuzeCpu = blad
                    {
                        SpelerKeuzeTxt.Text = "Steen";
                        CpuKeuzeTxt.Text = "Blad";
                        ResultaatTxt.Text = "Jij verliest! Steen verliest van Blad";
                        CpuWint();
                    }
                    else // keuzeCpu = Schaar
                    {
                        SpelerKeuzeTxt.Text = "Steen";
                        CpuKeuzeTxt.Text = "Schaar";
                        ResultaatTxt.Text = "Jij wint! Steen wint van schaar.";
                        SpelerWint();
                    }

                    break;
                case "Schaar":
                    if (keuzeCpu == 3) // keuzeCpu = Schaar
                    {
                        SpelerKeuzeTxt.Text = "Schaar";
                        CpuKeuzeTxt.Text = "Schaar";
                        ResultaatTxt.Text = "Gelijkspel! Jullie kozen beide voor schaar.";
                        GelijkSpel();
                    }
                    else if (keuzeCpu == 1) //keuzeCpu = blad
                    {
                        SpelerKeuzeTxt.Text = "Schaar";
                        CpuKeuzeTxt.Text = "Blad";
                        ResultaatTxt.Text = "Jij wint! Schaar wint van blad.";
                        SpelerWint();
                    }
                    else // keuzeCpu = Steen
                    {
                        SpelerKeuzeTxt.Text = "Schaar";
                        CpuKeuzeTxt.Text = "Steen";
                        ResultaatTxt.Text = "Jij verliest! Schaar verliest van steen.";
                        CpuWint();
                    }

                    break;


                default:
                    MessageBox.Show("Ongeldige input");
                    break;
            }




        }

        private void BladBtn_Click(object sender, RoutedEventArgs e)
        {
            VergelijkKeuze("Blad");
        }

        private void SteenBtn_Click(object sender, RoutedEventArgs e)
        {
            VergelijkKeuze("Steen");
        }

        private void SchaarBtn_Click(object sender, RoutedEventArgs e)
        {
            VergelijkKeuze("Schaar");
        }
    }
}
