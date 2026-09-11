namespace Deucarian.UIBinding
{
    /// <summary>Typed handle issued for an actual row; it cannot remove a replacement view or another list's row.</summary>
    public sealed class UIListItemHandle<T>
    {
        internal UIListItemHandle(object owner, object key, ISettableItem<T> view)
        { Owner = owner; Key = key; View = view; }
        internal object Owner { get; }
        internal object Key { get; }
        internal ISettableItem<T> View { get; }
    }
}
