using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField]
    Transform[] startingPosiitonsArr;

  
  public  GameObject[] rooms; //index 0 --> LR,index 1 --> LRB,index 2 --> LRT,index 3 --> LRBT

    private int roomDirection;
    public float moveAmount;
    private float timeBtwRoom, startTimeBtwRoom=0.25f;

    public bool hasToStop;

    public float minX, maxX, minY;

    public LayerMask roomLayer;
    private int downCounter;
    
    void Start()
    {
        int randomStartingPositions = Random.Range(0, startingPosiitonsArr.Length);
        transform.position = startingPosiitonsArr[randomStartingPositions].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);
        roomDirection = Random.Range(1, 6);
    }


    private void Update()
    {

        if (timeBtwRoom <= 0 && hasToStop == false)
        {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        }
        else
        {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    void Move()
    {
       
        if (roomDirection == 1 || roomDirection == 2)//move right
        {
            downCounter = 0;

            if (transform.position.x < maxX)
            {
                Vector2 newPosition = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPosition;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);


                roomDirection = Random.Range(1, 6);

                if (roomDirection == 3)
                {
                    roomDirection = 2;
                }else if (roomDirection == 4)
                {
                    roomDirection = 5;
                }
            }
            else
            {
                roomDirection = 5;
            }
           
        }
        else if (roomDirection == 3 || roomDirection == 4)//move left
        {
            downCounter = 0;
            if (transform.position.x > minX)
            {
                Vector2 newPosition = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPosition;

                int rand = Random.Range(0, rooms.Length);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);


                roomDirection = Random.Range(3, 6);

               
            }
            else
            {
                roomDirection = 5;
            }
            
        }
        else if (roomDirection == 5 )//move down
        {
            downCounter++;

            if (transform.position.y > minY)
            {
                Collider2D roomDetector = Physics2D.OverlapCircle(transform.position, 1, roomLayer);
                if(roomDetector.GetComponent<RoomType>().type != 1 && roomDetector.GetComponent<RoomType>().type != 3)
                {
                    if (downCounter >= 2)
                    {
                        roomDetector.GetComponent<RoomType>().DesctroyRoom();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomDetector.GetComponent<RoomType>().DesctroyRoom();

                        int randRoomBottom = Random.Range(1, 4);

                        if (randRoomBottom == 2)
                        {
                            randRoomBottom = 1;
                        }
                        Instantiate(rooms[randRoomBottom], transform.position, Quaternion.identity);
                    }
                   

                }



                Vector2 newPosition = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPosition;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                roomDirection = Random.Range(1, 6);
            }
            else
            {
                hasToStop = true;
            }
           
        }
       
        
    }
}
