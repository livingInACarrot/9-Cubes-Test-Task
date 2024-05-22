using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
1 - red
2 - yellow
3 - blue
4 - purple

=NOTE=
I added just one extra thing - a 0,2 sec cooldown between keystrokes.
It helps to prevent double clicking and makes the program execution more stable.
*/

public class ColorChange : MonoBehaviour
{
    public List<GameObject> cubes;

    public Color red;
    public Color yellow;
    public Color blue;
    public Color purple;
    public Color dflt;

    private char[][] textArray;
    private int[] current;
    private float cooldown;

    void Start()
    {
        string path = @"..\Test task\Assets\Files\test_1.txt";

        using (StreamReader reader = new(path))
        {
            string line;
            int lineNumber = 0;
            while ((line = reader.ReadLine()) != null)
            {
                Array.Resize(ref textArray, lineNumber + 1);
                textArray[lineNumber] = line.ToCharArray();
                ++lineNumber;
            }
        }
        Debug.Log("File reading is completed");

        current = new int[2];
        System.Random rand = new();
        current[0] = rand.Next(0, textArray.Length);
        current[1] = rand.Next(0, textArray[0].Length);
        Recolor();
    }
    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown < 0.2f)
            return;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            --current[0];
            if (current[0] < 0)
                current[0] = textArray.Length - 1;
            cooldown = 0;
            Recolor();
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            ++current[0];
            if (current[0] > textArray.Length - 1)
                current[0] = 0;
            cooldown = 0;
            Recolor();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            --current[1];
            if (current[1] < 0)
                current[1] = textArray[0].Length - 1;
            cooldown = 0;
            Recolor();
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ++current[1];
            if (current[1] > textArray[0].Length - 1)
                current[1] = 0;
            cooldown = 0;
            Recolor();
        }
    }

    private void Recolor()
    {
        Debug.Log("row: " + current[0] + "    col: " + current[1]);
        int k = 0;
        for (int i = -1; i <= 1; ++i)
        {
            for (int j = -1; j <= 1; ++j)
            {
                int row = current[0] + i;
                int col = current[1] + j;

                if (row < 0)
                    row = textArray.Length - 1;
                if (row > textArray.Length - 1)
                    row = 0;
                if (col < 0)
                    col = textArray[0].Length - 1;
                if (col > textArray[0].Length - 1)
                    col = 0;

                cubes[k].GetComponent<MeshRenderer>().material.color = ChooseColor(textArray[row][col]);
                ++k;
            }
        }
    }

    private Color ChooseColor(char x)
    {
        switch (x)
        {
            case '1':
                return red;
            case '2':
                return yellow;
            case '3':
                return blue;
            case '4':
                return purple;
            default:
                return dflt;
        }
    }
}
