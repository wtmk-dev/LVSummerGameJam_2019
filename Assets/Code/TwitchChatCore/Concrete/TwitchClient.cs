using TwitchLib.Client.Models;
using TwitchLib.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TwitchClient
{
    public Action<string,string> OnMessageReceived;
    public Action<string,string> OnWhisperReceived;

    private Client client;
    private string channelName;
    private string botName;

    public TwitchClient(string channelName, string botName, string botAccessToken)
    {
        Connect(channelName,botName,botAccessToken);

        this.channelName = channelName;
        this.botName = botName;
    }

    private void Connect(string channelName, string botName, string botAccessToken)
    {
        Application.runInBackground = true;
        ConnectionCredentials credentials = new ConnectionCredentials(botName, botAccessToken);
        client = new Client();
        client.Initialize(credentials, channelName);
        client.Connect();

        client.OnMessageReceived += MessageReceived;
        client.OnWhisperReceived += WhisperReceived;
    }

    private void MessageReceived(object sender, TwitchLib.Client.Events.OnMessageReceivedArgs e)
    {
        OnMessageReceived(e.ChatMessage.Username,e.ChatMessage.Message.ToLower());
    }

    private void WhisperReceived(object sender, TwitchLib.Client.Events.OnWhisperReceivedArgs e)
    {
        OnWhisperReceived(e.WhisperMessage.Username, e.WhisperMessage.Message.ToLower());
    }

    public Client GetClient()
    {
        return client;
    }

    public bool InChannel()
    {
        return client.JoinedChannels.Count > 0;
    }

    public void SendMessage(string txt)
    {
        client.SendMessage(client.JoinedChannels[0], txt);
    }

    public void SendWhisper(string userName, string txt)
    {
        Debug.Log(userName + txt);
        client.SendWhisper(userName, txt);
    }
}