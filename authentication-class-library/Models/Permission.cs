namespace authentication_class_library.Models
{
    /// <summary>
    /// Permission enum contains all permissions
    /// </summary>
    [Flags]
    public enum Permission
    {
        USER,
        ADMIN
    }
}
