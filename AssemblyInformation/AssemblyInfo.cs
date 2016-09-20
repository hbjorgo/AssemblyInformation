using System.Reflection;

namespace AssemblyInformation
{
    public class AssemblyInfo
    {
        public AssemblyInfo(string fileName, string fullPath, string fileExtension)
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            this.FileExtension = fileExtension;
        }

        public AssemblyInfo(string fileName, string fullPath, string fileExtension, PortableExecutableKinds portableExecutableKinds, ImageFileMachine imageFileMachine)
        {
            this.FileName = fileName;
            this.FullPath = fullPath;
            this.FileExtension = fileExtension;
            this.PortableExecutableKinds = portableExecutableKinds;
            this.ImageFileMachine = imageFileMachine;
        }

        public string FileName { get; private set; }
        public string FullPath { get; private set; }
        public string FileExtension { get; private set; }
        public PortableExecutableKinds PortableExecutableKinds { get; private set; }
        public ImageFileMachine ImageFileMachine { get; private set; }
    }
}