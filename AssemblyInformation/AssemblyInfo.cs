using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyInformation
{
    [Serializable]
    public class AssemblyInfo
    {
        public AssemblyInfo(string fileName, string fullPath, string fileExtension)
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            this.FileExtension = fileExtension;
        }

        public AssemblyInfo(
            string fileName,
            string fullPath,
            string fileExtension,
            PortableExecutableKinds portableExecutableKinds,
            ImageFileMachine imageFileMachine,
            IEnumerable<AssemblyName> referencedAssemblies
            )
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            this.FileExtension = fileExtension;
            this.PortableExecutableKinds = portableExecutableKinds;
            this.ImageFileMachine = imageFileMachine;
            this.ReferencedAssemblies = referencedAssemblies;
        }

        public string FileName { get; private set; }
        public string FullPath { get; private set; }
        public string FileExtension { get; private set; }
        public PortableExecutableKinds PortableExecutableKinds { get; private set; }
        public ImageFileMachine ImageFileMachine { get; private set; }
        public IEnumerable<AssemblyName> ReferencedAssemblies { get; private set; }
    }
}