public static class MathExtension
{
    public static int Pack(int x, int y)
    {
        return x & 0xFFFF | (y << 16);
    }
}