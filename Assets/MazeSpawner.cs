using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(x, y), Quaternion.identity);

                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
            }
        }
    }
}