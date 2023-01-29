using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// PatientsWpf.xaml etkileşim mantığı
    /// </summary>
    public partial class PatientsWpf : Window
    {
        int PatId;
        Patients updates;
        public PatientsWpf()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();

            dgwList.ItemsSource = context.Patients.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            Patients pat = new Patients();
            pat.Name = txtName.Text;
            pat.SurName = txtSurname.Text;
            pat.Tckn = txtTckn.Text;
            pat.Gender = txtGender.Text;
            pat.DayOfBirth = txtDayOfBirth.Text;
            


            try
            {
                context.Patients.Add(pat);
                context.SaveChanges();
                dgwList.ItemsSource = context.Patients.ToList();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        private void dgwList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            if (dgwList.SelectedItems.Count > 0)
            {

                //  DepId = Convert.ToInt32(dgwList.SelectedItems[0].ToString());

                updates = (Patients)dgwList.SelectedItem;// context.Departments.Find(DepId);
                PatId = updates.Id;
                txtName.Text = updates.Name;
                txtSurname.Text = updates.SurName;
                txtTckn.Text = updates.Tckn;
                txtGender.Text = updates.Gender;
                txtDayOfBirth.Text = updates.DayOfBirth;


            }
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();

            dgwList.ItemsSource = context.Patients.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            HospitalEntities context = new HospitalEntities();
            if (dgwList.SelectedItems.Count > 0)
            {
                MessageBoxResult sonuc = MessageBox.Show("Are You Sure to Delete", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (sonuc == MessageBoxResult.Yes)
                {


                    Patients patient = (from r in context.Patients where r.Id == PatId select r).SingleOrDefault();
                    context.Patients.Remove(patient);
                    context.SaveChanges();
                    dgwList.ItemsSource = context.Patients.ToList();

                }
                else
                {
                    MessageBox.Show("Can't delete.");

                }
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

            HospitalEntities context = new HospitalEntities();
            updates = context.Patients.Find(PatId);
            updates.Name = txtName.Text;
            updates.SurName = txtSurname.Text;
            updates.Tckn = txtTckn.Text;
            updates.Gender = txtGender.Text;
            updates.DayOfBirth = txtDayOfBirth.Text;
            

            context.SaveChanges();
            dgwList.ItemsSource = context.Patients.ToList();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.patientsWpf = null;
        }
    }
            
           
    
        
    
}
