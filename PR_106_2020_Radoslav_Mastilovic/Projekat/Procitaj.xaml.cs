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

namespace Projekat
{
	/// <summary>
	/// Interaction logic for Procitaj.xaml
	/// </summary>
	public partial class Procitaj : Window
	{
		public Procitaj(int index)
		{
			Barselona barsa = MainWindow.Barsa[index];
			InitializeComponent();

			textBoxNaziv.Text = barsa.nazivIgraca;
			textBoxBroj.Text = "Broj dresa je: " + Convert.ToString(barsa.brojDresa);
			textBoxDatum.Text = "Datum je: " + barsa.datumPrelaska.ToString() + ".";

			Uri uri = new Uri(barsa.Slika);
			imageSlika.Source = new BitmapImage(uri);

			TextRange textRange;
			System.IO.FileStream fileStream;

			if (System.IO.File.Exists(barsa.Fajl))
			{
				textRange = new TextRange(richTextBoxBarselona.Document.ContentStart, richTextBoxBarselona.Document.ContentEnd);
				using (fileStream = new System.IO.FileStream(barsa.Fajl, System.IO.FileMode.OpenOrCreate))
				{
					textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
				}
			}

		}
		private void buttonZatvori_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void UIPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();

		}
	}
}
