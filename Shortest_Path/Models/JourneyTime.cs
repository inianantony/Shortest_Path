using System;

namespace Shortest_Path.Models
{
    public class JourneyTime
    {
        private readonly InputOption _inputOption;
        private bool _morningPeak;
        private bool _eveningPeak;
        private bool _night;
        private bool _isWeekEnd;

        public JourneyTime(InputOption inputOption)
        {
            _inputOption = inputOption;
        }

        public bool IsDisabled()
        {
            return _inputOption.StartTime == DateTime.MinValue;
        }

        public bool IsPeak()
        {
            _morningPeak = _inputOption.StartTime.TimeOfDay >= new TimeSpan(6, 0, 0) && _inputOption.StartTime.TimeOfDay <= new TimeSpan(9, 0, 0);
            _eveningPeak = _inputOption.StartTime.TimeOfDay >= new TimeSpan(18, 0, 0) && _inputOption.StartTime.TimeOfDay <= new TimeSpan(21, 0, 0);
            _isWeekEnd = _inputOption.StartTime.DayOfWeek == DayOfWeek.Sunday || _inputOption.StartTime.DayOfWeek == DayOfWeek.Saturday;
            return (_morningPeak || _eveningPeak) && !_isWeekEnd;
        }

        public bool IsNight()
        {
            _night = _inputOption.StartTime.TimeOfDay >= new TimeSpan(22, 0, 0) && _inputOption.StartTime.TimeOfDay <= new TimeSpan(23, 59, 0) ||
                     _inputOption.StartTime.TimeOfDay >= new TimeSpan(00, 00, 00) && _inputOption.StartTime.TimeOfDay <= new TimeSpan(6, 0, 0);
            return _night;
        }

        public bool IsNonPeak()
        {
            return !IsPeak();
        }

        public bool IsNonPeakBeforeNight()
        {
            return IsNonPeak() && !IsNight();
        }
    }
}