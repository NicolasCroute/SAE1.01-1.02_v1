﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SAE1._01_1._02
{
    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        private ImageBrush butParametreSkin = new ImageBrush();
        private ImageBrush solFacileSkin = new ImageBrush();
        private ImageBrush solDifficileSkin = new ImageBrush();
        private ImageBrush fontFacileSkin = new ImageBrush();
        private ImageBrush fontDifficileSkin = new ImageBrush();
        private ImageBrush but_OKSkin = new ImageBrush();

        public bool modeJeu = false;

        private string modeFacileText = "EASY";
        private string modeDifficileText = "HARD";

        public string valeurAvancer;
        public string valeurGauche;
        public string valeurDroite;
        public string valeurReculer;
        public string valeurTire;


        public Menu()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            
            solFacileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
            solDifficileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_Difficile.png"));

            fontFacileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/font/fontBleu.png"));
            fontDifficileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/font/fontRouge.png"));

            butParametreSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/but_parametre.png"));
            but_OKSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/Retour.png")); 

            parametre.Background = butParametreSkin;
            mode.Fill = solFacileSkin;
            fontMode.Fill = fontFacileSkin;
            but_OK.Background = but_OKSkin;
        }

        private void but_Mode_Click(object sender, RoutedEventArgs e)
        {
            modeJeu = !modeJeu;

            if (modeJeu == false)
            {
                mode.Fill = solFacileSkin;
                fontMode.Fill = fontFacileSkin;
                but_Mode.Content = modeFacileText;
            }
            else
            {
                mode.Fill = solDifficileSkin;
                fontMode.Fill = fontDifficileSkin;
                but_Mode.Content = modeDifficileText;
            }

        }

        private bool difficulter;
        public bool Difficulter
        {
            get
            {
                return modeJeu;
            }
            
        }

        private void but_Jouer_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void parametre_Click(object sender, RoutedEventArgs e)
        {
            menuParametre.Visibility = Visibility.Visible;
            menuPrincipale.Visibility = Visibility.Hidden;
        }
        private void but_OK_Click(object sender, RoutedEventArgs e)
        {
            menuParametre.Visibility = Visibility.Hidden;
            menuPrincipale.Visibility = Visibility.Visible;
        }
        private void but_defReculer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            but_defReculer.Content = e.Key.ToString();
            valeurReculer= (string)but_defReculer.Content;

        }

        private string toucheReculer;
        public string ToucheReculer
        {
            get
            {
                return valeurReculer;
            }
        }

        private void but_defAvancer_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            but_defAvancer.Content = e.Key.ToString();
            valeurAvancer = (string)but_defAvancer.Content;
            
        }

        private string toucheAvancer;

        public string ToucheAvancer
        {
            get
            {
                return valeurAvancer;
            }
        }

        private void but_defGauche_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            but_defGauche.Content = e.Key.ToString();
            valeurGauche = (string)but_defGauche.Content;
        }

        private string toucheGauche;
        public string ToucheGauche
        {
            get
            {
                return valeurGauche;
            }
        }


        private void but_defDroite_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            but_defDroite.Content = e.Key.ToString();
            valeurDroite = (string)but_defDroite.Content;

        }

        private string toucheDroite;
        public string ToucheDroite
        {
            get
            {
                return valeurDroite;
            }
        }

        private void but_defTire_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            but_defTire.Content = e.Key.ToString();
            valeurTire = (string)but_defTire.Content;

        }

        private string toucheTire;
        public string ToucheTire
        {
            get
            {
                return valeurTire;
            }
        }
    }

}
