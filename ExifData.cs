using System;
using ExifLib;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos.Spatial;

namespace ReadImageExif
{
    public class FileData
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get { return FileName.GetHashCode().ToString(); } }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public ExifData ExifData { get; set; }
    }
    public class ExifData
    {
        public string GPSVersionID { get; set; }
        public string GPSLatitudeRef { get; set; }
        public Double[] GPSLatitude { get; set; }
        public string GPSLongitudeRef { get; set; }
        public Double[] GPSLongitude { get; set; }
        public byte[] GPSAltitudeRef { get; set; }
        public Double GPSAltitude { get; set; }
        public Double[] GPSTimestamp { get; set; }
        public int GPSSatellites { get; set; }
        public string GPSStatus { get; set; }
        public string GPSMeasureMode { get; set; }
        public Double GPSDOP { get; set; }
        public string GPSSpeedRef { get; set; }
        public Double GPSSpeed { get; set; }
        public string GPSTrackRef { get; set; }
        public string GPSTrack { get; set; }
        public string GPSImgDirectionRef { get; set; }
        public Double GPSImgDirection { get; set; }
        public string GPSMapDatum { get; set; }
        public string GPSDestLatitudeRef { get; set; }
        public Double GPSDestLatitude { get; set; }
        public string GPSDestLongitudeRef { get; set; }
        public Double GPSDestLongitude { get; set; }
        public string GPSDestBearingRef { get; set; }
        public Double GPSDestBearing { get; set; }
        public string GPSDestDistanceRef { get; set; }
        public Double GPSDestDistance { get; set; }
        public byte[] GPSProcessingMethod { get; set; }
        public string GPSAreaInformation { get; set; }
        public string GPSDateStamp { get; set; }
        public string GPSDifferential { get; set; }
        public string GPSHPositioningError { get; set; }
        public UInt32 ImageWidth { get; set; }
        public UInt32 ImageLength { get; set; }
        public int BitsPerSample { get; set; }
        public string Compression { get; set; }
        public string PhotometricInterpretation { get; set; }
        public string ImageDescription { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string StripOffsets { get; set; }
        public UInt16 Orientation { get; set; }
        public Double SamplesPerPixel { get; set; }
        public Double RowsPerStrip { get; set; }
        public Double StripByteCounts { get; set; }
        public Double XResolution { get; set; }
        public Double YResolution { get; set; }
        public string PlanarConfiguration { get; set; }
        public UInt16 ResolutionUnit { get; set; }
        public string TransferFunction { get; set; }
        public string Software { get; set; }
        public DateTime DateTime { get; set; }
        public string Artist { get; set; }
        public string WhitePoint { get; set; }
        public string PrimaryChromaticities { get; set; }
        public string JPEGInterchangeFormat { get; set; }
        public string JPEGInterchangeFormatLength { get; set; }
        public string YCbCrCoefficients { get; set; }
        public string YCbCrSubSampling { get; set; }
        public UInt16 YCbCrPositioning { get; set; }
        public string ReferenceBlackWhite { get; set; }
        public string Copyright { get; set; }
        public Double ExposureTime { get; set; }
        public Double FNumber { get; set; }
        public UInt16 ExposureProgram { get; set; }
        public string SpectralSensitivity { get; set; }
        public UInt16 PhotographicSensitivity { get; set; }
        public string OECF { get; set; }
        public string SensitivityType { get; set; }
        public string StandardOutputSensitivity { get; set; }
        public string RecommendedExposureIndex { get; set; }
        public string ISOSpeed { get; set; }
        public string ISOSpeedLatitudeyyy { get; set; }
        public string ISOSpeedLatitudezzz { get; set; }
        public byte[] ExifVersion { get; set; }
        public DateTime DateTimeOriginal { get; set; }
        public DateTime DateTimeDigitized { get; set; }
        public byte[] ComponentsConfiguration { get; set; }
        public string CompressedBitsPerPixel { get; set; }
        public Double ShutterSpeedValue { get; set; }
        public Double ApertureValue { get; set; }
        public Double BrightnessValue { get; set; }
        public Double ExposureBiasValue { get; set; }
        public Double MaxApertureValue { get; set; }
        public Double SubjectDistance { get; set; }
        public UInt16 MeteringMode { get; set; }
        public string LightSource { get; set; }
        public UInt16 Flash { get; set; }
        public Double FocalLength { get; set; }
        public string SubjectArea { get; set; }
        public byte[] MakerNote { get; set; }
        public string UserComment { get; set; }
        public string SubsecTime { get; set; }
        public string SubsecTimeOriginal { get; set; }
        public string SubsecTimeDigitized { get; set; }
        public string XPTitle { get; set; }
        public string XPComment { get; set; }
        public string XPAuthor { get; set; }
        public string XPKeywords { get; set; }
        public string XPSubject { get; set; }
        public byte[] FlashpixVersion { get; set; }
        public UInt16 ColorSpace { get; set; }
        public UInt32 PixelXDimension { get; set; }
        public UInt32 PixelYDimension { get; set; }
        public string RelatedSoundFile { get; set; }
        public string FlashEnergy { get; set; }
        public string SpatialFrequencyResponse { get; set; }
        public string FocalPlaneXResolution { get; set; }
        public string FocalPlaneYResolution { get; set; }
        public string FocalPlaneResolutionUnit { get; set; }
        public string SubjectLocation { get; set; }
        public string ExposureIndex { get; set; }
        public UInt16 SensingMethod { get; set; }
        public string FileSource { get; set; }
        public byte SceneType { get; set; }
        public string CFAPattern { get; set; }
        public UInt16 CustomRendered { get; set; }
        public UInt16 ExposureMode { get; set; }
        public UInt16 WhiteBalance { get; set; }
        public Double DigitalZoomRatio { get; set; }
        public UInt16 FocalLengthIn35mmFilm { get; set; }
        public UInt16 SceneCaptureType { get; set; }
        public string GainControl { get; set; }
        public UInt16 Contrast { get; set; }
        public UInt16 Saturation { get; set; }
        public UInt16 Sharpness { get; set; }
        public string DeviceSettingDescription { get; set; }
        public UInt16 SubjectDistanceRange { get; set; }
        public string ImageUniqueID { get; set; }
        public string CameraOwnerName { get; set; }
        public string BodySerialNumber { get; set; }
        public string LensSpecification { get; set; }
        public string LensMake { get; set; }
        public string LensModel { get; set; }
        public string LensSerialNumber { get; set; }
        public Double? GPSLatitudeDecimal
        {
            get
            {
                return GPSLatitude == null ? new Double?() :
                    (GPSLatitude[0] + GPSLatitude[1] / 60d + GPSLatitude[2] / 3600d) * (GPSLatitudeRef == "S" ? -1d : 1d);
            }
        }
        public Double? GPSLongitudeDecimal
        {
            get
            {
                return GPSLongitude == null ? new Double?() :
                    (GPSLongitude[0] + GPSLongitude[1] / 60d + GPSLongitude[2] / 3600d) * (GPSLongitudeRef == "W" ? -1d : 1d);
            }
        }
        [JsonProperty(PropertyName = "location")]
        public Point Location
        {
            get
            {
                return GPSLongitudeDecimal.HasValue && GPSLatitudeDecimal.HasValue
                    ? new Point(GPSLongitudeDecimal.Value, GPSLatitudeDecimal.Value)
                    : null;
            }
        }
    }


}