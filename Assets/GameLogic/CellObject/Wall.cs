﻿namespace Assets.GameLogic.CellObject
{
    public class Wall : BaseCellObject
    {
        public Wall(int x, int y) : base(x, y, '#', System.ConsoleColor.DarkGray) {
            DescAction = "Boom. Hey! There is wall here";
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            return false;
        }
    }
}