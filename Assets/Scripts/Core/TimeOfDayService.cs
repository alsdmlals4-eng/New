using System;
using UnityEngine;

namespace MyLittleBoat
{
    public static class TimeOfDayService
    {
        public static TimePeriod GetCurrentPeriod()
        {
            int hour = DateTime.Now.Hour;
            return hour >= 6 && hour < 18 ? TimePeriod.MorningSea : TimePeriod.NightSea;
        }

        public static string GetKoreanName(TimePeriod period)
        {
            return period == TimePeriod.MorningSea ? "오전 바다" : "밤바다";
        }

        public static void ApplyCameraBackground(Camera camera, GameMood mood)
        {
            if (camera == null)
            {
                return;
            }

            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = MoodUtility.GetSkyColor(mood, GetCurrentPeriod());
        }
    }
}
