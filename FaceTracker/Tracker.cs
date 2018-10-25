using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FaceTracker
{
    public class Tracker
    {
        private readonly IFaceClient faceClient;

        public Tracker()
        {
            //TODO: store these somewhere
            var key = "";
            var endpoint = "https://southcentralus.api.cognitive.microsoft.com";

            faceClient = new FaceClient(
                new ApiKeyServiceClientCredentials(key),
                new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = endpoint;
        }

        /// <summary>
        /// Determines the emotion and gaze direction of a face in an image
        /// </summary>
        /// <param name="image">Picture of a face</param>
        /// <returns></returns>
        public async Task<(Emotion Emotion, Direction GazeDirection)> Analyze(Stream imageFileStream)
        {
            try
            {
                var face = await UploadAndDetectFace(imageFileStream);
                return (GetEmotion(face), GetGazeDirection(face));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                Debug.WriteLine(e);
                return (Emotion.Neutral, Direction.Center); //default face
            }
        }

        private async Task<DetectedFace> UploadAndDetectFace(Stream imageFileStream)
        {
            // The list of Face attributes to get
            var faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.Emotion
            };

            var faceList = await faceClient.Face.DetectWithStreamAsync(
                imageFileStream,
                false, //get faceId
                true, //get face landmarks
                faceAttributes);

            return faceList.SingleOrDefault(); //if it picked up more than one face, something is wrong
        }

        /// <summary>
        /// Uses Microsoft's Face API to determine emotion
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Emotion GetEmotion(DetectedFace face)
        {
            //Since Face API returns an object with properties for each emotion
            //with the corresponding confidence level, you have to check each
            //one to see which is the max.
            //It would be more efficient to write an if statement for each emotion
            //and keep a running max value, but this is way cleaner code.
            var json = JsonConvert.SerializeObject(face.FaceAttributes.Emotion);
            var dict = JsonConvert.DeserializeObject<Dictionary<Emotion, double>>(json);
            var emotion = dict.Keys.Aggregate((x, y) => dict[x] > dict[y] ? x : y); //goes through the dictionary and compares each value with the next
            return emotion;
        }

        /// <summary>
        /// Gets the direction the eyes are looking
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Direction GetGazeDirection(DetectedFace face)
        {
            //compare differences in x-coordinates of pupils and nose tip
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts an image to a byte array
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static byte[] ConvertToByteArray(object image)
        {
            throw new NotImplementedException();
        }
    }
}
