using UnityEngine;

public enum Player_AnimState
{
    None = 0,
    Idle,
    Walk
}

public class FujimaruAnimationController : MonoBehaviour
{
    [SerializeField] private Animator Animator_Entity;
    private Player_AnimState _currentAnimState;

    public void SetState(Player_AnimState newState)
    {
        if (newState == _currentAnimState)
        {
            return;
        }

        _currentAnimState = newState;

        switch (_currentAnimState)
        {
            case Player_AnimState.Idle:
                ResetAllAnimParameters();
                break;
            case Player_AnimState.Walk:
                Animator_Entity.SetBool("IsMoving", true);
                break;
            default:
                ResetAllAnimParameters();
                break;
        }
    }

    private void ResetAllAnimParameters()
    {
        Animator_Entity.SetBool("IsMoving", false);
    }
}
