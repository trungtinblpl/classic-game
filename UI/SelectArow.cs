
using UnityEngine;
using UnityEngine.UI;


public class SelectArow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    // [SerializeField] private AudioClip changeSound;
    // [SerializeField] private AudioClip interactSound;
    private RectTransform rect;
    private int currentPosition;

    private void Awake(){
        rect = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        currentPosition = 0;
        ChangePosition(0);
    }

    private void Update(){
        //Change the position of the selection arrow
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        //Interact with current option
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

     private void ChangePosition(int _change){
        currentPosition += _change;

        // if (_change != 0)
        //     SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }

    private void AssignPosition(){
        //Assign the Y position of the current option to the rect (basically moving it up and down)
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y);
    }

    private void Interact(){
        // SoundManager.instance.PlaySound(interactSound);

        //Access the button component on each option and call its function
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
