using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] levelPrefabs;
    [SerializeField] private float levelLength;
    public GameObject exit;
    private int LevelCount
    {
        get
        {
            return transform.childCount - (exit == null ? 0 : 1);
        }
    }
    private NavMeshSurface surface;
    private GameObject latestLevel;

    private void OnValidate()
    {
        if(surface==null) surface = GetComponent<NavMeshSurface>();
    }

    private void GenerateDaExit()
    {
        if (exit == null)
        {
            Transform temp = transform.Find("Exit");
            if (temp == null)
            {
                exit = new GameObject("Exit");
                exit.transform.parent = gameObject.transform;
            }
            else exit = temp.gameObject;
        }

        exit.transform.localPosition = new Vector3(0, 0, levelLength * LevelCount);
        //Debug.Log("Exit is at: " + exit.transform.position);
    }
    private void Awake()
    {
        GenerateDaExit();


        if (surface == null) surface = GetComponent<NavMeshSurface>();
        surface.BuildNavMesh();
    }

    public void GenerateNewLevel(int count = 1)
    {
        GenerateDaExit();
        for(int i = 0; i < count; i++)
        {
            latestLevel = Instantiate(
                    levelPrefabs[Random.Range(0, levelPrefabs.Length)],
                    exit.transform.position,
                    Quaternion.identity,
                    gameObject.transform);
            latestLevel.name = LevelCount.ToString();
            exit.transform.localPosition = new Vector3(0, 0, levelLength * LevelCount);
        }
        surface.BuildNavMesh();
        //Debug.Log("New level generated! There are " + LevelCount + " levels, now.");
    }

    public void DestroyAllLevels()
    {
        GenerateDaExit();
        foreach(Transform levelTransform in transform)
        {
            if (levelTransform.gameObject.name != "Exit") Destroy(levelTransform.gameObject);
        }
        exit.transform.localPosition = Vector3.zero;
        surface.BuildNavMesh();
    }
    
    public void DestroyAllLevelsImmediately()
    {
        GenerateDaExit();
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.name != "Exit")
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
                i--;
            }
        }
        exit.transform.localPosition = Vector3.zero;
        surface.BuildNavMesh();
    }
}
