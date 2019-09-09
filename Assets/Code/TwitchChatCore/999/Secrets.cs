using SimpleJSON;
using UnityEngine;
public class Secrets
{

    public string client_id;
    public string client_secret;
    public string channel_name;
    public string bot0_name;
    public string bot0_access_token;
    public string bo0_refresh_token;

    public Secrets ( string data )
    {
        Init(data);
    }

    private void Init( string data )
    {
        var json = JSON.Parse(data);
        
        client_id = json["keys"]["client_id"];
        client_secret = json["keys"]["client_secret"];
        channel_name = json["keys"]["channel_name"];
        bot0_name = json["keys"]["bot0_name"];
        bot0_access_token = json["keys"]["bot0_access_token"];
        bo0_refresh_token = json["keys"]["bo0_refresh_token"];
    }

    public string GetBotAccessTokenByName(string botName)
    {
        return bot0_access_token;
    }
}