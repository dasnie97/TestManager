namespace TestManager.Extensions
{
    public static class ExtensionMethods
    {
        public static TimeSpan Average(this IEnumerable<TimeSpan?> spans)
        {
            int count = 0;
            double sum = 0;
            foreach (var span in spans)
            {
                if (span == null) continue;
                TimeSpan timeSpan = (TimeSpan)span;
                count+= 1;
                sum += timeSpan.TotalSeconds;
            }
            return TimeSpan.FromSeconds(sum/count);
        }
    }
}
