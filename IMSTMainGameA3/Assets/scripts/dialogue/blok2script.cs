using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class blok2script : MonoBehaviour
{
public NPCConversation Conversation;

 public speler player;

 private void Update() { if (ConversationManager.Instance != null) {
 if (ConversationManager.Instance.IsConversationActive)
 {
 if (Input.GetKeyDown(KeyCode.UpArrow))
 ConversationManager.Instance.SelectPreviousOption();

 else if (Input.GetKeyDown(KeyCode.DownArrow))
 ConversationManager.Instance.SelectNextOption();

 else if (Input.GetKeyDown(KeyCode.F))
 ConversationManager.Instance.PressSelectedOption();
 }
 } 
        if(player.heeftPlayer2Gekozen == true){
            ConversationManager.Instance.SetBool("blok1heeft2gekozen", true);
        }
 }


public void interactieTest1(){
ConversationManager.Instance.StartConversation(Conversation);

}


}
