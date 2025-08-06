public class GameIdGenerator
{
    protected static long nextId = 1;

    public static long GetNextId()
    {
        return nextId++;
    }
}
