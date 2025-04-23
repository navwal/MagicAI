using System;
using System.Windows.Forms;
using System.Collections.Generic;

using CISP.AI;
using System.Linq;

namespace CISP.UI
{
    public partial class Gameplay : Form
    {
        GameEngine gameEngine = new GameEngine();
        public Gameplay()
        {
            InitializeComponent();
        }

        #region button click events
        // draw a card for myself
        private void button1_Click(object sender, EventArgs e)
        {   // do nothing is selected item equals null
            if (listBox1.SelectedItem != null)
            {   // add the selected card into the hand
                listBox2.Items.Add(listBox1.SelectedItem);
            }
        }

        // draw a card for my opponent
        private void button2_Click(object sender, EventArgs e)
        {   // do nothing is selected item equals null
            if (listBox3.SelectedItem != null)
            {   // add the selected card into the hand
                listBox4.Items.Add(listBox3.SelectedItem);
            }
        }

		// we drew the wrong card, remove it from our hand
		private void button3_Click(object sender, EventArgs e)
		{	// remove the selected card
			listBox2.Items.Remove(listBox2.SelectedItem);
		}

		// they drew the wrong card, remove it from our hand
		private void button4_Click(object sender, EventArgs e)
		{	// remove the selected card
			listBox4.Items.Remove(listBox4.SelectedItem);
		}

		private void button12_Click(object sender, EventArgs e)
		{	// clear the text out of the rich text box
			richTextBox1.Text = "";
		}
        #endregion

		#region selected index changed events
		// handle the selcted index change, display class attributes and proper image
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {	// this if block prevents an error, when listbox is empty
			if (listBox2.SelectedItem != null)
			{	// create a card to hold selected card
				Card a = (Card)listBox2.SelectedItem;

				// display the card attribute values
				label6.Text = a.Name;
				label8.Text = a.Type;
				label10.Text = a.Color;

                // if group, to display proper image
                if (a.Idx == "for") { pictureBox1.Image = Properties.Resources.forest; }
                if (a.Idx == "pou") { pictureBox1.Image = Properties.Resources.pouncing_jaguar; }
                if (a.Idx == "mul") { pictureBox1.Image = Properties.Resources.multanis_acolyte; }
                if (a.Idx == "acr") { pictureBox1.Image = Properties.Resources.acridian; }
                if (a.Idx == "gor") { pictureBox1.Image = Properties.Resources.gorilla_warrior; }
                if (a.Idx == "gra") { pictureBox1.Image = Properties.Resources.yavimaya_granger; }
                if (a.Idx == "sci") { pictureBox1.Image = Properties.Resources.yavimaya_scion; }
                if (a.Idx == "swa") { pictureBox1.Image = Properties.Resources.swamp; }
                if (a.Idx == "exp") { pictureBox1.Image = Properties.Resources.expunge; }
                if (a.Idx == "swt") { pictureBox1.Image = Properties.Resources.swat; }
                if (a.Idx == "bef") { pictureBox1.Image = Properties.Resources.befoul; }
                if (a.Idx == "cor") { pictureBox1.Image = Properties.Resources.corrupt; }
                if (a.Idx == "phy") { pictureBox1.Image = Properties.Resources.phyrexian_debaser; }
                if (a.Idx == "gia") { pictureBox1.Image = Properties.Resources.giant_cockroach; }
                if (a.Idx == "hol") { pictureBox1.Image = Properties.Resources.hollow_dogs; }
                if (a.Idx == "sym") { pictureBox1.Image = Properties.Resources.symbiosis; }
                if (a.Idx == "bla") { pictureBox1.Image = Properties.Resources.blanchwood_treefolk; }
                if (a.Idx == "win") { pictureBox1.Image = Properties.Resources.winding_wurm; }
                if (a.Idx == "mou") { pictureBox1.Image = Properties.Resources.mountain; }
                if (a.Idx == "fie") { pictureBox1.Image = Properties.Resources.fiery_mantle; }
                if (a.Idx == "sho") { pictureBox1.Image = Properties.Resources.shower_of_sparks; }
                if (a.Idx == "hea") { pictureBox1.Image = Properties.Resources.heat_ray; }
                if (a.Idx == "arc") { pictureBox1.Image = Properties.Resources.arc_lightning; }
                if (a.Idx == "pat") { pictureBox1.Image = Properties.Resources.goblin_patrol; }
                if (a.Idx == "bug") { pictureBox1.Image = Properties.Resources.goblin_war_buggy; }
                if (a.Idx == "rai") { pictureBox1.Image = Properties.Resources.goblin_raider; }
                if (a.Idx == "via") { pictureBox1.Image = Properties.Resources.viashino_outrider; }
			}
			else
			{ 	// display the card attribute values
				label6.Text = "-";
				label8.Text = "-";
				label10.Text = "-";

				// display the card back image
				pictureBox1.Image = Properties.Resources.card_back;
			}
        }

		// handle the selcted index change, display class attributes and proper image
		private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
		{	// this if block prevents an error, when listbox is empty
			if (listBox4.SelectedItem != null)
			{	// create a card to hold selected card
				Card a = (Card)listBox4.SelectedItem;

				// display the card attribute values
				label15.Text = a.Name;
				label13.Text = a.Type;
				label11.Text = a.Color;

				// if group, to display proper image
				if (a.Idx == "for") { pictureBox2.Image = Properties.Resources.forest; }
				if (a.Idx == "pou") { pictureBox2.Image = Properties.Resources.pouncing_jaguar; }
				if (a.Idx == "mul") { pictureBox2.Image = Properties.Resources.multanis_acolyte; }
				if (a.Idx == "acr") { pictureBox2.Image = Properties.Resources.acridian; }
				if (a.Idx == "gor") { pictureBox2.Image = Properties.Resources.gorilla_warrior; }
				if (a.Idx == "gra") { pictureBox2.Image = Properties.Resources.yavimaya_granger; }
				if (a.Idx == "sci") { pictureBox2.Image = Properties.Resources.yavimaya_scion; }
				if (a.Idx == "swa") { pictureBox2.Image = Properties.Resources.swamp; }
				if (a.Idx == "exp") { pictureBox2.Image = Properties.Resources.expunge; }
				if (a.Idx == "swt") { pictureBox2.Image = Properties.Resources.swat; }
				if (a.Idx == "bef") { pictureBox2.Image = Properties.Resources.befoul; }
				if (a.Idx == "cor") { pictureBox2.Image = Properties.Resources.corrupt; }
				if (a.Idx == "phy") { pictureBox2.Image = Properties.Resources.phyrexian_debaser; }
				if (a.Idx == "gia") { pictureBox2.Image = Properties.Resources.giant_cockroach; }
				if (a.Idx == "hol") { pictureBox2.Image = Properties.Resources.hollow_dogs; }
				if (a.Idx == "sym") { pictureBox2.Image = Properties.Resources.symbiosis; }
				if (a.Idx == "bla") { pictureBox2.Image = Properties.Resources.blanchwood_treefolk; }
				if (a.Idx == "win") { pictureBox2.Image = Properties.Resources.winding_wurm; }
				if (a.Idx == "mou") { pictureBox2.Image = Properties.Resources.mountain; }
				if (a.Idx == "fie") { pictureBox2.Image = Properties.Resources.fiery_mantle; }
				if (a.Idx == "sho") { pictureBox2.Image = Properties.Resources.shower_of_sparks; }
				if (a.Idx == "hea") { pictureBox2.Image = Properties.Resources.heat_ray; }
				if (a.Idx == "arc") { pictureBox2.Image = Properties.Resources.arc_lightning; }
				if (a.Idx == "pat") { pictureBox2.Image = Properties.Resources.goblin_patrol; }
				if (a.Idx == "bug") { pictureBox2.Image = Properties.Resources.goblin_war_buggy; }
				if (a.Idx == "rai") { pictureBox2.Image = Properties.Resources.goblin_raider; }
				if (a.Idx == "via") { pictureBox2.Image = Properties.Resources.viashino_outrider; }
			}
			else
			{ 	// display the card attribute values
				label15.Text = "-";
				label13.Text = "-";
				label11.Text = "-";

				// display the card back image
				pictureBox2.Image = Properties.Resources.card_back;
			}
		}
		#endregion

		private void Form1_Load(object sender, EventArgs e)
		{
            #region handle start form events
            Form sf = new Start();
			DialogResult dr = sf.ShowDialog();

			bool x = false;
			bool y = false;

			if (dr == DialogResult.OK) { x = true; y = false; }
			if (dr == DialogResult.Cancel) { x = false; y = true; }

			Game g = new Game(x, y);

            //connect this to the partnums for the steps/phases
			if (dr == DialogResult.OK) { g.Turn = "Us"; }
			if (dr == DialogResult.Cancel) { g.Turn = "Them"; }
			#endregion

			#region set the game state
			textBox3.Text = g.Parts1.ToString();
			textBox1.Text = g.Steps1.ToString();

			textBox4.Text = g.Parts2.ToString();
			textBox2.Text = g.Steps2.ToString();

			
			#endregion

			#region load up the card decks
			// create the card deck arrays
			Card[] c = new Card[51];    // all cards
			Card[] d = new Card[15];    // black/green
			Card[] f = new Card[14];    // green/red

			// all cards, from both decks
            // Alphabetize
			c[0] = new Card("for", "Forest", "Land", "Green", -1, -1, -1, -1, "Land", false);
			c[1] = new Card("sym", "Symbiosis", "Instant", "Green", 1, 1, 2, 2, "Buff", false);
			c[2] = new Card("pou", "Pouncing Jaguar (1)", "Summon", "Green", 0, 1, 2, 2, "Echo", true);
            c[3] = new Card("pou", "Pouncing Jaguar (2)", "Summon", "Green", 0, 1, 2, 2, "Echo", true);
            c[4] = new Card("pou", "Pouncing Jaguar (3)", "Summon", "Green", 0, 1, 2, 2, "Echo", true);
            c[5] = new Card("pou", "Pouncing Jaguar (4)", "Summon", "Green", 0, 1, 2, 2, "Echo", true);
            c[6] = new Card("bla", "Blanchwood Treefolk", "Summon", "Green", 4, 1, 4, 5, "", true);
            c[7] = new Card("win", "Winding Wurm (1)", "Summon", "Green", 4, 1, 6, 6, "Echo", true);
            c[8] = new Card("win", "Winding Wurm (2)", "Summon", "Green", 4, 1, 6, 6, "Echo", true);
            c[9] = new Card("win", "Winding Wurm (3)", "Summon", "Green", 4, 1, 6, 6, "Echo", true);
            c[10] = new Card("win", "Winding Wurm (4)", "Summon", "Green", 4, 1, 6, 6, "Echo", true);
            c[11] = new Card("mou", "Mountain", "Land", "Red", -1, -1, -1, -1, "Land", false);
			c[12] = new Card("fie", "Fiery Mantle", "Enchant", "Red", 1, 1, 1, 0, "Mana Buff", false);
			c[13] = new Card("sho", "Shower of Sparks", "Instant", "Red", 0, 1, 1, 0, "Damage Creature and Player", false);
			c[14] = new Card("hea", "Heat Ray", "Instant", "Red", 1, 1, 1, 0, "Mana Damage Creature", false);
			c[15] = new Card("arc", "Arc Lightning", "Sorcery", "Red", 2, 1, 3, 0, "Damage Creature or Player", false);
			c[16] = new Card("pat", "Goblin Patrol (1)", "Summon", "Red", 0, 1, 2, 1, "Echo", true);
            c[17] = new Card("pat", "Goblin Patrol (2)", "Summon", "Red", 0, 1, 2, 1, "Echo", true);
            c[18] = new Card("pat", "Goblin Patrol (3)", "Summon", "Red", 0, 1, 2, 1, "Echo", true);
            c[19] = new Card("pat", "Goblin Patrol (4)", "Summon", "Red", 0, 1, 2, 1, "Echo", true);
            c[20] = new Card("bug", "Goblin War Buggy (1)", "Summon", "Red", 1, 1, 2, 2, "Echo", false);
            c[21] = new Card("bug", "Goblin War Buggy (2)", "Summon", "Red", 1, 1, 2, 2, "Echo", false);
            c[22] = new Card("bug", "Goblin War Buggy (3)", "Summon", "Red", 1, 1, 2, 2, "Echo", false);
            c[23] = new Card("bug", "Goblin War Buggy (4)", "Summon", "Red", 1, 1, 2, 2, "Echo", false);
            c[24] = new Card("rai", "Goblin Raider", "Summon", "Red", 1, 1, 2, 2, "Cannot Block", true);
			c[25] = new Card("via", "Viashino Outrider (1)", "Summon", "Red", 2, 1, 4, 3, "Echo", true);
            c[26] = new Card("via", "Viashino Outrider (2)", "Summon", "Red", 2, 1, 4, 3, "Echo", true);
            c[27] = new Card("via", "Viashino Outrider (3)", "Summon", "Red", 2, 1, 4, 3, "Echo", true);
            c[28] = new Card("via", "Viashino Outrider (4)", "Summon", "Red", 2, 1, 4, 3, "Echo", true);
            c[29] = new Card("mul", "Multani's Acolyte (1)", "Summon", "Green", 0, 2, 2, 1, "Echo", true);
            c[30] = new Card("mul", "Multani's Acolyte (2)", "Summon", "Green", 0, 2, 2, 1, "Echo", true);
            c[31] = new Card("mul", "Multani's Acolyte (3)", "Summon", "Green", 0, 2, 2, 1, "Echo", true);
            c[32] = new Card("mul", "Multani's Acolyte (4)", "Summon", "Green", 0, 2, 2, 1, "Echo", true);
            c[33] = new Card("acr", "Acridian", "Summon (1)", "Green", 1, 1, 2, 4, "Echo", true);
            c[34] = new Card("acr", "Acridian", "Summon (2)", "Green", 1, 1, 2, 4, "Echo", true);
            c[35] = new Card("acr", "Acridian", "Summon (3)", "Green", 1, 1, 2, 4, "Echo", true);
            c[36] = new Card("acr", "Acridian", "Summon (4)", "Green", 1, 1, 2, 4, "Echo", true);
            c[37] = new Card("gor", "Gorilla Warrior", "Summon", "Green", 2, 1, 3, 2, "", true);
			c[38] = new Card("gra", "Yavimaya Granger (1)", "Summon", "Green", 2, 1, 2, 2, "Echo", true);
            c[39] = new Card("gra", "Yavimaya Granger (2)", "Summon", "Green", 2, 1, 2, 2, "Echo", true);
            c[40] = new Card("gra", "Yavimaya Granger (3)", "Summon", "Green", 2, 1, 2, 2, "Echo", true);
            c[41] = new Card("gra", "Yavimaya Granger (4)", "Summon", "Green", 2, 1, 2, 2, "Echo", true);
            c[42] = new Card("sci", "Yavimaya Scion", "Summon", "Green", 4, 1, 4, 4, "", true);
			c[43] = new Card("swa", "Swamp", "Land", "Black", -1, -1, -1, -1, "Land", false);
			c[44] = new Card("exp", "Expunge", "Instant", "Black", 2, 1, 0, 3, "Destroy Creature Non-Black/Cycle", false);
			c[45] = new Card("swt", "Swat", "Instant", "Black", 1, 2, 0, 2, "Destroy Creature Power 2 or Less/Cycle", false);
			c[46] = new Card("bef", "Befoul", "Sorcery", "Black", 2, 2, 0, 3, "Destroy Land or Non-Black Creature", false);
			c[47] = new Card("cor", "Corrupt", "Sorcery", "Black", 5, 1, 1, 0, "Damage Creature or Player equal to swamps", false);
			c[48] = new Card("phy", "Phyrexian Debaser", "Summon", "Black", 3, 1, 2, 2, "Flying/Tap,Sac give Creature -2/-2", true);
			c[49] = new Card("gia", "Giant Cockroach", "Summon", "Black", 3, 1, 4, 2, "", true);
			c[50] = new Card("hol", "Hollow Dogs", "Summon", "Black", 4, 1, 3, 3, "Buff power on attack by 2", true);

			// black and green deck
			/*d[0] = c[0]; d[1] = c[2]; d[2] = c[14]; d[3] = c[15];
			d[4] = c[16]; d[5] = c[17]; d[6] = c[18]; d[7] = c[19];
			d[8] = c[20]; d[9] = c[21]; d[10] = c[22]; d[11] = c[23];
			d[12] = c[24]; d[13] = c[25]; d[14] = c[26];

			// green and red deck
			f[0] = c[0]; f[1] = c[1]; f[2] = c[2]; f[3] = c[3];
			f[4] = c[4]; f[5] = c[5]; f[6] = c[6]; f[7] = c[7];
			f[8] = c[8]; f[9] = c[9]; f[10] = c[10]; f[11] = c[11];
			f[12] = c[12]; f[13] = c[13];*/
			#endregion
            
            // add button to choose the deck
			#region load up the card list boxes
			for (int i = 0; i < c.Length; i++) // My Deck, David Krogmann
			{   // add the card to the listbox
				listBox1.Items.Add(c[i]);
                listBox3.Items.Add(c[i]);
			}
            

			/*foreach (Card j in c)   // Opponents Cards
			{   // add the card to the listbox
				listBox3.Items.Add(j);
                listBox1.Items.Add(j);
			}*/

			#endregion
		}
		//advisor button
		//AI
        public void button6_Click(object sender, EventArgs e)
        {

            string parts = textBox3.Text;
            string steps = textBox1.Text;
            string osteps = textBox2.Text;

            gameEngine.GetMyUIGameState(getHand(), getBoard(), getLands(), getTapped());
            gameEngine.GetOppUIGameState(getOppBoard(), getOppLands(), getOppTapped());

            switch (parts)
            {
                case "Start":
                    if (steps == "Deal")
                    {
                        richTextBox1.Text = gameEngine.Start();
                    }
                    else if (steps == "Mulligan")
                    {
                        richTextBox1.Text = gameEngine.Mulligan();
                    }
                    break;
                case "Begin":
                    richTextBox1.Text = gameEngine.Begin(steps) + " If it is not your turn press opponent next.";
                    break;
                case "Main":
                    richTextBox1.Text = gameEngine.Main_1() + " If it is not your turn press opponent next.";
                    break;
                case "Combat":
                    if (steps == "Attackers")
                    {
                        richTextBox1.Text = gameEngine.Combat();
                    }
                    else if (steps == "Damage")
                    {
                        richTextBox1.Text = "Remove any creatures that died and press next.";
                    }
                    else if (steps == "Blockers")
                    {
                        richTextBox1.Text = gameEngine.Defend();
                    }
                    else 
                    {
                        richTextBox1.Text = "Go to next step. If it is not your turn press opponent next.";
                    }
                    break;
                case "Main Again":
                    richTextBox1.Text = gameEngine.Main_2() + " If it is not your turn press opponent next.";
                    break;
                case "End":
                    if (steps == "Wait")
                    {
                        richTextBox1.Text = gameEngine.End_opponent();
                    }
                    else
                    {
                        richTextBox1.Text = gameEngine.End();
                    }
                    break;
            }
            
        }

        List<AI.Card> getHand()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox2.Items)
            {
                list.Add(i);
            }
            return list;
        }
       List<AI.Card> getBoard()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox6.Items)
            {
                list.Add(i);
            }
            return list;
        }
        List<AI.Card> getLands()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox5.Items)
            {
                list.Add(i);
            }
            return list;
        }
        List<AI.Card> getTapped()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox7.Items)
            {
                list.Add(i);
            }
            return list;
        }
        List<AI.Card> getOppBoard()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox9.Items)
            {
                list.Add(i);
            }
            return list;
        }
        List<AI.Card> getOppLands()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox10.Items)
            {
                list.Add(i);
            }
            return list;
        }
        List<AI.Card> getOppTapped()
        {
            List<AI.Card> list = new List<AI.Card>();
            foreach (AI.Card i in listBox8.Items)
            {
                list.Add(i);
            }
            return list;
        }
        //keeps track of steps and phases
        public int partnum = 0;
		public int stepnum = 0;
		public int opartnum = 1;
		public int ostepnum = 1;

		//next button
        private void button5_Click(object sender, EventArgs e)
        {
            //add "click advisor" in advisor box
			if (partnum == 0)
			{
				textBox1.Text = Game.Steps.Mulligan.ToString();
				textBox2.Text = Game.Steps.Mulligan.ToString();
				partnum++;
				stepnum++;
			}
			//Begin
			else if(partnum == 1)
			{
				if (stepnum == 1)
				{
                    textBox3.Text = Game.Parts.Begin.ToString();
					textBox4.Text = Game.Parts.Begin.ToString();
                    textBox1.Text = Game.Steps.Untap.ToString();
					textBox2.Text = Game.Steps.Wait.ToString();
                    stepnum++;
                }
				else if (stepnum == 2)
				{
                    textBox1.Text = Game.Steps.Upkeep.ToString();
                    stepnum++;
                }
				else
				{
					textBox1.Text = Game.Steps.Draw.ToString();
					stepnum++;
					partnum++;
				}
			}
			//Main
			else if (partnum == 2)
			{
				if (stepnum == 4)
				{
                    textBox3.Text = Game.Parts.Main.ToString();
					textBox4.Text = Game.Parts.Main.ToString();
                    textBox1.Text = Game.Steps.Begin.ToString();
                    stepnum++;
                }
				else
				{
                    textBox1.Text = Game.Steps.Wait.ToString();
                    stepnum++;
					partnum++;
                }
			}
			//Combat
			else if (partnum == 3)
			{
				if (stepnum == 6)
				{
                    textBox3.Text = Game.Parts.Combat.ToString();
					textBox4.Text = Game.Parts.Combat.ToString();
                    textBox1.Text = Game.Steps.Begin.ToString();
                    stepnum++;
                }
                else if (stepnum == 7)
                {
                    textBox1.Text = Game.Steps.Attackers.ToString();
                    stepnum++;
                }
                else if (stepnum == 8)
                {
                    textBox1.Text = Game.Steps.Wait.ToString();
					textBox2.Text = Game.Steps.Blockers.ToString();
                    stepnum++;
                }
                else if (stepnum == 9)
                {
                    textBox1.Text = Game.Steps.Damage.ToString();
					textBox2.Text = Game.Steps.Wait.ToString();
                    stepnum++;
                }
				else
				{
                    textBox1.Text = Game.Steps.End.ToString();
                    stepnum++;
                    partnum++;
                }
            }
			//Second Main
			else if (partnum == 4)
			{
                if (stepnum == 11)
                {
					textBox3.Text = Game.Parts.Main.ToString() + " " + Game.Parts.Again.ToString();
                    textBox4.Text = Game.Parts.Main.ToString() + " " + Game.Parts.Again.ToString();
                    textBox1.Text = Game.Steps.Begin.ToString();
                    stepnum++;
                }
                else if (stepnum == 12)
                {
                    textBox1.Text = Game.Steps.Wait.ToString();
                    stepnum++;
                }
                else
                {
                    textBox1.Text = Game.Steps.End.ToString();
                    stepnum++;
                    partnum++;
                }
            }
			//End
			else if (partnum == 5)
			{
                if (stepnum == 14)
                {
					textBox3.Text = Game.Parts.End.ToString();
					textBox4.Text = Game.Parts.End.ToString();
                    textBox1.Text = Game.Steps.Cleanup.ToString();
                    stepnum++;
                }
                else if (stepnum == 15)
                {
                    textBox1.Text = Game.Steps.Finish.ToString();
                    stepnum++;
                }
                else
                {
                    textBox1.Text = Game.Steps.End.ToString();
                    stepnum = -1;
                    partnum = -1;
					ostepnum = 1;
					opartnum = 1;
                }
            }
			else
			{
				//message box "it is not your turn"
			}
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
		//cast spell/play land
        private void button17_Click(object sender, EventArgs e)
        {
            Card a = (Card)listBox2.SelectedItem;
            if (a != null)
            {
                if (a.Type == "Land")
                {
                    listBox5.Items.Add(listBox2.SelectedItem);
                    listBox2.Items.Remove(listBox2.SelectedItem);
                   
                    
                }
                else if (a.Type == "Summon")
                {
                    listBox6.Items.Add(listBox2.SelectedItem);
                    listBox2.Items.Remove(listBox2.SelectedItem);
                }
            }
        }
		//tap land
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox5.SelectedItem != null)
            {
                listBox7.Items.Add(listBox5.SelectedItem);
                listBox5.Items.Remove(listBox5.SelectedItem);
            }
        }
		//remove land from board
        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox5.SelectedItem != null)
            {
                listBox5.Items.Remove(listBox5.SelectedItem);
            }
        }
		//remove from board
        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox6.SelectedItem != null)
            {
                listBox6.Items.Remove(listBox6.SelectedItem);
            }
        }
		//atk button
        private void button22_Click(object sender, EventArgs e)
        {
            if (listBox6.SelectedItem != null)
            {
                listBox7.Items.Add(listBox6.SelectedItem);
                listBox6.Items.Remove(listBox6.SelectedItem);
            }
        }
		//defend button
        private void button21_Click(object sender, EventArgs e)
        {

        }
		//untap board
        private void button8_Click(object sender, EventArgs e)
        {
            Card a = (Card)listBox7.SelectedItem;
            if (a != null)
            {
                if (a.Type == "Land")
                {
                    listBox5.Items.Add(listBox7.SelectedItem);
                    listBox7.Items.Remove(listBox7.SelectedItem);
                }
                else if (a.Type == "Summon")
                {
                    listBox6.Items.Add(listBox7.SelectedItem);
                    listBox7.Items.Remove(listBox7.SelectedItem);
                }
            }
        }
		//opponent next
        private void button7_Click(object sender, EventArgs e)
        {
            //Add "click advisor" statement in advisor box
            //Begin
            if (opartnum == 1)
            {
                if (ostepnum == 1)
                {
                    textBox4.Text = Game.Parts.Begin.ToString();
                    textBox3.Text = Game.Parts.Begin.ToString();
                    textBox2.Text = Game.Steps.Untap.ToString();
                    textBox1.Text = Game.Steps.Wait.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 2)
                {
                    textBox2.Text = Game.Steps.Upkeep.ToString();
                    ostepnum++;
                }
                else
                {
                    textBox2.Text = Game.Steps.Draw.ToString();
                    ostepnum++;
                    opartnum++;
                }
            }
            //Main
            else if (opartnum == 2)
            {
                if (ostepnum == 4)
                {
                    textBox4.Text = Game.Parts.Main.ToString();
                    textBox3.Text = Game.Parts.Main.ToString();
                    textBox2.Text = Game.Steps.Begin.ToString();
                    ostepnum++;
                }
                else
                {
                    textBox2.Text = Game.Steps.Wait.ToString();
                    ostepnum++;
                    opartnum++;
                }
            }
            //Combat
            else if (opartnum == 3)
            {
                if (ostepnum == 6)
                {
                    textBox4.Text = Game.Parts.Combat.ToString();
                    textBox3.Text = Game.Parts.Combat.ToString();
                    textBox2.Text = Game.Steps.Begin.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 7)
                {
                    textBox2.Text = Game.Steps.Attackers.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 8)
                {
                    textBox2.Text = Game.Steps.Wait.ToString();
                    textBox1.Text = Game.Steps.Blockers.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 9)
                {
                    textBox2.Text = Game.Steps.Damage.ToString();
                    textBox1.Text = Game.Steps.Wait.ToString();
                    ostepnum++;
                }
                else
                {
                    textBox2.Text = Game.Steps.End.ToString();
                    ostepnum++;
                    opartnum++;
                }
            }
            //Second Main
            else if (opartnum == 4)
            {
                if (ostepnum == 11)
                {
                    textBox4.Text = Game.Parts.Main.ToString() + " " + Game.Parts.Again.ToString();
                    textBox3.Text = Game.Parts.Main.ToString() + " " + Game.Parts.Again.ToString();
                    textBox2.Text = Game.Steps.Begin.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 12)
                {
                    textBox2.Text = Game.Steps.Wait.ToString();
                    ostepnum++;
                }
                else
                {
                    textBox2.Text = Game.Steps.End.ToString();
                    ostepnum++;
                    opartnum++;
                }
            }
            //End
            else if (opartnum == 5)
            {
                if (ostepnum == 14)
                {
                    textBox4.Text = Game.Parts.End.ToString();
                    textBox3.Text = Game.Parts.End.ToString();
                    textBox2.Text = Game.Steps.Cleanup.ToString();
                    ostepnum++;
                }
                else if (ostepnum == 15)
                {
                    textBox2.Text = Game.Steps.Finish.ToString();
                    ostepnum++;
                }
                else
                {
                    textBox2.Text = Game.Steps.End.ToString();
                    ostepnum = -1;
                    opartnum = -1;
                    stepnum = 1;
                    partnum = 1;
                }
            }
            else
            {
                //Message box "it isn't your turn"
            }
        }
		// opponent cast/play land
        private void button18_Click(object sender, EventArgs e)
        {
            Card a = (Card)listBox4.SelectedItem;
            if (a != null)
            {
                if (a.Type == "Land")
                {
                    listBox10.Items.Add(listBox4.SelectedItem);
                    listBox4.Items.Remove(listBox4.SelectedItem);
                    gameEngine.myLand.Remove(a);
                }
                else if (a.Type == "Summon")
                {
                    listBox9.Items.Add(listBox4.SelectedItem);
                    listBox4.Items.Remove(listBox4.SelectedItem);
                }
            }
        }
		//opponent tap land
        private void button14_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItem != null)
            {
                listBox8.Items.Add(listBox10.SelectedItem);
                listBox10.Items.Remove(listBox10.SelectedItem);
            }
        }
		//opponent remove land
        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox10.SelectedItem != null)
            {
                listBox10.Items.Remove(listBox10.SelectedItem);
            }
        }
		//opponent remove from board
        private void button15_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItem != null)
            {
                listBox9.Items.Remove(listBox9.SelectedItem);
            }
        }
		//opponent atk button
        private void button19_Click(object sender, EventArgs e)
        {
            if (listBox9.SelectedItem != null)
            {
                listBox8.Items.Add(listBox9.SelectedItem);
                listBox9.Items.Remove(listBox9.SelectedItem);
            }
        }
		//opponent def button
        private void button20_Click(object sender, EventArgs e)
        {

        }
        //opponent untap button
        private void button16_Click(object sender, EventArgs e)
        {
            Card a = (Card)listBox8.SelectedItem;
            if (a != null)
            { 
                if (a.Type == "Land")
                {
                    listBox10.Items.Add(listBox8.SelectedItem);
                    listBox8.Items.Remove(listBox8.SelectedItem);
                }
                else if (a.Type == "Summon")
                {
                    listBox9.Items.Add(listBox8.SelectedItem);
                    listBox8.Items.Remove(listBox8.SelectedItem);
                }
            }
        }
		//phase box
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
		//step box
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Untap")
            {
                foreach (Card item in listBox7.Items)
                {
                    if (item.Type == "Land")
                    {
                        listBox5.Items.Add(item);
                    }
                    else if (item.Type == "Summon")
                    {
                        listBox6.Items.Add(item);
                    }
                }
                listBox7.Items.Clear();
            }
        }
		//my health
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
		//opponent health
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "Untap")
            {
                foreach (Card item in listBox8.Items)
                {
                    if (item.Type == "Land")
                    {
                        listBox10.Items.Add(item);
                    }
                    else if (item.Type == "Summon")
                    {
                        listBox9.Items.Add(item);
                    }
                }
                listBox8.Items.Clear();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
