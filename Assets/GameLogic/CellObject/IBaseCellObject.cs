using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic.CellObject
{
    public interface IBaseCellObject
    {
        int X { get; set; }
        int Y { get; set; }
        char Chapter { get; set; }
        ConsoleColor Color { get; set; }
        string DescAction { get; set; }
        Action CallAfterStep { get; set; }

        bool TryToStepHere(IDungeon dungeon);
    }
}
