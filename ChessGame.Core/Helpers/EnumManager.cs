namespace ChessGame.Core
{
    public enum FieldState
    {
        EmptyState,
        ClickedState,
        MoveState,
        AttackState,
        CaptureState,
        CheckState,
        MateState
    }

    public enum Player
    {
        White,
        Black
    }
}
