using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Printing;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
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
using static System.Net.Mime.MediaTypeNames;
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
        private bool delaiTire = true;

        private ImageBrush joueurSkin = new ImageBrush();
        private ImageBrush sol1Skin = new ImageBrush();
        private ImageBrush ennemiSkin = new ImageBrush();
        private ImageBrush perduSkin = new ImageBrush();
        private ImageBrush quitterSkin = new ImageBrush();

        private DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        private string direction = "A";
        private string imageDuJoueur = AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png";
        private string imageDuJoueurDebut = AppDomain.CurrentDomain.BaseDirectory;
        private string[] tableauApparenceDroite = { "images/hero_droites/HeroDroite_1.png", "images/hero_droites/HeroDroite_2.png", "images/hero_droites/HeroDroite_3.png", "images/hero_droites/HeroDroite_4.png", "images/hero_droites/HeroDroite_5.png", "images/hero_droites/HeroDroite_6.png", "images/hero_droites/HeroDroite_7.png", "images/hero_droites/HeroDroite_8.png", "images/hero_droites/HeroDroite_9.png" };
        private string[] tableauApparenceGauche = { "images/hero_gauche/HeroGauche_1.png", "images/hero_gauche/HeroGauche_2.png", "images/hero_gauche/HeroGauche_3.png", "images/hero_gauche/HeroGauche_4.png", "images/hero_gauche/HeroGauche_5.png", "images/hero_gauche/HeroGauche_6.png", "images/hero_gauche/HeroGauche_7.png", "images/hero_gauche/HeroGauche_8.png", "images/hero_gauche/HeroGauche_9.png" };
        private string[] tableauApparenceHaut = { "images/hero_haut/HeroHaut_1.png", "images/hero_haut/HeroHaut_2.png", "images/hero_haut/HeroHaut_3.png", "images/hero_haut/HeroHaut_4.png", "images/hero_haut/HeroHaut_5.png", "images/hero_haut/HeroHaut_6.png", "images/hero_haut/HeroHaut_7.png", "images/hero_haut/HeroHaut_8.png", "images/hero_haut/HeroHaut_9.png" };
        private string[] tableauApparenceBas = { "images/hero_bas/HeroBas_1.png", "images/hero_bas/HeroBas_2.png", "images/hero_bas/HeroBas_3.png", "images/hero_bas/HeroBas_4.png", "images/hero_bas/HeroBas_5.png", "images/hero_bas/HeroBas_6.png", "images/hero_bas/HeroBas_7.png", "images/hero_bas/HeroBas_8.png", "images/hero_bas/HeroBas_9.png" };
        
        private int tempsEntreMisesAJour = 16;
        private int changementApparence = 0;
        private int vitesseTireJoueur = 15;
        private int vitesseEnnemi = 0;
        private int nbEnnemis = 3;
        private int vitesseJoueur = 5;
        private int compteur = 0;
        private int scorePartie = 0;
        private int delaiTireDuree = 10;


        private Random nombreAleatoire = new Random();
        private Rectangle nouvelEnnemi;
        private List<Rectangle> supprimer = new List<Rectangle>();
        private List<Rectangle> ennemiListe = new List<Rectangle>();
        private List<int> directionsEnnemiListe = new List<int>();

        private string toucheAvancer;
        private string toucheReculer;
        private string toucheGauche;
        private string toucheDroite;
        private string toucheTire;

        private Rect joueur;
        private Menu accesMenu;


        public MainWindow()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;

            accesMenu = new Menu();
            accesMenu.ShowDialog();
            if (accesMenu.DialogResult == false)
            {
                System.Windows.Application.Current.Shutdown();
            }

            dispatcherTimer.Tick += Jeu;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(tempsEntreMisesAJour);
            dispatcherTimer.Start();

            joueurSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/HeroFace.png"));
            joueur1.Fill = joueurSkin;


            perduSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/text/gameOver.png"));
            gameOver.Fill = perduSkin;


            quitterSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/bouton_quitter.png"));
            but_quitter.Background = quitterSkin;

            toucheAvancer = accesMenu.valeurAvancer;
            toucheReculer = accesMenu.valeurReculer;
            toucheGauche = accesMenu.valeurGauche;
            toucheDroite = accesMenu.valeurDroite;
            toucheTire = accesMenu.valeurTire;

            modeJeuRecup = accesMenu.modeJeu;

#if DEBUG
            Console.WriteLine("Mode de jeu actuel : " + modeJeuRecup);
#endif

            ModeDeJeu(modeJeuRecup);

        }
        private void Jeu(object sender, EventArgs e)
        {
            joueur = new Rect(Canvas.GetLeft(joueur1), Canvas.GetTop(joueur1), joueur1.Width, joueur1.Height);
#if DEBUG
            Console.WriteLine(joueur + "joueur");
#endif

            DeplacementJoueur();
            ChangementApparence();
            MouvementEnnemiesEtCollision(joueur);

            foreach (Rectangle y in supprimer)
            {
                Canvas.Children.Remove(y);
            }

            foreach (Rectangle x in Canvas.Children.OfType<Rectangle>())
            {
                if (x.Tag != null && x is Rectangle && ((string)x.Tag).Substring(0, ((string)x.Tag).Length - 1) == "tireJoueur")
                    TestTireJoueur(x);
            }

            if (!PasDennemiEnVie())
            {
                CreationEnnemie(nbEnnemis);
                nbEnnemis++;
                Console.WriteLine(nbEnnemis);
            }

            if (compteur % delaiTireDuree == 0)
            {
                delaiTire = true;
            }

            compteur++;

        }

        private void JouerSon()
        {
            son.Play();
        }

        private void ModeDeJeu(bool modeJeuRecup)
        {
#if DEBUG
            Console.WriteLine("Mode de jeu actuel dans modeJeu : " + modeJeuRecup);
#endif
            if (modeJeuRecup == false)
            {
                sol1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
                sol1.Fill = sol1Skin;
                vitesseEnnemi = 2;
            }
            else if (modeJeuRecup == true)
            {
                sol1Skin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_difficile.png"));
                sol1.Fill = sol1Skin;
                vitesseEnnemi = 4;
            }
        }

        
        private bool PasDennemiEnVie()
        {
            foreach (var child in Canvas.Children)
            {
                if (child is Rectangle && ((Rectangle)child).Tag != null && ((string)((Rectangle)child).Tag).StartsWith("ennemie"))
                {
                    return true;
                }
            }
            return false;
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
            if (e.Key == Key.X)
            {
                nbEnnemis += 15;
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
            if ((e.Key.ToString() == toucheTire || e.Key == Key.Space) && delaiTire == true )
            {
                CreerNouveauTire();
            }
        }
        private void CreerNouveauTire()
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
            delaiTire = false;
            Canvas.SetTop(nouveauTire, Canvas.GetTop(joueur1) - nouveauTire.Height + joueur1.Height / 2);
            Canvas.SetLeft(nouveauTire, Canvas.GetLeft(joueur1) + joueur1.Width / 2);
            Canvas.Children.Add(nouveauTire);
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
            changementApparence++;

            if (changementApparence >= tableauApparenceDroite.Length)
            {
                changementApparence = 0;
            }
            if (droite == true)
            {
                imageDuJoueur = imageDuJoueurDebut + tableauApparenceDroite[changementApparence];
            }
            else if (gauche == true)
            {
                imageDuJoueur = imageDuJoueurDebut + tableauApparenceGauche[changementApparence];
            }
            else if (haut == true)
            {

                imageDuJoueur = imageDuJoueurDebut + tableauApparenceHaut[changementApparence];
            }
            else if (bas == true)
            {
                imageDuJoueur = imageDuJoueurDebut + tableauApparenceBas[changementApparence];
            }
            else if (tireHaut == true)
            {
                imageDuJoueur = imageDuJoueurDebut + "images/hero_tire/tire_haut_1.png";
                tireHaut = false;
            }
            else if (tireBas == true)
            {
                imageDuJoueur = imageDuJoueurDebut + "images/hero_tire/tire_bas.png";
                tireBas = false;
            }
            else if (tireGauche == true)
            {
                imageDuJoueur = imageDuJoueurDebut + "images/hero_tire/tire_gauche.png";
                tireGauche = false;
            }
            else if (tireDroite == true)
            {
                imageDuJoueur = imageDuJoueurDebut + "images/hero_tire/tire_droite.png";
                tireDroite = false;
            }

            joueurSkin.ImageSource = new BitmapImage(new Uri(imageDuJoueur));
            joueur1.Fill = joueurSkin;
        }


        private void CreationEnnemie(int nombreEnnemie)
        {
            Random rdm1 = new Random();
            Random rdm2 = new Random();

            for (int i = 0; i < nombreEnnemie; i++)
            {

                //g++;
                //int gauche = tableauApparitionEnnemie[0, g];
                //int hauteur = tableauApparitionEnnemie[g, 0];
                double yspawnEnnemi = rdm1.Next(0, 500);
                double droiteouGauche = rdm2.Next(0, 2);
                Console.WriteLine(droiteouGauche);
                ennemiSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/zombiecourse/frame-1.gif"));
                nouvelEnnemi = new Rectangle
                {

                    Tag = "ennemie",
                    Height = 120,
                    Width = 120,
                    Fill = ennemiSkin,
                };

                directionsEnnemiListe.Add(0);  
                Canvas.SetTop(nouvelEnnemi, yspawnEnnemi);
                if (droiteouGauche == 0)
                {
                    Canvas.SetLeft(nouvelEnnemi, 0);
                }
                else
                {
                    Canvas.SetLeft(nouvelEnnemi, 1200);
                }

                Canvas.Children.Add(nouvelEnnemi);
                ennemiListe.Add(nouvelEnnemi);

            }
        }
        private void MouvementEnnemiesEtCollision(Rect joueur)
        {
            int directionaléatoire = 1;

            for (int i = ennemiListe.Count - 1; i >= 0; i--)
            {
                if (compteur % 50 == 0)
                {
                    directionaléatoire = nombreAleatoire.Next(1, 5);
                    directionsEnnemiListe[i] = directionaléatoire;
                }

                switch (directionsEnnemiListe[i])
                {
                    case 1:
                        if (Canvas.GetLeft(ennemiListe[i]) < 1100)
                        {
                            Canvas.SetLeft(ennemiListe[i], Canvas.GetLeft(ennemiListe[i]) + vitesseEnnemi + Math.Sin((double)compteur / 50));
                        }
                        break;

                    case 2:
                        if (Canvas.GetLeft(ennemiListe[i]) > 50)
                        {
                            Canvas.SetLeft(ennemiListe[i], Canvas.GetLeft(ennemiListe[i]) - vitesseEnnemi + Math.Sin((double)compteur / 50));
                        }
                        break;

                    case 3:
                        if (Canvas.GetTop(ennemiListe[i]) > 0)
                        {
                            Canvas.SetTop(ennemiListe[i], Canvas.GetTop(ennemiListe[i]) - vitesseEnnemi + Math.Sin((double)compteur / 50));
                        }
                        break;

                    case 4:
                        if (Canvas.GetTop(ennemiListe[i]) < 400)
                        {
                            Canvas.SetTop(ennemiListe[i], Canvas.GetTop(ennemiListe[i]) + vitesseEnnemi + Math.Sin((double)compteur / 50));
                        }
                        break;
                }

                foreach (var x in Canvas.Children.OfType<Rectangle>())
                {
                    Rect ennemie = new Rect(Canvas.GetLeft(ennemiListe[i]), Canvas.GetTop(ennemiListe[i]), ennemiListe[i].Width, ennemiListe[i].Height);

                    if (x.Tag != null && ((string)x.Tag).Substring(0, ((string)x.Tag).Length - 1) == "tireJoueur")
                    {
                        Rect tir = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        if (tir.IntersectsWith(ennemie))
                        {
                            supprimer.Add(x);

                            supprimer.Add(ennemiListe[i]);
                            ennemiListe.RemoveAt(i);
                            scorePartie = scorePartie + 10;
                            textScore.Text = scorePartie.ToString();
                            testScoreFinal.Text = scorePartie.ToString();
                            break;
                        }
                    }

                    if (joueur.IntersectsWith(ennemie))
                    {
                        dispatcherTimer.Stop();
                        Canvas.Visibility = Visibility.Hidden;
                        canvas_gameOver.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void but_quitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}