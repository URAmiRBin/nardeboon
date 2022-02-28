public static class ListUtils {
    public static int ClampListIndex(int index, int listSize) {
        return ((index % listSize) + listSize) % listSize;
    }
}
