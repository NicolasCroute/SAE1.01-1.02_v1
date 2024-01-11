using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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


        private bool haut, gauche, droite, bas = false;

        private ImageBrush joueurSkin = new ImageBrush();
        private ImageBrush sol1Skin = new ImageBrush();
        private ImageBrush buissonHautSkin = new ImageBrush();
        private ImageBrush buissonBasSkin = new ImageBrush();
        private ImageBrush buissonGaucheSkin = new ImageBrush();
        private ImageBrush buissonDroiteSkin = new ImageBrush();
        private ImageBrush ennemieZombieSkin = new ImageBrush();
        private ImageBrush ennemieSquSkin = new ImageBrush();
        //private ImageBrush ennemi1 = new ImageBrush();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int vitesseJoueur = 5;
        int compteur = 0, sprite = 1;
        private string direction;
        private string[] tableauApparenceDroite = { "images/hero_droites/HeroDroite_1.png", "images/hero_droites/HeroDroite_2.png", "images/hero_droites/HeroDroite_3.png", "images/hero_droites/HeroDroite_4.png", "images/hero_droites/HeroDroite_5.png", "images/hero_droites/HeroDroite_6.png", "images/hero_droites/HeroDroite_7.png", "images/hero_droites/HeroDroite_8.png", "images/hero_droites/HeroDroite_9.png" };
        private string[] tableauApparenceGauche = { "images/hero_gauche/HeroGauche_1.png", "images/hero_gauche/HeroGauche_2.png", "images/hero_gauche/HeroGauche_3.png", "images/hero_gauche/HeroGauche_4.png", "images/hero_gauche/HeroGauche_5.png", "images/hero_gauche/HeroGauche_6.png", "images/hero_gauche/HeroGauche_7.png", "images/hero_gauche/HeroGauche_8.png", "images/hero_gauche/HeroGauche_9.png" };
        private string[] tableauApparenceHaut = { "images/hero_haut/HeroHaut_1.png", "images/hero_haut/HeroHaut_2.png", "images/hero_haut/HeroHaut_3.png", "images/hero_haut/HeroHaut_4.png", "images/hero_haut/HeroHaut_5.png", "images/hero_haut/HeroHaut_6.png", "images/hero_haut/HeroHaut_7.png", "images/hero_haut/HeroHaut_8.png", "images/hero_haut/HeroHaut_9.png" };
        private string[] tableauApparenceBas = { "images/hero_bas/HeroBas_1.png", "images/hero_bas/HeroBas_2.png", "images/hero_bas/HeroBas_3.png", "images/hero_bas/HeroBas_4.png", "images/hero_bas/HeroBas_5.png", "images/hero_bas/HeroBas_6.png", "images/hero_bas/HeroBas_7.png", "images/hero_bas/HeroBas_8.png", "images/hero_bas/HeroBas_9.png" };
        private string[] tableauApparenceSqueletteDroite = { "images/squelette_marche_droite/squelette_marche_Droite_1.png", "images/squelette_marche_droite/squelette_marche_Droite_2.png", "images/squelette_marche_droite/squelette_marche_Droite_3.png", "images/squelette_marche_droite/squelette_marche_Droite_4.png", "images/squelette_marche_droite/squelette_marche_Droite_5.png", "images/squelette_marche_droite/squelette_marche_Droite_6.png", "images/squelette_marche_droite/squelette_marche_Droite_7.png", "images/squelette_marche_droite/squelette_marche_Droite_8.png", "images/squelette_marche_droite/squelette_marche_Droite_9.png", "images/squelette_marche_droite/squelette_marche_Droite_10.png" };
        private string[] tableauApparenceZombieDroite = { "image/zombiecourse/frame-1.gif", "image/zombiecourse/frame-2.gif", "image/zombiecourse/frame-3.gif", "image/zombiecourse/frame-4.gif", "image/zombiecourse/frame-5.gif", "image/zombiecourse/frame-6.gif", "image/zombiecourse/frame-7.gif", "image/zombiecourse/frame-8.gif", "image/zombiecourse/frame-9.gif", "image/zombiecourse/frame-99.gif" };
        private int tempsEntreMisesAJour = 16;
        private int tempsEcouleDepuisChangement = 0;
        private int intervalleChangementApparence = 34;
        private int changement = 0;
        private int vitesseennemie = 3;
        private int maxEnnemiemillisecond;
        private int delaiapparitionennemie = 500;
        private Random déplacementAléatoire = new Random();
        private int vitesseTireJoueur = 15;
        private List<Rectangle> supprimer = new List<Rectangle>();
        //private int[,] tableauApparitionEnnemie = { { 30, 100 },{500,100} };

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            //------------------A comprendre--------------
            Menu accesMenu = new Menu();
            accesMenu.ShowDialog();
            //----------------------?????-------------------
            //accesMenu.Owner = this;
            if (accesMenu.DialogResult == false)
            {
                Application.Current.Shutdown();
            }


            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(16);
            dispatcherTimer.Start();

            sol1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
            sol1.Fill = sol1Skin;
            joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png"));
            joueur1.Fill = joueurSkin;
            buissonBasSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_bas.png"));
            buissonBas.Fill = buissonBasSkin;

            buissonHautSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_haut.png"));
            buissonHaut.Fill = buissonHautSkin;

            buissonGaucheSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_gauche.png"));
            buissonGauche.Fill = buissonGaucheSkin;

            buissonDroiteSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_droite.png"));
            buissonDroite.Fill = buissonDroiteSkin;
            CreationEnnemie(1);


        }
        private void Jeu(object sender, EventArgs e)
        {
            Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1),
            joueur1.Width, joueur1.Height);
            DeplacementJoueur();
            ChangementApparence();

            foreach (Rectangle y in supprimer)
            {
                // on les enlève du canvas
                Canvas.Children.Remove(y);
            }


            foreach (Rectangle x in Canvas.Children.OfType<Rectangle>())
            {
                TestTireJoueur(x);

            }

            compteur++;
            if (compteur % delaiapparitionennemie == 0 && delaiapparitionennemie > 100)
            {

                delaiapparitionennemie -= 25;
                compteur = 0;
            }
            //else 
            //{ 
            //    gg 
            //} 

        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Left)
            {
                gauche = true;
                direction = "G";
            }
            if (e.Key == Key.Right)
            {
                droite = true;
                direction = "D";
            }
            if (e.Key == Key.Up)
            {
                haut = true;
                direction = "H";
            }
            if (e.Key == Key.Down)
            {
                bas = true;
                direction = "B";
            }
        }

        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {

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
            if (e.Key == Key.Space)
            {
                supprimer.Clear();
                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueur",
                    Height = 10,
                    Width = 10,
                    Fill = Brushes.Red,
                    Stroke = Brushes.White,
                };
                Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height / 2);
                Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
                Canvas.Children.Add(nouveauTire);
            }


        }
        private void TestTireJoueur(Rectangle x)
        {

            if (x is Rectangle && (string)x.Tag == "tireJoueur" && direction == "G")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireGauche = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                x.Tag = "TireG";
            }

            if ((string)x.Tag == "TireG")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) < 80)
                {
                    supprimer.Add(x);
                }
            }



            if (x is Rectangle && (string)x.Tag == "tireJoueur" && direction == "H")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                x.Tag = "TireH";


            }

            if ((string)x.Tag == "TireH")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetTop(x) < 0)
                {
                    supprimer.Add(x);
                }
            }



            if (x is Rectangle && (string)x.Tag == "tireJoueur" && direction == "B")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireBas = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                x.Tag = "TireB";


            }
            if ((string)x.Tag == "TireB")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetTop(x) > 500)
                {
                    supprimer.Add(x);
                }
            }



            if (x is Rectangle && (string)x.Tag == "tireJoueur" && direction == "D")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireDroite = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                x.Tag = "TireD";


            }
            if ((string)x.Tag == "TireD")
            {
                Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                if (Canvas.GetLeft(x) > 1200)
                {
                    supprimer.Add(x);
                }
            }
        }





        private void DeplacementJoueur()
        {
            if (gauche && Canvas.GetLeft(joueur1) > 0)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) - vitesseJoueur);
            }
            if (droite && Canvas.GetLeft(joueur1) > 0)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) + vitesseJoueur);
            }
            if (haut && Canvas.GetTop(joueur1) > 0)
            {
                Canvas.SetTop(joueur1, Canvas.GetTop(joueur1) - vitesseJoueur);
            }
            if (bas && Canvas.GetTop(joueur1) > 0)
            {
                Canvas.SetTop(joueur1, Canvas.GetTop(joueur1) + vitesseJoueur);
            }

        }

        private void ChangementApparence()
        {


            if (droite == true)
            {
                changement++;

                if (changement >= tableauApparenceDroite.Length)
                {
                    changement = 0;
                }
                string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceDroite[changement];
                joueurSkin.ImageSource = new BitmapImage(new Uri(image));
                joueur1.Fill = joueurSkin;
            }
            else if (gauche == true)
            {
                changement++;

                if (changement >= tableauApparenceDroite.Length)
                {
                    changement = 0;
                }
                string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceGauche[changement];
                joueurSkin.ImageSource = new BitmapImage(new Uri(image));
                joueur1.Fill = joueurSkin;

            }
            else if (haut == true)
            {
                changement++;

                if (changement >= tableauApparenceDroite.Length)
                {
                    changement = 0;
                }
                string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceHaut[changement];
                joueurSkin.ImageSource = new BitmapImage(new Uri(image));
                joueur1.Fill = joueurSkin;

            }
            else if (bas == true)
            {
                changement++;

                if (changement >= tableauApparenceDroite.Length)
                {
                    changement = 0;
                }
                string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceBas[changement];
                joueurSkin.ImageSource = new BitmapImage(new Uri(image));
                joueur1.Fill = joueurSkin;

            }
            else
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png"));
                joueur1.Fill = joueurSkin;
            }



        }
        private void CreationEnnemie(int nombreennemie)
        {
            int gauche = 100;
            for (int i = 0; i < nombreennemie; i++)
            {
                ImageBrush ennemieSkin = new ImageBrush();
                Rectangle newEnnemie = new Rectangle
                {
                    Tag = "ennemie",
                    Height = 75,
                    Width = 75,
                    Fill = ennemieSkin,
                };
                Canvas.SetTop(newEnnemie, 300);
                Canvas.SetLeft(newEnnemie, gauche);
                Canvas.Children.Add(newEnnemie);
                gauche = -50;
                ennemieSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/squelette_marche_droite/squelette_marche_Droite_1.png"));
                //pas mettre png ???
                //for (int j = 0; j < tableauApparenceZombieDroite.Length; j++)
                //{
                //    ennemieSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + tableauApparenceSqueletteDroite[j]));
                //    if (j <= tableauApparenceZombieDroite.Length)
                //    {
                //        j = 0;
                //    }
                //}
            }
        }

    }
}

    


    



 
