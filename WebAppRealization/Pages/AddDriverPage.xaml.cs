using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class AddBoatBrandPage : Page
    {
        private ObservableCollection<BoatModel> BoatModels = new ();
        private CapitanModel? Capitan = null;
        
        public AddBoatBrandPage()
        {
            InitializeComponent();
        }
        
        public AddBoatBrandPage(CapitanModel capitanModel)
        {
            InitializeComponent();
            Capitan = capitanModel;

            NameTextBox.Text = Capitan.Name;
            CountryManifactureTextBox.Text = Capitan.Address;

            ButtonAction.Content = "Обновить";
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            BoatModels.Add(new BoatModel
            {
                Brend = VinNumberTextBox.Text,
                Model = NameBoatModelTextBox.Text
            });

            if (Capitan == null)
            {
                var item = new CapitanCreationModel()
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Boats = BoatModels
                };
                if (await new ApiClient().PostCreateCapitanAsync(item) == null)
                {
                    MessageBox.Show("Ошибка: Капитан не добавлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Капитан добавлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                var item = new CapitanCreationModel
                {
                    Name = NameTextBox.Text,
                    Address = CountryManifactureTextBox.Text,
                    Boats = BoatModels
                };
                if (await new ApiClient().PutUpdateCapitandAsync(item, Capitan.Id) != null)
                {
                    MessageBox.Show("Ошибка: Капитан не обновлен.", "Ошибка", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    MessageBox.Show("Капитан Обновлен.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void CreateBoatModelButton_Click(object sender, RoutedEventArgs e)
        {
            if (VinNumberTextBox.Text == "" || NameBoatModelTextBox.Text == "") return;
            
            var item = new BoatCreationModel
            {
                Brend = VinNumberTextBox.Text,
                Model = NameBoatModelTextBox.Text,
            };

            if(await new ApiClient().PostBoatForCapitanAsync(item, Capitan.Id) != null)
            {
                MessageBox.Show("Лодка добавлена.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Лодка не добавлена.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            VinNumberTextBox.Text = "";
            NameBoatModelTextBox.Text = "";
        }
    }
}