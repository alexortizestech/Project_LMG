using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class Enemies : ScriptableObject
{
    public string EnemyName;

    public int EnemyHealth;
    public Vector3[] spawnPoints;

}

// Update is called once per frame


