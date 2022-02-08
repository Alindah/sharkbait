using UnityEngine;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject backButton;
    private GameController gameController;
    private int index;
    private const string GAME_CONTROLLER_NAME = "GameController";
    private const string DONE_STRING = "Done";

    void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        index = transform.GetSiblingIndex();

        // If last page, "next" becomes "done"
        if (index == gameController.instructionsContainer.childCount - 1)
            nextButton.transform.GetComponentInChildren<Text>().text = DONE_STRING; 
    }

    public void NextPage()
    {
        // If on the last page, return to start menu
        if (index >= gameController.instructionsContainer.childCount - 1)
            gameController.SwitchPanels(gameController.instructionPages[index], gameController.startPanel);
        else
            gameController.SwitchPanels(gameObject, gameController.instructionPages[index + 1]);
    }

    public void PreviousPage()
    {
        // If first page, return to start menu
        if (index == 0)
            gameController.SwitchPanels(gameController.instructionPages[index], gameController.startPanel);
        else
            gameController.SwitchPanels(gameObject, gameController.instructionPages[index - 1]);
    }
}
