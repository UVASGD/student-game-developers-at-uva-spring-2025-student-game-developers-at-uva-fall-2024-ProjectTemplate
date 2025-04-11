using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public MazeCell mazeCellPrefab;
    public int mazeWidth;
    public int mazeDepth;
    public MazeCell[][] mazeGrid;
    private float _prefabLocalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _prefabLocalScale = mazeCellPrefab.transform.localScale.x;
        CreateMaze();
    }

    public void CreateMaze(){
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }
        mazeGrid = new MazeCell[mazeWidth][];
        for (int x = 0; x < mazeWidth; x++)
        {
            mazeGrid[x] = new MazeCell[mazeDepth];
            for (int z = 0; z < mazeDepth; z++)
            {
                MazeCell newCell = Instantiate(mazeCellPrefab, transform, false);
                newCell.transform.localPosition = new Vector3(x * _prefabLocalScale, 0, z * _prefabLocalScale);
                mazeGrid[x][z] = newCell;
            }
        }

        GenerateMaze(null, mazeGrid[0][0]);
        mazeGrid[mazeWidth / 2][0]?.ClearBackWall();
        mazeGrid[0][mazeDepth / 2]?.ClearLeftWall();
        mazeGrid[mazeWidth - 1][mazeDepth / 2]?.ClearRightWall();
    }

    void GenerateMaze(MazeCell previousCell, MazeCell currentCell){
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null){
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    MazeCell GetNextUnvisitedCell(MazeCell currentCell){
        var unvisitedCells = GetUnvisitedcells(currentCell);
        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    IEnumerable<MazeCell> GetUnvisitedcells(MazeCell currentCell){
        int x = (int)(currentCell.transform.localPosition.x / _prefabLocalScale);
        int z = (int)(currentCell.transform.localPosition.z / _prefabLocalScale);
        if (x+1 < mazeWidth){
            var cellToRight = mazeGrid[x+1][z];
            if (cellToRight.isVisited == false){
                yield return cellToRight;
            }
        }

        if (x-1 >= 0){
            var cellToLeft = mazeGrid[x-1][z];
            if (cellToLeft.isVisited == false){
                yield return cellToLeft;
            }
        }

        if (z+1 < mazeDepth){
            var cellToFront = mazeGrid[x][z+1];
            if (cellToFront.isVisited == false){
                yield return cellToFront;
            }
        }

        if (z-1 >= 0){
            var cellToBack = mazeGrid[x][z-1];
            if (cellToBack.isVisited == false){
                yield return cellToBack;
            }
        }
    }

    void ClearWalls(MazeCell previousCell, MazeCell currentCell){
        if (previousCell == null){
            return;
        }

        if (previousCell.transform.localPosition.x < currentCell.transform.localPosition.x){
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x){
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z){
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z){
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }
}
