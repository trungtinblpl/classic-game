using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrgger : MonoBehaviour
{
  public Dialogue dialogueScript;
  private bool playerDetected;

  //detect trigger with player
  private void OnTriggerEnter2D(Collider2D collision){
    //phat hien va cham va hien doan hoi thoai
    if(collision.tag == "Player"){
      playerDetected = true;
      dialogueScript.ToggleIndicator(playerDetected);
    }
  }

  private void OnTriggerExit2D(Collider2D collision){
    //lost trigger(phat hien) by player => tat cai show noi dung
    if(collision.tag == "Player"){
      playerDetected = false;
      dialogueScript.ToggleIndicator(playerDetected);
      dialogueScript.EndDialogue();
    }
  }

  //while detected if we interact start the dialogue
  private void Update(){
    if(playerDetected && Input.GetKeyDown(KeyCode.B)){
      dialogueScript.StartDialogue();
    }
  }
}
