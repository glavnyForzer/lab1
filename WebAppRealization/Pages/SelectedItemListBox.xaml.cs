using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using WebAppIImpl.remote.models;

namespace WebAppIImpl.Pages
{
    public partial class SelectedItemListBox : Page
    {
        private CapitanModel? capitanModel;
        private CompanyModel? companyModel;
        public SelectedItemListBox(CapitanModel? _capitanModel)
        {
            InitializeComponent();
            capitanModel = _capitanModel;
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Brend"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Model"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            //FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            //vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Model"));
            //stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить водителя и добавить авто";
            DeleteButton.Content = "Удалить водителя";
            DeleteBoatModelOrEmployeeButton.Content = "Удалить выбранное авто";
        }
        public SelectedItemListBox(CompanyModel? _companyModel)
        {
            InitializeComponent();
            companyModel = _companyModel;
            
            DataTemplate dataTemplate = new DataTemplate();

            FrameworkElementFactory stackPanelFactory = new FrameworkElementFactory(typeof(StackPanel));

            FrameworkElementFactory nameTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Name"));
            stackPanelFactory.AppendChild(nameTextBlockFactory);

            FrameworkElementFactory colorTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            colorTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Age"));
            stackPanelFactory.AppendChild(colorTextBlockFactory);

            FrameworkElementFactory vinTextBlockFactory = new FrameworkElementFactory(typeof(TextBlock));
            vinTextBlockFactory.SetBinding(TextBlock.TextProperty, new Binding("Position"));
            stackPanelFactory.AppendChild(vinTextBlockFactory);

            dataTemplate.VisualTree = stackPanelFactory;
            
            listBox.ItemTemplate = dataTemplate;
            listBox.SelectedItem = null;
            
            UpdateButton.Content = "Обновить информацию о компании";
            DeleteButton.Content = "Удалить Компанию";
            DeleteBoatModelOrEmployeeButton.Content = "Удалить выбранного сотрудника";
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                listBox.ItemsSource = await new ApiClient().GetEmployeesByCompanyAsync(companyModel.Id);
            }
            else
            {
                listBox.ItemsSource = await new ApiClient().GetBoatsByCapitanAsync(capitanModel.Id);
            }
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                await new ApiClient().DeleteCapitandAsync(companyModel.Id);
                
                NavigationService?.GoBack();
            }
            else
            {
                await new ApiClient().DeleteCapitandAsync(capitanModel.Id);
                
                NavigationService?.GoBack();
            }
        }

        private async void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                NavigationService.Navigate(new AddCompanyPage());
            }
            else
            {
                NavigationService.Navigate(new AddBoatBrandPage(capitanModel));
            }
        }

        private async void DeleteBoatModelOrEmployeeButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (companyModel != null)
            {
                var selectedItem = listBox.SelectedItem as EmployeeModel;
            }
            else
            {
                var selectedItem = listBox.SelectedItem as BoatModel;
                
                await new ApiClient().DeleteBoatAsync(selectedItem.Id, capitanModel.Id);
            }
            
            
        }
    }
}