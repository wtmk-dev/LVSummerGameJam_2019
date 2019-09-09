using WTMK.Command;
using UnityEngine;
public class SpawnObjectCommand : ICommand
{
    private GameObjectPooler goPooler;
    private Vector3 sapwnPoint;
    private int id;
    //private PoolableData data;
    

    public SpawnObjectCommand(GameObjectPooler goPooler, Vector3 sapwnPoint, int id)
    {
        this.goPooler = goPooler;
        this.sapwnPoint = sapwnPoint;
        this.id = id;
        //this.data = data;
    }

    public void Execute()
    {
        GameObject go = goPooler.GetPoolable();
        IPoolable iPoolable = go.GetComponent<IPoolable>();
        go.transform.position = sapwnPoint;
        Debug.Log("Spawning... " + id);
        iPoolable.Dress(id);
        iPoolable.Act();
    }

    public void Unexecute()
    {

    }
}