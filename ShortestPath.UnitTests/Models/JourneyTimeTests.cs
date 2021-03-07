using System;
using NUnit.Framework;
using Shortest_Path.Models;

namespace ShortestPath.UnitTests.Models
{
    class JourneyTimeTests
    {
        //Peak hours (6am-9am and 6pm-9pm on Mon-Fri)
        [TestCase(2021, 3, 5, 5, 59, 59, false, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 6, 0, 0, true, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 9, 0, 0, true, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 9, 0, 1, false, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 17, 59, 59, false, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 18, 0, 0, true, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 21, 0, 0, true, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 21, 0, 1, false, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 6, 6, 0, 0, false, TestName = "Saturday Any Time")]
        [TestCase(2021, 3, 7, 6, 0, 0, false, TestName = "Sunday Any Time")]

        public void IsPeak_Returns_True_On_WeekDays_Between_PeakHours(int year, int month, int date, int hours, int minute, int second, bool expected)
        {
            var startTime = new DateTime(year, month, date, hours, minute, second);
            Assert.AreEqual(expected, new JourneyTime(new InputOption { StartTime = startTime }).IsPeak());
        }

        //Night hours (10pm-6am on Mon-Sun)
        [TestCase(2021, 3, 5, 21, 59, 59, false, TestName = "Non Night Hours")]
        [TestCase(2021, 3, 5, 22, 00, 00, true, TestName = "Any Night Hours")]
        [TestCase(2021, 3, 5, 06, 00, 00, true, TestName = "Any Night Hours")]
        [TestCase(2021, 3, 5, 06, 00, 01, false, TestName = "Non Night Hours")]
        public void IsNight_Returns_True_On_NightHours(int year, int month, int date, int hours, int minute, int second, bool expected)
        {
            var startTime = new DateTime(year, month, date, hours, minute, second);
            Assert.AreEqual(expected, new JourneyTime(new InputOption { StartTime = startTime }).IsNight());
        }

        //Non-Peak hours (all other times)
        [TestCase(2021, 3, 5, 5, 59, 59, true, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 6, 0, 0, false, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 9, 0, 0, false, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 9, 0, 1, true, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 17, 59, 59, true, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 5, 18, 0, 0, false, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 21, 0, 0, false, TestName = "WeekDay Peak")]
        [TestCase(2021, 3, 5, 21, 0, 1, true, TestName = "WeekDay Non Peak")]
        [TestCase(2021, 3, 6, 6, 0, 0, true, TestName = "Saturday Any Time")]
        [TestCase(2021, 3, 7, 6, 0, 0, true, TestName = "Sunday Any Time")]

        public void IsNonPeak_Returns_True_On_WeekDays_Between_PeakHours(int year, int month, int date, int hours, int minute, int second, bool expected)
        {
            var startTime = new DateTime(year, month, date, hours, minute, second);
            Assert.AreEqual(expected, new JourneyTime(new InputOption { StartTime = startTime }).IsNonPeak());
        }

        [TestCase(2021, 3, 5, 21, 30, 0, true, TestName = "WeekDay Non Peak Before Night")]
        [TestCase(2021, 3, 5, 6, 0, 0, false, TestName = "WeekDay Peak")]
        public void IsNonPeakBeforeNight_Returns_True_After_Peak_Hour_And_Before_Night(int year, int month, int date, int hours, int minute, int second, bool expected)
        {
            var startTime = new DateTime(year, month, date, hours, minute, second);
            Assert.AreEqual(expected, new JourneyTime(new InputOption { StartTime = startTime }).IsNonPeakBeforeNight());
        }
    }
}
