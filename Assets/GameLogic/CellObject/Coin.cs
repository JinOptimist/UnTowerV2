using System;

namespace Assets.GameLogic.CellObject
{
    public class Coin : BaseCellObject
    {
        private IHero _hero;
        public const ConsoleColor CoinColor = ConsoleColor.Yellow;
        public Coin(int x, int y, IHero hero) : base(x, y, '©', CoinColor) {
            DescAction = "Here! I found coin";
            _hero = hero;
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            dungeon.ReplaceToGround(this);
            _hero.Money++;
            return true;
        }
    }
}