using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    [SerializeField] private GameObject switchUI;
    [SerializeField] private PlayerSwitchClient[] clients;
    public int currentIndex;
    bool canvasActive;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            switchUI.SetActive(!canvasActive);
        }
    }
    public void Switch(int switchIndex)
    {
        for(int i = 0; i < clients.Length; i++)
        {
            clients[i].Deactivate();
        }
        clients[switchIndex].Activate();
        switchUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
