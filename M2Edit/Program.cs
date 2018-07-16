using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using JokLibs.M2File;
using JokLibs.M2Reader;
using JokLibs.M2Skel;
using JokLibs.M2SkelReader;

namespace M2Edit
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = @"C:\Users\Jok\source\repos\M2Edit\TestFiles\lightforgeddraeneifemale.skel";

            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                M2SkeletonFile skel = new M2SkeletonFile();
                M2SkelReader.Start(reader, skel, file);

                M2SkelReader.ReadHeaderChunk();

                /* Animations Chunk */
                M2SkelReader.ReadAnimationsChunk();
                M2SkelReader.ReadGlobalLoops();
                M2SkelReader.ReadSequences();
                /* M2SkelReader.ReadSequencesLookups(); < need testing with skel 
                 who have sequences lookups*/

                /* Bones Chunk */
                M2SkelReader.ReadBonesChunk();
                //M2SkelReader.ReadBones();

                foreach (var prop in skel.GetType().GetProperties())
                {
                    Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(skel, null));
                }

                Console.ReadLine();
            }
        }

        public static void M2ReadAlmostComplete()
        {
            Stopwatch time = new Stopwatch();
            time.Start();

            var file = @"C:\Users\Jok\source\repos\WoWM2Reader\WoWM2Reader\bin\Debug\netcoreapp2.0\humanmale_HD.m2";

            using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
            {
                M2 readed = new M2();
                M2Reader.Start(reader, readed, file);

                readed.magic = reader.ReadUInt32();

                M2Reader.LegionOrNot();
                M2Reader.M2Header();
                M2Reader.M2GlobalSequences();
                M2Reader.M2Sequences();
                M2Reader.M2SequencesLookups();
                M2Reader.M2Bones();
                M2Reader.M2KeyBoneLookup();
                M2Reader.M2Vertices();
                M2Reader.M2Textures();
                M2Reader.M2TextureWeight();
                M2Reader.M2TextureReplace();
                M2Reader.M2Materials();
                M2Reader.M2BonesLookup();
                M2Reader.M2TextureLookup();
                M2Reader.M2TextureWeightsLookup();
                M2Reader.M2BoundingTriangle();
                M2Reader.M2BoundingVertice();
                M2Reader.M2BoundingNormal();
                M2Reader.M2Attachement();
                M2Reader.M2AttachementLookup();
                M2Reader.M2Event();
                M2Reader.M2Camera();
                M2Reader.M2CameraLookup();

                foreach (var prop in readed.GetType().GetProperties())
                {
                    Console.WriteLine("{0}={1}", prop.Name, prop.GetValue(readed, null));
                }

                Console.WriteLine($"animationCount={readed.sequences.Count()}");
                Console.WriteLine($"animationLookupCount={readed.seqLookups.Count()}");
                Console.WriteLine($"bonesCount={readed.bones.Count()}");
                Console.WriteLine($"boneLookupsCount={readed.keyBoneLookups.Count()}");
                Console.WriteLine($"verticesCount={readed.vertices.Count()}");
                Console.WriteLine($"texturesCount={readed.textures.Count()}");
                Console.WriteLine($"texturesWeightCount={readed.texweight.Count()}");
                Console.WriteLine($"texturesReplacement={readed.texreplace.Count()}");
                Console.WriteLine($"materials={readed.materials.Count()}");
                Console.WriteLine($"bonesLookups={readed.boneLookups.Count()}");
                Console.WriteLine($"texturesLookups={readed.textureLookups.Count()}");
                Console.WriteLine($"texturesWeightsLookups={readed.textureWeightsLookups.Count()}");
                Console.WriteLine($"boundingTriangles={readed.boundingTriangles.Count()}");
                Console.WriteLine($"boundingVertices={readed.boundingVertices.Count()}");
                Console.WriteLine($"boundingNormals={readed.boundingNormals.Count()}");
                Console.WriteLine($"attachments={readed.attachments.Count()}");
                Console.WriteLine($"attachmentsLookups={readed.attachmentsLookups.Count()}");
                Console.WriteLine($"events={readed.events.Count()}");
                Console.WriteLine($"cameras={readed.cameras.Count()}");
                Console.WriteLine($"camerasLookups={readed.camerasLookups.Count()}");
            }

            M2Reader.LookLegionChunk();
            time.Stop();
            TimeSpan ts = time.Elapsed;
            Console.WriteLine(string.Format("Elapsed time: {0}:{1}", Math.Floor(ts.TotalMinutes), ts.ToString("ss\\.ff")));
            Console.Read();
        }
    }
}
