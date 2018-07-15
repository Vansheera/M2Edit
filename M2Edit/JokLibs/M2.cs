using System;
using System.Collections.Generic;

namespace JokLibs.M2File
{
    public class M2Sequences
    {
        public uint animationId;
        public uint subAnimationId;
        public uint length;
        public float movingSpeed;
        public uint flags;
        public uint probability;
        public uint _padding;
        public uint minRepetitions;
        public uint maxRepetitions;
        public uint blendTime;
        public float[] boundsMinExtent = new float[1];
        public float[] boundsMaxExtent = new float[1];
        public float boundRadius;
        public uint nextAnimation;
        public uint aliasNext;
    }

    public class M2Bones
    {
        public uint keyBoneId;
        public uint flags;
        public uint parentBone;
        public uint subMeshId;
        public uint[] unk;

        public uint[] translationHeader;

        public uint translationTimestampNb;
        public uint translationTimestampPos;
        public uint[] translationTimeStamp;

        public uint translationKeysNb;
        public uint translationKeysPos;
        public uint[] translationKeys;

        public uint[] rotationHeader;

        public uint rotationTimestampNb;
        public uint rotationTimestampPos;
        public uint[] rotationTimestamp;

        public uint rotationKeysNb;
        public uint rotationKeysPos;
        public uint[] rotationKeys;

        public uint[] scaleHeader;

        public uint scaleTimestampNb;
        public uint scaleTimestampPos;
        public uint[] scaleTimestamp;

        public uint scaleKeysNb;
        public uint scaleKeysPos;
        public uint[] scaleKeys;

        public float[] pivot;
    }

    public class M2Vertex
    {
        public float[] position;
        public byte[] boneWeights;
        public byte[] boneIndices;
        public float[] normal;
        public float[] texCoords1;
        public float[] texCoords2;
    }

    public class M2Texture
    {
        public uint type;
        public uint flags;

        public uint textureLength;
        public uint texturePos;

        public byte[] texturePath;
    }

    public class M2TextureWeight
    {
        public uint[] header;

        public uint timeStampNb;
        public uint timeStampPos;
        public uint timeStampValue;

        public uint keysNb;
        public uint keysPos;
        public uint keysValue;
    }

    public class M2Material
    {
        public uint flags;
        public uint blendMode;
    }

    public class M2BoudingVertice
    {
        public float[] vector;
    }

    public class M2BoundingNormal
    {
        public float[] vector;
    }

    public class M2Attachement
    {
        public uint attachId;
        public uint attachedBone;
        public float[] position;

        public uint[] header;

        public uint timeStampNb;
        public uint timeStampPos;
        public uint timeStampValue;

        public uint keysNb;
        public uint keysPos;
        public uint keysValue;

    }

    public class M2Event
    {
        public byte[] identifier;
        public uint data;
        public uint bone;
        public float[] position;

        public uint[] header;

        public uint timeStampNb;
        public uint timeStampPos;
        public uint timeStampValue;
    }

    public class M2Camera
    {
        public uint type;
        public float farClip;
        public float nearClip;

        /* Translation Position */
        public uint[] translationPosHeader;
        public uint translationPosTimeStampNb;
        public uint translationPosTimeStampPos;
        public uint translationPosTimeStampValue;

        public uint translationPosKeyNb;
        public uint translationPosKeyPos;
        public uint translationPosKeyValue;

        public float[] position;
        /* Translation Position */

        /* Translation Target */
        public uint[] translationTarHeader;
        public uint translationTarTimeStampNb;
        public uint translationTarTimeStampPos;
        public uint translationTarTimeStampValue;

        public uint translationTarKeyNb;
        public uint translationTarKeyPos;
        public uint translationTarKeyValue;

        public float[] target;
        /* Translation Target */

        /* Roll */
        public uint[] rollHeader;
        public uint rollTimestampNb;
        public uint rollTimestampPos;
        public uint rollTimestampValue;

        public uint rollKeysNb;
        public uint rollKeysPos;
        public uint rollKeysValue;
        /* Roll */

        /* FOV */
        public uint[] fovHeader;
        public uint fovTimestampNb;
        public uint fovTimestampPos;
        public uint fovTimestampValue;

        public uint fovKeysNb;
        public uint fovKeysPos;
        public uint fovKeysValue;
        /* FOV */
    }

    public class M2
    {
        public uint magicLegion { get; set; }
        public uint magic { get; set; }

        public uint version { get; set; }

        public uint nameNum { get; set; }
        public uint namePos { get; set; }

        public uint flags { get; set; }

        public uint globalSeqNum { get; set; }
        public uint globalSeqPos { get; set; }

        public uint seqNum { get; set; }
        public uint seqPos { get; set; }

        public uint seqLookNum { get; set; }
        public uint seqLookPos { get; set; }

        public uint bonesNum { get; set; }
        public uint bonesPos { get; set; }

        public uint keyBoneNum { get; set; }
        public uint keyBonePos { get; set; }

        public uint verticesNum { get; set; }
        public uint verticesPos { get; set; }

        public uint skinProfilesNum { get; set; }

        public uint colorsNum { get; set; }
        public uint colorsPos { get; set; }

        public uint texturesNum { get; set; }
        public uint texturesPos { get; set; }

        public uint textWeightsNum { get; set; }
        public uint textWeightsPos { get; set; }

        public uint uvAnimNum { get; set; }
        public uint uvAnimPos { get; set; }

        public uint textReplaceNum { get; set; }
        public uint textReplacePos { get; set; }

        public uint materialsNum { get; set; }
        public uint materialsPos { get; set; }

        public uint boneLookNum { get; set; }
        public uint boneLookPos { get; set; }

        public uint textureLookNum { get; set; }
        public uint textureLookPos { get; set; }

        public uint unk0 { get; set; }
        public uint unk1 { get; set; }

        public uint textWeightsLookNum { get; set; }
        public uint textWeightsLookPos { get; set; }

        public uint uvAnimLookNum { get; set; }
        public uint uvAnimLookPos { get; set; }

        public float[] vertexBoxLower { get; set; }
        public float[] vertexBoxUpper { get; set; }
        public float vertexRadius { get; set; }

        public float[] boundingBoxLower { get; set; }
        public float[] boundingBoxUpper { get; set; }
        public float boundingRadius { get; set; }

        public uint boundingTrianglesNum { get; set; }
        public uint boundingTrianglesPos { get; set; }

        public uint boundingVerticesNum { get; set; }
        public uint boundingVerticesPos { get; set; }

        public uint boundingNormalsNum { get; set; }
        public uint boundingNormalsPos { get; set; }

        public uint attachmentsNum { get; set; }
        public uint attachmentsPos { get; set; }

        public uint attachmentsLookupNum { get; set; }
        public uint attachmentsLookupPos { get; set; }

        public uint eventNum { get; set; }
        public uint eventPos { get; set; }

        public uint lightsNum { get; set; }
        public uint lightsPos { get; set; }

        public uint camerasNum { get; set; }
        public uint camerasPos { get; set; }


        public uint camerasLookNum { get; set; }
        public uint camerasLookPos { get; set; }

        public List<uint> globalseq = new List<uint>();
        public List<M2Sequences> sequences = new List<M2Sequences>();
        public List<UInt16> seqLookups = new List<UInt16>();
        public List<M2Bones> bones = new List<M2Bones>();
        public List<UInt16> keyBoneLookups = new List<UInt16>();
        public List<M2Vertex> vertices = new List<M2Vertex>();
        public List<M2Texture> textures = new List<M2Texture>();
        public List<M2TextureWeight> texweight = new List<M2TextureWeight>();
        public List<UInt16> texreplace = new List<UInt16>();
        public List<M2Material> materials = new List<M2Material>();
        public List<UInt16> boneLookups = new List<UInt16>();
        public List<UInt16> textureLookups = new List<UInt16>();
        public List<UInt16> textureWeightsLookups = new List<UInt16>();
        public List<UInt16> boundingTriangles = new List<UInt16>();
        public List<M2BoudingVertice> boundingVertices = new List<M2BoudingVertice>();
        public List<M2BoundingNormal> boundingNormals = new List<M2BoundingNormal>();
        public List<M2Attachement> attachments = new List<M2Attachement>();
        public List<UInt16> attachmentsLookups = new List<UInt16>();
        public List<M2Event> events = new List<M2Event>();
        public List<M2Camera> cameras = new List<M2Camera>();
        public List<UInt16> camerasLookups = new List<UInt16>();
    }
}