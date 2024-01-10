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
        private bool modeJeu = false;
        

        public Menu()
        {
            InitializeComponent();

            WindowState = WindowState.Maximized;

            solDifficileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_Difficile.png"));
            solFacileSkin.ImageSource = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images/sol/sol_facile.png"));
            mode.Fill = solFacileSkin;


        }

        private void ChangeImage()
        {
            if (modeJeu == false)
            {
                
                mode.Fill = solFacileSkin;
            }
            else if (modeJeu == true) 
            {
                
                mode.Fill = solDifficileSkin;
            }

            //inverse l'état initiale des touche
            modeJeu = !modeJeu;
            
        }

        private void but_Mode_Click(object sender, RoutedEventArgs e)
        {
            
            ChangeImage();
        }
    }
}
