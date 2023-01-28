namespace BTreeUtility
{
    public interface IBTContext
    {
        /// <summary>
        /// Use instead Time.DeltaTime if execution is not every frame
        /// </summary>
        float DeltaTime { get; set; }
    }
}