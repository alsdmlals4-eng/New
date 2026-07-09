using UnityEngine;

namespace MyLittleBoat
{
    public static class MyLittleBoatSession
    {
        public static GameMood SelectedMood = GameMood.Calm;
        public static readonly AlbumData Album = new AlbumData();

        private static float companionAffection;

        public static int ShellCount { get; private set; }
        public static int CompanionLevel { get; private set; } = 1;

        public static void StartNewVoyage(GameMood mood)
        {
            SelectedMood = mood;
            companionAffection = 0f;
            CompanionLevel = 1;
            ShellCount = 0;
        }

        public static void AddShells(int amount)
        {
            ShellCount = Mathf.Max(0, ShellCount + amount);
        }

        public static void AddCompanionAffection(float amount)
        {
            companionAffection += Mathf.Max(0f, amount);
            CompanionLevel = Mathf.Clamp(1 + Mathf.FloorToInt(companionAffection / 2.5f), 1, 3);
        }
    }
}
