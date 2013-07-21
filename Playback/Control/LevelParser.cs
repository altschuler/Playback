using System;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Playback.Control;
using Playback.Data.Definition;
using Windows.ApplicationModel;
using Windows.Storage;

namespace Playback.Data
{
    public class LevelParser
    {
        public async static Task<Level> Parse(string fileName)
        {
            var text = await ReadFile(fileName);

            dynamic json = JsonConvert.DeserializeObject(text);

            var worldDef = WorldDefinition.Parse(json);

            return Level.ParseWorldDefinition(worldDef);
        }

        public static async Task<string> ReadFile(string fileName)
        {
            var folder = await Package.Current.InstalledLocation.GetFolderAsync("Content");
            folder = await folder.GetFolderAsync("levels");
            var storageFile = await folder.GetFileAsync("testlevel.json");
            var configData = await FileIO.ReadTextAsync(storageFile);

            return configData;
        }

        public static Vector2 ParsePosition(dynamic model)
        {
            try
            {
                return new Vector2((float)model);
            }
            catch
            {
                return new Vector2((float)model.x, -(float)model.y);
            }
        }
    }
}