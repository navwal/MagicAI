namespace CISP.AI
{
    public class Card
    {
        #region attributes
        // attributes
		private string idx;
        private string name;
        private string type;
        private string color;
        private int mananocol;
        private int manacol;
        private int power;
        private int toughness;
        private string keyword;
        private bool sick;
        #endregion

        #region constructors
        // default constructors
        public Card()
        {
			this.idx = "";
            this.name = "";
            this.type = "";
            this.color = "";
            this.mananocol = -1;
            this.manacol = -1;
            this.power = -1;
            this.toughness = -1;
            this.keyword = "";
            this.sick = true;
        }

        // overloaded constructor
        public Card(string idx, string name, string type, string color, int mananocol, int manacol, int power, int toughness, string keyword, bool sick)
        {
			this.idx = idx;
            this.name = name;
            this.type = type;
            this.color = color;
            this.mananocol = mananocol;
            this.manacol = manacol;
            this.power = power;
            this.toughness = toughness;
            this.keyword = keyword;
            this.sick = sick;
        }
        #endregion

        #region accessors and mutators
		// index attribute accessor and mutator
		public string Idx
		{
			get { return idx; }
			set { idx = value; }
		}

		// name attribute accessor and mutator
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        // type attribute accessor and mutator
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        // color attribute accessor and mutator
        public string Color
        {
            get { return color; }
            set { color = value; }
        }

        // mana cost attribute accessor and mutator
        public int Mananocol
        {
            get { return mananocol; }
            set { mananocol = value; }
        }

        // colored mana cost
        public int Manacol
        {
            get { return manacol; }
            set { manacol = value; }
        }
        // Power
        public int Power
        {
            get { return power; }
            set { power = value; }
        }
        // Toughness
        public int Toughness
        {
            get { return toughness; }
            set { toughness = value; }
        }
        // Keyword
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }
        // summoning sickness
        public bool Sick
        {
            get { return sick; }
            set { sick = value; }
        }
        #endregion

        #region overridden ToString() method
        public override string ToString()
        {
            return Name;
        }
        #endregion
    }
}
