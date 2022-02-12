using UnityEngine;

public class Move : MonoBehaviour
{
    private const float START_X = -5F;
    private const float END_X = 7.5F;
    private const float RESET_SPEED = 2F;
    private bool _collision = false;
    private bool _stop = false;

    public float Speed = 1F;

    public void Update()
    {
        if (this._stop)
        {
            return;
        }

        Vector2 factor;
        if (!this._collision)
        {
            factor = Vector2.right * (this.Speed * Time.deltaTime);
        }
        else
        {
            factor = Vector2.left * (RESET_SPEED * Time.deltaTime);
        }
        this.transform.Translate(factor, Space.World);

        switch (this._collision)
        {
            case false when this.transform.position.x >= END_X:
                this._stop = true;
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case true when this.transform.position.x <= START_X:
                this._collision = false;
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        this._collision = true;
    }
}
