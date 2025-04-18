namespace CISP.UI
{
	class Game
	{	// parts of the turn, enumerations
		public enum Parts { Start, Begin, Main, Combat, Again, End, Defender }
		public enum Steps { Deal, Mullagin, Untap, Upkeep, Draw, Main, Begin, Attackers, Blockers, Damage, Finish, Again, End, Cleanup, Wait }

		// member attributes
		private Parts parts1 = Parts.Start;
		private Parts parts2 = Parts.Start;

		private Steps steps1 = Steps.Deal;
		private Steps steps2 = Steps.Deal;
	
		private bool who1 = false;
		private bool who2 = false;

        private string turn = "";

		// default constructor
		public Game()
		{
		}

		// overloaded constructor
		public Game(bool who1, bool who2)
		{
			this.who1 = who1;
			this.who2 = who2;

			// if (who1) { parts2 = Parts.Defender; steps2 = Steps.Wait; }
			// if (who2) { parts1 = Parts.Defender; steps1 = Steps.Wait; }
		}

		// accessors and mutators
		public Parts Parts1
		{
			get { return parts1; }
			set { parts1 = value; }
		}
		public Parts Parts2
		{
			get { return parts2; }
			set { parts2 = value; }
		}
		public Steps Steps1
		{
			get { return steps1; }
			set { steps1 = value; }
		}
		public Steps Steps2
		{
			get { return steps2; }
			set { steps2 = value; }
		}
		public bool Who1
		{
			get { return who1; }
			set { who1 = value; }
		}
		public bool Who2
		{
			get { return who2; }
			set { who2 = value; }
		}
        public string Turn
        {
            get { return turn; }
            set { turn = value; }
        }
	}
}
