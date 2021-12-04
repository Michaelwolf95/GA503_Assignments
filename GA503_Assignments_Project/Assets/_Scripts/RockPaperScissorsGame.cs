using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum HandChoice
{
    Rock = 0,
    Paper = 1,
    Scissors = 2
}

public class RockPaperScissorsGame : MonoBehaviour
{
    [SerializeField] private Button rockButton;
    [SerializeField] private Button paperButton;
    [SerializeField] private Button scissorsButton;

    [SerializeField] private Image playerHandImage;
    [SerializeField] private Image opponentHandImage;
    [SerializeField] private Sprite[] handSprites;

    [SerializeField] private TextMeshProUGUI resultText;

    private void Awake()
    {
        rockButton.onClick.AddListener((() =>
        {
            PlayRockPaperScissors(HandChoice.Rock);
        }));
        
        paperButton.onClick.AddListener((() =>
        {
            PlayRockPaperScissors(HandChoice.Paper);
        }));
        
        scissorsButton.onClick.AddListener((() =>
        {
            PlayRockPaperScissors(HandChoice.Scissors);
        }));
    }

    private void PlayRockPaperScissors(HandChoice argChoice)
    {
        Debug.Log("You played: " + argChoice);
        playerHandImage.sprite = handSprites[(int) argChoice];

        HandChoice opponentChoice = (HandChoice) UnityEngine.Random.Range(0, 3);
        
        Debug.Log("Opponent played: " + opponentChoice);
        opponentHandImage.sprite = handSprites[(int) opponentChoice];

        playerHandImage.enabled = true;
        opponentHandImage.enabled = true;

        switch (GetRockPaperScissorsResult(argChoice, opponentChoice))
        {
            case 0:
                Debug.Log("Tie!");
                resultText.text = "Tie!";
                playerHandImage.color = Color.gray;
                opponentHandImage.color = Color.gray;
                break;
            case 1:
                Debug.Log("You Win!");
                resultText.text = "You Win!";
                playerHandImage.color = Color.green;
                opponentHandImage.color = Color.red;
                break;
            case -1:
                Debug.Log("You Lose!");
                resultText.text = "You Lose!";
                playerHandImage.color = Color.red;
                opponentHandImage.color = Color.green;
                break;
        }
        
    }

    private int GetRockPaperScissorsResult(HandChoice argPlayerChoice, HandChoice argOpponentChoice)
    {
        switch (argPlayerChoice)
        {
            case HandChoice.Rock:
                switch (argOpponentChoice)
                {
                    case HandChoice.Rock:     return 0;
                    case HandChoice.Paper:    return -1;
                    case HandChoice.Scissors: return 1;
                }
                break;
            case HandChoice.Paper:
                switch (argOpponentChoice)
                {
                    case HandChoice.Rock:     return 1;
                    case HandChoice.Paper:    return 0;
                    case HandChoice.Scissors: return -1;
                }
                break;
            case HandChoice.Scissors:
                switch (argOpponentChoice)
                {
                    case HandChoice.Rock:     return -1;
                    case HandChoice.Paper:    return 1;
                    case HandChoice.Scissors: return 0;
                }
                break;
        }

        return 0;
    }
}
