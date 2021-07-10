using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public string recipeFilePath, spoonsFilePath, bowlsFilePath;
    public Vector2 incrementValues;

    private GameObject stageParent; // the parent of all of the conversation sprites and buttons
    private TXTReader parser; // TXT parsing script
    private GameObject itemsParent;

    private void Start()
    {
        // Turning the stage off to start
        stageParent = transform.GetChild(0).gameObject;
        stageParent.SetActive(false);

        startInventory();
    }

    public void startInventory()
    {
        stageParent.SetActive(true);
        parser = GetComponent<TXTReader>();

        itemsParent = stageParent.transform.GetChild(4).gameObject;

        loadTab("recipes");
    }

    public void loadTab(string inventoryType)
    {
        string targetPath;

        switch (inventoryType)
        {
            case "recipes":
                targetPath = recipeFilePath;
                break;
            case "spoons":
                targetPath = spoonsFilePath;
                break;
            case "bowls":
                targetPath = bowlsFilePath;
                break;
            default:
                targetPath = recipeFilePath;
                break;
        }

        if (itemsParent.transform.childCount > 0)
        {
            for (int i = 0; i < itemsParent.transform.childCount; i++)
            {
                Destroy(itemsParent.transform.GetChild(i).gameObject);
            }
        }

        string[] items = parser.readFromWholeFile(targetPath);
        Vector2 startingPosition = new Vector2(-5.5f, 2);
        float itemOffset = 0;

        for (int j = 0; j < items.Length; j++)
        {
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Inventory_Item"), itemsParent.transform);

            newItem.GetComponentInChildren<TMP_Text>().text = items[j];
            newItem.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(items[j]);

            newItem.transform.position = new Vector3(itemOffset + startingPosition.x, startingPosition.y, 0);
            itemOffset += incrementValues.x;

            if (j == 1 || j == 3 || j == 5 || j == 7 || j == 9 || j == 11)
            {
                startingPosition = new Vector2(startingPosition.x, startingPosition.y - incrementValues.y);
                itemOffset = 0;
            }
        }
    }
}
