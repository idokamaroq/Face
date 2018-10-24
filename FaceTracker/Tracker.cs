using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
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
            var key = Environment.GetEnvironmentVariable("FaceAPI_KEY");
            faceClient = new FaceClient(
                new ApiKeyServiceClientCredentials(key),
                new System.Net.Http.DelegatingHandler[] { });
            faceClient.Endpoint = "https://southcentralus.api.cognitive.microsoft.com"; ///face/v1.0"; //TODO: put this in a config file
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
                //FaceAttributeType.Smile, //might be able to do cool stuff with this in the future
                FaceAttributeType.Emotion
            };

            var faceList = await faceClient.Face.DetectWithStreamAsync( //TODO: this is getting a 404 I think
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
            throw new NotImplementedException();
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
