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
using System.Windows.Shapes;

namespace StringerWPF.WPF_UI_Tutorial
{
    /// <summary>
    /// Interaction logic for Basics.xaml
    /// </summary>
    public partial class Basics : Window
    {
        public Basics()
        {
            InitializeComponent();
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The description is: {this.DescriptionText.Text}");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckBox.IsChecked = this.AssemblyCheckBox.IsChecked = this.PlasmaCheckBox.IsChecked =
            this.LaserCheckBox.IsChecked = this.PurchaseCheckBox.IsChecked = this.LatheCheckBox.IsChecked =
            this.DrillCheckBox.IsChecked = this.FoldCheckBox.IsChecked = this.RollCheckBox.IsChecked = this.SawCheckBox.IsChecked = false;

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            this.Length.Text += ((CheckBox)sender).Content; 

        }

        private void FinishDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteText == null)
                return;

            var combo = (ComboBox)sender;
            var value = (ComboBoxItem)combo.SelectedValue;

            this.NoteText.Text = (string)value.Content;
        }

        private void SupplierNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MassText.Text = this.SupplierNameText.Text;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
