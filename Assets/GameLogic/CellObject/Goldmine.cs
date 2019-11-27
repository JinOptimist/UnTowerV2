namespace Assets.GameLogic.CellObject
{
    public class Goldmine : BaseCellObject
    {
        public int Money { get; private set; } = 3;

        public Goldmine(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkRed)
        {
            DescAction = "Money, money, money...";
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            if (Money > 0)
            {
                Hero.GetHero.Money++;
                Money--;
                if (Money == 0)
                {
                    dungeon.ReplaceToGround(this);
                    DescAction = "This was last coin. This is sad";
                    return true;
                }
            }

            return false;
        }
    }
}