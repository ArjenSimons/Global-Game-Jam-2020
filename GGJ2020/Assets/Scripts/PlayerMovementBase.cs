using UnityEngine;

public class PlayerMovementBase : MonoBehaviour
{
    [SerializeField] protected Animator animController;

    public bool CarryingCanonBall { get; protected set; }

    public void LoseCanonBall()
    {
        CarryingCanonBall = false;
    }
}
