using UnityEngine;

public class PlayerMovementBase : MonoBehaviour
{
    public bool CarryingCanonBall { get; protected set; }

    public void LoseCanonBall()
    {
        CarryingCanonBall = false;
    }
}
