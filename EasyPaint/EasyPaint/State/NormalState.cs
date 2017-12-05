using EasyPaint.Shapes;

namespace EasyPaint.State
{
    public class NormalState : BasicState
    {
        private static BasicState Instance;

        public static BasicState GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NormalState();
            }
            return Instance;
        }

        public override void Draw(Shape SelectedShape)
        {
            SelectedShape.RenderOnNormal();
        }

        public override void Select(Shape SelectedShape)
        {
            SelectedShape.ChangeState(ModifyState.GetInstance());
        }
    }
}
