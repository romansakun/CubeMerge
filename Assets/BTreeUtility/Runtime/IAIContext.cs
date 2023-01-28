namespace BTreeUtility
{
    public interface IAIContext
    {
        /// <summary>
        /// Use instead Time.DeltaTime if execution is not every frame
        /// </summary>
        float DeltaTime { get; set; }
    }
}