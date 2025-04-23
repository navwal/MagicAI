//using CISP.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CISP.AI
{
    public class GameEngine
    {
        public List<Card> myHand = new List<Card>();
        public List<Card> myLand = new List<Card>();
        List<Card> myBoard = new List<Card>();
        List<Card> myTapped = new List<Card>();
        
        List<Card> oland = new List<Card>();
        List<Card> oboard = new List<Card>();
        List<Card> otapped = new List<Card>();

        int damage;

        bool landplayed = false;
        public string Start()
        {
            if (myHand.Count != 7)
                return "Draw until you have seven cards in hand";
            else
                return "Hit next";
        }
        public string Mulligan()
        {
            bool lands = false;
            bool mana = false;
            bool match = false;
            string[] landname = new string[2] { "", "" };
            string[] color = new string[2] { "", "" };
            int landcount = 0;
            foreach (Card n in myHand)
            {
                if (n.Type == "Land")
                {
                    myLand.Add(n);
                    if (landname[0] == "")
                    {
                        landname[0] = n.Color;
                        landcount++;
                    }
                    else if (landname[0] != "" && n.Color != landname[0])
                    {
                        landname[1] = n.Color;
                        landcount++;
                    }
                    else
                        landcount++;
                }
                else if (n.Mananocol + n.Manacol <= 3)
                {
                    mana = true;
                    if (color[0] == "")
                    {
                        color[0] = n.Color;
                    }
                    else if (color[0] != "" && n.Color != color[0])
                    {
                        color[1] = n.Color;
                    }
                }
            }
            if (color[0] == landname[0] || color[0] == landname[1] || color[1] == landname[0] || color[1] == landname[1])
            {
                match = true;
            }
            if (landcount >= 2)
            {
                lands = true;
            }
            if (lands && match && mana)
                return "keep. Press next if its your turn otherwise press opponent next";
            else
                return "Mulligan";
        }
        public string Begin(string step)
        {
            landplayed = false;
            if (step == "Untap")
            {
                foreach (Card n in myBoard)
                {
                    n.Sick = false;
                }
                return "Make sure everything is untapped";
            }
            else if (step == "Upkeep")
            {
                foreach (Card n in myBoard)
                {
                    if (n.Keyword == "Echo")
                    {
                        string echo = "";
                        echo = Echo(n);
                        return echo;
                    }
                }
                return "Go to next step";
            }
            else
            {
                return "Draw a card and go to next phase, unless this is the first turn of the game";
            }
            return "Go to next step";
        }
        public string Main_1()
        {
            List<Card> hand = myHand;
            List<Card> mland = myLand;
            List<Card> obfield = oboard;
            List<Card> opland = oland;
            List<Card> spells = new List<Card>();

            int[] landcount = new int[3] { 0, 0, 0 };
            int[] olandcount = new int[3] { 0, 0, 0 };
            string target = "";

            foreach (Card n in mland)
            {
                if (n.Color == "Green")
                    landcount[0]++;
                else if (n.Color == "Red")
                    landcount[1]++;
                else
                    landcount[2]++;
            }

            foreach (Card n in opland)
            {
                if (n.Color == "Green")
                    olandcount[0]++;
                else if (n.Color == "Red")
                    olandcount[1]++;
                else
                    olandcount[2]++;
            }
            foreach (Card n in hand)
            {
                if (n.Type == "Sorcery" && ManaCost(n, landcount))
                {
                    spells.Add(n);
                }
            }
            foreach (Card n in hand)
            {
                if (n.Type == "Summon")
                {
                    if (ManaCost(n, landcount) && !n.Sick)
                        Cast(n, mland, "");
                }
            }
            foreach (Card n in spells)
            {
                //fix "Non-Forest" lands to swamps and mountains
                //fixed
                if (n.Keyword.Contains("Destroy Land"))
                {
                    if (/*olandcount[0] < olandcount[1] && */olandcount[0] > 0 /*|| olandcount[0] < olandcount[2] && olandcount[0] > 0*/)
                        target = "Forest";
                    else if (olandcount[1] > 0)
                        target = "Mountain";
                    else if (olandcount[2] > 0)
                        target = "Swamp";
                    return Cast(n, mland, target);
                }
                else if (n.Keyword.Contains("Destroy Creature") && obfield.Count > 0)
                {
                    if (obfield.Count == 1 && obfield[0].Color != "Black")
                    {
                        target = n.Name;
                        return Cast(n, mland, target);
                    }
                }
                else if (n.Keyword.Contains("Damage Creature"))
                {
                    if (n.Name == "Corrupt")
                    {
                        if (obfield.Count == 1)
                        {
                            target = obfield[0].Name;
                            return Cast(n, mland, target);
                        }
                        else
                        {
                            target = "Opponent";
                            return Cast(n, mland, target);
                        }
                    }
                    if (obfield.Count == 1)
                    {
                        if (n.Power >= obfield[0].Toughness)
                        {
                            target = obfield[0].Name;
                            return Cast(n, mland, target);
                        }
                    }
                    else if (obfield.Count > 1)
                    {
                        for (int i = 0; i < obfield.Count; i++)
                        {
                            for (int j = 0; j < obfield.Count; j++)
                            {
                                if (obfield[i].Toughness > obfield[j].Toughness && n.Power >= obfield[i].Toughness)
                                    target = obfield[i].Name;
                                else if (n.Power >= obfield[j].Toughness)
                                    target = obfield[j].Name;
                            }
                        }
                        if (target != "")
                            return Cast(n, mland, target);
                    }
                }
            }
            return "Go to next step";
        }
        public string Combat()
        {
            List<Card> board = myBoard;
            List<Card> opboard = oboard;

            List<Card> Creatures = new List<Card>();
            List<Card> Blockers = new List<Card>();
            List<Card> Attackers = new List<Card>();
            foreach (Card n in board)
            {
                if (n.Type == "Summon")
                    Creatures.Add(n);
            }
            foreach (Card n in opboard)
            {
                if (n.Type == "Summon")
                    Blockers.Add(n);
            }


            //Sort Creatures by lowest power first
            Creatures = Creatures.OrderBy(x => x.Power).ToList();
            Creatures.Reverse();
            //Sort blockers by toughness
            Blockers = Blockers.OrderBy(x => x.Toughness).ToList();
            string creaturestoattackwith = "";
            // If we have no creatures able to attack, skip to next phase
            if (Creatures.Count == 0)
            {
                return "Hit next until Main_2 Phase.";
            }
            // If our opponent has no blockers, attack with everything we can
            else if (Blockers.Count == 0)
            {
                for (int i = 0; i < Creatures.Count; i++)
                {
                    creaturestoattackwith += Creatures[i].Name + ", ";
                }
                return "attack with " + creaturestoattackwith;
            }
            // Compares our creatures toughness with blocker power and only attacks with creatures that will survive
            for (int i = 0; i < Creatures.Count; i++)
            {
                for (int j = 0; j < Blockers.Count; j++)
                {
                    if (Creatures[i].Toughness > Blockers[j].Power)
                    {
                        Attackers.Add(Creatures[i]);
                    }
                }
            }
            if (Attackers.Count > 0)
            {
                for (int i = 0; i < Attackers.Count; i++)
                {
                    creaturestoattackwith += Attackers[i].Name + ", ";
                }
                return "Attack with " + creaturestoattackwith;
            }
            else
                return "Hit next until Main_2 Phase  .";
        }
        public string Defend()
        {
            List<Card> optapped = otapped;
            List<Card> hand = myHand;
            List<Card> board = myBoard;

            List<Card> Attackers = new List<Card>();
            List<Card> Creatures = new List<Card>();
            List<Card> Spells = new List<Card>();

            int damage = 0;

            string blockers = "";
            foreach (Card n in optapped)
            {
                if (n.Type == "Summon")
                    Attackers.Add(n);
            }
            foreach (Card n in board)
            {
                if (n.Type == "Summon")
                    Creatures.Add(n);
            }
            // For combat tricks if we wanted to program it
            foreach (Card n in hand)
            {
                if (n.Type == "Instant")
                    Spells.Add(n);
            }
            List<string> Block = new List<string>();
            List<Card> Unblocked = new List<Card>();
            Attackers = Attackers.OrderBy(x => x.Power).ToList();
            Creatures = Creatures.OrderBy(x => x.Toughness).ToList();
            Creatures.Reverse();
            foreach (Card n in Attackers)
            {
                Unblocked.Add(n);
            }
            for (int i = Creatures.Count - 1; i > 0; i--)
            {
                for (int j = 0; j < Attackers.Count; j++)
                {
                    if (Creatures[i].Toughness > Attackers[j].Toughness)
                    {
                        Block.Add("Block ");
                        Block.Add(Attackers[j].Name);
                        Block.Add("With ");
                        Block.Add(Creatures[i].Name);
                        Unblocked.Remove(Attackers[j]);
                    }
                }
            }
            foreach (Card card in Unblocked)
            {
                damage += card.Power;
            }
            string damagedealt = Damage(damage);
            if (Block.Count > 0)
            {
                foreach (string s in Block)
                {
                    blockers += s + " ";
                }
                return blockers + damagedealt;
            }
            return "Pass to next step";
        }
        public string Main_2()
        {
            string output;
            List<Card> hand = myHand;
            List<Card> landcount = myLand;
            

            List<Card> Lands = new List<Card>();
            List<Card> Creature = new List<Card>();
            List<Card> Enchant = new List<Card>();
            int[] mana = new int[3] { 0, 0, 0 };
            int[] manainhand = new int[3] { 0, 0, 0 };
            //fix "Non-Forest" lands to swamps and mountains
            foreach (Card n in landcount)
            {
                if (n.Color == "Green")
                    mana[0]++;
                else if (n.Color == "Red")
                    mana[1]++;
                else
                    mana[2]++;
            }
            // Good up to here
            foreach (Card n in hand)
            {
                if (n.Type == "Land")
                    Lands.Add(n);
                else if (n.Type == "Summon")
                    Creature.Add(n);
                else if (n.Type == "Enchant")
                    Enchant.Add(n);
            }
            Creature = Creature.OrderBy(x => x.Power).ToList();
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            foreach (Card n in Lands)
            {
                if (n.Color == "Green")
                    manainhand[0]++;
                else if (n.Color == "Red")
                    manainhand[1]++;
                else
                    manainhand[2]++;
            }
            if (!landplayed && Lands.Count > 0)
            {
                //fix "Non-Forest" lands to swamps and mountains
                //fixed
                landplayed = true;
                //add ai to look at spells to prioritize those color Lands first
                if (manainhand[0] >= 1)
                //if (manainhand[1] == 0 || manainhand[2] == 0)
                {
                    return "Play a Forest";
                }
                else if (manainhand[1] >= 1)
                //else if (manainhand[0] == 0 || manainhand[2] == 0)
                {
                    return "Play a Mountain";
                }
                else if (manainhand[2] >= 1)
                //else if (manainhand[0] == 0 || manainhand[1] == 0)
                {
                    return "Play a Swamp";
                }
                else
                {
                    if (mana[0] > mana[1] && manainhand[1] > 0)
                    {
                        return "play a mountain";
                    }
                    else if (mana[0] > mana[2] && manainhand[2] > 0)
                    {
                        return "Play a swamp";
                    }
                    //Fix this
                    else if (manainhand[0] > 0)
                    {
                        return "play a forest";
                    }
                }
            }
            else if (Creature.Count > 0)
            {
                foreach (Card n in Creature)
                {
                    if (ManaCost(n, mana))
                    {
                        output = Cast(n, landcount, "");
                        return output;
                    }
                }
            }
            return "Pass to next phase";
        }
        public string End()
        {
            return "Hit next until you pass the turn";
        }
        public string End_opponent()
        {
            List<Card> hand = myHand;
            List<Card> opboard = oboard;
            List<Card> Lands = myLand;
            List<Card> Instants = new List<Card>();
            List<Card> Creatures = new List<Card>();
            int[] mana = new int[3] { 0, 0 , 0};
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            for (int i = 0; i < Lands.Count; i++)
            {
                if (Lands[i].Color == "Green")
                    mana[0]++;
                else if (Lands[i].Color == "Red")
                    mana[1]++;
                else
                    mana[2]++;
            }

            if (opboard != null)
            {
                foreach (Card n in hand)
                {
                    if (n.Type == "Instant")
                    {
                        Instants.Add(n);
                    }
                }
                foreach (Card n in opboard)
                {
                    if (n.Type == "Summon")
                    {
                        Creatures.Add(n);
                    }
                }
                Creatures = Creatures.OrderBy(x => x.Toughness).ToList();
                foreach (Card n in Instants)
                {
                    if (n.Keyword == "Destroy Creature" && ManaCost(n, mana))
                    {
                        if (n.Name == "Expunge")
                        {
                            for (int i = 0; i < Creatures.Count; i++)
                            {
                                if (Creatures[i].Color != "Black")
                                    Cast(n, Lands, Creatures[i].Name);
                            }
                        }
                        else if (n.Name == "Swat")
                        {
                            for (int i = 0; i < Creatures.Count; i++)
                            {
                                if (Creatures[i].Power < 3)
                                    Cast(n, Lands, Creatures[i].Name);
                            }
                        }
                    }
                    else if (n.Keyword == "Damage Creature" && ManaCost(n, mana))
                    {
                        foreach (Card m in Creatures)
                        {
                            if (n.Power >= m.Toughness)

                                Cast(n, Lands, m.Name);
                        }
                    }
                }
            }
            return "Pass to next step";
        }
        //Decide what return type should be
        public string Cast(Card card, List<Card> lands, string spelltarget)
        {
            string[] tland = TapLands(card, lands);
            string landtotap = "";
            /*for (int i = 0; i < tland.Length; i++)
            {
                landstotap += tland[i] + ", "; 
            }*/
            foreach (string s in tland)
            {
                landtotap += s + ", ";
            }
            //landtotap = landtotap.Substring(0, tland.Length - 2);
            //display which lands to tap
            if (spelltarget == "")
            {
                return "tap " + landtotap + " to cast " + card.Name;
            }
            else
                return "tap " + landtotap + " to cast " + card.Name + " targeting " + spelltarget;
        }
        public string[] TapLands(Card spellcard, List<Card> landcount)
        {
            int colorless = spellcard.Mananocol;
            int colored = spellcard.Manacol;
            int[] mana = new int[3] { 0, 0, 0 };
            string[] tap = new string[landcount.Count + 1];

            //fix here
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            foreach (Card card in landcount)
            {
                if (card.Color == "Green")
                    mana[0]++;
                else if (card.Color == "Red")
                    mana[1]++;
                else
                    mana[2]++;
            }
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            if (spellcard.Color == "Green")
            {
                mana[0] = mana[0] - spellcard.Manacol;
                for (int i = 0; i < spellcard.Manacol; i++)
                    tap[i] = "Forest";
            }
            else if (spellcard.Color == "Red")
            {
                mana[1] = mana[1] - spellcard.Manacol;
                for (int i = 0; i < spellcard.Manacol; i++)
                    tap[i] = "Mountain";
            }
            else
            {
                mana[2] = mana[2] - spellcard.Manacol;
                for (int i = 0; i < spellcard.Manacol; i++)
                    tap[i] = "Swamp"; 
            }
            int c = colored;
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            while (colorless > 0)
            {
                //if (mana[0] == 0 && mana[2] == 0)
                if (mana[1] >= 1)
                {
                    tap[c] = "Mountain";
                    mana[1]--;
                }
                //else if (mana[0] == 0 && mana[1] == 0)
                else if (mana[2] >= 1)
                {
                    tap[c] = "Swamp";
                    mana[2]--;
                }
                //else if (mana[1] == 0 && mana[2] == 0)
                else if (mana[0] >= 1)
                {
                    tap[c] = "Forest";
                    mana[0]--;
                }
                else if (colorless % 2 == 0 && mana[2] == 0)
                {
                    tap[c] = "Mountain";
                }
                else if (colorless % 2 == 0 && mana[1] == 0)
                {
                    tap[c] = "Swamp";
                }
                else
                {
                    tap[c] = "Forest";
                }
                c++;
                colorless--;
            }
            return tap;
        }
        public string Echo(Card card)
        {
            card.Keyword = "";
            List<Card> Lands = myLand;
            int[] mana = new int[3] { 0, 0, 0 };
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            foreach (Card c in Lands)
            {
                if (c.Color == "Green")
                    mana[0]++;
                else if (c.Color == "Red")
                    mana[1]++;
                else
                    mana[2]++;

            }
            //fix tap.ToString();
            if (ManaCost(card, mana))
            {
                string[] tap = TapLands(card, Lands);
                string landtotap = "";
                foreach (string s in tap)
                {
                    landtotap += s + ", ";
                }
                return "Pay echo by tapping " + landtotap;
            }
            return "Sacrifice " + card.Name;
        }
        public bool ManaCost(Card spell, int[] Lands)
        {
            int color = 0;
            int colorless = 0;
            if (spell.Color == "Green")
            {
                color = Lands[0] - spell.Manacol;
                if (color > 0 && Lands[1] > 0)
                {
                    color = color + Lands[1];
                    if (color >= spell.Mananocol)
                        return true;
                }
                else if (color > 0 && Lands[2] > 0)
                {
                    color = color + Lands[2];
                    if (color >= spell.Mananocol)
                        return true;
                }
                else if (color == 0 && spell.Mananocol == 0)
                {
                    return true;
                }
            }
            //fix "Non-Forest" lands to swamps and mountains
            //fixed
            else if (spell.Color == "Red")
            {
                color = Lands[1] - spell.Manacol;
                if (color > 0)
                {
                    color = color + Lands[0];
                    if (color >= spell.Mananocol)
                        return true;
                }
                else if (color == 0 && spell.Mananocol == 0)
                    return true;
            }
            else
            {
                color = Lands[2] - spell.Manacol;
                if (color > 0)
                {
                    color = color + Lands[0];
                    color = color + Lands[1];
                    if (color >= spell.Mananocol)
                        return true;
                }
                else if (color == 0 && spell.Mananocol == 0)
                    return true;
            }
            return false;
        }
        public string Damage(int damage)
        {
            string damagedealt = "";
            damagedealt = Convert.ToString(damage);
            damage = 0;
            return ". Subtract " + damagedealt + " From your life total";
        }
        public void GetMyUIGameState(List<Card> hand, List<Card> board, List<Card> Lands, List<Card> Tapped)
        {
            myHand = hand;
            myBoard = board;
            myLand = Lands;
            myTapped = Tapped;
        }
        public void GetOppUIGameState(List<Card> board, List<Card> Lands, List<Card> Tapped)
        {
            oboard = board;
            oland = Lands;
            otapped = Tapped;
        }

    }

}
