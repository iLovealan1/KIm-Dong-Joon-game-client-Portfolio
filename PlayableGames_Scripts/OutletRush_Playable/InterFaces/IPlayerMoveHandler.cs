namespace luna_sportshop.Playable002
{
    public interface IPlayerMoveHandler : IPositionReturner
    {
        void MovePlayer(float vertical , float horizontal);
        bool IsOKToMove {get; set;}
    }
}
