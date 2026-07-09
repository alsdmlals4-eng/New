using UnityEngine;

namespace MyLittleBoat
{
    public enum GameMood
    {
        Calm,
        Tired,
        Lonely,
        Excited
    }

    public enum TimePeriod
    {
        MorningSea,
        NightSea
    }

    public enum DriftSpeed
    {
        Slow,
        Normal,
        Fast
    }

    public static class MoodUtility
    {
        public static string GetKoreanName(GameMood mood)
        {
            switch (mood)
            {
                case GameMood.Tired:
                    return "지침";
                case GameMood.Lonely:
                    return "외로움";
                case GameMood.Excited:
                    return "설렘";
                default:
                    return "평온";
            }
        }

        public static string GetMoodDescription(GameMood mood)
        {
            switch (mood)
            {
                case GameMood.Tired:
                    return "느린 비, 낮은 파도, 쉬어갈 수 있는 바다";
                case GameMood.Lonely:
                    return "밤빛, 등대, 조용한 편지가 어울리는 바다";
                case GameMood.Excited:
                    return "노을빛, 물고기 떼, 따뜻한 우연이 많은 바다";
                default:
                    return "맑은 빛, 갈매기, 잔잔한 수평선의 바다";
            }
        }

        public static Color GetSeaColor(GameMood mood, TimePeriod period)
        {
            if (period == TimePeriod.NightSea)
            {
                return new Color(0.12f, 0.24f, 0.42f);
            }

            switch (mood)
            {
                case GameMood.Tired:
                    return new Color(0.45f, 0.58f, 0.78f);
                case GameMood.Lonely:
                    return new Color(0.33f, 0.47f, 0.66f);
                case GameMood.Excited:
                    return new Color(0.44f, 0.74f, 0.80f);
                default:
                    return new Color(0.42f, 0.67f, 0.84f);
            }
        }

        public static Color GetSkyColor(GameMood mood, TimePeriod period)
        {
            if (period == TimePeriod.NightSea)
            {
                return new Color(0.07f, 0.10f, 0.22f);
            }

            switch (mood)
            {
                case GameMood.Tired:
                    return new Color(0.72f, 0.74f, 0.90f);
                case GameMood.Lonely:
                    return new Color(0.60f, 0.69f, 0.88f);
                case GameMood.Excited:
                    return new Color(1.00f, 0.73f, 0.58f);
                default:
                    return new Color(0.74f, 0.95f, 0.92f);
            }
        }
    }
}
