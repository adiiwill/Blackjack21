using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack21
{
    internal class GameEntity
    {
        private string name;
        private bool isPlayer;
        public List<Card> hand = new List<Card>();

        public GameEntity(string name = "noname", bool isPlayer = false)
        {
            this.name = name;
            this.isPlayer = isPlayer;
        }

        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// The value of the Entity's hand. (Hidden cards not included)
        /// </summary>
        public int handValue
        {
            get
            {
                int sum = 0;
                foreach (var c in hand)
                {
                    if (!c.isHidden) sum += c.Value;
                }
                return sum;
            }
        }

        /// <summary>
        /// The true value of the Entity's hand. (Hidden cards included)
        /// </summary>
        public int trueHandValue
        {
            get
            {
                int sum = 0;
                foreach (var c in hand)
                {
                    sum += c.Value;
                }
                return sum;
            }
        }

        /// <summary>
        /// Adds a card to the Entity's deck.
        /// </summary>
        /// <param name="card">The card to add.</param>
        public void AddCard(Card card)
        {
            if (isPlayer) card.isHidden = false;
            hand.Add(card);
            card.isDrawn = true;
        }

        /// <summary>
        /// Decides the ace value.
        /// </summary>
        /// <param name="value">The value to change the ace to.</param>
        public void SetAceValue(int value)
        {
            foreach (var c in hand)
            {
                if (c.Name == "A")
                {
                    c.Value = value;
                    break;
                }
            }
        }
    }
}
