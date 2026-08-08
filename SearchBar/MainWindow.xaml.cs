using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchBar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<string> _people = new()
    {
        "Anders Hansen",
        "Anna Olsen",
        "Bjørn Johansen",
        "Emma Hansen",
        "David Andersen",
        "Anders Hansen",
        "Anna Olsen",
        "Bjørn Johansen",
      
       
    };
    public MainWindow()
    {
        InitializeComponent();

    }

    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {


        string searchText = SearchTextBox.Text;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            ResultsListBox.ItemsSource = null;
            return;
        }
        var results = _people.Where(person => person.Contains(
            searchText, StringComparison.OrdinalIgnoreCase)).ToList();

        ResultsListBox.ItemsSource = results;


    }
}