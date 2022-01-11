using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public List<GameObject> List;

    public void MakeRandomMoves()
    {
        foreach(var animal in List)
            StartCoroutine(animal.GetComponent<Animal>().MakeRandomMoves(5));

    }

    public int GetDeersCount()
    {
        var count = 0;
        foreach(var animal in List)
            if (animal.GetComponent<Animal>().AnimalType == "Deer")
                count++;
        return count;
    }

    public int GetWolvesCount()
    {
        var count = 0;
        foreach (var animal in List)
            if (animal.GetComponent<Animal>().AnimalType == "Wolf")
                count++;
        return count;
    }

    public void DeleteRandomDeer()
    {
        foreach (var animal in List)
            if (animal.GetComponent<Animal>().AnimalType == "Deer")
            {
                List.Remove(animal);
                Destroy(animal);
                break;
            }
    }

    public void DeleteRandomWolf()
    {
        foreach (var animal in List)
            if (animal.GetComponent<Animal>().AnimalType == "Wolf")
            {
                List.Remove(animal);
                Destroy(animal);
                break;
            }
    }
}
