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

public class ConversationController : MonoBehaviour
{
    public TextAsset conversationTree; // conversation JSON text file
    public Sprite backdropSprite; // sprite for the backdrop when the conversation pops up
    public Sprite playerSprite; // sprite for the player in the conversation
    public Sprite NPCSprite; // sprite for the NPC in the conversation
    public GameObject playerChoice1; // choice 1
    public GameObject playerChoice2; // choice 2
    public GameObject playerChoice3; // choice 3
    public TMP_Text NPCDialogue; // dialogue

    private JSONReader parser; // JSON parsing script
    private string currentNode; // the current node
    private GameObject stageParent; // the parent of all of the conversation sprites and buttons

    private void Start()
    {
        // Turning the stage off to start
        stageParent = transform.GetChild(0).gameObject;
        stageParent.SetActive(false);
    }

    // Method to load the first node and initalize sprites
    public void startConversation()
    {
        stageParent.SetActive(true);

        // Loading the node
        parser = GetComponent<JSONReader>();
        parser.parseJSON(conversationTree);
        loadSpecificNode("0");

        // Loading the sprites
        SpriteRenderer[] sprites = transform.GetComponentsInChildren<SpriteRenderer>();
        sprites[0].sprite = backdropSprite;
        sprites[1].sprite = playerSprite;
        sprites[2].sprite = NPCSprite;
    }

    // Method to load a specific node
    private void loadSpecificNode(string givenNode)
    {
        currentNode = givenNode;

        try
        {
            NPCDialogue.text = parser.getDialogueFromID(currentNode);
            playerChoice1.GetComponentInChildren<TMP_Text>().text = parser.getResponsesFromID(currentNode)[0];
            playerChoice2.GetComponentInChildren<TMP_Text>().text = parser.getResponsesFromID(currentNode)[1];
            playerChoice3.GetComponentInChildren<TMP_Text>().text = parser.getResponsesFromID(currentNode)[2];
        }
        // When the nodes run out
        catch
        {
            stageParent.SetActive(false);
            
            // DO SOME EFFECT HERE AFTER WINNING OR LOSING
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
}
