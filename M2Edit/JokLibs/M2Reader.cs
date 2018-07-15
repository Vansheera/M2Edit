using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JokLibs.M2File;
using JokLibs.M2Skel;

namespace JokLibs.M2Reader
{
    public class M2Reader
    {
        public static BinaryReader reader;
        public static M2 openedFile;
        public static M2SkeletonFile openedSkel;
        public static string filePath;
        public static bool isLegion;
        public static long oldPos;

        public static void Pos()
        {
            Console.Title = $"Reader pos: {reader.BaseStream.Position}";
            Console.WriteLine($"Reader pos: {reader.BaseStream.Position}");
        }

        public static void DisplayArray(Array a)
        {
            Console.WriteLine("[{0}]", string.Join(", ", a));
        }

        public static void Start(BinaryReader r, M2 m, string f)
        {
            reader = r;
            openedFile = m;
            filePath = f;
        }

        public static void StartSkel(BinaryReader r, M2SkeletonFile s, string f)
        {
            reader = r;
            openedSkel = s;
            filePath = f;
        }

        public static void LegionOrNot()
        {
            if (openedFile.magic == 825377869)
            {
                openedFile.magicLegion = openedFile.magic;
                reader.BaseStream.Seek(4, SeekOrigin.Current);
                openedFile.magic = reader.ReadUInt32();
                isLegion = true;
            }
        }

        public static void SpecialSeek(uint pos)
        {
            if (isLegion)
            {
                reader.BaseStream.Seek(pos + 8, SeekOrigin.Begin);
            }
            else
            {
                reader.BaseStream.Seek(pos, SeekOrigin.Begin);
            }
        }

        public static void M2Header()
        {
            openedFile.version = reader.ReadUInt32();
            openedFile.nameNum = reader.ReadUInt32();
            openedFile.namePos = reader.ReadUInt32();
            openedFile.flags = reader.ReadUInt32();
            openedFile.globalSeqNum = reader.ReadUInt32();
            openedFile.globalSeqPos = reader.ReadUInt32();
            openedFile.seqNum = reader.ReadUInt32();
            openedFile.seqPos = reader.ReadUInt32();
            openedFile.seqLookNum = reader.ReadUInt32();
            openedFile.seqLookPos = reader.ReadUInt32();
            openedFile.bonesNum = reader.ReadUInt32();
            openedFile.bonesPos = reader.ReadUInt32();
            openedFile.keyBoneNum = reader.ReadUInt32();
            openedFile.keyBonePos = reader.ReadUInt32();
            openedFile.verticesNum = reader.ReadUInt32();
            openedFile.verticesPos = reader.ReadUInt32();
            openedFile.skinProfilesNum = reader.ReadUInt32();
            openedFile.colorsNum = reader.ReadUInt32();
            openedFile.colorsPos = reader.ReadUInt32();
            openedFile.texturesNum = reader.ReadUInt32();
            openedFile.texturesPos = reader.ReadUInt32();
            openedFile.textWeightsNum = reader.ReadUInt32();
            openedFile.textWeightsPos = reader.ReadUInt32();
            openedFile.uvAnimNum = reader.ReadUInt32();
            openedFile.uvAnimPos = reader.ReadUInt32();
            openedFile.textReplaceNum = reader.ReadUInt32();
            openedFile.textReplacePos = reader.ReadUInt32();
            openedFile.materialsNum = reader.ReadUInt32();
            openedFile.materialsPos = reader.ReadUInt32();
            openedFile.boneLookNum = reader.ReadUInt32();
            openedFile.boneLookPos = reader.ReadUInt32();
            openedFile.textureLookNum = reader.ReadUInt32();
            openedFile.textureLookPos = reader.ReadUInt32();
            openedFile.unk0 = reader.ReadUInt32(); /* unk for Anduin2 */
            openedFile.unk1 = reader.ReadUInt32(); /* unk for Anduin2 */
            openedFile.textWeightsLookNum = reader.ReadUInt32();
            openedFile.textWeightsLookPos = reader.ReadUInt32();
            openedFile.uvAnimLookNum = reader.ReadUInt32();
            openedFile.uvAnimLookPos = reader.ReadUInt32();
            openedFile.vertexBoxLower = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
            openedFile.vertexBoxUpper = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
            openedFile.vertexRadius = reader.ReadUInt32();
            openedFile.boundingBoxLower = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
            openedFile.boundingBoxUpper = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
            openedFile.boundingRadius = reader.ReadUInt32();
            openedFile.boundingTrianglesNum = reader.ReadUInt32();
            openedFile.boundingTrianglesPos = reader.ReadUInt32();
            openedFile.boundingVerticesNum = reader.ReadUInt32();
            openedFile.boundingVerticesPos = reader.ReadUInt32();
            openedFile.boundingNormalsNum = reader.ReadUInt32();
            openedFile.boundingNormalsPos = reader.ReadUInt32();
            openedFile.attachmentsNum = reader.ReadUInt32();
            openedFile.attachmentsPos = reader.ReadUInt32();
            openedFile.attachmentsLookupNum = reader.ReadUInt32();
            openedFile.attachmentsLookupPos = reader.ReadUInt32();
            openedFile.eventNum = reader.ReadUInt32();
            openedFile.eventPos = reader.ReadUInt32();
            openedFile.lightsNum = reader.ReadUInt32();
            openedFile.lightsPos = reader.ReadUInt32();
            openedFile.camerasNum = reader.ReadUInt32();
            openedFile.camerasPos = reader.ReadUInt32();
            openedFile.camerasLookNum = reader.ReadUInt32();
            openedFile.camerasLookPos = reader.ReadUInt32();
        }

        public static void M2GlobalSequences()
        {
            for (int i = 0; i < openedFile.globalSeqNum; i++)
            {
                SpecialSeek(openedFile.globalSeqPos);
                openedFile.globalseq.Add(reader.ReadUInt32());
            }
        }

        public static void M2Sequences()
        {
            SpecialSeek(openedFile.seqPos);

            for (int i = 0; i < openedFile.seqNum; i++)
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

                openedFile.sequences.Add(seq);
            }
        }

        public static void M2SequencesLookups()
        {
            SpecialSeek(openedFile.seqLookPos);

            for (int i = 0; i < openedFile.seqLookNum; i++)
            {
                openedFile.seqLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2Bones()
        {
            SpecialSeek(openedFile.bonesPos);

            for (int i = 0; i < openedFile.bonesNum; i++)
            {
                M2Bones bones = new M2Bones();
                bones.keyBoneId = reader.ReadUInt32();
                bones.flags = reader.ReadUInt32();
                bones.parentBone = reader.ReadUInt16();
                bones.subMeshId = reader.ReadUInt16();
                bones.unk = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };

                /* Translation TODO FUNCTION get values */
                bones.translationHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bones.translationTimestampNb = reader.ReadUInt32();
                bones.translationTimestampPos = reader.ReadUInt32();
                bones.translationKeysNb = reader.ReadUInt32();
                bones.translationKeysPos = reader.ReadUInt32();

                /* Rotation TODO FUNCTION get values */
                bones.rotationHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bones.rotationTimestampNb = reader.ReadUInt32();
                bones.rotationTimestampPos = reader.ReadUInt32();
                bones.rotationKeysNb = reader.ReadUInt32();
                bones.rotationKeysPos = reader.ReadUInt32();

                /* Scale TODO FUNCTION get values */
                bones.scaleHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                bones.rotationTimestampNb = reader.ReadUInt32();
                bones.rotationTimestampPos = reader.ReadUInt32();
                bones.rotationKeysNb = reader.ReadUInt32();
                bones.rotationKeysPos = reader.ReadUInt32();

                bones.pivot = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                openedFile.bones.Add(bones);
            }
        }

        public static void M2KeyBoneLookup()
        {
            SpecialSeek(openedFile.keyBonePos);

            for (int i = 0; i < openedFile.keyBoneNum; i++)
            {
                openedFile.keyBoneLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2Vertices()
        {
            SpecialSeek(openedFile.verticesPos);

            for (int i = 0; i < openedFile.verticesNum; i++)
            {
                M2Vertex vert = new M2Vertex();
                vert.position = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                vert.boneWeights = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
                vert.boneIndices = new byte[] { reader.ReadByte(), reader.ReadByte(), reader.ReadByte(), reader.ReadByte() };
                vert.normal = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                vert.texCoords1 = new float[] { reader.ReadUInt32(), reader.ReadUInt32() };
                vert.texCoords2 = new float[] { reader.ReadUInt32(), reader.ReadUInt32() };
                openedFile.vertices.Add(vert);
            }
        }

        public static void M2Colors()
        {
            /* TODO */
        }

        public static void M2Textures()
        {
            SpecialSeek(openedFile.texturesPos);

            for (int i = 0; i < openedFile.texturesNum; i++)
            {
                M2Texture text = new M2Texture();
                text.type = reader.ReadUInt32();
                text.flags = reader.ReadUInt32();

                text.textureLength = reader.ReadUInt32();
                text.texturePos = reader.ReadUInt32();

                oldPos = reader.BaseStream.Position;

                SpecialSeek(text.texturePos);
                text.texturePath = reader.ReadBytes((int)text.textureLength);
                reader.BaseStream.Seek(oldPos, SeekOrigin.Begin);
                openedFile.textures.Add(text);
            }
        }

        public static void M2TextureWeight()
        {
            SpecialSeek(openedFile.textWeightsPos);

            /* TODO get values */
            for (int i = 0; i < openedFile.textWeightsNum; i++)
            {
                M2TextureWeight tex = new M2TextureWeight();
                tex.header = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                tex.timeStampNb = reader.ReadUInt32();
                tex.timeStampPos = reader.ReadUInt32();

                tex.keysNb = reader.ReadUInt32();
                tex.keysPos = reader.ReadUInt32();
                openedFile.texweight.Add(tex);
            }
        }

        public static void M2UVAnimations()
        {
            /* TODO */
        }

        public static void M2TextureReplace()
        {
            SpecialSeek(openedFile.textReplacePos);

            for (int i = 0; i < openedFile.textReplaceNum; i++)
            {
                openedFile.texreplace.Add(reader.ReadUInt16());
            }
        }

        public static void M2Materials()
        {
            SpecialSeek(openedFile.materialsPos);

            for (int i = 0; i < openedFile.materialsNum; i++)
            {
                M2Material mat = new M2Material();
                mat.flags = reader.ReadUInt16();
                mat.blendMode = reader.ReadUInt16();
                openedFile.materials.Add(mat);
            }
        }

        public static void M2BonesLookup()
        {
            SpecialSeek(openedFile.boneLookPos);

            for (int i = 0; i < openedFile.boneLookNum; i++)
            {
                openedFile.boneLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2TextureLookup()
        {
            SpecialSeek(openedFile.textureLookPos);

            for (int i = 0; i < openedFile.textureLookNum; i++)
            {
                openedFile.textureLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2TextureWeightsLookup()
        {
            SpecialSeek(openedFile.textWeightsLookPos);

            for (int i = 0; i < openedFile.textWeightsLookNum; i++)
            {
                openedFile.textureWeightsLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2UVAnimation()
        {
            /* TODO */
        }

        public static void M2BoundingTriangle()
        {
            SpecialSeek(openedFile.boundingTrianglesPos);

            for (int i = 0; i < openedFile.boundingTrianglesNum; i++)
            {
                openedFile.boundingTriangles.Add(reader.ReadUInt16());
            }
        }

        public static void M2BoundingVertice()
        {
            SpecialSeek(openedFile.boundingVerticesPos);

            for (int i = 0; i < openedFile.boundingVerticesNum; i++)
            {
                M2BoudingVertice vert = new M2BoudingVertice();
                vert.vector = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                openedFile.boundingVertices.Add(vert);
            }
        }

        public static void M2BoundingNormal()
        {
            SpecialSeek(openedFile.boundingNormalsPos);

            for (int i = 0; i < openedFile.boundingNormalsNum; i++)
            {
                M2BoundingNormal normal = new M2BoundingNormal();
                normal.vector = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                openedFile.boundingNormals.Add(normal);
            }
        }

        public static void M2Attachement()
        {
            SpecialSeek(openedFile.attachmentsPos);

            for (int i = 0; i < openedFile.attachmentsNum; i++)
            {
                M2Attachement attach = new M2Attachement();
                attach.attachId = reader.ReadUInt32();
                attach.attachedBone = reader.ReadUInt32();
                attach.position = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                attach.header = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                attach.timeStampNb = reader.ReadUInt32();
                attach.timeStampPos = reader.ReadUInt32();
                attach.keysNb = reader.ReadUInt32();
                attach.keysPos = reader.ReadUInt32();

                openedFile.attachments.Add(attach);
            }
        }

        public static void M2AttachementLookup()
        {
            SpecialSeek(openedFile.attachmentsLookupPos);

            for (int i = 0; i < openedFile.attachmentsLookupNum; i++)
            {
                openedFile.attachmentsLookups.Add(reader.ReadUInt16());
            }
        }

        public static void M2Event()
        {
            SpecialSeek(openedFile.eventPos);

            for (int i = 0; i < openedFile.eventNum; i++)
            {
                M2Event evt = new M2Event();
                evt.identifier = reader.ReadBytes(4);
                evt.data = reader.ReadUInt32();
                evt.bone = reader.ReadUInt32();
                evt.position = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };
                evt.header = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                evt.timeStampNb = reader.ReadUInt32();
                evt.timeStampPos = reader.ReadUInt32();
                openedFile.events.Add(evt);
            }
        }

        public static void M2Light()
        {
            /* TODO */
        }

        public static void M2Camera()
        {
            SpecialSeek(openedFile.camerasPos);

            for (int i = 0; i < openedFile.camerasNum; i++)
            {
                M2Camera cam = new M2Camera();
                cam.type = reader.ReadUInt32();
                cam.farClip = reader.ReadUInt32();
                cam.nearClip = reader.ReadUInt32();

                /* Translation Position */
                cam.translationPosHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                cam.translationPosTimeStampNb = reader.ReadUInt32();
                cam.translationPosTimeStampPos = reader.ReadUInt32();
                cam.translationPosKeyNb = reader.ReadUInt32();
                cam.translationPosKeyPos = reader.ReadUInt32();

                cam.position = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };

                /* Translation Target */
                cam.translationTarHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                cam.translationTarTimeStampNb = reader.ReadUInt32();
                cam.translationTarTimeStampPos = reader.ReadUInt32();

                cam.translationTarKeyNb = reader.ReadUInt32();
                cam.translationTarKeyPos = reader.ReadUInt32();
                cam.target = new float[] { reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32() };

                /* Roll */
                cam.rollHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                cam.rollTimestampNb = reader.ReadUInt32();
                cam.rollTimestampPos = reader.ReadUInt16();
                cam.rollKeysNb = reader.ReadUInt16();
                cam.rollKeysPos = reader.ReadUInt16();

                /* FOV */
                cam.fovHeader = new uint[] { reader.ReadUInt16(), reader.ReadUInt16() };
                cam.fovTimestampNb = reader.ReadUInt32();
                cam.fovTimestampPos = reader.ReadUInt32();
                cam.fovKeysNb = reader.ReadUInt32();
                cam.fovKeysPos = reader.ReadUInt32();

                openedFile.cameras.Add(cam);
            }
        }

        public static void M2CameraLookup()
        {
            SpecialSeek(openedFile.camerasLookPos);

            for (int i = 0; i < openedFile.camerasLookNum; i++)
            {
                openedFile.camerasLookups.Add(reader.ReadUInt16());
            }
        }

        public static void LookLegionChunk()
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            if (Encoding.UTF8.GetString(fileData).Contains("PFID"))
            {
                Console.WriteLine("PFID chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("SFID"))
            {
                Console.WriteLine("SFID chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("AFID"))
            {
                Console.WriteLine("AFID chunk  found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("BFID"))
            {
                Console.WriteLine("BFID chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("TXAC"))
            {
                Console.WriteLine("TXAC chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("EXPT"))
            {
                Console.WriteLine("EXPT chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("EXP2"))
            {
                Console.WriteLine("EXP2 chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("PABC"))
            {
                Console.WriteLine("PABC chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("PADC"))
            {
                Console.WriteLine("PADC chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("PSBC"))
            {
                Console.WriteLine("PSBC chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("PEDC"))
            {
                Console.WriteLine("PEDC chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("SKID"))
            {
                Console.WriteLine("SKID chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("TXID"))
            {
                Console.WriteLine("TXID chunk found.");
            }

            if (Encoding.UTF8.GetString(fileData).Contains("LDV1"))
            {
                Console.WriteLine("LDV1 chunk found.");
            }
        }
    }
}