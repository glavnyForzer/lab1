using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void BoatBrands_Click(object sender, RoutedEventArgs e)
        {
            BoatBrandsListBox.ItemsSource = await new ApiClient().GetCapitansAsync();
        }

        private async void Companies_Click(object sender, RoutedEventArgs e)
        {
            CompaniesListBox.ItemsSource = await new ApiClient().GetCompaniesAsync();
        }

        private void BoatBrandsListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(BoatBrandsListBox.SelectedItem as CapitanModel));
        }

        private void CompaniesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigate(new SelectedItemListBox(CompaniesListBox.SelectedItem as CompanyModel));
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            TokenManager.Token = null;
            NavigationService.GoBack();
        }

        private void AddBoatBrandButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBoatBrandPage());
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBoatBrandPage());
        }
    }
}