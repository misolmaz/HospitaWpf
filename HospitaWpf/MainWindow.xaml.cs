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

namespace HospitaWpf
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public static DEpartmentsWpf departmentsWpf = null;
        public static DoctorsWpf doctorsWpf = null;
        public static PatientsWpf patientsWpf = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            

            MenuItem mnu = (MenuItem)sender;
           
            switch (mnu.Header.ToString())

            {

                case "Departments":

                   
                    
                    if (departmentsWpf==null)
                    {
                        departmentsWpf = new DEpartmentsWpf();
                        departmentsWpf.Name = "departmentsWpf";
                        departmentsWpf.Owner = this;
                        departmentsWpf.Show();
                    }



                    break;

                case "Doctors":

                    if (doctorsWpf == null)
                    {
                        doctorsWpf = new DoctorsWpf();
                        doctorsWpf.Name = "doctorsWpf";
                        doctorsWpf.Owner = this;
                        doctorsWpf.Show();
                    }

                    break;

                case "Patients":

                    if (patientsWpf == null)
                    {
                        patientsWpf = new PatientsWpf();
                        patientsWpf.Name = "patientsWpf";
                        patientsWpf.Owner = this;
                        patientsWpf.Show();
                    }


                    break;

                case "Exit":
                   
                    this.Close();

                    break;
            }
           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
