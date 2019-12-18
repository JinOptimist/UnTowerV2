using Assets.GameLogic.CellObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GameLogic
{
    public interface ILabyrinthLevel
    {
        List<List<BaseCellObject>> Cells { get; set; }
        int Width { get; }
        int Height { get; }

        BaseCellObject this[int x, int y] { get; set; }

        List<BaseCellObject> AllCells();
    }
}
