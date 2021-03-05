using System;

namespace Shortest_Path.Models
{
    public class JourneyTime
    {
        private Options _options;
        private bool _morningPeak;
        private bool _eveningPeak;
        private bool _night;
        private bool _isWeekEnd;

        public JourneyTime(Options options)
        {
            _options = options;
        }

        public bool IsDisabled()
        {
            return _options.StartTime == DateTime.MinValue;
        }

        public bool IsPeak()
        {
            _morningPeak = _options.StartTime.TimeOfDay >= new TimeSpan(6, 0, 0) && _options.StartTime.TimeOfDay <= new TimeSpan(9, 0, 0);
            _eveningPeak = _options.StartTime.TimeOfDay >= new TimeSpan(18, 0, 0) && _options.StartTime.TimeOfDay <= new TimeSpan(21, 0, 0);
            _isWeekEnd = _options.StartTime.DayOfWeek == DayOfWeek.Sunday || _options.StartTime.DayOfWeek == DayOfWeek.Saturday;
            return (_morningPeak || _eveningPeak) && !_isWeekEnd;
        }

        public bool IsNight()
        {
            _night = _options.StartTime.TimeOfDay >= new TimeSpan(22, 0, 0) && _options.StartTime.TimeOfDay <= new TimeSpan(23, 59, 0) ||
                     _options.StartTime.TimeOfDay >= new TimeSpan(00, 00, 00) && _options.StartTime.TimeOfDay <= new TimeSpan(6, 0, 0);
            return _night;
        }

        public bool IsNonPeak()
        {
            return !IsPeak();
        }
    }
}