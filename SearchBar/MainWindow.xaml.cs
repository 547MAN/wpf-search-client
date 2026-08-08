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
using Npgsql;

namespace SearchBar;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly NpgsqlDataSource _dataSource;
    
    public MainWindow()
    {
        InitializeComponent();
        var connectionString =
        Environment.GetEnvironmentVariable("SEARCHBAR_DB_CONNECTION")
        ?? throw new InvalidOperationException(
            "Environment variable SEARCHBAR_DB_CONNECTION is not configured.");
        _dataSource = NpgsqlDataSource.Create(connectionString);


    }

    private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {


        string searchText = SearchTextBox.Text;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            ResultsListBox.ItemsSource = null;
            return;
        }

        await using var command = _dataSource.CreateCommand(
            """
            SELECT name
            FROM people
            WHERE name ILIKE $1
            ORDER BY name;
            """);
        command.Parameters.Add(
            new NpgsqlParameter
            {
                Value = $"%{searchText}%"
            });

        await using var reader = await command.ExecuteReaderAsync();

        var results = new List<string>();

        while (await reader.ReadAsync())
        {
            results.Add(reader.GetString(0));
        }
        

        ResultsListBox.ItemsSource = results;


    }
}