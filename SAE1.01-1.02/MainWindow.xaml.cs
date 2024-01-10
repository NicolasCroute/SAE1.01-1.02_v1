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
        private ImageBrush ennemieSkin = new ImageBrush();
        //private ImageBrush ennemi1 = new ImageBrush();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int vitesseJoueur = 5;
        int compteur = 0, sprite = 1;
        private string[] tableauApparenceDroite = { "images/hero_droites/HeroDroite_1.png", "images/hero_droites/HeroDroite_2.png", "images/hero_droites/HeroDroite_3.png", "images/hero_droites/HeroDroite_4.png", "images/hero_droites/HeroDroite_5.png", "images/hero_droites/HeroDroite_6.png", "images/hero_droites/HeroDroite_7.png", "images/hero_droites/HeroDroite_8.png", "images/hero_droites/HeroDroite_9.png" };
        private string[] tableauApparenceGauche = { "images/hero_gauche/HeroGauche_1.png", "images/hero_gauche/HeroGauche_2.png", "images/hero_gauche/HeroGauche_3.png", "images/hero_gauche/HeroGauche_4.png", "images/hero_gauche/HeroGauche_5.png", "images/hero_gauche/HeroGauche_6.png", "images/hero_gauche/HeroGauche_7.png", "images/hero_gauche/HeroGauche_8.png", "images/hero_gauche/HeroGauche_9.png" };
        private string[] tableauApparenceHaut = { "images/hero_haut/HeroHaut_1.png", "images/hero_haut/HeroHaut_2.png", "images/hero_haut/HeroHaut_3.png", "images/hero_haut/HeroHaut_4.png", "images/hero_haut/HeroHaut_5.png", "images/hero_haut/HeroHaut_6.png", "images/hero_haut/HeroHaut_7.png", "images/hero_haut/HeroHaut_8.png", "images/hero_haut/HeroHaut_9.png" };
        private string[] tableauApparenceBas = { "images/hero_bas/HeroBas_1.png", "images/hero_bas/HeroBas_2.png", "images/hero_bas/HeroBas_3.png", "images/hero_bas/HeroBas_4.png", "images/hero_bas/HeroBas_5.png", "images/hero_bas/HeroBas_6.png", "images/hero_bas/HeroBas_7.png", "images/hero_bas/HeroBas_8.png", "images/hero_bas/HeroBas_9.png" };
        private string[] tableauApparenceSqueletteDroite = { "images/squelette_marche_droite/squelette_marche_Droite_1.png", "images/squelette_marche_droite/squelette_marche_Droite_2.png", "images/squelette_marche_droite/squelette_marche_Droite_3.png", "images/squelette_marche_droite/squelette_marche_Droite_4.png", "images/squelette_marche_droite/squelette_marche_Droite_5.png", "images/squelette_marche_droite/squelette_marche_Droite_6.png", "images/squelette_marche_droite/squelette_marche_Droite_7.png", "images/squelette_marche_droite/squelette_marche_Droite_8.png", "images/squelette_marche_droite/squelette_marche_Droite_9.png", "images/squelette_marche_droite/squelette_marche_Droite_10.png" };
        private int tempsEntreMisesAJour = 16;
        private int tempsEcouleDepuisChangement = 0;
        private int intervalleChangementApparence = 34;
        private int changement = 0;
        private int vitesseennemie1 = 3;
        private int maxEnnemiemillisecond;
        private int delaiapparitionennemie = 500;
        private string directionEnnemie;

        //------------------------Modif--------------------
        private bool deplacementDroiteSquelette, deplacementGaucheSquelette = false;


        private int vitesseTireJoueur = 15;
        private List<Rectangle> itemsToRemove = new List<Rectangle>();

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
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
            ennemie1(100);



        }
        private void Jeu(object sender, EventArgs e)
        {
            Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1),
            joueur1.Width, joueur1.Height);
            DeplacementJoueur();

            
            tempsEcouleDepuisChangement = tempsEcouleDepuisChangement + tempsEntreMisesAJour;
            if (tempsEcouleDepuisChangement >= intervalleChangementApparence)
            {
                ChangementApparence();
                tempsEcouleDepuisChangement = 0;





                //----------------modif-------------------
                if (deplacementDroiteSquelette == true)
                {
                    ApparenceEnnemie1();
                }




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

            //tire joueur 
            if (e.Key == Key.Z)
            {

                itemsToRemove.Clear();
                // création un nouveau tir 
                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueurHaut", //permet de tagger les rectangles 
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.Black
                };

                // on place le tir à l’endroit du joueur 
                Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height / 2);
                Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
                // on place le tir dans le canvas 
                Canvas.Children.Add(nouveauTire);
            }

            if (e.Key == Key.Q)
            {

                itemsToRemove.Clear();
                // création un nouveau tir 

                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueurGauche", //permet de tagger les rectangles 
                    Height = 5,
                    Width = 20,
                    Fill = Brushes.Black
                };

                // on place le tir à l’endroit du joueur 
                Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height / 2);
                Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
                // on place le tir dans le canvas 
                Canvas.Children.Add(nouveauTire);
            }

            if (e.Key == Key.S)

            {

                itemsToRemove.Clear();
                // création un nouveau tir 
                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueurBas", //permet de tagger les rectangles 
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.Black
                };

                // on place le tir à l’endroit du joueur 
                Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height);
                Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
                // on place le tir dans le canvas 
                Canvas.Children.Add(nouveauTire);

            }

            if (e.Key == Key.D)
            {

                itemsToRemove.Clear();
                // création un nouveau tir 
                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueurDroite",
                    //permet de tagger les rectangles 
                    Height = 5,
                    Width = 20,
                    Fill = Brushes.Black
                };
                // on place le tir à l’endroit du joueur 
                Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height / 2);
                Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
                // on place le tir dans le canvas 
                Canvas.Children.Add(nouveauTire);

            }
        }


        private void ennemie1(int limit)
        {

            int left = 0;
            maxEnnemiemillisecond = limit;
            if (compteur % delaiapparitionennemie == 0 && delaiapparitionennemie > maxEnnemiemillisecond)
            {
                
                Rectangle newEnnemie = new Rectangle
                {
                    Tag = "ennemie",
                    Height = 120,
                    Width = 120,
                    Fill = ennemieSkin,
                };
                delaiapparitionennemie -= 25;
                compteur = 0;
                Canvas.SetTop(newEnnemie, 30);
                Canvas.SetLeft(newEnnemie, left);
                Canvas.Children.Add (newEnnemie);
                ennemieSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/zombiecourse/frame-15.gif"));







                //-----ajouter peut etre plusieur rectangle pour chaque enemis ???

                //----------------imaginon---------------
                deplacementDroiteSquelette = true;
                //----------------alors-----------------
                

                






                
            }

        }

        private void ApparenceEnnemie1()
        {
            changement++;

            if (changement >= tableauApparenceSqueletteDroite.Length)
            {
                changement = 0;
            }
            string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceSqueletteDroite[changement];
            ennemieSkin.ImageSource = new BitmapImage(new Uri(image));
            
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
            if(haut && Canvas.GetTop(joueur1)>0)
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
        private void TestTireJoueur(Rectangle x)

        {
            if (x is Rectangle && (string)x.Tag == "tireJoueurHaut")
            {
                // si c’est un tir joueur on le déplace vers le haut 
                Canvas.SetTop(x, Canvas.GetTop(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie) 
                if (Canvas.GetTop(x) < 10)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer 
                    itemsToRemove.Add(x);
                }

            }

            if (x is Rectangle && (string)x.Tag == "tireJoueurGauche")
            {

                // si c’est un tir joueur on le déplace vers le haut 
                Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireDroite = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie) 
                if (Canvas.GetTop(x) < 10)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer 
                    itemsToRemove.Add(x);
                }

            }

            if (x is Rectangle && (string)x.Tag == "tireJoueurDroite")
            {
                // si c’est un tir joueur on le déplace vers le haut 
                Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireDroite = new Rect(Canvas.GetTop(x), Canvas.GetLeft(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie) 
                if (Canvas.GetTop(x) < 10)

                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer 
                    itemsToRemove.Add(x);
                }

            }

            if (x is Rectangle && (string)x.Tag == "tireJoueurBas")
            {

                // si c’est un tir joueur on le déplace vers le haut 
                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseTireJoueur);
                // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
                Rect TireHaut = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                // on vérifie que le tir a quitté le le haut du canvas (pas de collision avec un ennemie) 
                if (Canvas.GetTop(x) < 10)
                {
                    // si c’est le cas on l’ajoute à la liste des éléments à supprimer 
                    itemsToRemove.Add(x);
                }

            }


        }
        //private Rect Ennemiemouvementetcollision(Rectangle x, Rect player)
        //{
        //    directionEnnemie =
        //    if (x is Rectangle && (string)x.Tag == "ennemie")
        //    {
        //        Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseennemie1);
        //    }
        //}

    }
 }
