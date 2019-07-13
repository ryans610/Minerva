namespace RyanJuan.Minerva
{
    /// <summary>
    /// 
    /// </summary>
    public enum FetchMode
    {
        /// <summary>
        /// Using default fetch mode setting by user,
        /// or system default value (<see cref="FetchMode.Buffer"/>).
        /// </summary>
        Default = 0,
        /// <summary>
        /// 
        /// </summary>
        Buffer = 1,
        /// <summary>
        /// 
        /// </summary>
        Stream = 2,
        /// <summary>
        /// 
        /// </summary>
        Hybrid = 3,
    }
}
