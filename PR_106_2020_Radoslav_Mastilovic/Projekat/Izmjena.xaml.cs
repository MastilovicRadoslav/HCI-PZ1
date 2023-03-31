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
	public partial class Izmjena : Window
	{
		#region Pomocna polja
		private int index = 0;
		private string pomoc = "";
		private string fajl_pomocni = "";
		private string slika_pomocna = "";
		#endregion

		public Izmjena(int idx)
		{
			InitializeComponent();

			#region Podešavanje početnih vrijednosti

			Barselona barsa = MainWindow.Barsa[idx];
			index = idx;

			ComboBoxFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
			ComboBoxSize.ItemsSource = new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };
			ComboBoxColor.ItemsSource = new List<string>() { "Black", "White", "Yellow", "Red", "Purple", "Orange", "Green", "Brown", "Blue" };
			ComboBoxColor.SelectedIndex = 0;
			ComboBoxFamily.SelectedIndex = 2;
			textBoxSlika.Text = "";
			textBoxSlika.Visibility = Visibility.Hidden;


			slika_pomocna = barsa.Slika;
			Uri uri = new Uri(barsa.Slika);
			imgSlika.Source = new BitmapImage(uri);

			fajl_pomocni = barsa.Fajl;

			textBoxNaziv.Text = barsa.nazivIgraca;
			textBoxBroj.Text = Convert.ToString(barsa.BrojDresa);

			TextRange textRange;
			System.IO.FileStream fileStream;

			if (System.IO.File.Exists(fajl_pomocni))
			{
				textRange = new TextRange(richTextBoxBarselona.Document.ContentStart, richTextBoxBarselona.Document.ContentEnd);
				using (fileStream = new System.IO.FileStream(fajl_pomocni, System.IO.FileMode.OpenOrCreate))
				{
					textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
				}

			}

			#endregion

		}

		#region Pomjeranje prozora
		private void UIPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
		#endregion

		#region	 Dugme za izlaz
		private void btnIzađi_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#endregion

		#region	Promjena u RichTextBoxu

		private void richTextBoxBarselona_SelectionChanged(object sender, RoutedEventArgs e)
		{

			object temp = richTextBoxBarselona.Selection.GetPropertyValue(Inline.FontStyleProperty);
			tglButtonItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));

			temp = richTextBoxBarselona.Selection.GetPropertyValue(Inline.FontWeightProperty);
			tglButtonBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));

			temp = richTextBoxBarselona.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
			tglButtonUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

			temp = richTextBoxBarselona.Selection.GetPropertyValue(Inline.FontFamilyProperty);
			ComboBoxFamily.SelectedItem = temp;
			temp = richTextBoxBarselona.Selection.GetPropertyValue(Inline.FontSizeProperty);
			ComboBoxSize.Text = temp.ToString();

		}

		#endregion

		#region Promjena tipa slova
		private void ComboBoxFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxFamily.SelectedItem != null)
			{
				richTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, ComboBoxFamily.SelectedItem);
			}
		}

		#endregion

		#region Promjena velicina slova
		private void ComboBoxSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (ComboBoxSize.SelectedValue != null)
			{
				richTextBoxBarselona.Selection.ApplyPropertyValue(Inline.FontSizeProperty, ComboBoxSize.SelectedValue);
			}

		}

		#endregion


		#region Promjena boje slova
		private void ComboBoxColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBoxColor.SelectedValue != null)
			{
				richTextBoxBarselona.Selection.ApplyPropertyValue(Inline.ForegroundProperty, ComboBoxColor.SelectedValue);
			}
		}
		#endregion


		#region Funckija za prebrojavanje rijeci
		private void BrojRijeci()
		{
			int brojRijeci = 0;
			int index = 0;
			string richText = new TextRange(richTextBoxBarselona.Document.ContentStart, richTextBoxBarselona.Document.ContentEnd).Text;

			while (index < richText.Length && char.IsWhiteSpace(richText[index]))
			{
				index++;
			}

			while (index < richText.Length)
			{
				while (index < richText.Length && !char.IsWhiteSpace(richText[index]))
				{
					index++;
				}

				brojRijeci++;

				while (index < richText.Length && char.IsWhiteSpace(richText[index]))
				{
					index++;
				}

			}
			TextBlockBrojReci.Text = brojRijeci.ToString();

		}
		#endregion


		#region Kod promjene teksta, poziva se funkcija za prebrojavanje
		private void richTextBoxBarselona_TextChanged(object sender, TextChangedEventArgs e)
		{
			BrojRijeci();

		}
		#endregion

		#region Dugme za izmjenu slike
		private void btnBrowse_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			textBoxSlika.Text = "";
			if (openFileDialog.ShowDialog() == true)
			{
				pomoc = openFileDialog.FileName;
				Uri uri = new Uri(pomoc);
				imgSlika.Source = new BitmapImage(uri);
				textBoxSlika.Text = "";
			}
		}
		#endregion


		#region Dugme za izmjenu igrača
		private void btnIzmijeni_Click(object sender, RoutedEventArgs e)
		{
			if (validate())
			{
				File.Delete(fajl_pomocni);

				if (pomoc == "") //provjera da li je slika uopste promijenjena, ako nije ostaje nam ista slika
				{
					pomoc = slika_pomocna;
				}
				MainWindow.Barsa[index] = new Class.Barselona(Int32.Parse(textBoxBroj.Text), textBoxNaziv.Text, DateTime.Now, pomoc, fajl_pomocni);

				TextRange textRange;
				FileStream fileStream;
				textRange = new TextRange(richTextBoxBarselona.Document.ContentStart, richTextBoxBarselona.Document.ContentEnd);
				fileStream = new FileStream(fajl_pomocni, FileMode.Create);
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

		#endregion

		#region Validacija unosa
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

				bool broj = int.TryParse(textBoxBroj.Text, out _);
				if (broj)
				{
					if (Int32.Parse(textBoxBroj.Text) > 0 && Int32.Parse(textBoxBroj.Text) < 100)
					{
						textBoxBroj.Foreground = Brushes.Black;
						textBoxBroj.BorderBrush = Brushes.Green;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.BorderThickness = new Thickness(0);
						textBoxGreskaBroj.Text = "";
					}
					else if (Int32.Parse(textBoxBroj.Text) <= 0)
					{
						result = false;
						textBoxBroj.Foreground = Brushes.Red;
						textBoxBroj.BorderBrush = Brushes.Red;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.Text = "Unesite pozitivan broj!";
						textBoxGreskaBroj.Foreground = Brushes.Red;
					}
					else
					{
						result = false;
						textBoxBroj.Foreground = Brushes.Red;
						textBoxBroj.BorderBrush = Brushes.Red;
						textBoxBroj.BorderThickness = new Thickness(1);
						textBoxGreskaBroj.Text = "Unesite broj manji od 100!";
						textBoxGreskaBroj.Foreground = Brushes.Red;
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
					textBoxGreskaBroj.Foreground = Brushes.Red;

				}

			}
			if (richTextBoxText.Text.Trim().Equals(""))
			{
				richTextBoxText.Text = "Obavezno polje!";
				richTextBoxText.Foreground = Brushes.Red;
				richTextBoxBarselona.BorderBrush = Brushes.Red;
				richTextBoxBarselona.BorderThickness = new Thickness(1);

			}
			else
			{
				richTextBoxText.Foreground = Brushes.Black;
				richTextBoxBarselona.BorderBrush = Brushes.Green;
			}


			return result;
		}

		#endregion

		#region Datum
		private void trenutniDatum_MouseEnter(object sender, MouseEventArgs e)
		{
			trenutniDatum.Text = DateTime.Now.ToString();
			trenutniDatum.IsEnabled = false;
		}
		#endregion
	}
}
