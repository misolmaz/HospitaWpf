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

namespace HospitaWpf
{
    /// <summary>
    /// DEpartmentsWpf.xaml etkileşim mantığı
    /// </summary>
    public partial class DEpartmentsWpf : Window
    {
        int DepId;
        Departments updates;
        public DEpartmentsWpf()
        {
            InitializeComponent();
        }

        private void Grid_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.departmentsWpf = null;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {


            HospitalEntities context = new HospitalEntities();

            dgwList.ItemsSource= context.Departments.ToList();
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

            HospitalEntities context = new HospitalEntities();
            Departments departments = new Departments();
            departments.Name = txtName.Text;
            context.Departments.Add(departments);
            context.SaveChanges();
            dgwList.ItemsSource = context.Departments.ToList();
        }

        private void dgwList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            if (dgwList.SelectedItems.Count > 0)
            {

                //  DepId = Convert.ToInt32(dgwList.SelectedItems[0].ToString());

                updates = (Departments) dgwList.SelectedItem;// context.Departments.Find(DepId);
                DepId = updates.DepId;
                txtName.Text = updates.Name;

            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            updates = context.Departments.Find(DepId);
            updates.Name = txtName.Text;

            context.SaveChanges();
            dgwList.ItemsSource = context.Departments.ToList();
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            dgwList.ItemsSource = context.Departments.ToList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            if (dgwList.SelectedItems.Count > 0)
            {
                MessageBoxResult sonuc = MessageBox.Show("Are You Sure to Delete", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (sonuc == MessageBoxResult.Yes)
                {
                    
                       
                    Departments department = (from r in context.Departments where r.DepId == DepId select r).SingleOrDefault();
                    context.Departments.Remove(department);
                    context.SaveChanges();
                    dgwList.ItemsSource = context.Departments.ToList();

                }
                else
                {
                    MessageBox.Show("Can't delete.");

                }
            }
        }
    }
}
