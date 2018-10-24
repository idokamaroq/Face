using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceTracker
{
    public static class Tracker
    {
        /// <summary>
        /// Determines the emotion and gaze direction of a face in an image
        /// </summary>
        /// <param name="image">Picture of a face</param>
        /// <returns></returns>
        public static (Emotion Emotion, Direction GazeDirection) Analyze(byte[] image)
        {
            return (GetEmotion(image), GetGazeDirection(image));
        }

        /// <summary>
        /// Uses Microsoft's Face API to determine emotion
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Emotion GetEmotion(byte[] image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the direction the eyes are looking
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static Direction GetGazeDirection(byte[] image)
        {
            //Dunno how I'm gonna do this yet. Might be infeasible
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
