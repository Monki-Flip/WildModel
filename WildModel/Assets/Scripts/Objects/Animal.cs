using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Animal : MonoBehaviour
{
    public string AnimalType;
    private bool IsMoving;

    private void Update()
    {
        Debug.Log(IsMoving + "     " + gameObject.name);
    }

    public IEnumerator MakeRandomMoves(int movesCount)
    {
        for (var i = 0; i < movesCount; i++)
        {
            yield return StartCoroutine(MakeRandomMove());
            yield return new WaitUntil(() => !IsMoving);
        }
    }


    public IEnumerator MakeRandomMove()
    {
        IsMoving = true;
        //Debug.Log("Á‡¯ÂÎ" + AnimalType);
        var rnd = new System.Random();
        var randTime = rnd.NextDouble();
        //yield return new WaitForSeconds((float)randTime);

        var currentCell = gameObject.GetComponentInParent<Cell>();
        var nextCells = currentCell.Neighbors.Where(x => x.GetComponent<Cell>().Name != "Mountain" && x.GetComponent<Cell>().Name != "Water" && !x.GetComponent<Cell>().IsAnimalOn).ToList();
        if (nextCells.Count != 0)
        {
            var randCell = nextCells[rnd.Next(0, nextCells.Count)];
            gameObject.transform.parent = randCell.transform;
            randCell.GetComponent<Cell>().Animal = gameObject;
            randCell.GetComponent<Cell>().IsAnimalOn = true;
            currentCell.IsAnimalOn = false;
            currentCell.Animal = null;

            var totalMovementTime = 0.75f; //the amount of time you want the movement to take
            var currentMovementTime = 0f; //The amount of time that has passed
            var origin = transform.position;
            var nextPos = randCell.transform.position + new Vector3(0f, 0.2f, 0f);
            while (Vector3.Distance(transform.position, nextPos) > 0)
            {
                //Debug.Log("·Â„Û");
                currentMovementTime += Time.deltaTime;
                transform.position = Vector3.Lerp(origin, nextPos, currentMovementTime / totalMovementTime);
                yield return null;
            }
            //Debug.Log("œ¿’¿ƒ»À" + AnimalType);
        }

        IsMoving = false;
    }
}