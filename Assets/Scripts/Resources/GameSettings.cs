 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Scripts/Resources/ScriptableObjects/GameSettings.asset")]

public class GameSettings : ScriptableObject
{
    [SerializeField]
    private string _gameversion = "0.0.0";
    public string GameVersion { get { return _gameversion; } }

    [SerializeField]
    private string _nickname = "CallEppz";
    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickname + value.ToString();
        }
    }
}
