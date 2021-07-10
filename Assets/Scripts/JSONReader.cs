/*
 * Project name: Souperhero
 * Team name: The Good Apples
 * Game Jam Name: GAMERella Global 2021
 * 
 * Script description: Quick an easy methods to retrieve JSON data for conversations, and parse it properly
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Full tree
[Serializable]
public class JSONtree { public JSONnode[] nodes; }

// Individual nodes
[Serializable]
public class JSONnode
{
    public string id; // identifier
    public string dialogue; // NPC dialogue
    public string response1; // player response 1
    public string response2; // player response 2
    public string response3; // player response 3
}

public class JSONReader : MonoBehaviour
{
    private JSONtree myTree; // local tree of conversation nodes
    private JSONnode[] nodeArray; // array of nodes in the tree
    private int nodeArrayLength; // length of the number of nodes in the tree

    public void parseJSON(TextAsset givenFile)
    {
        myTree = JsonUtility.FromJson<JSONtree>(givenFile.text); // Parsing the JSON file

        // Getting the tree array and its length
        nodeArray = myTree.nodes;
        nodeArrayLength = myTree.nodes.Length;
    }

    // Method to quickly return NPC dialogue from a given ID
    public string getDialogueFromID(string givenID)
    {
        string temp = null;

        for (int i = 0; i < nodeArrayLength; i++)
        {
            if (nodeArray[i].id.Equals(givenID))
            {
                temp = nodeArray[i].dialogue;
            }
        }

        return temp;
    }

    // Method to quickly return an array of player responses from a given ID
    public string[] getResponsesFromID(string givenID)
    {
        string[] temp = null;

        for (int i = 0; i < nodeArrayLength; i++)
        {
            if (nodeArray[i].id.Equals(givenID))
            {
                temp = new string[3];

                temp[0] = nodeArray[i].response1;
                temp[1] = nodeArray[i].response2;
                temp[2] = nodeArray[i].response3;
            }
        }

        return temp;
    }
}
