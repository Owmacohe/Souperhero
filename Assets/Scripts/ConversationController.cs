/*
 * Project name: Souperhero
 * Team name: The Good Apples
 * Game Jam Name: GAMERella Global 2021
 * 
 * Script description: Management script to load, iterate through, and display conversation nodes
 */

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConversationController : MonoBehaviour
{
    public GameObject conversationTarget;
    public TextAsset[] conversationTrees; // conversation JSON text file
    public Sprite backdropSprite; // sprite for the backdrop when the conversation pops up
    public Sprite playerSprite; // sprite for the player in the conversation
    public Sprite NPCSprite; // sprite for the NPC in the conversation
    public GameObject playerChoice1; // choice 1
    public GameObject playerChoice2; // choice 2
    public GameObject playerChoice3; // choice 3
    public TMP_Text NPCDialogue; // dialogue
    public bool doesGiveItem;
    public string recipeFilePath, spoonsFilePath, bowlsFilePath;
    public string recipeListFilePath, spoonsListFilePath, bowlsListFilePath;

    private TXTReader TXTparser; // TXT parsing script
    private JSONReader JSONparser; // JSON parsing script
    private string currentNode; // the current node
    private GameObject stageParent; // the parent of all of the conversation sprites and buttons
    private bool isHittingPlayer;
    private GameObject player;
    private bool isExhausted;

    private void Start()
    {
        isExhausted = false;

        player = GameObject.FindGameObjectWithTag("Player");

        // Turning the stage off to start
        stageParent = transform.GetChild(0).gameObject;
        stageParent.SetActive(false);

        exitConversation();
        GetComponent<CircleCollider2D>().offset = new Vector2(
            gameObject.transform.InverseTransformPoint(conversationTarget.transform.position).x,
            gameObject.transform.InverseTransformPoint(conversationTarget.transform.position).y - 1f
        );
    }

    private void Update()
    {
        if (!isExhausted && Input.GetKeyDown(KeyCode.E) && isHittingPlayer)
        {
            if (stageParent.activeSelf)
            {
                exitConversation();
            }
            else
            {
                startConversation();
            }
        }
    }

    // Method to load the first node and initalize sprites
    public void startConversation()
    {
        stageParent.SetActive(true);
        conversationTarget.SetActive(false);
        player.SetActive(false);

        if (doesGiveItem)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + 2, gameObject.transform.position.y - 2.5f);
        }

        // Loading the node
        TXTparser = GetComponent<TXTReader>();
        JSONparser = GetComponent<JSONReader>();
        JSONparser.parseJSON(conversationTrees[Mathf.RoundToInt(Random.Range(0, conversationTrees.Length - 1))]);
        loadSpecificNode("0");

        // Loading the sprites
        SpriteRenderer[] sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        sprites[0].sprite = backdropSprite;
        sprites[1].sprite = playerSprite;
        sprites[2].sprite = NPCSprite;
    }

    public void exitConversation()
    {
        stageParent.SetActive(false);
        conversationTarget.SetActive(true);
        player.SetActive(true);
    }

    // Method to load a specific node
    private void loadSpecificNode(string givenNode)
    {
        currentNode = givenNode;

        try
        {
            NPCDialogue.text = JSONparser.getDialogueFromID(currentNode);
            playerChoice1.GetComponentInChildren<TMP_Text>().text = JSONparser.getResponsesFromID(currentNode)[0];
            playerChoice2.GetComponentInChildren<TMP_Text>().text = JSONparser.getResponsesFromID(currentNode)[1];
            playerChoice3.GetComponentInChildren<TMP_Text>().text = JSONparser.getResponsesFromID(currentNode)[2];
        }
        // When the nodes run out
        catch
        {
            exitConversation();
            isExhausted = true;

            if (doesGiveItem)
            {
                string[] itemList;

                switch (Mathf.RoundToInt(Random.Range(0, 2))) {
                    case 0:
                        itemList = TXTparser.readFromWholeFile(recipeListFilePath);
                        TXTparser.writeToEndOfFile(recipeFilePath, itemList[Mathf.RoundToInt(Random.Range(0, itemList.Length - 1))]);
                        break;
                    case 1:
                        itemList = TXTparser.readFromWholeFile(spoonsListFilePath);
                        TXTparser.writeToEndOfFile(spoonsFilePath, itemList[Mathf.RoundToInt(Random.Range(0, itemList.Length - 1))]);
                        break;
                    case 2:
                        itemList = TXTparser.readFromWholeFile(bowlsListFilePath);
                        TXTparser.writeToEndOfFile(bowlsFilePath, itemList[Mathf.RoundToInt(Random.Range(0, itemList.Length - 1))]);
                        break;
                }
            }

            player.GetComponent<PlayerMovements>().talkCount++;

            if (player.GetComponent<PlayerMovements>().talkCount >= 2)
            {
                SceneManager.LoadScene("Epilogue");
            }
        }
    }

    // Method to load a specific node choice
    public void loadNextNode(int dialogueChoice)
    {
        // Adding the appropriate suffix for the next node
        switch (dialogueChoice)
        {
            case 1:
                currentNode = currentNode + ".0";
                break;
            case 2:
                currentNode = currentNode + ".1";
                break;
            case 3:
                currentNode = currentNode + ".2";
                break;
        }

        loadSpecificNode(currentNode); // Loading the next node
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHittingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHittingPlayer = false;
    }
}
