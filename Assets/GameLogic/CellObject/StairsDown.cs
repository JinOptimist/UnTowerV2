namespace Assets.GameLogic.CellObject
{
    public class StairsDown : BaseCellObject
    {
        public StairsDown(int x, int y) : base(x, y, '>') {
            DescAction = "I go down!";
        }

        public override bool TryToStepHere(IDungeon dungeon)
        {
            CallAfterStep = dungeon.GoDown;
            return true;
        }
    }
}