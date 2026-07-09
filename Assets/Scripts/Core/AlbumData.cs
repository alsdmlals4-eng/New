using System.Collections.Generic;

namespace MyLittleBoat
{
    public class AlbumData
    {
        public readonly List<string> Photos = new List<string>();
        public readonly List<string> Scenery = new List<string>();
        public readonly List<string> Letters = new List<string>();
        public readonly List<string> MoodRecords = new List<string>();

        public void AddPhoto(string text)
        {
            Photos.Insert(0, text);
        }

        public void AddScenery(string text)
        {
            Scenery.Insert(0, text);
        }

        public void AddLetter(string text)
        {
            Letters.Insert(0, text);
        }

        public void AddMoodRecord(string text)
        {
            MoodRecords.Insert(0, text);
        }
    }
}
