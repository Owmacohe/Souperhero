using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public string recipeFilePath, spoonsFilePath, bowlsFilePath;
    public TextAsset recipeDescriptionFilePath, spoonsDescriptionFilePath, bowlsDescriptionFilePath;
    public Sprite recipesBackdrop, spoonsBackdrop, bowlsBackdrop;
    public GameObject recipesDescription, spoonsDescription, bowlsDescription;
    public GameObject recipeItemsParent, spoonItemsParent, bowlItemsParent;
    public GameObject player;
    public bool isFollowingPlayer;

    private GameObject stageParent; // the parent of all of the conversation sprites and buttons
    private TXTReader TXTparser; // TXT parsing script
    private JSONReader JSONparser; // JSON parsing script
    private GameObject itemParent;
    private TextAsset targetDescriptionPath;

    private void Start()
    {
        // Turning the stage off to start
        stageParent = transform.GetChild(0).gameObject;
        stageParent.SetActive(false);

        exitInventory();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (stageParent.activeSelf)
            {
                exitInventory();
            }
            else
            {
                startInventory();
            }
        }
    }

    public void startInventory()
    {
        stageParent.SetActive(true);
        TXTparser = GetComponent<TXTReader>();
        JSONparser = GetComponent<JSONReader>();

        loadTab("recipes");

        player.SetActive(false);
    }

    public void exitInventory()
    {
        stageParent.SetActive(false);
        player.SetActive(true);
    }

    public void loadTab(string inventoryType)
    {
        string targetPath;

        switch (inventoryType)
        {
            case "recipes":
                targetPath = recipeFilePath;
                targetDescriptionPath = recipeDescriptionFilePath;
                itemParent = recipeItemsParent;
                recipeItemsParent.SetActive(true);
                spoonItemsParent.SetActive(false);
                bowlItemsParent.SetActive(false);
                break;
            case "spoons":
                targetPath = spoonsFilePath;
                targetDescriptionPath = spoonsDescriptionFilePath;
                itemParent = spoonItemsParent;
                recipeItemsParent.SetActive(false);
                spoonItemsParent.SetActive(true);
                bowlItemsParent.SetActive(false);
                break;
            case "bowls":
                targetPath = bowlsFilePath;
                targetDescriptionPath = bowlsDescriptionFilePath;
                itemParent = bowlItemsParent;
                recipeItemsParent.SetActive(false);
                spoonItemsParent.SetActive(false);
                bowlItemsParent.SetActive(true);
                break;
            default:
                targetPath = recipeFilePath;
                targetDescriptionPath = recipeDescriptionFilePath;
                itemParent = recipeItemsParent;
                recipeItemsParent.SetActive(true);
                spoonItemsParent.SetActive(false);
                bowlItemsParent.SetActive(false);
                break;
        }

        if (isFollowingPlayer)
        {
            itemParent.transform.position = player.transform.position;
            itemParent.transform.position = new Vector3(itemParent.transform.position.x, itemParent.transform.position.y, 0);
        }

        string[] inv = TXTparser.readFromWholeFile(targetPath);

        for (int i = 0; i < itemParent.transform.childCount; i++)
        {
            if (i < inv.Length && inv[i] != null)
            {
                itemParent.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites\\" + inv[i]);
            }
        }
    }

    public void highlightItem(GameObject givenSprite)
    {
        string itemName = givenSprite.GetComponent<SpriteRenderer>().sprite.name;

        itemParent.transform.GetChild(itemParent.transform.childCount - 1).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(
            "Sprites\\" + itemName
        );

        JSONparser.parseDescriptionJSON(targetDescriptionPath);
        JSONDescriptionnode[] descriptions = JSONparser.nodeDescriptionArray;

        for (int i = 0; i < descriptions.Length; i++)
        {
            if (descriptions[i].id.Equals(itemName))
            {
                itemParent.transform.GetChild(itemParent.transform.childCount - 2).GetComponent<TMP_Text>().text = descriptions[i].name;
                itemParent.transform.GetChild(itemParent.transform.childCount - 3).GetComponent<TMP_Text>().text = descriptions[i].description;
            }
        }
    }
}
