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
using System.Windows.Shapes;

namespace SAE1._01_1._02
{

    /// <summary>
    /// Logique d'interaction pour Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {

        private ImageBrush solFacileSkin = new ImageBrush();
        private ImageBrush solDifficileSkin = new ImageBrush();
        private ImageBrush fontFacileSkin = new ImageBrush();
        private ImageBrush fontDifficileSkin = new ImageBrush();
        private bool modeJeu = false;
        

        public Menu()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;

            
            solFacileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
            solDifficileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_Difficile.png"));

            fontFacileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/font/fontBleu.png"));
            fontDifficileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/font/fontRouge.png"));
            mode.Fill = solFacileSkin;
            fontMode.Fill = fontFacileSkin;


        }

        private void but_Mode_Click(object sender, RoutedEventArgs e)
        {

            ChangeImage();
            //ChangeFond();
        }

        //--------------Simplifier en compilant les 2 codes---------------

        private void ChangeImage()
        {
            if (modeJeu == false)
            {
                
                mode.Fill = solFacileSkin;
                fontMode.Fill = fontFacileSkin;
            }
            else if (modeJeu == true) 
            {
                
                mode.Fill = solDifficileSkin;
                fontMode.Fill = fontDifficileSkin;
            }

            //inverse l'état initiale des touche
            modeJeu = !modeJeu;
            
        }
        /*
        private void ChangeFond()
        {
            if (modeJeu == false)
            {
                fontMode.Fill = fontFacileSkin;
            }
            else if (modeJeu == true)
            {
                fontMode.Fill = fontDifficileSkin;
            }
            //inverse l'état initiale des touche
            modeJeu = !modeJeu;

        }
        */





        private void but_Jouer_Click(object sender, RoutedEventArgs e)
        {
            
            this.DialogResult = true;
        }
    }
}
