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
using CS.ClassLayer;
using cl = CS.ClassLayer.ClassLayer;
using bl = CS.BusinessLayer.BusinessLayer;
using dl = CS.DataLayer.DataLayer;
using av = CS.ApplicationVariables.ApplicationVariables;

namespace CS.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void createNewFile()
        {
            using (bl bl1 = new bl())
            {
                cl.Films films = new cl.Films();
                films = bl1.GetFilms(av.CsvPaths.MoviesCSV);

                List<cl.Director> directors = bl1.GetDistinctDirectorsFromFilms(films);
                List<cl.Actor> actors = bl1.GetDistinctActorsFromFilms(films);

                List<string> codeLines = bl1.fileLines(films, actors, directors);
                using (dl dl1 = new dl())
                {
                    dl1.createFile(codeLines,textBox.Text);
                }
                
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            createNewFile();
            MessageBox.Show("File Created!");
            Application.Current.Shutdown();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO check if text is okey
            button.IsEnabled = true;
        }
    }
}
