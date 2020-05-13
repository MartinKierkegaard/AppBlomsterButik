using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace AppBlomsterButik
{
    public class PersistencyService
    {
        private static readonly string filnavn = "blomster1.json";
        
        /// <summary>
        /// Gemmer json data fra liste i localfolder
        /// </summary>
        public static async Task GemDataTilDiskAsyncPS(ObservableCollection<OrdreBlomst> oc_blomst)
        {
            string jsonText = GetJsonPS(oc_blomst);
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.CreateFileAsync(filnavn, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonText);
        }

        /// <summary>
        /// Giver mig Jsonformat for OC_blomster object
        /// </summary>
        /// <returns></returns>
        public static string GetJsonPS(ObservableCollection<OrdreBlomst> oc_blomst)
        {
            string json = JsonConvert.SerializeObject(oc_blomst);
            return json;
        }


        /// <summary>
        /// metode som modtager en string af json og deserialiserer til objekter af OrdreBlomst
        /// </summary>
        /// <param name="jsonText"></param>
        private static List<OrdreBlomst> DeserialiserJson(string jsonText)
        {
            List<OrdreBlomst> nyListe = JsonConvert.DeserializeObject<List<OrdreBlomst>>(jsonText);
            return nyListe;
        }

        /// <summary>
        /// Henter en json fil fra disken 
        /// </summary>
        public static async Task<List<OrdreBlomst>> HentDataFraDiskAsync()
        {
            StorageFolder localfolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localfolder.GetFileAsync(filnavn);
            string jsonText = await FileIO.ReadTextAsync(file);
            List<OrdreBlomst> list = new List<OrdreBlomst>();
            list = DeserialiserJson(jsonText);

            return list;
        }

    }
}
