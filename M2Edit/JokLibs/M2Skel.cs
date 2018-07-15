using System;
using System.Collections.Generic;
using System.Text;
using JokLibs.M2File;
using JokLibs.M2Reader;

namespace JokLibs.M2Skel
{
    public class M2SkeletonFile
    {
        public uint magic { get; set; }
        public uint chunkSize { get; set; }
        public uint unk0 { get; set; } /* 256 on all .skel ¯\_(ツ)_/¯*/
        public uint modelNameLength { get; set; }
        public uint modelNamePos { get; set; }
        public string modelName { get; set; }
        public uint unk1 { get; set; }

        public SkelAnimations animations { get; set; }
    }

    public class SkelAnimations
    {
        public uint magic;
        public uint chunkSize;

        public uint globalLoopsNum;
        public uint globalLoopsPos;

        public uint sequencesNum;
        public uint sequencesPos;

        public uint sequencesLookNum;
        public uint sequencesLookPos;
    }

    class UnusedBytes
    {
        public uint[] unused;
    }
}
