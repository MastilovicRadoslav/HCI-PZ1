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
	public partial class DetaljniPodaci : Window
	{
		private string fajl_pomocni = "";
		private string slika_pomocna = "";

		public DetaljniPodaci(int index)
		{
			#region Ime, Broj dresa, Datum, Sika
			Barselona barsa = MainWindow.Barsa[index];
			InitializeComponent();

			textBoxNaziv.Text = barsa.nazivIgraca;
			textBoxBroj.Text = "Broj dresa je: " + Convert.ToString(barsa.brojDresa);
			textBoxDatum.Text = "Datum je: " + barsa.datumPrelaska.ToString() + ".";

			//slika_pomocna = barsa.Slika;
			
			Uri uri = new Uri(barsa.Slika);
			imgSlika.Source = new BitmapImage(uri);

			TextRange textRange;
			System.IO.FileStream fileStream;

			//fajl_pomocni = barsa.Fajl;

			if (System.IO.File.Exists(barsa.Fajl))
			{
				textRange = new TextRange(richTextBoxBarselona.Document.ContentStart, richTextBoxBarselona.Document.ContentEnd);
				using (fileStream = new System.IO.FileStream(barsa.Fajl, System.IO.FileMode.OpenOrCreate))
				{
					textRange.Load(fileStream, System.Windows.DataFormats.Rtf);
				}
			}
			#endregion

		}

		#region Dugme dodaj
		private void btnZatvori_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
		#endregion

		#region Pomjeranje prozora
		private void UIPath_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
		#endregion
	}
}
