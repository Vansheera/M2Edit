using JokLibs.M2Skel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JokLibs.M2Reader;

namespace JokLibs.M2SkelReader
{
    class M2SkelReader
    {
        public static BinaryReader reader;
        public static M2SkeletonFile openedFile;
        public static string filePath;
        public static long oldPos;

        public static void Start(BinaryReader r, M2SkeletonFile s, string f)
        {
            reader = r;
            openedFile = s;
            filePath = f;
        }

        public static void ReadHeaderChunk()
        {
            openedFile.magic = reader.ReadUInt32();
            openedFile.chunkSize = reader.ReadUInt32();
            openedFile.unk0 = reader.ReadUInt32();
            openedFile.modelNameLength = reader.ReadUInt32() - 1;
            openedFile.modelNamePos = reader.ReadUInt32();
            openedFile.unk1 = reader.ReadUInt32();

            /* Get model name in string = ) */
            var modelName = reader.ReadBytes((int)openedFile.modelNameLength);
            openedFile.modelName = Encoding.Default.GetString(modelName);

            /* Go to the end of the chunk */
            reader.BaseStream.Seek(openedFile.chunkSize, SeekOrigin.Begin);

            /* Unused 8-bytes at the end, get out! */
            UnusedBytes ununsed = new UnusedBytes();
            ununsed.unused = new uint[] { reader.ReadUInt32(), reader.ReadUInt32() };
        }

        public static void ReadAnimationsChunk()
        {
            SkelAnimations anim = new SkelAnimations();
            anim.magic = reader.ReadUInt32();
            anim.chunkSize = reader.ReadUInt32();
            anim.globalLoopsNum = reader.ReadUInt32();
            anim.globalLoopsPos = reader.ReadUInt32();
            anim.sequencesNum = reader.ReadUInt32();
            anim.sequencesPos = reader.ReadUInt32();
            anim.sequencesLookNum = reader.ReadUInt32();
            anim.sequencesLookPos = reader.ReadUInt32();

            openedFile.animations = anim;
        }
    }
}
