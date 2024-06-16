using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private TutorialItemPickup tutorialItemPickup;
    [SerializeField] private List<ItemPickup> itemPickups = new List<ItemPickup>();

    [SerializeField] private int cost;
    [SerializeField] private GameObject costUI;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private float itemLaunchForce;

    private EntityAudioController entityAudioController;

    public enum ChestType 
    {
        TutorialChest,
        NormalChest
    }
    public ChestType chestType;

    private bool isOpened = false;

    private void Start()
    {
        InitInteractable();

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    public void InitInteractable()
    {
        costText.text = cost.ToString();
        costUI.SetActive(false);
    }

    public void OnEnterRange()
    {
        costUI.SetActive(true);
    }

    public void OnExitRange()
    {
        costUI.SetActive(false);
    }

    public void OnInteract()
    {
        if (isOpened)
            return;
        //sound
        entityAudioController.PlayAudio("chestopen");
        if (chestType == ChestType.TutorialChest)
        {
            Rigidbody2D item = Instantiate(tutorialItemPickup, transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            item.AddForce(transform.up * itemLaunchForce, ForceMode2D.Impulse);
        }
        else if (chestType == ChestType.NormalChest)
        {
            int randNum = Random.Range(0, itemPickups.Count);
            Rigidbody2D item = Instantiate(itemPickups[randNum], transform.position, Quaternion.identity).GetComponent<Rigidbody2D>();
            item.AddForce(transform.up * itemLaunchForce, ForceMode2D.Impulse);
        }

        isOpened = true;
        costUI.SetActive(false);
    }

    public void SetCost(int newCost)
    {
        cost = newCost;
        costText.text = cost.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out PlayerController playerController) || isOpened)
            return;

        OnEnterRange();
        playerController.SetInteractable(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent<PlayerController>(out PlayerController playerController) || isOpened)
            return;

        OnExitRange();
        playerController.SetInteractable(null);
    }
}