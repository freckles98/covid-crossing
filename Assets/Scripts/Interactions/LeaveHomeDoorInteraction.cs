using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// LeaveHomeDoorInteraction inherits from the Interaction class and allows for the player to interact with the door, which lets the player exit the living room and enter the shop.
/// </summary>
public class LeaveHomeDoorInteraction : Interaction
{
    public IronBoardInteraction ironBoardInteraction;
    public string toLoad;
    public static bool choseWear; // choose to wear mask used by MaskShopActivaion

    /// <summary>
    /// An overriden class from Interaction that creates dialogue when the player interats with the door in the living room, this causes a dialogue to pop up asking the player a set of questions about whether they want to wear a mask and correct mask usage. Players will also incur a penalty if they have not previously ironed their mask before exiting the house.
    /// </summary>
    protected override void Interact()
    {
        //when click on door: initiate dialogue asking to wear mask

        SetWasUsedToday(); //how check if used today

        ScoreManager scoreManager = SharedCanvas.Instance.scoreManager;
        bool woreMask = false;
        void onWearMaskOverNose()
        {
            scoreManager.ChangeCommunityScore(15);
            scoreManager.ChangePersonalScore(-5);
            ChangeScene();
            choseWear = true;
        }
        void onWearMaskNoNose()
        {
            scoreManager.ChangePersonalScore(5); //plus for comfort
            scoreManager.ChangeCommunityScore(-10); //small penalty
            ChangeScene();
            choseWear = true;
        }
        void onDenyMask()
        {
            scoreManager.ChangeCommunityScore(-30); //big community penalty
            ChangeScene();
            choseWear = false;
        }
        // Checks whether the player ironed their mask before going to the shop and decreases community score if they did not.
        void WoreMask()
        {
            woreMask = true;
            if (woreMask && !ironBoardInteraction.WasUsedToday())
            {
                DialogueElement secondDialogue = new DialogueElement(null, "You forgot to iron your mask. Ironing your mask would reduce germs in your mask.", () => scoreManager.ChangeCommunityScore(-15), new DialogueTerminal("OK"));

                SharedCanvas.Instance.dialogueManager.RunDialogue(secondDialogue);
            }
            
        }

        void ChangeScene()
        {
            SharedCanvas.Instance.previousRoom = SceneManager.GetActiveScene().name; //set to current room
            SceneManager.LoadScene(toLoad, LoadSceneMode.Single); //then change scene
        }

        DialogueElement dialogue = new DialogueElement(null, "You are about to go shopping, would you like to wear your mask?", null,
            new DialogueElement("Yes", "Responsible choice. How would you like to wear it?", null,
                new DialogueElement("Over mouth and nose", "The best way to wear it, with minor discomfort.", onWearMaskOverNose,
                    new DialogueTerminal("OK", WoreMask)),
                new DialogueElement("Under the nose", "Slightly more comfortable, but not as safe for you and those around you.", onWearMaskNoNose,
                    new DialogueTerminal("OK", WoreMask))),
            new DialogueElement("No", "Not a very responsible choice.", onDenyMask,
                new DialogueTerminal("OK")));;

        SharedCanvas.Instance.dialogueManager.RunDialogue(dialogue);
    }

    protected override string GetName() => "LeaveHomeDoor";
}


