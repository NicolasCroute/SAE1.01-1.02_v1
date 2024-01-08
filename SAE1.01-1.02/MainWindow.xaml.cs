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

namespace SAE1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool haut, gauche,droite,bas = false;
        private ImageBrush joueurSkin = new ImageBrush();
        private ImageBrush sol1 = new ImageBrush();
        private ImageBrush ennemi1 = new ImageBrush();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int vitesseJoueur = 10;
        int compteur = 0, sprite = 1;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            //joueur1=joueurSkin
            
        }
        private void Jeu(object sender, EventArgs e)
        {
            Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1),
            joueur1.Width, joueur1.Height);

        }



        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction de l’appui de la touche
            if (e.Key == Key.Left)
            {
                gauche = true;
            }
            if (e.Key == Key.Right)
            {
                droite = true;
            }
            if (e.Key == Key.Up) 
            {
                haut = true;
            }
            if (e.Key == Key.Down)
            {
                bas = true;
            }
        }

        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {
            // on gère les booléens gauche et droite en fonction du relâchement de la touche
            if (e.Key == Key.Left)
            {
                gauche = false;
            }
            if (e.Key == Key.Right)
            {
                droite = false;
            }
            if (e.Key == Key.Up)
            {
                haut = false;
            }
            if (e.Key == Key.Down)
            {
                bas = false;
            }
        }
        private void DeplacementJoueur()
        {
            if (gauche && Canvas.GetLeft(joueur1)>0)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) - vitesseJoueur);
            }
            if (droite && Canvas.GetLeft(joueur1) > 0)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) + vitesseJoueur);
            }

        }
    }
 }
