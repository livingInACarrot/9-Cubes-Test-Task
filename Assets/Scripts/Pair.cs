public class Pair<T, U>
{
    public Pair() 
    {
    }

    public Pair(T row, U col)
    {
        Row = row;
        Col = col;
    }

    public T Row { get; set; }
    public U Col { get; set; }
};