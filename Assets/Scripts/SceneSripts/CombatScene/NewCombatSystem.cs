using System.Collections;
using System.Collections.Generic;
using CharacterCore;
using UnityEngine;

public class NewCombatSystem : CharacterComponents
{
    [SerializeField] GameObject swordPref = null;
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void HandleAbility()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = mousePos;
            Vector2 attackDir = (mousePos2D - (Vector2)this.transform.position).normalized;
            SpawnAttackEffect(mousePos2D, Quaternion.identity);
            Debug.Log(attackDir);
        }
    }

    private void SpawnAttackEffect(Vector2 pos,Quaternion rotation)
    {
        GameObject attakEffect = Instantiate(swordPref, pos, rotation);
        Destroy(attakEffect, 1f);
    }
}
