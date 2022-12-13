namespace Blackjack21
{
    internal class Card
    {
        private string name;
        private int value;
        private bool isUpsideDown = false;
        private bool is_drawn = false;

        public Card(string name, int value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Value
        {
            get { return value; }

            set { this.value = value; }
        }
        public bool isHidden
        {
            get { return isUpsideDown; }
            set { isUpsideDown = value; }
        }
        public bool isDrawn
        {
            get { return is_drawn; }
            set { is_drawn = value; }
        }
    }
}
