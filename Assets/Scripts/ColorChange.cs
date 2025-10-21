using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private const float PRESS_COOLDOWN = 0.2f;
    private readonly string PATH = Path.Combine(Directory.GetCurrentDirectory(), "Assets", "Files", "test_1.txt");

    [Tooltip("Нулевым цветом должен быть дефолтный (например, черный), а далее в таком порядке: красный, жёлтый, синий, фиолетовый")]
    [SerializeField] private List<Color> colors;

    private Cube[] cubes;
    private int[][] textArray;
    private Pair<int, int> current;
    private float cooldown;

    public void Start()
    {
        cubes = GetComponentsInChildren<Cube>();

        InitializeTextArray();

        System.Random rand = new();
        current = new(rand.Next(0, textArray.Length), rand.Next(0, textArray[0].Length));

        Recolor();
    }

    public void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown < PRESS_COOLDOWN)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            current.Row = CorrectCoordinate(current.Row - 1, textArray.Length);
            OnPressed();
        }

        if (Input.GetKey(KeyCode.S))
        {
            current.Row = CorrectCoordinate(current.Row + 1, textArray.Length);
            OnPressed();
        }

        if (Input.GetKey(KeyCode.A))
        {
            current.Col = CorrectCoordinate(current.Col - 1, textArray[0].Length);
            OnPressed();
        }

        if (Input.GetKey(KeyCode.D))
        {
            current.Col = CorrectCoordinate(current.Col + 1, textArray[0].Length);
            OnPressed();
        }
    }

    private void InitializeTextArray()
    {
        using StreamReader reader = new(PATH);
        string line;
        int lineNumber = 0;
        while ((line = reader.ReadLine()) != null)
        {
            Array.Resize(ref textArray, lineNumber + 1);
            textArray[lineNumber] = new int[line.Length];
            for (int i = 0; i < line.Length; ++i)
            {
                textArray[lineNumber][i] = line[i] - '0';
            }
            ++lineNumber;
        }
    }

    private void Recolor()
    {
        int k = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                int row = CorrectCoordinate(current.Row + i, textArray.Length);
                int col = CorrectCoordinate(current.Col + j, textArray[0].Length);
                cubes[k].Recolor(colors[textArray[row][col]]);
                ++k;
            }
        }
    }

    private int CorrectCoordinate(int current, int limit)
    {
        if (current < 0)
            return limit - 1;
        else if (current >= limit)
            return current - limit;
        else
            return current;
    }

    private void OnPressed()
    {
        cooldown = 0;
        Recolor();
    }
}
