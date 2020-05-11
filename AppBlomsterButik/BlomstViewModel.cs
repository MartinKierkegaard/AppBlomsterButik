using BlomstViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;

namespace AppBlomsterButik
{
    public class BlomstViewModel
    {
        private string navnBlomst;
        private int antalBlomst;
        private string farveBlomst;

        public ObservableCollection<OrdreBlomst> OC_blomster { get; set; }

        public string NavnBlomst { get => navnBlomst; set => navnBlomst = value; }
        public int AntalBlomst { get => antalBlomst; set => antalBlomst = value; }
        public string FarveBlomst { get => farveBlomst; set => farveBlomst = value; }


        public OrdreBlomst SelectedOrdreBlomst { get; set; }

        public RelayCommand AddNyBlomst { get; set; }

        public RelayCommand SletSelectedBlomst { get; set; }

        public RelayCommand Gem { get; set; }


        private string jsonText1;
        /// <summary>
        /// variabel til at gemme json tekst i 
        /// </summary>
        public string jsonText { get => jsonText1; set => jsonText1 = value; }

        StorageFolder localfolder = null;
        private readonly string filnavn = "blomster.json";

        public BlomstViewModel()
        {
            localfolder = ApplicationData.Current.LocalFolder;

            OC_blomster = new ObservableCollection<OrdreBlomst>();

            //Testdata 
            OC_blomster.Add(new OrdreBlomst("Tulipan", 4, "Rød"));
            OC_blomster.Add(new OrdreBlomst("Tulipan", 3, "Hvid"));
            OC_blomster.Add(new OrdreBlomst("Tulipan", 2, "Gul"));

            AddNyBlomst = new RelayCommand(AddBlomst);
            SletSelectedBlomst = new RelayCommand(SletBlomst, canDeleteBlomsterListe);

            SelectedOrdreBlomst = new OrdreBlomst();

            Gem = new RelayCommand(GemDataTilDiskAsync);
        }

        /// <summary>
        /// metode til at tilføje en ny ordreblomst til listen
        /// </summary>
        public void AddBlomst()
        {
            OrdreBlomst oBlomst = new OrdreBlomst(NavnBlomst, AntalBlomst, FarveBlomst);

            OC_blomster.Add(oBlomst);

            SletSelectedBlomst.RaiseCanExecuteChanged();
        }

        private void SletBlomst()
        {
            OC_blomster.Remove(SelectedOrdreBlomst);
            SletSelectedBlomst.RaiseCanExecuteChanged();
        }

        private bool canDeleteBlomsterListe()
        {
            return OC_blomster.Count > 0;
        }


        /// <summary>
        /// Giver mig Jsonformat for OC_blomster object
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            string json = JsonConvert.SerializeObject(OC_blomster);
            return json;
        }



        /// <summary>
        /// Gemmer json data fra liste i localfolder
        /// </summary>
        public async void GemDataTilDiskAsync()
        {
            this.jsonText = GetJson();
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, this.jsonText);
        }


        /// <summary>
        /// Henter data fra localfolder
        /// Der skrives en fejlmeddelse i en 
        /// messageDialog hvis filen ikke findes
        /// </summary>
        public async void HentdataFraDiskAsync()
        {
            try
            {
                StorageFile file = await localfolder.GetFileAsync(filnavn);
                string jsonText = await FileIO.ReadTextAsync(file);
                this.OC_blomster.Clear();
                //.IndsætJson(jsonText);
            }
            catch (Exception)
            {
                MessageDialog messageDialog = new MessageDialog("Ændret filnavn eller har du ikke gemt ?", "File not found");
                await messageDialog.ShowAsync();
            }

        }


    }
}
