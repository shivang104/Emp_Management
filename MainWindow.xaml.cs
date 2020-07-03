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
        public List<Employee> employees = new List<Employee>();
        public List<Department> departments = new List<Department>
        {
            new Department {Depid = "1", Depname = "FES", Depmanager = "Mr.Rakesh Gandhi"},
            new Department {Depid = "2", Depname = "TechOps", Depmanager = "Mr.Karan Wadhawan"},
            new Department {Depid = "3", Depname = "Proteas", Depmanager = "Ms.Dhwani Bhatia"},
            new Department {Depid = "4", Depname = "HR", Depmanager = "Ms.Priyanka Gubrele"},
        };

        public MainWindow()
        {
            InitializeComponent();
            empdepartment.ItemsSource = departments;
            empdepartment.DisplayMemberPath = "Depname";
            empdepartment.SelectedValuePath = "Depname";
            empdepartment.SelectedIndex = 0;
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
 
            employee.department = empdepartment.SelectedValue.ToString();


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

                foreach (Department dep in departments){
                    if (empl.department == dep.Depname)
                    {
                        empdepartment.SelectedValue = dep.Depname;
                    }
                }
                empid.IsEnabled = false;
                msg.Text = "Record found.";
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            string id = empid.Text;
            IEnumerable<Employee> employeetoupdate = from emp in employees where emp.empid == id select emp;
            Employee empl = employeetoupdate.First();
            empl.name = empname.Text;
            ComboBoxItem comboBoxItem = (ComboBoxItem)empdepartment.SelectedItem;
            empl.department = empdepartment.SelectedValue.ToString();
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

        public void save_btn2_Click(object sender, RoutedEventArgs e)
        {
            Department department = new Department();
            department.Depid = deptid.Text;
            department.Depname = deptname.Text;
            department.Depmanager = deptmanager.Text;
  
            if ((from dep in departments where dep.Depid == deptid.Text select dep).Count() > 0)
            {
                msg.Text = "Record not saved. Department already exists.";
            }
            else
            {
                departments.Add(department);
                msg.Text = "Record saved.";
            }
        }

        public void clear_btn2_Click(object sender, RoutedEventArgs e)
        {
            deptid.Text = null;
            deptname.Text = null;
            deptmanager.Text = null;
            deptid.IsEnabled = true;
            msg.Text = null;
        }

        private void delete_btn2_Click(object sender, RoutedEventArgs e)
        {
            deptid.IsEnabled = true;
            string id = deptid.Text;
            IEnumerable<Department> departmenttodelete = from dep in departments where dep.Depid == id select dep;
            departments = departments.Except(departmenttodelete).ToList();
            msg.Text = "Record deleted.";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string id = deptid.Text;
            if ((from dep in departments where dep.Depid == deptid.Text select dep).Count() == 0)
            {
                msg.Text = "Record does not exists.";
            }
            else
            {
                IEnumerable<Department> departmenttoupdate = from dep in departments where dep.Depid == id select dep;
                Department dept = departmenttoupdate.First();
                deptid.Text = dept.Depid;
                deptname.Text = dept.Depname;
                deptmanager.Text = dept.Depmanager;
                empid.IsEnabled = false;
                msg.Text = "Record found.";
            }
        }

        private void update_btn2_Click(object sender, RoutedEventArgs e)
        {

            string id = deptid.Text;
            IEnumerable<Department> departmenttoupdate = from dep in departments where dep.Depid == id select dep;
            Department dept = departmenttoupdate.First();
            dept.Depname = deptname.Text;
            dept.Depmanager = deptmanager.Text;
            departments.Add(dept);
            msg.Text = "Record updated.";
        }

    }
}
