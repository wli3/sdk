using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.NET.Build.Tasks
{
    public class ConvertReferencesToFrameworkReferences : TaskBase
    {
        public ITaskItem[] References { get; set; }

        public ITaskItem[] FrameworkReferences { get; set; }

        public ITaskItem[] AssembliesInFrameworks { get; set; }

        [Output]
        public ITaskItem[] ReferencesToRemove { get; set; }

        [Output]
        public ITaskItem[] FrameworkReferencesToAdd { get; set; }

        protected override void ExecuteCore()
        {
            if (References == null)
            {
                return;
            }

            HashSet<string> frameworkReferences = FrameworkReferences.Select(fr => fr.ItemSpec).ToHashSet();
            Dictionary<string, string> assembliesInFrameworks = AssembliesInFrameworks.ToDictionary(aif => aif.ItemSpec, aif => aif.GetMetadata(MetadataKeys.FrameworkName), StringComparer.OrdinalIgnoreCase);

            HashSet<string> referencesToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            HashSet<string> frameworkReferencesToAdd = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var reference in References.Where(r => string.IsNullOrEmpty(r.GetMetadata(MetadataKeys.HintPath))))
            {
                if (assembliesInFrameworks.TryGetValue(reference.ItemSpec, out string frameworkName))
                {
                    referencesToRemove.Add(reference.ItemSpec);
                    if (!frameworkReferences.Contains(frameworkName))
                    {
                        frameworkReferencesToAdd.Add(frameworkName);
                    }
                }
            }

            ReferencesToRemove = referencesToRemove.Select(r => new TaskItem(r)).ToArray();
            FrameworkReferencesToAdd = frameworkReferencesToAdd.Select(r => new TaskItem(r)).ToArray();
        }
    }
}
