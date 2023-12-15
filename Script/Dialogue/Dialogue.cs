using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Dialogue : MonoBehaviour
{
    //fields
   //window
   public GameObject window;
   //indicator
   public GameObject indicator;
   //text componet
   public TMP_Text dialogueText;
   //dialogues list
   public List<string> dialogues;
   //writing speed
   public float writingSpeed;
    //index on dialogue
   private int index;
   //character index
   private int charIndex;
   //Started boolean
   private bool started;
   //next bool
   private bool waitForNext;

   private void Awake(){
    ToggleIndicator(false);
    ToggleWindow(false);
   }

    private void ToggleWindow(bool show){
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show){
        indicator.SetActive(show);
    }

   //start dialogue
    public void StartDialogue(){
        if(started)
            return;

        //boolean
        started = true;
        //show
        ToggleWindow(true);
        //hide
        ToggleIndicator(false);
        //start first
        GetDialogue(0);
    }

    private void GetDialogue(int i){
        //start index at zero
        index = i;
        //reset
        charIndex = 0;
        //clear
        dialogueText.text = string.Empty;
        //start writing
        StartCoroutine(Wrinting());
    }
   //end dialogue
    public void EndDialogue(){
        //started is disabled
        started = false;
        //disabled wait for next as well
        waitForNext = false;
        //stop all ienumerators
        StopAllCoroutines();
        //hine window
        ToggleWindow(false);
    }

   //logic
   IEnumerator Wrinting(){
    yield return new WaitForSeconds(writingSpeed);
    
    string currentDialogue = dialogues[index];
    //wrie the character
    dialogueText.text += currentDialogue[charIndex];
    //increa
    charIndex++;
    //rached the end of the sentence
    if(charIndex < currentDialogue.Length){
        //wait
        yield return new WaitForSeconds(writingSpeed);
        //restart
        StartCoroutine(Wrinting());
    } else {
        waitForNext = true;
    }
   }

   private void Update(){
    if(!started)
        return;

    if(waitForNext && Input.GetKeyDown(KeyCode.B)){
        waitForNext = false;
        index++;

    if(index < dialogues.Count){
        //if so fetch the dialogue
        GetDialogue(index);
    } else {
        //if not end the dialogue process
        ToggleIndicator(true);
        EndDialogue();
    }
 
    }
   }
}
