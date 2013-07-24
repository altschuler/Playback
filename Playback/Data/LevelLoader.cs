using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Playback.Data.Definition;
using Playback.Data.Element;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Playback.Data
{
    public class LevelLoader
    {
        public async static Task<Level> LoadFromFile(string fileName)
        {
            var text = await ReadFile(fileName);

            dynamic json = JsonConvert.DeserializeObject(text);

            var worldDef = WorldDefinition.Parse(json);

            return LevelParser.ParseWorldDefinition(worldDef);
        }

        public static async Task<string> ReadFile(string fileName)
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Content");
            folder = await folder.GetFolderAsync("levels");
            var storageFile = await folder.GetFileAsync("testlevel.json");
            var configData = await FileIO.ReadTextAsync(storageFile);

            return configData;
        }
    }
}