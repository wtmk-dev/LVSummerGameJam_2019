using WTMK.Command;
using UnityEngine;
public class PlayerCollectCommand : ICommand
{
    private PlayerModel playerModel;
    private int value;


    public PlayerCollectCommand(PlayerModel playerModel, int value)
    {
        this.playerModel = playerModel;
        this.value = value;
    }

    public void Execute()
    {

        Debug.Log(value);
        switch(value)
        {
            case 0:
            playerModel.CurrentConfidence += 0.1f;
            break;
            case 1:
            playerModel.CurrentConfidence += 0.07f;
            break;
            case 2:
            playerModel.CurrentConfidence += 0.04f;
            break;
            case 3:
            playerModel.CurrentConfidence += 0.01f;
            break;
            case 4:
            playerModel.CurrentConfidence -= 0.5f;
            break;
            case 5:
            playerModel.CurrentConfidence -= 0.2f;
            break;
            case 6:
            playerModel.CurrentConfidence -= 0.1f;
            break;

        }
    }

    public void Unexecute()
    {

    }
}