using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsDamage = Animator.StringToHash("IsDamage"); // 숫자로 쓰는 게 더 편해서 변환과정.

    protected Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void Move(Vector2 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f); // 넉벡 등으로 인해 0.5보다 느리게 움직이면 안 움직이는 걸로 판정해서 false
        // SetBool이 앞쪽 매개변수를 int로 받아서 위에서 IsMoving, IsDamage 선언할 때 int로 한 것
    }

    public void Damage()
    {
        animator.SetBool(IsDamage, true); 
    }

    public void InvincibilityEnd()
    {
        animator.SetBool(IsDamage, false); // 무적시간 끝나면 false값 반환해서 Idle로.
    }


}