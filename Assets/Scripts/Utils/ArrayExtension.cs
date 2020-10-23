
public static class ArrayExtension
{
    public static T GetRandomElement<T>(this T[] array)
    {
        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }
}