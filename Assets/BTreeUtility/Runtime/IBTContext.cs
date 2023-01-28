namespace BTreeUtility
{
    public interface IBTContext
    {
        /// <summary>
        /// It is time between BTClient executions
        /// </summary>
        float DeltaTime { get; set; }
    }
}