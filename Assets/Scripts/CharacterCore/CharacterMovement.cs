using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponents
{
    [Header("General Settings")]
    [Tooltip("This firld is used in start Func, changing this value in game will get nothing")] public float startWalkSpeed = 7f;

    [Header("Animation Fields")]
    public bool enableAnimation = false;

    public float MoveSpeed { get; set; }

    private readonly int RunParametr = Animator.StringToHash("Run"); //The string to convert to Id.

    protected override void Start()
    {
        base.Start();
        MoveSpeed = startWalkSpeed;
    }


    protected override void HandleAbility()
    {
        base.HandleAbility();
        MoveCharacter();

        if(enableAnimation)
        {
            UpdateAnimations();
        }
    }

    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        Vector2 moveInput = movement;
        Vector2 movementNormalized = moveInput.normalized;
        Vector2 movementSpeed = movementNormalized * MoveSpeed;
        controller.SetMovement(movementSpeed);
    }

    public void UpdateAnimations()
    {
        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            anim.SetBool(RunParametr, true);
        }
        else
        {
            anim.SetBool(RunParametr, false);
        }
    }

    public void ResetSpeed()
    {
        MoveSpeed = startWalkSpeed;
    }

    public void SetHorizontal(float value)
    {
        horizontalInput = value;
    }

    public void SetVertical(float value)
    {
        verticalInput = value;
    }
}
