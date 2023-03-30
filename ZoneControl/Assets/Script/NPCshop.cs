using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCshop : MonoBehaviour
{
    public Transform playerPosition;
    public float interactDistance;
    public GameObject shopUI;
    public Button shopButton;
    public int upgradeCost;
    public int gelVanMma;
    public TMP_Text moneyText;

    private InputManager shopInteract;

    private void Awake()
    {
        shopInteract = new InputManager();
        shopInteract.Enable();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, playerPosition.position) < interactDistance)
        {
            if (shopInteract.Player.NPCinteract.WasPerformedThisFrame())
            {
                shopUI.SetActive(true);
            }
        }
        else
        {
            shopUI.SetActive(false);
        }

        if (gelVanMma >= upgradeCost)
        {
            shopButton.interactable = true;
        }
        else if (gelVanMma < upgradeCost)
        {
            shopButton.interactable = false;
        }

        moneyText.text = gelVanMma.ToString();
    }

    public void BuyUpgrade()
    {
        if (gelVanMma >= upgradeCost)
        {
            playerPosition.GetComponent<PlayerHealth>().dealtDamage = 2;
            gelVanMma -= upgradeCost;
        }
    }
}
