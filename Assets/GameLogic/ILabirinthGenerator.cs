﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public interface ILabirinthGenerator
    {
        ILabyrinthLevel GenerateLevel(int stairsX = 0, int stairsY = 0, int levelNumber = 0);

        ILabyrinthLevel GenerateStoreLevel(int levelNumber = 0);
    }
}
