using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRound : MonoBehaviour
{
    public float delaySecond = 1;

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.SetActive(false);

            ModelSelect();
        }
    }

    public void ModelSelect(){
        StartCoroutine(LoadAfterDelay());
    }

    IEnumerator LoadAfterDelay(){
        yield return new WaitForSeconds(delaySecond);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
