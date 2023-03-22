using Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region Dodatna polja

		private DataIO serializer = new DataIO();
		public static BindingList<Barselona> Barsa { get; set; }

		public static bool cbOznacen = false;					

		private static BindingList<Barselona> brisanje = new BindingList<Barselona>();
		public static BindingList<Barselona> Brisanje { get => brisanje; set => brisanje = value; }

		#endregion

		public MainWindow()
		{
			#region Inicijalizacija

			//MessageBox.Show("Dobrodošli na stranicu FK Barselona.", "Obavještenje!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
			Barsa = serializer.DeSerializeObject<BindingList<Barselona>>("barselona.xml");
			if (Barsa == null)
			{
				Barsa = new BindingList<Barselona>();
			}

			DataContext = this;

			#endregion

			InitializeComponent();
		}

		#region Dugme dodaj

		private void buttonDodaj_Click(object sender, RoutedEventArgs e)
		{
			Dodaj dodaj = new Dodaj();
			dodaj.ShowDialog();
		}
		#endregion

		#region Dugme za zatvaranje
		private void buttonZatvori_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		#endregion


		#region Cuvanje u fajl

		private void Window_Closing(object sender, CancelEventArgs e)
		{
			serializer.SerializeObject<BindingList<Barselona>>(Barsa, "barselona.xml");
		}

		#endregion


		#region Manipulisanje prozorom
		private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.DragMove();
		}
		#endregion


		#region Dugme obrisi
		private void buttonObrisi_Click(object sender, RoutedEventArgs e)
		{
			if(Barsa.Count > 0)
			{
				for (int i = 0; i < brisanje.Count; i++)
				{
					Barsa.Remove(brisanje[i]);
				}

				for (int i = 0; i < brisanje.Count; i++)
				{
					if (brisanje[i] != null)
					{
						string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, brisanje[i].Fajl);
						try
						{
							File.Delete(filePath);
						}
						catch (IOException exp)
						{
							Console.WriteLine(exp.Message);
						}
					}
				}
			}
			else
			{
				MessageBox.Show("Nije moguce brisati iz prazne tabele.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		#endregion

		#region Promjena selekcije u DataGrid
		private void dataGridBarselona_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (cbOznacen == true)
			{
				brisanje.Add((Barselona)dataGridBarselona.SelectedItem);
			}
			cbOznacen = false;
		}
		#endregion


		#region Hyperlink
		private void Hyperlink_Click(object sender, RoutedEventArgs e)
		{

			Logovanje logovanje = new Logovanje();


			if (Logovanje.ime.Equals("admin") && Logovanje.sifra.Equals("admin123"))
			{
				Izmeni izmijeni = new Izmeni(dataGridBarselona.SelectedIndex);
				izmijeni.ShowDialog();


			}
			else if (Logovanje.ime.Equals("posjetioc") && Logovanje.sifra.Equals("posjetioc123"))
			{
				Procitaj procitaj = new Procitaj(dataGridBarselona.SelectedIndex);
				procitaj.textBoxNaziv.IsEnabled = false;
				procitaj.textBoxDatum.IsEnabled = false;
				procitaj.textBoxBroj.IsEnabled = false;
				procitaj.richTextBoxBarselona.IsEnabled = false;
				procitaj.imageSlika.IsEnabled = false;
				procitaj.RichTextBoxText.FontSize = 25;
				procitaj.ShowDialog();

			}
			else
			{
				MessageBox.Show("Pogresan unos admina ili posjetioca.", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}

		#endregion

		private void cbBrisanje_MouseEnter(object sender, MouseEventArgs e)
		{
			cbOznacen = true;

		}
	}
}
