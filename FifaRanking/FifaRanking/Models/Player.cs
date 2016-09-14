using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace FifaRanking
{
	[DataContract(Name = "player")]
	public class Player
	{
		[DataMember(Name = "name")]
		public string Name { get; set; }

		[DataMember(Name = "wins")]
		public int Wins { get; set; }

		[DataMember(Name = "loses")]
		public int Loses { get; set; }

		[DataMember(Name = "draws")]
		public int Draws { get; set; }

		[DataMember(Name = "goalsScored")]
		public int GoalsScored { get; set; }

		[DataMember(Name = "goalsAgainst")]
		public int GoalsAgainst { get; set; }

		[DataMember(Name = "ranking")]
		public int? Ranking { get; set; }

		[DataMember(Name = "bestRanking")]
		public int? BestRanking { get; set; }

		[DataMember(Name = "straightWins")]
		public int StraightWins { get; set; }

		[DataMember(Name = "maxStraightWins")]
		public int MaxStraightWins { get; set; }

		[JsonIgnore]
		public int GoalsDifference 
		{ 
			get 
			{
				return GoalsScored - GoalsAgainst;
			}
		}

		[JsonIgnore]
		public bool HasChanged { get; set; }
	}
}

