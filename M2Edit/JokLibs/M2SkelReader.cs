using JokLibs.M2Skel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JokLibs.M2Reader;
using JokLibs.M2File;

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

            UnusedBytes unused = new UnusedBytes();
            unused.unused = new uint[] { reader.ReadUInt32(), reader.ReadUInt32() };

            openedFile.animations = anim;
        }

        public static void ReadGlobalLoops()
        {
            for (int i = 0; i < openedFile.animations.globalLoopsNum; i++)
            {
                openedFile.animations.globalLoops.Add(reader.ReadUInt32());
            }
        }

        public static void ReadSequences()
        {
            for (int i = 0; i < openedFile.animations.sequencesNum; i++)
            {
                M2Sequences seq = new M2Sequences();
                seq.animationId = reader.ReadUInt16();
                seq.subAnimationId = reader.ReadUInt16();
                seq.length = reader.ReadUInt32();
                seq.movingSpeed = reader.ReadUInt32();
                seq.flags = reader.ReadUInt32();
                seq.probability = reader.ReadUInt16();
                seq._padding = reader.ReadUInt16();
                seq.minRepetitions = reader.ReadUInt32();
                seq.maxRepetitions = reader.ReadUInt32();
                seq.blendTime = reader.ReadUInt32();

                float min1 = reader.ReadUInt32();
                float min2 = reader.ReadUInt32();
                float min3 = reader.ReadUInt32();
                seq.boundsMinExtent = new float[] { min1, min2, min3 };

                float max1 = reader.ReadUInt32();
                float max2 = reader.ReadUInt32();
                float max3 = reader.ReadUInt32();
                seq.boundsMaxExtent = new float[] { max1, max2, max3 };

                seq.boundRadius = (float)reader.ReadUInt32();
                seq.nextAnimation = reader.ReadUInt16();
                seq.aliasNext = reader.ReadUInt16();

                openedFile.animations.sequences.Add(seq);
            }
        }

        public static void ReadSequencesLookups()
        {
            for (int i = 0; i < openedFile.animations.sequencesLookNum; i++)
            {
                openedFile.animations.seqLookups.Add(reader.ReadUInt16());
            }
        }

        public static void ReadBonesChunk()
        {
            SkelBones bones = new SkelBones();
            bones.magic = reader.ReadUInt32();
            bones.chunkSize = reader.ReadUInt32();

            bones.bonesNum = reader.ReadUInt32();
            bones.bonesPos = reader.ReadUInt32();

            bones.bonesKeyNum = reader.ReadUInt32();
            bones.bonesKeyPos = reader.ReadUInt32();

            openedFile.bones = bones;
        }

        public static void ReadBones()
        {
            for (int i = 0; i < openedFile.bones.bonesNum; i++)
            {
                M2Bones bone = new M2Bones();
                bone.keyBoneId = reader.ReadUInt32();
                bone.flags = reader.ReadUInt32();
                bone.parentBone = reader.ReadUInt16();
                bone.subMeshId = reader.ReadUInt16();
                bone.unk = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };

                /* Translation TODO FUNCTION get values */
                bone.translationHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bone.translationTimestampNb = reader.ReadUInt32();
                bone.translationTimestampPos = reader.ReadUInt32();
                bone.translationKeysNb = reader.ReadUInt32();
                bone.translationKeysPos = reader.ReadUInt32();

                /* Rotation TODO FUNCTION get values */
                bone.rotationHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bone.rotationTimestampNb = reader.ReadUInt32();
                bone.rotationTimestampPos = reader.ReadUInt32();
                bone.rotationKeysNb = reader.ReadUInt32();
                bone.rotationKeysPos = reader.ReadUInt32();

                /* Scale TODO FUNCTION get values */
                bone.scaleHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bone.rotationTimestampNb = reader.ReadUInt32();
                bone.rotationTimestampPos = reader.ReadUInt32();
                bone.rotationKeysNb = reader.ReadUInt32();
                bone.rotationKeysPos = reader.ReadUInt32();

                bone.pivot = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                openedFile.bones.bones.Add(bone);
            }
        }

        public static void ReadBonesLookup()
        {
            for (int i = 0; i < openedFile.keyBoneNum; i++)
            {
                openedFile.bones.bonesLookups.Add(reader.ReadUInt16());
            }
        }
    }
}
