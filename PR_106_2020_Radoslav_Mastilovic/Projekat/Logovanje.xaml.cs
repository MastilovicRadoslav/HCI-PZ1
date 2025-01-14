﻿using System;
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
	/// Interaction logic for Logovanje.xaml
	/// </summary>
	public partial class Logovanje : Window
	{
		#region Pomocna polja
		public static string ime;
		public static string sifra;
		#endregion

		public Logovanje()
		{
			#region Početne vrijednosti
			InitializeComponent();
			textBoxIme.Text = "unesite ime";
			textBoxIme.Foreground = Brushes.LightSlateGray;
			#endregion
		}

		#region Dugme za prijavu
		private void btnPrijava_Click(object sender, RoutedEventArgs e)
		{
			if (Validate())
			{
				if (textBoxIme.Text.Trim().Equals("admin") && passwordBoxSifra.Password.Equals("admin123"))
				{


					MessageBox.Show("Dobrodošli na stranicu FK Barselona kao Admin.", "Obavještenje!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
					ime = "admin";
					sifra = "admin123";
					MainWindow window = new MainWindow();
					window.ShowDialog();


				}
				else if (textBoxIme.Text.Trim().Equals("posjetioc") && passwordBoxSifra.Password.Equals("posjetioc123"))
				{

					MessageBox.Show("Dobrodošli na stranicu FK Barselona kao Posjetilac.", "Obavještenje!", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
					ime = "posjetioc";
					sifra = "posjetioc123";
					MainWindow window = new MainWindow();
					window.dataGridBarselona.CanUserAddRows = false;
					window.dataGridBarselona.CanUserDeleteRows = false;
					window.dataGridBarselona.CanUserReorderColumns = false;
					window.dataGridBarselona.CanUserResizeColumns = false;
					window.dataGridBarselona.CanUserResizeRows = false;
					window.dataGridBarselona.CanUserSortColumns = false;
					window.btnObrisi.IsEnabled = false;
					window.btnDodaj.IsEnabled = false;
					//window.btnObrisi.Visibility = Visibility.Hidden;
					//window.btnDodaj.Visibility = Visibility.Hidden;
					window.ShowDialog();

				}else if (textBoxIme.Text.Trim().Equals("admin") && !passwordBoxSifra.Password.Equals("admin123"))
				{
					passwordBoxSifra.Password = "";
					labelSifraGreska.Content = "Pogresna sifra za admina";
					passwordBoxSifra.BorderBrush = Brushes.Red;
				}else if (!textBoxIme.Text.Trim().Equals("admin") && passwordBoxSifra.Password.Equals("admin123"))
				{
					textBoxIme.Text = "";
					labelImeGreska.Content = "Pogresno ime za admina";
					textBoxIme.BorderBrush = Brushes.Red;

				}else if (textBoxIme.Text.Trim().Equals("posjetioc") && !passwordBoxSifra.Password.Equals("posjetioc123"))
				{
					passwordBoxSifra.Password = "";
					labelSifraGreska.Content = "Pogresna sifra za posjetioca";
					passwordBoxSifra.BorderBrush = Brushes.Red;
				}else if (!textBoxIme.Text.Trim().Equals("posjetioc") && passwordBoxSifra.Password.Equals("posjetioc123"))
				{
					textBoxIme.Text = "";
					labelImeGreska.Content = "Pogresno ime za posjetioca";
					textBoxIme.BorderBrush = Brushes.Red;

				}
				else
				{					
					passwordBoxSifra.Password = "";
					labelSifraGreska.Content = "Pogresna sifra";
					passwordBoxSifra.BorderBrush = Brushes.Red;
					textBoxIme.Text = "";
					labelImeGreska.Content = "Pogresno ime";
					textBoxIme.BorderBrush = Brushes.Red;				
				}
			}
		}
		#endregion

		#region Validacija unosa
		private bool Validate()
		{
			bool result = true;

			//IME
			if (textBoxIme.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("unesite ime"))
			{
				result = false;
				labelImeGreska.Content = "Popunite podatke!";
				textBoxIme.BorderBrush = Brushes.Red;
			}
			else
			{
				labelImeGreska.Content = "";
				textBoxIme.BorderBrush = Brushes.Gray;
			}
			//SIFRA
			if (passwordBoxSifra.Password.Equals("") || passwordBoxSifra.Password.Equals("unesite šifru"))
			{
				result = false;
				labelSifraGreska.Content = "Popunite podatke!";
				passwordBoxSifra.BorderBrush = Brushes.Red;
			}
			else
			{
				labelSifraGreska.Content = "";
				passwordBoxSifra.BorderBrush = Brushes.Gray;
			}
			if (textBoxIme.Text.Trim().Equals("") || textBoxIme.Text.Trim().Equals("unesite ime") && passwordBoxSifra.Password.Equals("") || passwordBoxSifra.Password.Equals("unesite šifru"))
			{
				MessageBox.Show("Morate unijeti podatke!", "Greska!", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			return result;
		}
		#endregion

		#region TextBoxIme
		private void textBoxIme_GotFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxIme.Text.Trim().Equals("unesite ime"))
			{
				textBoxIme.Text = "";
				textBoxIme.Foreground = Brushes.LightGray;
			}
			labelImeGreska.Content = "";
			textBoxIme.Foreground = Brushes.LightGray;
		}

		private void textBoxIme_LostFocus(object sender, RoutedEventArgs e)
		{
			if (textBoxIme.Text.Trim().Equals(string.Empty))
			{
				textBoxIme.Text = "unesite ime";
				textBoxIme.Foreground = Brushes.LightGray;

			}
		}
		#endregion

		#region PasswordBoxSifra
		private void passwordBoxSifra_GotFocus(object sender, RoutedEventArgs e)
		{
			if (passwordBoxSifra.Password.Trim().Equals(String.Empty))
			{
				passwordBoxSifra.Password = "";
				passwordBoxSifra.Foreground = Brushes.LightGray;
			}
			labelSifraGreska.Content = "";
			passwordBoxSifra.Foreground = Brushes.LightGray;

		}

		private void passwordBoxSifra_LostFocus(object sender, RoutedEventArgs e)
		{
			if (passwordBoxSifra.Password.Trim().Equals(string.Empty))
			{
				passwordBoxSifra.Password = "";
				passwordBoxSifra.Foreground = Brushes.LightGray;
			}
			if (!passwordBoxSifra.Password.Trim().Equals(string.Empty))
			{
				labelSifraGreska.Content = "";
				passwordBoxSifra.Foreground = Brushes.LightGray;
			}
		}
		#endregion



		#region Pomjeranje prozora
		private void Window_MouseDown(object sender, MouseButtonEventArgs e) //Ako je pritisnut mis na aplikaciji
		{
			if (e.LeftButton == MouseButtonState.Pressed) //Pomjeraj
			{
				DragMove();
			}
		}
		#endregion

		#region Minimizacija prozora
		private void btnMinimize_Click(object sender, RoutedEventArgs e)//Ako je pritisnuto dugme Minimize
		{
			WindowState = WindowState.Minimized; //Smanji prozor

		}
		#endregion

		#region Izlazak iz prozora
		private void btnClose_Click(object sender, RoutedEventArgs e)//Ako je pritisnuto dugme Close
		{
			Application.Current.Shutdown();	//Zatvori prozor
		}
		#endregion

		#region Dugme za izlaz
		private void btnIzlaz_Click(object sender, RoutedEventArgs e) //Ako je pritisnuto dugme za izlaza
		{
			this.Close(); //Izadji
			Application.Current.Shutdown(); //Zatvori aplikaciju
		}

		#endregion


	}
}

