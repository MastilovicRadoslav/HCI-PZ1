using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class
{
	[Serializable]
	public class Barselona
	{
		public int brojDresa;   //brojevnog tipa
		public string nazivIgraca;  //tekstualno
		public DateTime datumPrelaska;  //datum
		private string slika;   //prikaz slike
		private string fajl; //referenca na *.rtf datoteku

		public Barselona()
		{
		}
		public Barselona(int brojDresa, string nazivIgraca, DateTime datumPrelaska, string slika, string fajl)
		{
			this.brojDresa = brojDresa;
			this.nazivIgraca = nazivIgraca;
			this.datumPrelaska = datumPrelaska;
			this.slika = slika;
			this.fajl = fajl;
		}

		public int BrojDresa
		{
			get { return brojDresa; }
			set { brojDresa = value; }

		}

		public string NazivIgraca
		{
			get { return nazivIgraca; }
			set { nazivIgraca = value; }
		}

		public DateTime DatumPrelaska
		{
			get { return datumPrelaska; }
			set { datumPrelaska = value; }
		}

		public string Slika
		{
			get { return slika; }
			set { slika = value; }
		}

		public string Fajl
		{
			get { return fajl; }
			set { fajl = value; }
		}
	}
}
