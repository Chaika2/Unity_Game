using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
   private Animator animator;
   private SpriteRenderer spriteRenderer;

   private const string IS_RUN = "IsRun";


   private void Awake(){
    animator = GetComponent<Animator>();
    spriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void Update(){
        animator.SetBool(IS_RUN, Player.Instance.IsRun());
        AdjustPlayerFacingDirection();
   }

   private void AdjustPlayerFacingDirection(){
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if(mousePos.x < playerPosition.x){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
}
