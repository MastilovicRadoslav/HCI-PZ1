using Class;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Projekat
{
	/// <summary>
	/// Interaction logic for Dodaj.xaml
	/// </summary>
	public partial class Dodaj : Window
	{
		private string slika = "";
		public Dodaj()
		{
			#region Podešavanje početnih vrijednosti
			InitializeComponent();
			textBoxNaziv.Text = "Unesite naziv igrača";
			textBoxNaziv.Foreground = Brushes.Thistle;

			textBoxBroj.Text = "Unesite broj dresa";
			textBoxBroj.Foreground = Brushes.Thistle;



			ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
			ComboBoxFamily.SelectedIndex = 2;
			ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
			ComboBoxColor.SelectedIndex = 0;
			#endregion	
		}


		#region Dugme za izlaz
		private void buttonIzađi_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Dugme za dodavanje igraca
		private void buttonDodaj_Click(object sender, RoutedEventArgs e)
		{
			if (validate())
			{
				if (buttonDodaj.Content.Equals("Dodaj"))
				{
					string naziv = "";
					naziv = textBoxNaziv.Text + ".rtf";


					TextRange textRange;
					FileStream fileStream;
					textRange = new TextRange(RichTextBoxBarselona.Document.ContentStart, RichTextBoxBarselona.Document.ContentEnd);
					fileStream = new FileStream(naziv, FileMode.Create);
					textRange.Save(fileStream, DataFormats.Rtf);
					fileStream.Close();
					this.Close();

					//datePickerDatum.SelectedDate.Value.Date

					Barselona igrac = new Barselona(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, slika, naziv);
					MainWindow.Barsa.Add(igrac);
				}

			}
			else
			{
				MessageBox.Show("Popunite sva polja!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}
		#endregion

		#region Validacija unosa
		private bool validate()
		{
			bool result = true;

			#region Naziv
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
			#endregion


			#region BrojDresa
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

				bool broj = int.TryParse(textBoxBroj.Text, out _);
				if (broj)
				{
					if (Int32.Parse(textBoxBroj.Text) > 0)
					{
						textBoxBroj.Foreground = Brushes.Black;
						textBoxBroj.BorderBrush = Brushes.Green;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.Text = "";
					}
					else
					{
						result = false;
						textBoxBroj.Foreground = Brushes.Red;
						textBoxBroj.BorderBrush = Brushes.Red;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.Text = "Unesite pozitivan broj!";
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
					textBoxGreskaBroj.BorderBrush = Brushes.Red;

				}
			}
			#endregion

			#region Slika

			if (textBoxSlika.Text.Trim().Equals("Slika"))
			{
				result = false;
				borderSlika.BorderBrush = Brushes.Red;
				borderSlika.BorderThickness = new Thickness(1);
				labelaGreskaSlika.Content = "Obavezno!";
				labelaGreskaSlika.Background = Brushes.LightGray;
				labelaGreskaSlika.Foreground = Brushes.Red;
				labelaGreskaSlika.BorderThickness = new Thickness(1);



			}
			else
			{
				borderSlika.BorderBrush = Brushes.Green;
				borderSlika.BorderThickness = new Thickness(0);
				labelaGreskaSlika.Content = "";
				labelaGreskaSlika.BorderThickness = new Thickness(0);
				textBoxSlika.Text = "";
			}
			#endregion

			#region Datum 
			if (DateTime.Now == null)
			{
				result = false;
				labelaGreskaDatum.FontSize = 12;
				labelaGreskaDatum.Content = "Obavezno polje!";
				labelaGreskaDatum.Foreground = Brushes.Red;
				labelaGreskaDatum.BorderBrush = Brushes.Red;
				labelaGreskaDatum.BorderThickness = new Thickness(1);

			}
			else
			{
				labelaGreskaDatum.Content = "";
				labelaGreskaDatum.BorderThickness = new Thickness(0);

			}


			#region RichTextBox
			if (RichTextBoxText.Text.Trim().Equals("Unesite opis igrača") || RichTextBoxText.Text.Trim().Equals(""))
			{
				result = false;
				RichTextBoxText.Text = "Obavezno polje!";
				RichTextBoxText.Foreground = Brushes.Red;
				RichTextBoxBarselona.BorderBrush= Brushes.Red;
				RichTextBoxBarselona.BorderThickness = new Thickness(1);

			}
			else
			{
				RichTextBoxText.Foreground = Brushes.Black;
				RichTextBoxBarselona.BorderBrush = Brushes.Gray;
				

			}
			#endregion


			#endregion
			return result;
		}
		#endregion

		#region TextBox Naziv

		private void textBoxNaziv_GotFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxNaziv.Text.Trim().Equals("Unesite naziv igrača"))
			{
				textBoxNaziv.Text = "";
				textBoxNaziv.Foreground = Brushes.Black;
			}

		}

		private void textBoxNaziv_LostFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxNaziv.Text.Trim().Equals(string.Empty))
			{
				textBoxNaziv.Text = "Unesite naziv igrača";
				textBoxNaziv.Foreground = Brushes.Gray;
			}

		}
		#endregion


		#region TextBox Broj dresa

		private void textBoxBroj_GotFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxBroj.Text.Trim().Equals("Unesite broj dresa"))
			{
				textBoxBroj.Text = "";
				textBoxBroj.Foreground = Brushes.Black;
			}

		}

		private void textBoxBroj_LostFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxBroj.Text.Trim().Equals(string.Empty))
			{
				textBoxBroj.Text = "Unesite broj dresa";
				textBoxBroj.Foreground = Brushes.Gray;
			}

		}

		#endregion

		#region RichTextBox opis igrača

		private void RichTextBoxBarselona_GotFocus(object sender, RoutedEventArgs e)
		{
			if(RichTextBoxText.Text.Trim().Equals("Unesite opis igrača") || RichTextBoxText.Text.Trim().Equals("Obavezno polje!"))
			{
				RichTextBoxText.Text = "";
				RichTextBoxText.Foreground = Brushes.Black;
			}
		}

		private void RichTextBoxBarselona_LostFocus(object sender, RoutedEventArgs e)
		{
			if (RichTextBoxText.Text.Trim().Equals(String.Empty))
			{
				RichTextBoxText.Text = "Unesite opis igrača";
				RichTextBoxText.Foreground = Brushes.Gray;
			}
		}
		#endregion

		#region Datum
		private void datePickerDatum_MouseEnter(object sender, MouseEventArgs e)
		{
			datePickerDatum.Text = DateTime.Now.ToString();
		}
		#endregion

		#region Dugme za dodavanje slike
		private void buttonBrowse_Click(object sender, RoutedEventArgs e)
		{
			textBoxSlika.Visibility = Visibility.Hidden;
			textBoxSlika.Text = "";

			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				slika = openFileDialog.FileName;
				Uri fileUri = new Uri(slika);
				imageSlika.Source = new BitmapImage(fileUri);
			}
		}
		#endregion

		#region Promjena u RichTextBoxu

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

			temp = RichTextBoxBarselona.Selection.GetPropertyValue(Inline.ForegroundProperty);

		}
		#endregion

		#region Promjena tipa slova
		private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxFamily.SelectedItem != null && !RichTextBoxBarselona.Selection.IsEmpty)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
			}

		}

		#endregion

		#region Promjena veličine slova
		private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxSize.SelectedValue != null && !RichTextBoxBarselona.Selection.IsEmpty)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
			}

		}
		#endregion

		#region Promjena boje slova
		private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxColor.SelectedValue != null && !RichTextBoxBarselona.Selection.IsEmpty)
			{
				RichTextBoxBarselona.Selection.ApplyPropertyValue(Inline.ForegroundProperty, ComboBoxColor.SelectedValue);
			}
		}
		#endregion



		#region Funkcija za prebrojavanje broja rijeci
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
		#endregion

		#region Prikaz broja izbrojanih rijeci
		private void RichTextBoxBarselona_TextChanged(object sender, TextChangedEventArgs e)
		{
			CountWords();

		}
		#endregion

		private void UIPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}


	}
}
