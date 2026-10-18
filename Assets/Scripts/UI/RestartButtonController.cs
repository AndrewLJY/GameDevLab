using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestartButtonController: MonoBehaviour, IInteractiveButton
{
    public UnityEvent gameRestart;

    public void ButtonClick()
    {
        gameRestart.Invoke();
    }

    //public void ButtonClick()
    //{
    //    GameManager.instance.GameRestart();
    //}
}
