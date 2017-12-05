using EasyPaint.Shapes;

namespace EasyPaint.State
{
    public class ModifyState : BasicState
    {
        private static BasicState Instance;

        public static BasicState GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ModifyState();
            }
            return Instance;
        }

        public override void Draw(Shape SelectedShape)
        {
            SelectedShape.RenderOnModify();
        }

        public override void Deselect(Shape SelectedShape)
        {
            SelectedShape.ChangeState(NormalState.GetInstance());
        }
    }
}
