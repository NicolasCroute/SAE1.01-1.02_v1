﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Printing;
using System.Reflection;
using System.Text;
using System.Threading;
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
        private bool ennemieSqueletteHaut, ennemieSqueletteGauche, ennemieSqueletteDroite, ennemieSqueletteBas = false;

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

        //private ImageBrush ennemi1 = new ImageBrush();
        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        private int vitesseJoueur = 5;
        int compteur = 0, sprite = 1;
        private string direction;
        private string[] tableauApparenceDroite = { "images/hero_droites/HeroDroite_1.png", "images/hero_droites/HeroDroite_2.png", "images/hero_droites/HeroDroite_3.png", "images/hero_droites/HeroDroite_4.png", "images/hero_droites/HeroDroite_5.png", "images/hero_droites/HeroDroite_6.png", "images/hero_droites/HeroDroite_7.png", "images/hero_droites/HeroDroite_8.png", "images/hero_droites/HeroDroite_9.png" };
        private string[] tableauApparenceGauche = { "images/hero_gauche/HeroGauche_1.png", "images/hero_gauche/HeroGauche_2.png", "images/hero_gauche/HeroGauche_3.png", "images/hero_gauche/HeroGauche_4.png", "images/hero_gauche/HeroGauche_5.png", "images/hero_gauche/HeroGauche_6.png", "images/hero_gauche/HeroGauche_7.png", "images/hero_gauche/HeroGauche_8.png", "images/hero_gauche/HeroGauche_9.png" };
        private string[] tableauApparenceHaut = { "images/hero_haut/HeroHaut_1.png", "images/hero_haut/HeroHaut_2.png", "images/hero_haut/HeroHaut_3.png", "images/hero_haut/HeroHaut_4.png", "images/hero_haut/HeroHaut_5.png", "images/hero_haut/HeroHaut_6.png", "images/hero_haut/HeroHaut_7.png", "images/hero_haut/HeroHaut_8.png", "images/hero_haut/HeroHaut_9.png" };
        private string[] tableauApparenceBas = { "images/hero_bas/HeroBas_1.png", "images/hero_bas/HeroBas_2.png", "images/hero_bas/HeroBas_3.png", "images/hero_bas/HeroBas_4.png", "images/hero_bas/HeroBas_5.png", "images/hero_bas/HeroBas_6.png", "images/hero_bas/HeroBas_7.png", "images/hero_bas/HeroBas_8.png", "images/hero_bas/HeroBas_9.png" };
        private string[] tableauApparenceSqueletteDroite = { "images/squelette/squelette_marche_droite/squelette/squelette_marche_Droite_1.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_2.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_3.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_4.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_5.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_6.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_7.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_8.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_9.png", "images/squelette/squelette_marche_droite/squelette_marche_Droite_10.png" };
        private string[] tableauApparenceZombieDroite = { "images/zombiecourse/frame-1.gif", "images/zombiecourse/frame-2.gif", "images/zombiecourse/frame-3.gif", "images/zombiecourse/frame-4.gif", "images/zombiecourse/frame-5.gif", "images/zombiecourse/frame-6.gif", "images/zombiecourse/frame-7.gif", "images/zombiecourse/frame-8.gif", "images/zombiecourse/frame-9.gif", "images/zombiecourse/frame-99.gif" };
        
        private int tempsEntreMisesAJour = 16;
        private int tempsEcouleDepuisChangement = 0;
        private int intervalleChangementApparence = 34;
        private int changement = 0;
        private int changementEnnemi = 0;
        private int vitesseennemie = 3;
        private int maxEnnemiemillisecond;
        private int delaiapparitionennemie = 500;
        private Random nombrealeatoire = new Random();
        private int vitesseTireJoueur = 15;
        private bool directionennemie = true;
        private List<Rectangle> supprimer = new List<Rectangle>();
        private int[,] tableauApparitionEnnemie = { { 100, 300 },{500,100} };
        private int directionaléatoire = 1;
        private int pvennemie = 3;

        //----------Deplacement Touche-------------

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

            perduSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/text/gameOver.png"));
            gameOver.Fill = perduSkin;

            rejouerSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/rejouer.png"));
            rejouer.Background = rejouerSkin;

            CreationEnnemie(3);
            

            toucheAvancer = accesMenu.valeurAvancer;
            toucheReculer = accesMenu.valeurReculer;
            toucheGauche = accesMenu.valeurGauche;
            toucheDroite = accesMenu.valeurDroite;
            toucheTire = accesMenu.valeurTire;
            


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
                if (x.Tag!= null && x is Rectangle && ((string)x.Tag).Substring(0, ((string)x.Tag).Length - 1) == "tireJoueur")                
                    TestTireJoueur(x);
                MouvementEnnemieEtCollision(x, joueur, 3);



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
        //pour les touche faire des touche de base

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


            if (x is Rectangle && (string)x.Tag == "tireJoueurG")
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

            // création d’un tir joueur à base d’un rectangle Rect (nécessaire pour la collision) 
            Rect tir = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
            foreach (var y in Canvas.Children.OfType<Rectangle>())
            {
                // si le rectangle est un ennemi
                if (y is Rectangle && (string)y.Tag == "ennemie")
                {
                    
                    // création d’un rectangle correspondant à l’ennemi
                    Rect ennemie = new Rect(Canvas.GetLeft(y), Canvas.GetTop(y), y.Width, y.Height);
                   // Console.WriteLine("test avec l'ennemi :" +ennemie);
                   // Console.WriteLine("test avec tir :" + tir);
                    // on vérifie la collision
                    // appel à la méthode IntersectsWith pour détecter la collision
                    if (tir.IntersectsWith(ennemie))
                    {
                        Console.WriteLine("touché");
                        // on ajoute l’ennemi de la liste à supprimer eton décrémente le nombre d’ennemis
                        supprimer.Add(x);
                        pvennemie--;
                        if (pvennemie == 0)
                        {
                            supprimer.Add(y);
                            pvennemie = 3;
                        }
                    }
                }
            }
            //Console.WriteLine("----------------------------");

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
            if (bas && Canvas.GetTop(joueur1) <500)
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
                tireDroite= false;
            }
            else if (ennemieSqueletteHaut == true)
            {
                

            }
            else if (ennemieSqueletteGauche == true)
            {
                
            }
            else if (ennemieSqueletteDroite == true)
            {
                changementEnnemi++;

                if (changementEnnemi >= tableauApparenceSqueletteDroite.Length)
                {
                    changementEnnemi = 0;
                }
                string image = AppDomain.CurrentDomain.BaseDirectory + tableauApparenceSqueletteDroite[changement];
                ennemiSkin.ImageSource = new BitmapImage(new Uri(image));
            }
            else if (ennemieSqueletteBas == true)
            {

            }
            /*
            else
            {
                joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png"));
                joueur1.Fill = joueurSkin;
            }
            */



        }
        private void CreationEnnemie(int nombreennemie)
        {
            int g = 100;

            for (int i = 0; i < nombreennemie; i++)
            {
                
                //g++;
                //int gauche = tableauApparitionEnnemie[0, g];
                //int hauteur = tableauApparitionEnnemie[g, 0];
                ImageBrush ennemieSkin = new ImageBrush();
                Rectangle newEnnemie = new Rectangle
                {

                    Tag = "ennemie",
                    Height = 120,
                    Width = 120,
                    Fill = ennemieSkin,
                };
                g += 200;
                Canvas.SetTop(newEnnemie, 300);
                Canvas.SetLeft(newEnnemie, g);
               
                Canvas.Children.Add(newEnnemie);
                ennemieSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/squelette/squelette_marche_droite/squelette_marche_Droite_1.png"));
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
        private Rect MouvementEnnemieEtCollision(Rectangle x, Rect joueur, int nombreennemie)
        {

            /*for (int i = 0; i < nombreennemie; i++)
            {
               
            }*/
            if (x is Rectangle && (string)x.Tag == "ennemie")
            {
                if (compteur % 100 == 0)
                {
                    directionaléatoire = nombrealeatoire.Next(1, 5);
                    
                    Console.WriteLine(directionaléatoire);
                }
                if (directionaléatoire == 1)
                { 
                    if (Canvas.GetLeft(x) > 1100)
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - 3 + Math.Sin((double)compteur / 50));
                    else Canvas.SetLeft(x, Canvas.GetLeft(x) + 3 + Math.Sin((double)compteur / 50));
                }

                
                if (directionaléatoire == 2 ) 
                {
                    if(Canvas.GetLeft(x) < 50)
                    { Canvas.SetLeft(x, Canvas.GetLeft(x) + 2 + Math.Sin((double)compteur / 50)); }
                    else Canvas.SetLeft(x, Canvas.GetLeft(x) - 2 + Math.Sin((double)compteur / 50));

                }
                if ( directionaléatoire == 3)
                    if(Canvas.GetTop(x)>0)
                    { Canvas.SetTop(x, Canvas.GetTop(x) - 2 + Math.Sin((double)compteur / 50)); }
                    else Canvas.SetTop(x, Canvas.GetTop(x) + 2 + Math.Sin((double)compteur / 50));

                if (directionaléatoire == 4)
                    if (Canvas.GetTop(x) < 400)
                    { Canvas.SetTop(x, Canvas.GetTop(x) + 2 + Math.Sin((double)compteur / 50)); }
                    else Canvas.SetTop(x, Canvas.GetTop(x) - 2 + Math.Sin((double)compteur / 50));






                /*if (directionennemie == "G" )
                {
                     Canvas.SetLeft(x, Canvas.GetLeft(x) - vitesseennemie);
                }

                else if(directionennemie == "D" ) 
                {
                     Canvas.SetLeft(x, Canvas.GetLeft(x) + vitesseennemie);
                }
                if (directionennemie == "H" )
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + vitesseennemie);
                }
                if (directionennemie == "B" )
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) - vitesseennemie);
                }*/

                Rect ennemie = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width-40, x.Height-40);
                    if (joueur.IntersectsWith(ennemie))
                    {
                        // collision avec le joueur et fin de la partie
                        dispatcherTimer.Stop();
                        MessageBox.Show("Perdu", "Fin de partie", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    
            }
            return joueur;

        }

    }
}

    


    



 
