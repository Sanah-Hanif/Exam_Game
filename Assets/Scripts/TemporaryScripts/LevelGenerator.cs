using UnityEngine;

[System.Serializable]
public class ColourToPrefab
{
    public Color col;
    public GameObject prefab;
}

public class LevelGenerator : MonoBehaviour
{
    public Texture2D level;
    public ColourToPrefab[] entries;
    public float startX, startY;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel ()
    {
        for (int i = 0; i < level.width; i++)
        {
            for (int j = 0; j < level.height; j++)
            {
                GenerateBlock(i, j);
            }
        }
    }

    void GenerateBlock(int x, int y)
    {
        Color myColour = level.GetPixel(x, y);
        if (myColour.a == 0)
            return;

        foreach(ColourToPrefab entry in entries)
        {
            if (entry.col.Equals(myColour))
            {
                Vector2 pos = new Vector2 (startX + x, startY + y);
                Instantiate(entry.prefab, pos, Quaternion.identity, transform);
            }
        }
    }
}
