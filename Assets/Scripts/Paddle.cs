using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
   public new Rigidbody2D rigidbody { get; private set;}
    public Vector2 direction { get; private set; }

    public float speed = 60f;

    public float maxBounceAngle = 25f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.LeftArrow)){
            this.direction = Vector2.left;
        }else if(Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.RightArrow)){
            this.direction = Vector2.right;
        }else{
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        if(this.direction != Vector2.zero)
        {
            this.rigidbody.AddForce(this.direction * this.speed);
        }
    }

    //for better ball movement when colliding with paddle
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if(ball != null)
        {
            Vector3 paddlePostion = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePostion.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);
            float bounceAngle = (offset / width)* this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle ,-this.maxBounceAngle,this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }

}

