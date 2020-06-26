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
using Model;
using System.Windows.Controls.Primitives;

namespace Emp_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> employees = new List<Employee>();
        public MainWindow()
        {
            InitializeComponent();
        }
 
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.empid = empid.Text;
            employee.name = empname.Text;
            if (male.IsChecked == true)
            {
                employee.gender = "Male";
            }
            else if (female.IsChecked == true)
            {
                employee.gender = "Female";
            }
            ComboBoxItem comboBoxItem = (ComboBoxItem)empdepartment.SelectedItem;
            employee.department = comboBoxItem.Content.ToString();


            if ((from emp in employees where emp.empid == empid.Text select emp).Count() > 0)
            {
                msg.Text = "Record not saved. Employee already exists.";
            }
            else
            {
                employees.Add(employee);
                msg.Text = "Record saved.";
            }
        }

        public void male_Checked(object sender, RoutedEventArgs e)
        {
            if (male.IsChecked == true)
            {
                female.IsChecked = false;
            }
        }

        public void female_Checked(object sender, RoutedEventArgs e)
        {
            if (female.IsChecked == true)
            {
                male.IsChecked = false;
            }
        }

        public void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            empid.Text = null;
            empname.Text = null;
            empdepartment.SelectedIndex = -1;
            male.IsChecked = false;
            female.IsChecked = false;
            empid.IsEnabled = true;
            msg.Text = null;
        }

        public void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            empid.IsEnabled = true;
            string id = empid.Text;
            //foreach (Employee emp in employees)
            //{
            //    if (emp.empid == id)
            //    {
            //        employees.Remove(emp);
            //    }
            //}
            IEnumerable<Employee> employeetodelete = from emp in employees where emp.empid == id select emp;
            employees = employees.Except(employeetodelete).ToList();
            msg.Text = "Record deleted.";
        }

        public void GetDetails_btn_Click(object sender, RoutedEventArgs e)
        {
            string id = empid.Text;
            if ((from emp in employees where emp.empid == empid.Text select emp).Count() == 0)
            {
                msg.Text = "Record does not exists.";
            }
            else
            {
                IEnumerable<Employee> employeetoupdate = from emp in employees where emp.empid == id select emp;
                Employee empl = employeetoupdate.First();
                empid.Text = empl.empid;
                empname.Text = empl.name;

                if (empl.gender == "Male")
                {
                    male.IsChecked = true;
                    female.IsChecked = false;
                }
                else if (empl.gender == "Female")
                {
                    male.IsChecked = false;
                    female.IsChecked = true;
                }

                if (empl.department == "TechOps")
                {
                    empdepartment.SelectedIndex = 0;
                }
                else if (empl.department == "FES")
                {
                    empdepartment.SelectedIndex = 1;
                }
                else if (empl.department == "Proteas")
                {
                    empdepartment.SelectedIndex = 2;
                }
                else if (empl.department == "HR")
                {
                    empdepartment.SelectedIndex = 3;
                }
                empid.IsEnabled = false;
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            string id = empid.Text;
            IEnumerable<Employee> employeetoupdate = from emp in employees where emp.empid == id select emp;
            Employee empl = employeetoupdate.First();
            empl.name = empname.Text;
            ComboBoxItem comboBoxItem = (ComboBoxItem)empdepartment.SelectedItem;
            empl.department = comboBoxItem.Content.ToString();
            //empl.department = empdepartment.SelectedValue.ToString();
            if (male.IsChecked == true)
            {
                empl.gender = "Male";
            }
            else if (female.IsChecked == true)
            {
                empl.gender = "Female";
            }
            employees.Add(empl);
            msg.Text = "Record updated.";
        }
    }
}
