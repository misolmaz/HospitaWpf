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
    /// DoctorsWpf.xaml etkileşim mantığı
    /// </summary>
    public partial class DoctorsWpf : Window
    {
        int DocId;
        Doctors updates;
        public DoctorsWpf()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();

            dgwList.ItemsSource = context.Doctors.ToList();

            List<Departments> ssloc = context.Departments.ToList();

            cmbDept.ItemsSource= ssloc;
            cmbDept.SelectedValuePath= "DepId";
            cmbDept.DisplayMemberPath = "Name";
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            MainWindow.doctorsWpf = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();
            Doctors doc = new Doctors();
            doc.Name = txtName.Text;
            doc.SurName = txtSurname.Text;
            doc.Tckn = txtTckn.Text;
            doc.Gender = txtGender.Text;
            doc.DayOfBirth = txtDayOfBirth.Text;
            doc.DepID = cmbDept.SelectedIndex;


            try
            {
                context.Doctors.Add(doc);
                context.SaveChanges();
                dgwList.ItemsSource = context.Doctors.ToList();
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

                updates = (Doctors)dgwList.SelectedItem;// context.Departments.Find(DepId);
                DocId = updates.DocId;
                txtName.Text = updates.Name;
                txtSurname.Text = updates.SurName;
                txtTckn.Text = updates.Tckn;
                txtGender.Text = updates.Gender;
                txtDayOfBirth.Text = updates.DayOfBirth;
                cmbDept.SelectedIndex = updates.DepID;

            }
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            HospitalEntities context = new HospitalEntities();

            dgwList.ItemsSource = context.Doctors.ToList();
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {

            HospitalEntities context = new HospitalEntities();
            updates = context.Doctors.Find(DocId);
            updates.Name = txtName.Text;
            updates.SurName = txtSurname.Text;
            updates.Tckn = txtTckn.Text;
            updates.Gender = txtGender.Text;
            updates.DayOfBirth = txtDayOfBirth.Text;
            updates.DepID = cmbDept.SelectedIndex;

            context.SaveChanges();
            dgwList.ItemsSource = context.Doctors.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

            HospitalEntities context = new HospitalEntities();
            if (dgwList.SelectedItems.Count > 0)
            {
                MessageBoxResult sonuc = MessageBox.Show("Are You Sure to Delete", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (sonuc == MessageBoxResult.Yes)
                {


                    Doctors doctor = (from r in context.Doctors where r.DocId ==DocId select r).SingleOrDefault();
                    context.Doctors.Remove(doctor);
                    context.SaveChanges();
                    dgwList.ItemsSource = context.Doctors.ToList();

                }
                else
                {
                    MessageBox.Show("Can't delete.");

                }
            }
        }
    }
}
