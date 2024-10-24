public static class GachaPetStatic
{
    private static GachaPetWinningsMono _bindedObject = null;

    public static bool CanBinding => _bindedObject == null;

    public static bool IsOpeningNow { get; private set; } = false;

    public static void Bind(GachaPetWinningsMono gacha)
    {
        if (CanBinding == false)
            return;

        _bindedObject = gacha;
    }

    public static void Unbind(GachaPetWinningsMono gacha)
    {
        if (_bindedObject == gacha)
            _bindedObject = null;
    }

    public static void RegisterOpening()
    {
        if (IsOpeningNow == false)
            IsOpeningNow = true;
    }

    public static void UnregisterOpening()
    {
        if (IsOpeningNow == true)
            IsOpeningNow = false;
    }
}