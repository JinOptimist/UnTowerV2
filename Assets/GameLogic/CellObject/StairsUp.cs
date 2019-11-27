﻿namespace Assets.GameLogic.CellObject
{
    public class StairsUp : BaseCellObject
    {
        public StairsUp(int x, int y) : base(x, y, '<') {
            DescAction = "I return one level up";
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            CallAfterStep = dungeon.GoUp;
            return true;
        }
    }
}