using Luxand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI_Etap3.Content
{
    /// <summary>
    /// Klasa ktora konwertuje wspolrzedne pixelowe cech,
    /// na zaleznosci miedzy cechami podane we flotach.
    /// Jako argument metody przyjmuja tablice FacialFeatures.
    /// </summary>
    public class FeatureConverter
    {
        /// <summary>
        /// Stosunek rozstawu oczu do dlugosci nosa
        /// Pewniak
        /// </summary>
        public static float EyesSpacing_NoseHeigh(FSDK.TPoint[] FacialFeatures)
        {
            int leftEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].x;
            int leftEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].y;
            int rightEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].x;
            int rightEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].y;

            int noseTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int noseTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int noseBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].x;
            int noseBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].y;

            double eyeSpace = Math.Sqrt(Math.Pow((double)(rightEyeX - leftEyeX), 2) + Math.Pow((double)(rightEyeY - leftEyeY), 2));
            double noseHeigh = Math.Sqrt(Math.Pow((double)(noseTopX - noseBottomX), 2) + Math.Pow((double)(noseTopY - noseBottomY), 2));

            return (float)(eyeSpace / noseHeigh);
        }

        /// <summary>
        /// Stosunek dlugosci nosa do szerokosci ust
        /// ????
        /// </summary>
        public static float NoseHeigh_MouthWidth(FSDK.TPoint[] FacialFeatures)
        {
            int noseTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int noseTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int noseBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].x;
            int noseBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].y;

            int mouthRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_RIGHT_CORNER].x;
            int mouthRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_RIGHT_CORNER].y;
            int mouthLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_LEFT_CORNER].x;
            int mouthLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_LEFT_CORNER].y;

            double mouthWidth = Math.Sqrt(Math.Pow((double)(mouthRightX - mouthLeftX), 2) + Math.Pow((double)(mouthRightY - mouthLeftY), 2));
            double noseHeigh = Math.Sqrt(Math.Pow((double)(noseTopX - noseBottomX), 2) + Math.Pow((double)(noseTopY - noseBottomY), 2));

            return (float)(noseHeigh / mouthWidth);
        }

        /// <summary>
        /// Stosunek rozstawu oczu do szerokosci ust
        /// ????
        /// </summary>
        public static float EyesSpacing_MouthWidth(FSDK.TPoint[] FacialFeatures)
        {
            int leftEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].x;
            int leftEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].y;
            int rightEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].x;
            int rightEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].y;

            int mouthRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_RIGHT_CORNER].x;
            int mouthRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_RIGHT_CORNER].y;
            int mouthLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_LEFT_CORNER].x;
            int mouthLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_MOUTH_LEFT_CORNER].y;

            double mouthWidth = Math.Sqrt(Math.Pow((double)(mouthRightX - mouthLeftX), 2) + Math.Pow((double)(mouthRightY - mouthLeftY), 2));
            double eyeSpace = Math.Sqrt(Math.Pow((double)(rightEyeX - leftEyeX), 2) + Math.Pow((double)(rightEyeY - leftEyeY), 2));

            return (float)(eyeSpace / mouthWidth);
        }

        /// <summary>
        /// Stosunek szerokosci twarzy do rozstawu oczu
        /// Pewniak
        /// </summary>
        public static float FaceWidth_EyesSpacing(FSDK.TPoint[] FacialFeatures)
        {
            int faceLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].x;
            int faceLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].y;
            int faceRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].x;
            int faceRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].y;

            int leftEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].x;
            int leftEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYE].y;
            int rightEyeX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].x;
            int rightEyeY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYE].y;

            double faceWidth = Math.Sqrt(Math.Pow((double)(faceRightX - faceLeftX), 2) + Math.Pow((double)(faceRightY - faceLeftY), 2));
            double eyeSpace = Math.Sqrt(Math.Pow((double)(rightEyeX - leftEyeX), 2) + Math.Pow((double)(rightEyeY - leftEyeY), 2));

            return (float)(faceWidth / eyeSpace);
        }

        /// <summary>
        /// Stosunek dlugosci nosa do szerokosci twarzy
        /// Pewniak
        /// </summary>
        public static float NoseHeigh_FaceWidth(FSDK.TPoint[] FacialFeatures)
        {
            int noseTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int noseTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int noseBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].x;
            int noseBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].y;

            int faceLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].x;
            int faceLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].y;
            int faceRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].x;
            int faceRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].y;

            double faceWidth = Math.Sqrt(Math.Pow((double)(faceRightX - faceLeftX), 2) + Math.Pow((double)(faceRightY - faceLeftY), 2));
            double noseHeigh = Math.Sqrt(Math.Pow((double)(noseTopX - noseBottomX), 2) + Math.Pow((double)(noseTopY - noseBottomY), 2));

            return (float)(noseHeigh / faceWidth);
        }

        /// <summary>
        /// Stosunek szerokosci nosa do szerokosci twarzy
        /// Pewniak
        /// </summary>
        public static float NoseWidth_FaceWidth(FSDK.TPoint[] FacialFeatures)
        {
            int noseLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_LEFT_WING_OUTER].x;
            int noseLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_LEFT_WING_OUTER].y;
            int noseRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_RIGHT_WING_OUTER].x;
            int noseRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_RIGHT_WING_OUTER].y;

            int faceLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].x;
            int faceLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].y;
            int faceRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].x;
            int faceRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].y;

            double faceWidth = Math.Sqrt(Math.Pow((double)(faceRightX - faceLeftX), 2) + Math.Pow((double)(faceRightY - faceLeftY), 2));
            double noseWidth = Math.Sqrt(Math.Pow((double)(noseRightX - noseLeftX), 2) + Math.Pow((double)(noseLeftY - noseRightY), 2));

            return (float)(noseWidth / faceWidth);
        }

        /// <summary>
        /// Stosunek szerokosci nosa do dlugosci nosa
        /// Pewniak
        /// </summary>
        public static float NoseWidth_NoseHeight(FSDK.TPoint[] FacialFeatures)
        {
            int noseTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int noseTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int noseBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].x;
            int noseBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].y;

            int noseLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_LEFT_WING_OUTER].x;
            int noseLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_LEFT_WING_OUTER].y;
            int noseRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_RIGHT_WING_OUTER].x;
            int noseRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_RIGHT_WING_OUTER].y;

            double noseWidth = Math.Sqrt(Math.Pow((double)(noseRightX - noseLeftX), 2) + Math.Pow((double)(noseLeftY - noseRightY), 2));
            double noseHeigh = Math.Sqrt(Math.Pow((double)(noseTopX - noseBottomX), 2) + Math.Pow((double)(noseTopY - noseBottomY), 2));

            return (float)(noseWidth / noseHeigh);
        }

        /// <summary>
        /// Stosunek szerokosci brwi do rozstawu brwi
        /// ?????
        /// </summary>
        public static float EybrowWidth_EyebrowSpaces(FSDK.TPoint[] FacialFeatures)
        {
            int eyebrow1LeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYEBROW_OUTER_CORNER].x;
            int eyebrow1LeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYEBROW_OUTER_CORNER].y;
            int eyebrow1RightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYEBROW_INNER_CORNER].x;
            int eyebrow1RightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_LEFT_EYEBROW_INNER_CORNER].y;

            int eyebrow2LeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYEBROW_INNER_CORNER].x;
            int eyebrow2LeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYEBROW_INNER_CORNER].y;
            int eyebrow2RightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYEBROW_OUTER_CORNER].x;
            int eyebrow2RightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_RIGHT_EYEBROW_OUTER_CORNER].y;


            double eyebrowWidth1 = Math.Sqrt(Math.Pow((double)(eyebrow1RightX - eyebrow1LeftX), 2) + Math.Pow((double)(eyebrow1RightY - eyebrow1LeftY), 2));
            double eyebrowWidth2 = Math.Sqrt(Math.Pow((double)(eyebrow2RightX - eyebrow2LeftX), 2) + Math.Pow((double)(eyebrow2RightY - eyebrow2LeftY), 2));
            //twarz moze byc troche odwrocona i zmieniac perspektywe, przez co jedna brew jest dluzsza a druga krotsza. Srednia 'prostuje' twarz i dlugosc brwi
            double averageEybrowWidth = (eyebrowWidth1 + eyebrowWidth2) / 2.0;

            double eybrowSpaces = Math.Sqrt(Math.Pow((double)(eyebrow2LeftX - eyebrow1RightX), 2) + Math.Pow((double)(eyebrow2LeftY - eyebrow1RightY), 2));

            return (float)(averageEybrowWidth / eybrowSpaces);
        }

        /// <summary>
        /// Stosunek dlugosci twarzy do szerokosci twarzy
        /// Pewniak
        /// </summary>
        public static float FaceHeigh_FaceWidth(FSDK.TPoint[] FacialFeatures)
        {
            int faceTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int faceTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int faceBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_BOTTOM].x;
            int faceBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_BOTTOM].y;

            int faceLeftX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].x;
            int faceLeftY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].y;
            int faceRightX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].x;
            int faceRightY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].y;

            double faceWidth = Math.Sqrt(Math.Pow((double)(faceRightX - faceLeftX), 2) + Math.Pow((double)(faceRightY - faceLeftY), 2));
            double faceHeigh = Math.Sqrt(Math.Pow((double)(faceTopX - faceBottomX), 2) + Math.Pow((double)(faceTopY - faceBottomY), 2));

            return (float)(faceHeigh / faceWidth);
        }

        /// <summary>
        /// Stosunek dlugosci twarzy do dlugosci nosa
        /// Pewniak
        /// </summary>
        public static float FaceHeigh_NoseHeigh(FSDK.TPoint[] FacialFeatures)
        {
            int faceTopX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].x;
            int faceTopY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BRIDGE].y;
            int faceBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_BOTTOM].x;
            int faceBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_BOTTOM].y;

            int noseBottomX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].x;
            int noseBottomY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_NOSE_BOTTOM].y;

            double noseHeigh = Math.Sqrt(Math.Pow((double)(faceTopX - noseBottomX), 2) + Math.Pow((double)(faceTopY - noseBottomY), 2));
            double faceHeigh = Math.Sqrt(Math.Pow((double)(faceTopX - faceBottomX), 2) + Math.Pow((double)(faceTopY - faceBottomY), 2));

            return (float)(faceHeigh / noseHeigh);
        }

        /// <summary>
        /// Wspolczynnik trapezowosci twarzy
        /// Pewniak
        /// </summary>
        public static float TrapezoidFace(FSDK.TPoint[] FacialFeatures)
        {
            int faceTopLX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].x;
            int faceTopLY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR14].y;
            int faceTopRX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].x;
            int faceTopRY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR15].y;

            int faceBottomLX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR2].x;
            int faceBottomLY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR2].y;
            int faceBottomRX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR12].x;
            int faceBottomRY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR12].y;


            double topTrapezoid = Math.Sqrt(Math.Pow((double)(faceTopRX - faceTopLX), 2) + Math.Pow((double)(faceTopRY - faceTopLY), 2));
            double lowTrapezoid = Math.Sqrt(Math.Pow((double)(faceBottomRX - faceBottomLX), 2) + Math.Pow((double)(faceBottomRY - faceBottomLY), 2));

            return (float)(topTrapezoid / lowTrapezoid);
        }
        /// <summary>
        /// Wspolczynnik trapezowosci szczeki
        /// Pewniak
        /// </summary>
        public static float TrapezoidChin(FSDK.TPoint[] FacialFeatures)
        {
            int faceTopLX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR2].x;
            int faceTopLY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR2].y;
            int faceTopRX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR12].x;
            int faceTopRY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_FACE_CONTOUR12].y;

            int faceBottomLX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_LEFT].x;
            int faceBottomLY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_LEFT].y;
            int faceBottomRX = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_RIGHT].x;
            int faceBottomRY = FacialFeatures[(int)FSDK.FacialFeatures.FSDKP_CHIN_RIGHT].y;


            double topTrapezoid = Math.Sqrt(Math.Pow((double)(faceTopRX - faceTopLX), 2) + Math.Pow((double)(faceTopRY - faceTopLY), 2));
            double lowTrapezoid = Math.Sqrt(Math.Pow((double)(faceBottomRX - faceBottomLX), 2) + Math.Pow((double)(faceBottomRY - faceBottomLY), 2));

            return (float)(topTrapezoid / lowTrapezoid);
        }
        //-------------------------------------------------------------------------------

        /// <summary>
        /// Metoda zwraca liste wszystkich przygotowanych cech
        /// </summary>
        public static List<float> GetFeatures(FSDK.TPoint[] FacialFeatures)
        {
            List<float> neuralFeatures = new List<float>();

            neuralFeatures.Add(EyesSpacing_NoseHeigh(FacialFeatures));
            neuralFeatures.Add(NoseHeigh_MouthWidth(FacialFeatures));
            neuralFeatures.Add(EyesSpacing_MouthWidth(FacialFeatures));
            neuralFeatures.Add(FaceWidth_EyesSpacing(FacialFeatures));
            neuralFeatures.Add(NoseHeigh_FaceWidth(FacialFeatures));
            neuralFeatures.Add(NoseWidth_FaceWidth(FacialFeatures));
            neuralFeatures.Add(NoseWidth_NoseHeight(FacialFeatures));
            neuralFeatures.Add(EybrowWidth_EyebrowSpaces(FacialFeatures));
            neuralFeatures.Add(FaceHeigh_FaceWidth(FacialFeatures));
            neuralFeatures.Add(FaceHeigh_NoseHeigh(FacialFeatures));
            neuralFeatures.Add(TrapezoidFace(FacialFeatures));
            neuralFeatures.Add(TrapezoidChin(FacialFeatures));

            return neuralFeatures;
        }
    }
}
