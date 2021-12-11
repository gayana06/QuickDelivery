using System;

namespace QuickDelivery.Extensions
{
    public static class Extensions
    {
        //Took this from https://stackoverflow.com/questions/7611402/how-to-get-the-date-of-the-next-sunday

        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek;
            if (target <= start)
                target += 7;
            return from.AddDays(target - start);
        }
    }
}
