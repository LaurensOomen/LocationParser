﻿using LocationParser.Current;
using LocationParser.Models.Internal;
using ConsoleAppBase;
using System.Linq;
using System;

namespace LocationParser.Commands.Selections
{
	[Command("accuracy", Description = "Select all entries between a certain accuracy range")]
	class AccuracyCommand : FilterCommand
	{
		[CommandArgument(1, Name = "minimum", Description = "The minimum value of accuracy", Required = true)]
		public int Minimum { get; set; }

		[CommandArgument(2, Name = "maximum", Description = "The maximum value of accuracy", Required = true)]
		public int Maximum { get; set; }

		public override int OnExecute()
		{
			var timeLine = CurrentFile.Read();
			switch (Type)
			{
				case SelectionType.INCLUDE:
					CurrentFile.Write(new TimeLine()
					{
						timeEntries = timeLine.timeEntries?.Where(e => e.accuracy >= Minimum && e.accuracy <= Maximum)
					});
					break;
				case SelectionType.EXCLUDE:
					CurrentFile.Write(new TimeLine()
					{
						timeEntries = timeLine.timeEntries?.Where(e => !(e.accuracy >= Minimum) && !(e.accuracy <= Maximum))
					});
					break;
				default:
					Console.WriteLine(HelpText);
					break;
			}
			return 0;
		}
	}
}
