using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private int _animIDHorizontalSpeed;
    private int _animIDVerticalSpeed;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        AssignAnimtionIDs();
    }

    private void AssignAnimtionIDs()
    {
        _animIDHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");
        _animIDVerticalSpeed = Animator.StringToHash("VerticalSpeed");
    }

    public void SetMovementSpeed(Vector2 speed)
    {
        _animator.SetFloat(_animIDHorizontalSpeed, speed.x);
        _animator.SetFloat(_animIDVerticalSpeed,speed.y);
    }
}
