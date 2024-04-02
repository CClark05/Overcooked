using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCommands : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ScoreCalculator.Instance.ModifyScore(20);
        }
    }
}
