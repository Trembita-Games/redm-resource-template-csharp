using System.Collections.Generic;

namespace TgResourceTemplateCSharp.Shared.Data
{

    /// <summary>
    /// Shared immutable resource metadata.
    /// </summary>
    /// <remarks>
    /// Static resource-owned data belongs in data/ so runtime entry points stay small.
    /// Future resources can replace this metadata with real verified static data while keeping
    /// the same immutable organization pattern.
    /// </remarks>
    public static class ResourceData
    {
        /// <summary>
        /// Shared immutable resource metadata entries for the server.
        /// </summary>
        public static readonly IReadOnlyList<ResourceDataEntry> ServerEntries =
            new List<ResourceDataEntry>
            {
                new ResourceDataEntry("source", "0.1.0"),
                new ResourceDataEntry("platform", "RedM server")
            };

        /// <summary>
        /// Shared immutable resource metadata entries for the client.
        /// </summary>
        public static readonly IReadOnlyList<ResourceDataEntry> ClientEntries =
            new List<ResourceDataEntry>
            {
                new ResourceDataEntry("source", "0.2.0"),
                new ResourceDataEntry("platform", "RedM client")
            };
    }

    /// <summary>
    /// Immutable resource metadata entry.
    /// </summary>
    public sealed class ResourceDataEntry
    {
        /// <summary>
        /// Metadata key.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Metadata value.
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Creates a new immutable metadata entry.
        /// </summary>
        /// <param name="name">
        /// Metadata key.
        /// </param>
        /// <param name="version">
        /// Metadata value.
        /// </param>
        public ResourceDataEntry(string name, string version)
        {
            Name = name;
            Version = version;
        }
    }
}
