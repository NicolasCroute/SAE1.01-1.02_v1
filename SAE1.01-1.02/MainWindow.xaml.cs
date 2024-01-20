using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Printing;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SAE1._01_1._02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private bool haut, gauche, droite, bas = false;
        private bool tireHaut, tireGauche, tireDroite, tireBas = false;
        private bool jeuEnPause = false;
        private bool modeJeuRecup;

        private ImageBrush joueurSkin = new ImageBrush();
        private ImageBrush sol1Skin = new ImageBrush();
        private ImageBrush buissonHautSkin = new ImageBrush();
        private ImageBrush buissonBasSkin = new ImageBrush();
        private ImageBrush buissonGaucheSkin = new ImageBrush();
        private ImageBrush buissonDroiteSkin = new ImageBrush();
        private ImageBrush ennemieZombieSkin = new ImageBrush();
        private ImageBrush ennemiSkin = new ImageBrush();
        private ImageBrush perduSkin = new ImageBrush();
        private ImageBrush rejouerSkin = new ImageBrush();
        private ImageBrush quitterSkin = new ImageBrush();

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int vitesseJoueur = 5;
        int compteur = 0, sprite = 1, compteurennemie =0;
        private string direction = "A";
        private string[] tableauApparenceDroite = { "images/hero_droites/HeroDroite_1.png", "images/hero_droites/HeroDroite_2.png", "images/hero_droites/HeroDroite_3.png", "images/hero_droites/HeroDroite_4.png", "images/hero_droites/HeroDroite_5.png", "images/hero_droites/HeroDroite_6.png", "images/hero_droites/HeroDroite_7.png", "images/hero_droites/HeroDroite_8.png", "images/hero_droites/HeroDroite_9.png" };
        private string[] tableauApparenceGauche = { "images/hero_gauche/HeroGauche_1.png", "images/hero_gauche/HeroGauche_2.png", "images/hero_gauche/HeroGauche_3.png", "images/hero_gauche/HeroGauche_4.png", "images/hero_gauche/HeroGauche_5.png", "images/hero_gauche/HeroGauche_6.png", "images/hero_gauche/HeroGauche_7.png", "images/hero_gauche/HeroGauche_8.png", "images/hero_gauche/HeroGauche_9.png" };
        private string[] tableauApparenceHaut = { "images/hero_haut/HeroHaut_1.png", "images/hero_haut/HeroHaut_2.png", "images/hero_haut/HeroHaut_3.png", "images/hero_haut/HeroHaut_4.png", "images/hero_haut/HeroHaut_5.png", "images/hero_haut/HeroHaut_6.png", "images/hero_haut/HeroHaut_7.png", "images/hero_haut/HeroHaut_8.png", "images/hero_haut/HeroHaut_9.png" };
        private string[] tableauApparenceBas = { "images/hero_bas/HeroBas_1.png", "images/hero_bas/HeroBas_2.png", "images/hero_bas/HeroBas_3.png", "images/hero_bas/HeroBas_4.png", "images/hero_bas/HeroBas_5.png", "images/hero_bas/HeroBas_6.png", "images/hero_bas/HeroBas_7.png", "images/hero_bas/HeroBas_8.png", "images/hero_bas/HeroBas_9.png" };
        private string[] tableauApparenceSqueletteDroite = { "images/squelette/squelette_marche_droite/squelette_marche_Droite_1.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_2.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_3.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_4.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_5.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_6.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_7.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_8.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_9.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_10.png" };
        private string[] tableauApparenceSqueletteGauche = { "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_1.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_2.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_3.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_4.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_5.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_6.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_7.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_8.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_9.png", "images/squelette/squelette_marche_gauche/squelette_marche_Gauche_10.png" };
        private string[] tableauApparenceZombieDroite = { "images/zombiecourse/frame-1.gif", "images/zombiecourse/frame-2.gif", "images/zombiecourse/frame-3.gif", "images/zombiecourse/frame-4.gif", "images/zombiecourse/frame-5.gif", "images/zombiecourse/frame-6.gif", "images/zombiecourse/frame-7.gif", "images/zombiecourse/frame-8.gif", "images/zombiecourse/frame-9.gif", "images/zombiecourse/frame-99.gif" };
        private int[] tableauspawnennemieVerticale = { 300, 100, 200, 400, 300, 200, 100, 400 };
        private int[] tableauspawnennemieHorizontale = { 0, 0, 0, 0, 1200, 1200, 1200, 1200 };
        private int tempsEntreMisesAJour = 16;
        private int tempsEcouleDepuisChangement = 0;
        private int intervalleChangementApparence = 40;
        private int changement = 0;
        private int changementEnnemi = 0;
        private int vitesseTireJoueur = 15;
        private int nombreEnnemie = 7;
        private int pvennemie = 3;

        private Random nombrealeatoire = new Random();
        private Rectangle newEnnemie;
        private List<Rectangle> supprimer = new List<Rectangle>();
        private List<Rectangle> ennemieListe = new List<Rectangle>();
        private List<int> directionsennemieListe = new List<int>();
        
        private string toucheAvancer;
        private string toucheReculer;
        private string toucheGauche;
        private string toucheDroite;
        private string toucheTire;

        private Menu accesMenu;

        
        public MainWindow()
        {
            InitializeComponent();
            
            WindowState = WindowState.Maximized;


            //------------------A comprendre--------------
            accesMenu = new Menu();
            accesMenu.ShowDialog();
            //----------------------?????-------------------
            //accesMenu.Owner = this;
            if (accesMenu.DialogResult == false)
            {
                Application.Current.Shutdown();
            }


            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(tempsEntreMisesAJour);
            dispatcherTimer.Start();

            

            joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png"));
            joueur1.Fill = joueurSkin;
            

            perduSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/text/gameOver.png"));
            gameOver.Fill = perduSkin;

            rejouerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/rejouer.png"));
            rejouer.Background = rejouerSkin;

            quitterSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/bouton_quitter.png"));
            but_quitter.Background = quitterSkin;


            toucheAvancer = accesMenu.valeurAvancer;
            toucheReculer = accesMenu.valeurReculer;
            toucheGauche = accesMenu.valeurGauche;
            toucheDroite = accesMenu.valeurDroite;
            toucheTire = accesMenu.valeurTire;

            //--------------Mode Jeu (recupérer depuis menu)------------------

            modeJeuRecup = accesMenu.modeJeu;
            Console.WriteLine("Mode de jeu actuel : " + modeJeuRecup);

            ModeDeJeu(modeJeuRecup);
            CreationEnnemie();
        }

        private void JouerSon()
        {
            son.Play();
        }

        private void ModeDeJeu(bool modeJeuRecup)
        {
            Console.WriteLine("Mode de jeu actuel dans modeJeu : " + modeJeuRecup);
            if (modeJeuRecup == false)
            {
                sol1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
                sol1.Fill = sol1Skin;

                buissonBasSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_bas.png"));
                buissonBas.Fill = buissonBasSkin;

                buissonHautSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_haut.png"));
                buissonHaut.Fill = buissonHautSkin;

                buissonGaucheSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_gauche.png"));
                buissonGauche.Fill = buissonGaucheSkin;

                buissonDroiteSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/buisson/buisson_droite.png"));
                buissonDroite.Fill = buissonDroiteSkin;
            }
            else if (modeJeuRecup == true)
            {
                sol1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_difficile.png"));
                sol1.Fill = sol1Skin;
            }
        }




        private void Jeu(object sender, EventArgs e)
        {
            Rect joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);
            //Console.WriteLine(joueur + "joueur");

            DeplacementJoueur();
            ChangementApparence();
            ChangementApparenceEnnemis(newEnnemie);
            MouvementEnnemiesEtCollision(joueur);

            foreach (Rectangle y in supprimer)
            {
                // on les enlève du canvas
                Canvas.Children.Remove(y);
            }

            foreach (Rectangle x in Canvas.Children.OfType<Rectangle>())
            {
                if (x.Tag != null && x is Rectangle && ((string)x.Tag).Substring(0, ((string)x.Tag).Length - 1) == "tireJoueur")
                    TestTireJoueur(x);
            }
            MouvementEnnemiesEtCollision(joueur);
            /* foreach (Rectangle ennemie in ennemieListe)
             {
                 TirEnnemi(ennemie, joueur);
             }*/
            
            foreach (Rectangle ennemie in ennemieListe)
            {
                if (tempsEcouleDepuisChangement >= intervalleChangementApparence)
                {
                    ChangementApparenceEnnemis(ennemie);
                    tempsEcouleDepuisChangement = 0;
                }
                else
                {
                    tempsEcouleDepuisChangement = tempsEcouleDepuisChangement + tempsEntreMisesAJour;
                }

            }
            
            
            compteur++;
            /* if (compteur % 100 == 0)
             {

             }
             if (compteur % delaiapparitionennemie == 0 && delaiapparitionennemie > 100)
             {

                 delaiapparitionennemie -= 25;
                 compteur = 0;
             }*/
            //else 
            //{ 
            //    gg 
            //} 

        }

        private void CanvasKeyIsDown(object sender, KeyEventArgs e)
        {


            if (e.Key.ToString() == toucheGauche || e.Key == Key.Left)
            {
                gauche = true;
                direction = "G";
            }

            if (e.Key.ToString() == toucheDroite || e.Key == Key.Right)
            {
                droite = true;
                direction = "D";
            }


            if (e.Key.ToString() == toucheAvancer || e.Key == Key.Up)
            {
                haut = true;
                direction = "H";
            }

            if (e.Key.ToString() == toucheReculer || e.Key == Key.Down)
            {
                bas = true;
                direction = "B";
            }

            if (e.Key == Key.Escape)
            {
                jeuEnPause = !jeuEnPause;
                if (jeuEnPause == true)
                {
                    dispatcherTimer.Stop();
                    canvasPause.Visibility = Visibility.Visible;
                }
                else
                {
                    dispatcherTimer.Start();
                    canvasPause.Visibility = Visibility.Hidden;
                }
            }

        }
        private void CanvasKeyIsUp(object sender, KeyEventArgs e)
        {

            if (e.Key.ToString() == toucheGauche || e.Key == Key.Left)
            {
                gauche = false;
            }
            if (e.Key.ToString() == toucheDroite || e.Key == Key.Right)
            {
                droite = false;

            }
            if (e.Key.ToString() == toucheAvancer || e.Key == Key.Up)
            {
                haut = false;

            }
            if (e.Key.ToString() == toucheReculer || e.Key == Key.Down)
            {
                bas = false;
            }
            if (e.Key.ToString() == toucheTire || e.Key == Key.Space)
            {
                supprimer.Clear();
                Rectangle nouveauTire = new Rectangle
                {
                    Tag = "tireJoueur" + direction,
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
            if ((string)x.Tag == "tireJoueurA")
            {
                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseTireJoueur);

                if (Canvas.GetTop(x) > 500)
                {
                    supprimer.Add(x);
                }
                tireBas = true;
            }

            else if (x is Rectangle && (string)x.Tag == "tireJoueurG")
            {

                Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseTireJoueur);



                if (Canvas.GetLeft(x) < 80)
                {
                    supprimer.Add(x);
                }
                tireGauche = true;
            }
            else if (x is Rectangle && (string)x.Tag == "tireJoueurD")
            {

                Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseTireJoueur);

                if (Canvas.GetLeft(x) > 1200)
                {
                    supprimer.Add(x);
                }
                tireDroite = true;
            }
            else if (x is Rectangle && (string)x.Tag == "tireJoueurH")
            {

                Canvas.SetTop(x, Canvas.GetTop(x) - vitesseTireJoueur);

                if (Canvas.GetTop(x) < 0)
                {
                    supprimer.Add(x);
                }


                tireHaut = true;


            }
            else if (x is Rectangle && (string)x.Tag == "tireJoueurB")
            {

                Canvas.SetTop(x, Canvas.GetTop(x) + vitesseTireJoueur);

                if (Canvas.GetTop(x) > 500)
                {
                    supprimer.Add(x);
                }
                tireBas = true;
            }

        }


        private void DeplacementJoueur()
        {
            if (gauche && Canvas.GetLeft(joueur1) > 50)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) - vitesseJoueur);
            }
            if (droite && Canvas.GetLeft(joueur1) < 1200)
            {
                Canvas.SetLeft(joueur1, Canvas.GetLeft(joueur1) + vitesseJoueur);
            }
            if (haut && Canvas.GetTop(joueur1) > 0)
            {
                Canvas.SetTop(joueur1, Canvas.GetTop(joueur1) - vitesseJoueur);
            }
            if (bas && Canvas.GetTop(joueur1) < 500)
            {
                Canvas.SetTop(joueur1, Canvas.GetTop(joueur1) + vitesseJoueur);
            }

        }

        private void ChangementApparence()
        {
            //------------------------------------------------A optimiser (pas crée changement a chauqe fois)----------------------------


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
            else if (tireHaut == true)
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/hero_tire/tire_haut_1.png"));
                joueur1.Fill = joueurSkin;
                tireHaut = false;
            }
            else if (tireBas == true)
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/hero_tire/tire_bas.png"));
                joueur1.Fill = joueurSkin;
                tireBas = false;
            }
            else if (tireGauche == true)
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/hero_tire/tire_gauche.png"));
                joueur1.Fill = joueurSkin;
                tireGauche = false;
            }
            else if (tireDroite == true)
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/hero_tire/tire_droite.png"));
                joueur1.Fill = joueurSkin;
                tireDroite = false;
            }


        }
        private void ChangementApparenceEnnemis(Rectangle newEnnemie)
        {
            ImageBrush ennemiSkin = new ImageBrush();

            changementEnnemi++;

            if (changementEnnemi >= tableauApparenceSqueletteDroite.Length)
            {
                changementEnnemi = 0;
            }
            string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceSqueletteDroite[changementEnnemi];
            ennemiSkin.ImageSource = new BitmapImage(new Uri(image));
            newEnnemie.Fill = ennemiSkin;

        }


        private void CreationEnnemie()
        {

            for (int i = 0; i < nombreEnnemie; i++)
            {

                //g++;
                //int gauche = tableauApparitionEnnemie[0, g];
                //int hauteur = tableauApparitionEnnemie[g, 0];
                newEnnemie = new Rectangle
                {

                    Tag = "ennemie",
                    Height = 120,
                    Width = 120,
                    Fill = ennemiSkin,
                };
                ennemieListe.Add(newEnnemie);
                directionsennemieListe.Add(0);
                // Console.WriteLine(ennemieListe[i].Tag);    
                Canvas.SetTop(newEnnemie, tableauspawnennemieVerticale[i]);
                Canvas.SetLeft(newEnnemie, tableauspawnennemieHorizontale[i]);

                Canvas.Children.Add(newEnnemie);
                ChangementApparenceEnnemis(newEnnemie);
                
            }
        }
        private void MouvementEnnemiesEtCollision(Rect joueur)
        {
            //Console.WriteLine("MouvementEnnemiesEtCollision");
            int directionaléatoire = 1;
            for (int i = ennemieListe.Count-1;i>=0; i--)
            {
                if (compteurennemie % 50 == 0)
                {
                    Console.WriteLine("Nouveau tirage");
                    directionaléatoire = nombrealeatoire.Next(1, 5);
                    Console.WriteLine(directionaléatoire);
                    directionsennemieListe[i] = (directionaléatoire);

                }
                //Console.WriteLine(ennemieListe.Count + "nombre ennemie");

                switch (directionsennemieListe[i])
                {
                    case 1:
                        {
                            Console.WriteLine("D");
                            if (Canvas.GetLeft(ennemieListe[i]) > 1100)
                            {
                                break;
                            }
                            else Canvas.SetLeft(ennemieListe[i], Canvas.GetLeft(ennemieListe[i]) + 2 + Math.Sin((double)compteur / 50));
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("G");
                            if (Canvas.GetLeft(ennemieListe[i]) < 50)
                            {
                               break;
                               }
                            else Canvas.SetLeft(ennemieListe[i], Canvas.GetLeft(ennemieListe[i]) - 2 + Math.Sin((double)compteur / 50));
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("H");
                            if (Canvas.GetTop(ennemieListe[i]) > 0)
                            {
                                break;
                            }
                            else Canvas.SetTop(ennemieListe[i], Canvas.GetTop(ennemieListe[i]) + 2 + Math.Sin((double)compteur / 50));
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("B");
                            if (Canvas.GetTop(ennemieListe[i]) < 400)
                            {
                                break;
                            }
                            else Canvas.SetTop(ennemieListe[i], Canvas.GetTop(ennemieListe[i]) - 2 + Math.Sin((double)compteur / 50));
                            break;
                        }
                }

                //Console.WriteLine(ennemie);
                // tester si l'ennemi est touché par un tir

                foreach (var x in Canvas.Children.OfType<Rectangle>())
                {
                    Rect ennemie = new Rect(Canvas.GetLeft(ennemieListe[i]), Canvas.GetTop(ennemieListe[i]), ennemieListe[i].Width, ennemieListe[i].Height);
                    if (x.Tag != null && ((string)x.Tag).Substring(0, ((string)x.Tag).Length - 1) == "tireJoueur")
                    {
                        
                        Rect tir = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (tir.IntersectsWith(ennemie))
                        {
                            supprimer.Add(x);
                            pvennemie--;
                            if (pvennemie == 0)
                            {
                                supprimer.Add(ennemieListe[i]);
                                ennemieListe.RemoveAt(i);
                                pvennemie = 3;
                                break;
                            }
                        }

                    }

                    if (joueur.IntersectsWith(ennemie))
                    {
                        // collision avec le joueur et fin de la partie
                        dispatcherTimer.Stop();
                        Canvas.Visibility = Visibility.Hidden;
                        canvas_gameOver.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        //---------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------réinitialiser tout les composant---------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        
        private void rejouer_Click(object sender, RoutedEventArgs e)
        {
            
            accesMenu = new Menu();
            accesMenu.ShowDialog();
            if (accesMenu.DialogResult == false)
            {
                Application.Current.Shutdown();
            }
            // réinitialiser le jeu
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //----------------------------------------------réinitialiser tout les composant---------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------

        private void but_quitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}