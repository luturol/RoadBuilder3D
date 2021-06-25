using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endGameTitle;
    [SerializeField] private TextMeshProUGUI endGameText;
    [SerializeField] private TextMeshProUGUI starsCountText;

    public void SetWinOrLose(bool win)
    {
        if (win)
        {
            endGameTitle.text = "Congratulations!!!";
            endGameText.text = "You have finished the game and collected a lot of stars!! As a present we doubled your stars";
        }
        else
        {
            endGameTitle.text = "Sorry, my friend =/";
            endGameText.text = "You can try again by pressing play again...";
        }
    }

    public void SetStarsCount(int stars)
    {
       starsCountText.text = "Stars: " + stars.ToString(); 
    }
}
