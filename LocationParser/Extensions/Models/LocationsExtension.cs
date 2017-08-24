﻿using LocationParser.Models.Google;
using LocationParser.Models.Internal;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LocationParser.Extensions.Models
{
	public static class LocationsExtension
	{
		public static TimeLine ToTimeLine(this Locations locations) => new TimeLine()
		{
			timeEntries = locations.locations.Select(l => l.ToTimeEntry())
		};

		public static TimeEntry ToTimeEntry(this Location location) => new TimeEntry()
		{
			timestamp = new DateTime(1970, 1, 1).AddMilliseconds(Convert.ToInt32(location.timestampMS)),
			coordinate = new Coordinate()
			{
				latitude = Convert.ToDouble(location.latitudeE7),
				longitude = Convert.ToDouble(location.latitudeE7)
			},
			altitude = Convert.ToInt32(location.altitude),
			accuracy = Convert.ToInt32(location.accuracy),
			movements = location.activity.First().ToMovements()
		};

		public static IEnumerable<Movement> ToMovements(this Activity activity) =>
			activity.activity.Select(a =>
			new Movement()
			{
				timestamp = new DateTime().FromEpoch(activity.timestampMS),
				confidence = Convert.ToInt32(a.confidence),
				movementType = (MovementType)Enum.Parse(typeof(MovementType), a.type)
			});
	}
}
