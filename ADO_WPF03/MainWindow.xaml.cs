using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Theme.WPF.Themes;
using ADO_WPF03.Models;

namespace ADO_WPF03
{
    public partial class MainWindow : Window
    {
        AnketaContext? a; // единый контекст данных
        WindowEdit? Edit;
        About? about;
        Employee? p;
        public MainWindow()
        {
            InitializeComponent();
            DataGridEmployee.Columns.Add(new DataGridTextColumn()
            { Header = "Табельный номер", Binding = new Binding("Tab") });
            DataGridEmployee.Columns.Add(new DataGridTextColumn()
            { Header = "Фамилия", Binding = new Binding("SecondName") });
            DataGridEmployee.Columns.Add(new DataGridTextColumn()
            { Header = "Имя", Binding = new Binding("FirstName") });
            DataGridEmployee.Columns.Add(new DataGridTextColumn()
            { Header = "Отчество", Binding = new Binding("ParentName") });
            a = new();
            DataGridEmployee.ItemsSource = a.Employees.ToList();
        }
        private void AddClick(object sender, RoutedEventArgs e)
        {
            // создаю окно редактирования
            Edit = new(a, 0); // передаю контекст и номер сотрудника = 0 (новый)
            Edit?.Show();
        }
        private void EditClick(object sender, RoutedEventArgs e)
        {
            // создаю окно редактирования
            // передаю контекст и номер сотрудника
            // SelectedIndex
            var emp = DataGridEmployee.SelectedItem as Employee;
            if (emp is not null)
            {
                Edit = new(a, emp.Tab);
                Edit?.Show();
            }
            else
            {
                Status.Content = "Для редактирования сотрудника выберите его !!!"; 
            }
            // обновляем список
            RefreshClick(sender, e);
        }
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            // SelectedIndex
            var emp = DataGridEmployee.SelectedItem as Employee;
            if (emp is not null)
            {
                a.Employees.Remove(emp);
                a.SaveChanges();
                RefreshClick(sender, e); // обновляем список
            }
            else
            {
                Status.Content = "Для удаления сотрудника выберите его !!!";
            }
         }
        private void RefreshClick(object sender, RoutedEventArgs e)
        {
            DataGridEmployee.ItemsSource = a?.Employees.ToList();
        }
        private void CloseWindow(object sender, EventArgs e)
        {
            Close();
        }
        private void About_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (about is null)
            {
                about = new();
            }
            */
            // создаю окно "О программе", если его нет
            about ??= new(); // надеюсь сейчас так все пишут, а не как в комментах ?
            about?.Show();
            about = null; 
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            a?.Dispose();
            Edit?.Close();
            about?.Close();
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
	    private void ChangeTheme(object sender, RoutedEventArgs e)
		{
            switch (((MenuItem)sender).Uid)
            {
                case "0":
                    ThemesController.SetTheme(ThemeType.DeepDark);
                    break;
                case "1":
                    ThemesController.SetTheme(ThemeType.SoftDark);
                    break;
                case "2":
                    ThemesController.SetTheme(ThemeType.DarkGreyTheme);
                    break;
                case "3":
                    ThemesController.SetTheme(ThemeType.GreyTheme);
                    break;
                case "4":
                    ThemesController.SetTheme(ThemeType.LightTheme);
                    break;
                case "5":
                    ThemesController.SetTheme(ThemeType.RedBlackTheme);
                    break;
            }
            e.Handled = true;
        }
    }
}