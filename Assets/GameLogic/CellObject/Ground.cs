namespace Assets.GameLogic.CellObject
{
    public class Ground : BaseCellObject
    {
        public Ground(int x, int y) : base(x, y, ' ') { }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            return true;
        }
    }
}