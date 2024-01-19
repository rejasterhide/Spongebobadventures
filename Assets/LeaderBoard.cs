using UnityEngine;
using System.Linq;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class LeaderBord : MonoBehaviour
{
    public GameObject PlayerHolder;

    [Header("Options")] public float refreshRate = 1f;

    [Header("UI")]
     public GameObject[] slots;

    [Space] public TextMeshProUGUI scoreText;

    public TextMeshProUGUI[] nameTexts;

    public TextMeshProUGUI[] scoreTexts;
    
    private void Start(){
        InvokeRepeating(nameof(Refresh), 1f,refreshRate);
    }
    public void Refresh(){
        foreach(var slot in slots){
            slot.SetActive(false);
        }
       var sortedPlayerList = (from Player in PhotonNetwork.PlayerList orderby Player.GetScore() descending select Player).ToList();
        var i=0;
        foreach (var Player in sortedPlayerList){
               slots[i].SetActive(true);
               if(Player.NickName == "")
                Player.NickName = "unnamed";
                nameTexts[i].text = Player.NickName;
                scoreTexts[i].text = Player.GetScore().ToString();
                i++;
        }   
    }
    private void Update(){
        PlayerHolder.SetActive(Input.GetKey(KeyCode.Tab));
    }
}
