using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WTMK.Command;
public class TIGPoolable : MonoBehaviour , IPoolable
{
    [SerializeField]
    private List<Sprite> sprites;
    [SerializeField]
    private List<PhysicsMaterial2D> physicsMaterials;
    private List<ICommand> commands;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    private GameObjectPooler goPooler;
    private CircleCollider2D boxCollider2D;

    private bool isActive;

    private int id = -1;

    void OnEnable()
    {
        isActive = true;
    }

    void OnDisable()
    {
        isActive = false;
    }

    void FixedUpdate()
    {
        //Act();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log(id);
        Debug.Log(other);

        PlayerMovement pm = other.gameObject.GetComponent<PlayerMovement>();

        if(pm == null)
        {
            return;
        }

        Collect();
    }

    public void Init(GameObjectPooler goPooler)
    {
        this.goPooler = goPooler;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<CircleCollider2D>();
    }

    public void SetCommands(List<ICommand> commands)
    {
        this.commands = commands;
    }

    public void Dress(int id)
    {
        this.id = id;
        spriteRenderer.sprite = sprites[id];
    }

    public void Act()
    {
        if(!isActive)
        {
            return;
        }
        Debug.Log(id + " is acting");
        switch(id)
        {
            case 0:
            //rigidbody2D.AddRelativeForce(new Vector2(2f,3f));
            rigidbody2D.AddTorque(15f);
            rigidbody2D.gravityScale = .2f;
            rigidbody2D.AddForce(new Vector2(150f,100f));
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
            case 1:
            rigidbody2D.AddTorque(8f);
            rigidbody2D.AddForce(new Vector2(400f,-100f));
            rigidbody2D.gravityScale = .3f;
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
            case 2:
            rigidbody2D.AddTorque(8f);
            rigidbody2D.AddForce(new Vector2(-400f,-100f));
            rigidbody2D.gravityScale = .3f;
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
            case 3:
            rigidbody2D.AddTorque(15f);
            rigidbody2D.gravityScale = .2f;
            rigidbody2D.AddForce(new Vector2(-150f,100f));
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
            case 4:
            rigidbody2D.AddTorque(65f);
            rigidbody2D.AddRelativeForce(new Vector2(600f,0f));
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
            case 5:
            rigidbody2D.AddTorque(65f);
            rigidbody2D.AddRelativeForce(new Vector2(-600f,0f));
            boxCollider2D.sharedMaterial = physicsMaterials[id];
            break;
        }
    }

    private void Collect()
    {
        if(!isActive)
        {
            return;
        }
        
        commands[id].Execute();
        goPooler.SetPoolable(gameObject);
    }
}
