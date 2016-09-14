using System;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace FifaRanking
{
	[DataContract(Name = "game")]
	public class Game
	{
		[DataMember(Name = "team1player1")]
		public string Team1Player1 { get; set; }

		[DataMember(Name = "team1player2")]
		public string Team1Player2 { get; set; }

		[DataMember(Name = "team2player1")]
		public string Team2Player1 { get; set; }

		[DataMember(Name = "team2player2")]
		public string Team2Player2 { get; set; }

		[DataMember(Name = "team1goals")]
		public int Team1Goals { get; set; }

		[DataMember(Name = "team2goals")]
		public int Team2Goals { get; set; }

		[DataMember(Name = "date")]
		public DateTime Date { get; set; }
	}
}

