using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool Visited = false;
    public int DistanceFromStart;
}

public class MazeGenerator
{
    public int Width = 23;
    public int Height = 15;

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeft = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottom = false;
        }

        RemoveWallsWithBacktracker(maze);

        PlaceMazeExit(maze);

        return maze;
    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0) furthest.WallLeft = false;
        else if (furthest.Y == 0) furthest.WallBottom = false;
        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
        else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;
    }
}