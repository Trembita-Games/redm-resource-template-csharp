namespace RedmResourceTemplateCSharp.Shared
{

    /// <summary>
    /// Shared resource configuration.
    /// </summary>
    /// <remarks>
    /// Keep values here when both client and server code need the same resource-level setting.
    /// This template starts with constants so startup behavior stays deterministic.
    /// </remarks>
    public static class ResourceConfiguration
    {
        /// <summary>
        /// Canonical resource name used in logs and validation output.
        /// </summary>
        public const string ResourceName = "redm-resource-template-csharp";

        /// <summary>
        /// Enables additional development logs when future resources need them.
        /// </summary>
        public const bool Debug = true;
    }
}
