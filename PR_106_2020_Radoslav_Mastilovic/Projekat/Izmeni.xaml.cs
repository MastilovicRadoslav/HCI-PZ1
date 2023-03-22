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
using Class;
using System.IO;
using Microsoft.Win32;

namespace Projekat
{
	/// <summary>
	/// Interaction logic for Izmeni.xaml
	/// </summary>
	public partial class Izmeni : Window
	{
		private int index = 0;
		private string pomocna = "";
		private string fajl_pomocni = "";
		private string slika_pomocna = "";

		public Izmeni(int idx)
		{
			InitializeComponent();
			Barselona barsa = MainWindow.Barsa[idx];
			index = idx;

			ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
			ComboBoxFamily.SelectedIndex = 2;
			ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
			ComboBoxColor.SelectedIndex = 0;

			textBoxNaziv.Text = barsa.nazivIgraca;
			textBoxBroj.Text = Convert.ToString(barsa.BrojDresa);

			slika_pomocna = barsa.Slika;
			Uri uri = new Uri(barsa.Slika);
			imageSlika.Source = new BitmapImage(uri);

			//datePickerDatum.Text = Convert.ToString(barsa.datumPrelaska);

			fajl_pomocni = barsa.Fajl;

			TextRange textRange;
			System.IO.FileStream fileStream;

			if (System.IO.File.Exists(fajl_pomocni))
			{
				textRange = new TextRange(RichTextBoxBarselona.Document.ContentStart, RichTextBoxBarselona.Document.ContentEnd);
				using (fileStream = new System.IO.FileStream(fajl_pomocni, System.IO.FileMode.OpenOrCreate))
				{
					textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
				}

			}

		}

		private void UIPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}

		private bool validate()
		{
			bool result = true;

			if (textBoxNaziv.Text.Trim().Equals("") || textBoxNaziv.Text.Trim().Equals("Unesite naziv igrača"))
			{
				result = false;
				textBoxNaziv.Foreground = Brushes.Red;
				textBoxNaziv.BorderBrush = Brushes.Red;
				textBoxNaziv.BorderThickness = new Thickness(1);
				textBoxGreskaNaziv.Text = "Obavezno popuniti!";
				textBoxGreskaNaziv.Foreground = Brushes.Red;
			}
			else
			{
				textBoxNaziv.Foreground = Brushes.Black;
				textBoxNaziv.BorderBrush = Brushes.Green;
				textBoxNaziv.BorderThickness = new Thickness(1);
				textBoxGreskaNaziv.Text = "";
				textBoxGreskaNaziv.Foreground = Brushes.Gray;

			}

			if (textBoxBroj.Text.Trim().Equals("") || textBoxBroj.Text.Trim().Equals("Unesite broj dresa"))
			{
				result = false;
				textBoxBroj.Foreground = Brushes.Red;
				textBoxBroj.BorderBrush = Brushes.Red;
				textBoxBroj.BorderThickness = new Thickness(1);
				textBoxGreskaBroj.Text = "Obavezno popuniti!";
				textBoxGreskaBroj.Foreground = Brushes.Red;
			}
			else
			{

				bool isNumeric = int.TryParse(textBoxBroj.Text, out _);
				if (isNumeric)
				{
					if (Int32.Parse(textBoxBroj.Text) >= 0)
					{
						textBoxBroj.Foreground = Brushes.Black;
						textBoxBroj.BorderBrush = Brushes.Green;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.BorderThickness = new Thickness(0);
						textBoxGreskaBroj.Text = "";
					}

					else
					{
						result = false;
						textBoxBroj.Foreground = Brushes.Red;
						textBoxBroj.BorderBrush = Brushes.Red;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.Text = "Unesite pozitivan broj!";
						textBoxGreskaBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.BorderBrush = Brushes.Red;
					}

				}
				else
				{
					result = false;
					textBoxBroj.Foreground = Brushes.Red;
					textBoxBroj.BorderBrush = Brushes.Red;
					textBoxBroj.BorderThickness = new Thickness(1);
					textBoxGreskaBroj.Text = "Unesite broj!";
					textBoxGreskaBroj.BorderThickness = new Thickness(1);
					textBoxGreskaBroj.BorderBrush = Brushes.Red;

				}

			}



			//if (DateTime.Now == null)
			//{
			//	result = false;
			//	labelaGreskaDatum.FontSize = 12;
			//	labelaGreskaDatum.Content = "Obavezno!";
			//	labelaGreskaDatum.Foreground = Brushes.Red;
			//	labelaGreskaDatum.BorderBrush = Brushes.Red;
			//	labelaGreskaDatum.BorderThickness = new Thickness(1);
			//}
			//else
			//{
			//	labelaGreskaDatum.Content = "";
			//}
			return result;
		}


		private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
		{
			if (validate())
			{
				if (pomocna == "")
				{
					pomocna = slika_pomocna;
				}
				MainWindow.Barsa[index] = new Class.Barselona(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, pomocna, fajl_pomocni);

				TextRange textRange;
				FileStream fileStream;
				textRange = new TextRange(RichTextBoxBarselona.Document.ContentStart, RichTextBoxBarselona.Document.ContentEnd);
				fileStream = new FileStream(fajl_pomocni, FileMode.Open);
				textRange.Save(fileStream, DataFormats.Rtf);
				fileStream.Close();

				this.Close();
			}
			else
			{
				MessageBox.Show("Popunite polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
				textBoxNaziv.Foreground = Brushes.Black;
				textBoxBroj.Foreground = Brushes.Black;
			}


		}



		private void buttonIzađi_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxFamily.SelectedItem != null)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
			}

		}

		private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (ComboBoxSize.SelectedValue != null)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
			}

		}

		private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxColor.SelectedValue != null)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.ForegroundProperty, ComboBoxColor.SelectedValue);
			}

		}

		private void RichTextBoxBarselona_SelectionChanged(object sender, RoutedEventArgs e)
		{

			object temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.FontStyleProperty);
			tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

			temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.FontWeightProperty);
			tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

			temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

			temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.FontFamilyProperty);
			ComboBoxFamily.SelectedItem = temp;
			temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.FontSizeProperty);
			ComboBoxSize.Text = temp.ToString();

		}

		private void CountWords()
		{
			int count = 0;
			int index = 0;
			string richText = new TextRange(RichTextBoxBarselona.Document.ContentStart, RichTextBoxBarselona.Document.ContentEnd).Text;

			while (index < richText.Length && char.IsWhiteSpace(richText[index]))
			{
				index++;
			}

			while (index < richText.Length)
			{
				while (index < richText.Length && !char.IsWhiteSpace(richText[index]))
					index++;

				count++;

				while (index < richText.Length && char.IsWhiteSpace(richText[index]))
					index++;

			}
			TextBlockBrojReci.Text = count.ToString();

		}

		private void RichTextBoxBarselona_TextChanged(object sender, TextChangedEventArgs e)
		{
			CountWords();

		}

		private void buttonBrowse_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			textBoxSlika.Text = "";
			if (openFileDialog.ShowDialog() == true)
			{
				pomocna = openFileDialog.FileName;
				Uri uri = new Uri(pomocna);
				imageSlika.Source = new BitmapImage(uri);
				textBoxSlika.Text = "";
			}
		}
	}
}
