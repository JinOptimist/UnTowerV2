﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.GameLogic.CellObject
{
    public class Hero : BaseCellObject, IHero
    {
        public int Money { get; set; }

        public static Hero _hero;
        public static Hero GetHero
        {
            get
            {
                if (_hero == null)
                {
                    _hero = new Hero(0, 0);
                }

                return _hero;
            }
        }

        private Hero(int x, int y) : base(x, y, '@') { }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            return false;
        }
    }
}
