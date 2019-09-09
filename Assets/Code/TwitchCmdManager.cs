using System.Collections.Generic;
using System.IO;
using SimpleJSON;
using UnityEngine;
public class TwitchCmdManager
{
    private List<string> validCmds;
    private string cmdData;
    private int totalCmds;

    public TwitchCmdManager(string data)
    {
        Init(data);
    }

    private void Init(string data)
    {
        validCmds = new List<string>();
        var json = JSON.Parse(data);
        
        totalCmds = json["total"];
        var count = 0;
        do
        {
            validCmds.Add(json["cmd"][count]);
            Debug.Log(json["cmd"][count]);
            count++;
        }while(count < totalCmds);

        totalCmds = json["negTotal"];

        count = 0;
        do
        {
            validCmds.Add(json["negCmd"][count]);
            Debug.Log(json["negCmd"][count]);
            count++;
        }while(count < totalCmds);
    }

    public List<string> GetValidCmds()
    {
        return validCmds;
    }
}