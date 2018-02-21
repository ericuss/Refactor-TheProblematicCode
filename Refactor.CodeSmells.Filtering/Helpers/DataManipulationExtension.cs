namespace Refactor.CodeSmells.Filtering.Helpers
{
    using System;

    public static class DataManipulationExtension
    {
        public static DateTime ChangeTime(this DateTime self, int hours, int minutes, int seconds = 0, int milliseconds = 0)
        {
            return new DateTime(self.Year, self.Month, self.Day, hours, minutes, seconds, milliseconds, self.Kind);
        }

        public static DateTime SetTimeToMin(this DateTime dateTime)
        {
            return dateTime.ChangeTime(0, 0, 0, 1);
        }

        public static DateTime SetTimeToMax(this DateTime dateTime)
        {
            return dateTime.ChangeTime(23, 59, 59, 999);
        }

        public static bool IsLesserOrEqualThan(this DateTime dateTimeA, DateTime datetimeB)
        {
            return dateTimeA <= datetimeB;
        }

        public static bool IsLesserOrEqualThan(this DateTime? dateTimeA, DateTime? datetimeB)
        {
            return dateTimeA.HasValue && datetimeB.HasValue && dateTimeA.Value.IsLesserOrEqualThan(datetimeB.Value);
        }

        public static bool IsGreatterOrEqualThan(this DateTime dateTimeA, DateTime datetimeB)
        {
            return dateTimeA >= datetimeB;
        }

        public static bool IsGreatterOrEqualThan(this DateTime? dateTimeA, DateTime? datetimeB)
        {
            return dateTimeA.HasValue && datetimeB.HasValue && dateTimeA.Value.IsGreatterOrEqualThan(datetimeB.Value);
        }
    }
}
