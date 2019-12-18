using Assets.GameLogic.CellObject;

namespace Assets.GameLogic
{
    public interface IDungeon
    {
        ILabyrinthLevel CurrentLevel { get; }
        int CurrentLevelNumber { get; }
        int Width { get; }
        int Height { get; }
        string DescLastAction { get; set; }

        void GoDown();
        void GoUp();
        //void HeroDoStep(Direction direction);
        void ReplaceToGround(IBaseCellObject cellToStep);
    }
}
