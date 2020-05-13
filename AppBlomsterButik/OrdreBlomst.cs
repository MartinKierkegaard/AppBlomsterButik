using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AppBlomsterButik
{
   public class OrdreBlomst : INotifyPropertyChanged
    {
		private string navn;

		public string Navn
		{
			get { return navn; }
			set { navn = value;
				OnPropertyChanged();
					}
		}

		private int antal;

		public int Antal
		{
			get { return antal; }
			set { antal = value;
				OnPropertyChanged();
			}
		}

		private string farve;

		public event PropertyChangedEventHandler PropertyChanged;





		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public string Farve
		{
			get { return farve; }
			set { farve = value;
				OnPropertyChanged();
			}
		}

		public OrdreBlomst(string navn,int antal , string farve)
		{
			this.Navn = navn;
			this.Antal = antal;
			this.Farve = farve;

		}
		public OrdreBlomst()
		{

		}

	}
}
