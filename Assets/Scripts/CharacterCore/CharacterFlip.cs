using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponents
{
   public float playerCurrentxScale;

    Transform GFX;
    float flipX;




    protected override void Start()
    {
        base.Start();

        GFX = GetComponentInChildren<Transform>();
        flipX = GFX.localScale.x;
        playerCurrentxScale = GFX.localScale.x;
    }

    protected override void Update()
    {
        base.Update();

        Flip();
    }

    private void Flip()
    {
        if (controller.CurrentMovement.x >= 0.1)
        {
            FlipToDir(1);
        }
        else if (controller.CurrentMovement.x <= -0.1)
        {
            FlipToDir(0);
        }
    }

    private void FlipToDir(int side)
    {
        if (side == 0)
        {
            GFX.localScale = new Vector2(flipX, GFX.localScale.y);
            playerCurrentxScale = GFX.localScale.x;
        }
        else
        {
            GFX.localScale = new Vector2((flipX * -1), GFX.localScale.y);
            playerCurrentxScale = GFX.localScale.x;
        }
    }

}
