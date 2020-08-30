using System;
using ExifLib;
using Newtonsoft.Json;

namespace ReadImageExif
{
    public static class Extensions
    {
        public static ExifData GetExifData(this string filePath)
        {
            ExifData exifData = new ExifData();

            try
            {
                using (ExifReader reader = new ExifReader(filePath))
                {
                    string gpsversionid; if (reader.GetTagValue<string>(ExifTags.GPSVersionID, out gpsversionid)) exifData.GPSVersionID = gpsversionid;
                    string gpslatituderef; if (reader.GetTagValue<string>(ExifTags.GPSLatitudeRef, out gpslatituderef)) exifData.GPSLatitudeRef = gpslatituderef;
                    Double[] gpslatitude; if (reader.GetTagValue<Double[]>(ExifTags.GPSLatitude, out gpslatitude)) exifData.GPSLatitude = gpslatitude;
                    string gpslongituderef; if (reader.GetTagValue<string>(ExifTags.GPSLongitudeRef, out gpslongituderef)) exifData.GPSLongitudeRef = gpslongituderef;
                    Double[] gpslongitude; if (reader.GetTagValue<Double[]>(ExifTags.GPSLongitude, out gpslongitude)) exifData.GPSLongitude = gpslongitude;
                    byte[] gpsaltituderef;
                    try
                    {
                        if (reader.GetTagValue<byte[]>(ExifTags.GPSAltitudeRef, out gpsaltituderef))
                            exifData.GPSAltitudeRef = gpsaltituderef;
                    }
                    catch (Exception)
                    {
                        byte gpsaltituderefbyte;
                        if (reader.GetTagValue<byte>(ExifTags.GPSAltitudeRef, out gpsaltituderefbyte))
                            exifData.GPSAltitudeRef = new byte[] { gpsaltituderefbyte };
                    }
                    Double gpsaltitude; if (reader.GetTagValue<Double>(ExifTags.GPSAltitude, out gpsaltitude)) exifData.GPSAltitude = gpsaltitude;
                    Double[] gpstimestamp; if (reader.GetTagValue<Double[]>(ExifTags.GPSTimestamp, out gpstimestamp)) exifData.GPSTimestamp = gpstimestamp;
                    int gpssatellites; if (reader.GetTagValue<int>(ExifTags.GPSSatellites, out gpssatellites)) exifData.GPSSatellites = gpssatellites;
                    string gpsstatus; if (reader.GetTagValue<string>(ExifTags.GPSStatus, out gpsstatus)) exifData.GPSStatus = gpsstatus;
                    string gpsmeasuremode; if (reader.GetTagValue<string>(ExifTags.GPSMeasureMode, out gpsmeasuremode)) exifData.GPSMeasureMode = gpsmeasuremode;
                    Double gpsdop; if (reader.GetTagValue<Double>(ExifTags.GPSDOP, out gpsdop)) exifData.GPSDOP = gpsdop;
                    string gpsspeedref; if (reader.GetTagValue<string>(ExifTags.GPSSpeedRef, out gpsspeedref)) exifData.GPSSpeedRef = gpsspeedref;
                    Double gpsspeed; if (reader.GetTagValue<Double>(ExifTags.GPSSpeed, out gpsspeed)) exifData.GPSSpeed = gpsspeed;
                    string gpstrackref; if (reader.GetTagValue<string>(ExifTags.GPSTrackRef, out gpstrackref)) exifData.GPSTrackRef = gpstrackref;
                    string gpstrack; if (reader.GetTagValue<string>(ExifTags.GPSTrack, out gpstrack)) exifData.GPSTrack = gpstrack;
                    string gpsimgdirectionref; if (reader.GetTagValue<string>(ExifTags.GPSImgDirectionRef, out gpsimgdirectionref)) exifData.GPSImgDirectionRef = gpsimgdirectionref;
                    Double gpsimgdirection; if (reader.GetTagValue<Double>(ExifTags.GPSImgDirection, out gpsimgdirection)) exifData.GPSImgDirection = gpsimgdirection;
                    string gpsmapdatum; if (reader.GetTagValue<string>(ExifTags.GPSMapDatum, out gpsmapdatum)) exifData.GPSMapDatum = gpsmapdatum;
                    string gpsdestlatituderef; if (reader.GetTagValue<string>(ExifTags.GPSDestLatitudeRef, out gpsdestlatituderef)) exifData.GPSDestLatitudeRef = gpsdestlatituderef;
                    Double gpsdestlatitude; if (reader.GetTagValue<Double>(ExifTags.GPSDestLatitude, out gpsdestlatitude)) exifData.GPSDestLatitude = gpsdestlatitude;
                    string gpsdestlongituderef; if (reader.GetTagValue<string>(ExifTags.GPSDestLongitudeRef, out gpsdestlongituderef)) exifData.GPSDestLongitudeRef = gpsdestlongituderef;
                    Double gpsdestlongitude; if (reader.GetTagValue<Double>(ExifTags.GPSDestLongitude, out gpsdestlongitude)) exifData.GPSDestLongitude = gpsdestlongitude;
                    string gpsdestbearingref; if (reader.GetTagValue<string>(ExifTags.GPSDestBearingRef, out gpsdestbearingref)) exifData.GPSDestBearingRef = gpsdestbearingref;
                    Double gpsdestbearing; if (reader.GetTagValue<Double>(ExifTags.GPSDestBearing, out gpsdestbearing)) exifData.GPSDestBearing = gpsdestbearing;
                    string gpsdestdistanceref; if (reader.GetTagValue<string>(ExifTags.GPSDestDistanceRef, out gpsdestdistanceref)) exifData.GPSDestDistanceRef = gpsdestdistanceref;
                    Double gpsdestdistance; if (reader.GetTagValue<Double>(ExifTags.GPSDestDistance, out gpsdestdistance)) exifData.GPSDestDistance = gpsdestdistance;
                    byte[] gpsprocessingmethod; if (reader.GetTagValue<byte[]>(ExifTags.GPSProcessingMethod, out gpsprocessingmethod)) exifData.GPSProcessingMethod = gpsprocessingmethod;
                    string gpsareainformation; if (reader.GetTagValue<string>(ExifTags.GPSAreaInformation, out gpsareainformation)) exifData.GPSAreaInformation = gpsareainformation;
                    string gpsdatestamp; if (reader.GetTagValue<string>(ExifTags.GPSDateStamp, out gpsdatestamp)) exifData.GPSDateStamp = gpsdatestamp;
                    string gpsdifferential; if (reader.GetTagValue<string>(ExifTags.GPSDifferential, out gpsdifferential)) exifData.GPSDifferential = gpsdifferential;
                    string gpshpositioningerror; if (reader.GetTagValue<string>(ExifTags.GPSHPositioningError, out gpshpositioningerror)) exifData.GPSHPositioningError = gpshpositioningerror;
                    UInt32 imagewidth;
                    try
                    {
                        if (reader.GetTagValue<UInt32>(ExifTags.ImageWidth, out imagewidth))
                            exifData.ImageWidth = imagewidth;
                    }
                    catch (Exception)
                    {
                        UInt16 imageWidth16;
                        if (reader.GetTagValue<UInt16>(ExifTags.ImageWidth, out imageWidth16))
                            exifData.ImageWidth = (UInt32)imageWidth16;
                    }
                    UInt32 imagelength;
                    try
                    {
                        if (reader.GetTagValue<UInt32>(ExifTags.ImageLength, out imagelength))
                            exifData.ImageLength = imagelength;
                    }
                    catch (Exception)
                    {
                        UInt16 imageLength16;
                        if (reader.GetTagValue<UInt16>(ExifTags.ImageLength, out imageLength16))
                            exifData.ImageLength = (UInt32)imageLength16;
                    }
                    int bitspersample; if (reader.GetTagValue<int>(ExifTags.BitsPerSample, out bitspersample)) exifData.BitsPerSample = bitspersample;
                    string compression; if (reader.GetTagValue<string>(ExifTags.Compression, out compression)) exifData.Compression = compression;
                    string photometricinterpretation; if (reader.GetTagValue<string>(ExifTags.PhotometricInterpretation, out photometricinterpretation)) exifData.PhotometricInterpretation = photometricinterpretation;
                    string imagedescription; if (reader.GetTagValue<string>(ExifTags.ImageDescription, out imagedescription)) exifData.ImageDescription = imagedescription;
                    string make; if (reader.GetTagValue<string>(ExifTags.Make, out make)) exifData.Make = make;
                    string model; if (reader.GetTagValue<string>(ExifTags.Model, out model)) exifData.Model = model;
                    string stripoffsets; if (reader.GetTagValue<string>(ExifTags.StripOffsets, out stripoffsets)) exifData.StripOffsets = stripoffsets;
                    UInt16 orientation; if (reader.GetTagValue<UInt16>(ExifTags.Orientation, out orientation)) exifData.Orientation = orientation;
                    Double samplesperpixel; if (reader.GetTagValue<Double>(ExifTags.SamplesPerPixel, out samplesperpixel)) exifData.SamplesPerPixel = samplesperpixel;
                    Double rowsperstrip; if (reader.GetTagValue<Double>(ExifTags.RowsPerStrip, out rowsperstrip)) exifData.RowsPerStrip = rowsperstrip;
                    Double stripbytecounts; if (reader.GetTagValue<Double>(ExifTags.StripByteCounts, out stripbytecounts)) exifData.StripByteCounts = stripbytecounts;
                    Double xresolution; if (reader.GetTagValue<Double>(ExifTags.XResolution, out xresolution)) exifData.XResolution = xresolution;
                    Double yresolution; if (reader.GetTagValue<Double>(ExifTags.YResolution, out yresolution)) exifData.YResolution = yresolution;
                    string planarconfiguration; if (reader.GetTagValue<string>(ExifTags.PlanarConfiguration, out planarconfiguration)) exifData.PlanarConfiguration = planarconfiguration;
                    UInt16 resolutionunit; if (reader.GetTagValue<UInt16>(ExifTags.ResolutionUnit, out resolutionunit)) exifData.ResolutionUnit = resolutionunit;
                    string transferfunction; if (reader.GetTagValue<string>(ExifTags.TransferFunction, out transferfunction)) exifData.TransferFunction = transferfunction;
                    string software; if (reader.GetTagValue<string>(ExifTags.Software, out software)) exifData.Software = software;
                    DateTime datetime; if (reader.GetTagValue<DateTime>(ExifTags.DateTime, out datetime)) exifData.DateTime = datetime;
                    string artist; if (reader.GetTagValue<string>(ExifTags.Artist, out artist)) exifData.Artist = artist;
                    string whitepoint; if (reader.GetTagValue<string>(ExifTags.WhitePoint, out whitepoint)) exifData.WhitePoint = whitepoint;
                    string primarychromaticities; if (reader.GetTagValue<string>(ExifTags.PrimaryChromaticities, out primarychromaticities)) exifData.PrimaryChromaticities = primarychromaticities;
                    string jpeginterchangeformat; if (reader.GetTagValue<string>(ExifTags.JPEGInterchangeFormat, out jpeginterchangeformat)) exifData.JPEGInterchangeFormat = jpeginterchangeformat;
                    string jpeginterchangeformatlength; if (reader.GetTagValue<string>(ExifTags.JPEGInterchangeFormatLength, out jpeginterchangeformatlength)) exifData.JPEGInterchangeFormatLength = jpeginterchangeformatlength;
                    string ycbcrcoefficients; if (reader.GetTagValue<string>(ExifTags.YCbCrCoefficients, out ycbcrcoefficients)) exifData.YCbCrCoefficients = ycbcrcoefficients;
                    string ycbcrsubsampling;
                    try
                    {
                        if (reader.GetTagValue<string>(ExifTags.YCbCrSubSampling, out ycbcrsubsampling))
                            exifData.YCbCrSubSampling = ycbcrsubsampling;
                    }
                    catch (Exception)
                    {
                        UInt16[] ycbcrsubsamplinguint16;
                        if (reader.GetTagValue<UInt16[]>(ExifTags.YCbCrSubSampling, out ycbcrsubsamplinguint16))
                            exifData.YCbCrSubSampling = JsonConvert.SerializeObject(ycbcrsubsamplinguint16);
                    }
                    UInt16 ycbcrpositioning; if (reader.GetTagValue<UInt16>(ExifTags.YCbCrPositioning, out ycbcrpositioning)) exifData.YCbCrPositioning = ycbcrpositioning;
                    string referenceblackwhite; if (reader.GetTagValue<string>(ExifTags.ReferenceBlackWhite, out referenceblackwhite)) exifData.ReferenceBlackWhite = referenceblackwhite;
                    string copyright; if (reader.GetTagValue<string>(ExifTags.Copyright, out copyright)) exifData.Copyright = copyright;
                    Double exposuretime; if (reader.GetTagValue<Double>(ExifTags.ExposureTime, out exposuretime)) exifData.ExposureTime = exposuretime;
                    Double fnumber; if (reader.GetTagValue<Double>(ExifTags.FNumber, out fnumber)) exifData.FNumber = fnumber;
                    UInt16 exposureprogram; if (reader.GetTagValue<UInt16>(ExifTags.ExposureProgram, out exposureprogram)) exifData.ExposureProgram = exposureprogram;
                    string spectralsensitivity; if (reader.GetTagValue<string>(ExifTags.SpectralSensitivity, out spectralsensitivity)) exifData.SpectralSensitivity = spectralsensitivity;
                    UInt16 photographicsensitivity; if (reader.GetTagValue<UInt16>(ExifTags.PhotographicSensitivity, out photographicsensitivity)) exifData.PhotographicSensitivity = photographicsensitivity;
                    string oecf; if (reader.GetTagValue<string>(ExifTags.OECF, out oecf)) exifData.OECF = oecf;
                    string sensitivitytype; if (reader.GetTagValue<string>(ExifTags.SensitivityType, out sensitivitytype)) exifData.SensitivityType = sensitivitytype;
                    string standardoutputsensitivity; if (reader.GetTagValue<string>(ExifTags.StandardOutputSensitivity, out standardoutputsensitivity)) exifData.StandardOutputSensitivity = standardoutputsensitivity;
                    string recommendedexposureindex; if (reader.GetTagValue<string>(ExifTags.RecommendedExposureIndex, out recommendedexposureindex)) exifData.RecommendedExposureIndex = recommendedexposureindex;
                    string isospeed; if (reader.GetTagValue<string>(ExifTags.ISOSpeed, out isospeed)) exifData.ISOSpeed = isospeed;
                    string isospeedlatitudeyyy; if (reader.GetTagValue<string>(ExifTags.ISOSpeedLatitudeyyy, out isospeedlatitudeyyy)) exifData.ISOSpeedLatitudeyyy = isospeedlatitudeyyy;
                    string isospeedlatitudezzz; if (reader.GetTagValue<string>(ExifTags.ISOSpeedLatitudezzz, out isospeedlatitudezzz)) exifData.ISOSpeedLatitudezzz = isospeedlatitudezzz;
                    byte[] exifversion; if (reader.GetTagValue<byte[]>(ExifTags.ExifVersion, out exifversion)) exifData.ExifVersion = exifversion;
                    DateTime datetimeoriginal; if (reader.GetTagValue<DateTime>(ExifTags.DateTimeOriginal, out datetimeoriginal)) exifData.DateTimeOriginal = datetimeoriginal;
                    DateTime datetimedigitized; if (reader.GetTagValue<DateTime>(ExifTags.DateTimeDigitized, out datetimedigitized)) exifData.DateTimeDigitized = datetimedigitized;
                    byte[] componentsconfiguration; if (reader.GetTagValue<byte[]>(ExifTags.ComponentsConfiguration, out componentsconfiguration)) exifData.ComponentsConfiguration = componentsconfiguration;
                    string compressedbitsperpixel; if (reader.GetTagValue<string>(ExifTags.CompressedBitsPerPixel, out compressedbitsperpixel)) exifData.CompressedBitsPerPixel = compressedbitsperpixel;
                    Double shutterspeedvalue; if (reader.GetTagValue<Double>(ExifTags.ShutterSpeedValue, out shutterspeedvalue)) exifData.ShutterSpeedValue = shutterspeedvalue;
                    Double aperturevalue; if (reader.GetTagValue<Double>(ExifTags.ApertureValue, out aperturevalue)) exifData.ApertureValue = aperturevalue;
                    Double brightnessvalue; if (reader.GetTagValue<Double>(ExifTags.BrightnessValue, out brightnessvalue)) exifData.BrightnessValue = brightnessvalue;
                    Double exposurebiasvalue; if (reader.GetTagValue<Double>(ExifTags.ExposureBiasValue, out exposurebiasvalue)) exifData.ExposureBiasValue = exposurebiasvalue;
                    Double maxaperturevalue; if (reader.GetTagValue<Double>(ExifTags.MaxApertureValue, out maxaperturevalue)) exifData.MaxApertureValue = maxaperturevalue;
                    Double subjectdistance; if (reader.GetTagValue<Double>(ExifTags.SubjectDistance, out subjectdistance)) exifData.SubjectDistance = subjectdistance;
                    UInt16 meteringmode; if (reader.GetTagValue<UInt16>(ExifTags.MeteringMode, out meteringmode)) exifData.MeteringMode = meteringmode;
                    string lightsource;
                    try
                    {
                        if (reader.GetTagValue<string>(ExifTags.LightSource, out lightsource))
                            exifData.LightSource = lightsource;
                    }
                    catch (Exception)
                    {
                        UInt32 lightsourceuint32;
                        if (reader.GetTagValue<UInt32>(ExifTags.LightSource, out lightsourceuint32))
                            exifData.LightSource = lightsourceuint32.ToString();

                    }
                    UInt16 flash; if (reader.GetTagValue<UInt16>(ExifTags.Flash, out flash)) exifData.Flash = flash;
                    Double focallength; if (reader.GetTagValue<Double>(ExifTags.FocalLength, out focallength)) exifData.FocalLength = focallength;
                    string subjectarea; if (reader.GetTagValue<string>(ExifTags.SubjectArea, out subjectarea)) exifData.SubjectArea = subjectarea;
                    byte[] makernote; if (reader.GetTagValue<byte[]>(ExifTags.MakerNote, out makernote)) exifData.MakerNote = makernote;
                    string usercomment; if (reader.GetTagValue<string>(ExifTags.UserComment, out usercomment)) exifData.UserComment = usercomment;
                    string subsectime; if (reader.GetTagValue<string>(ExifTags.SubsecTime, out subsectime)) exifData.SubsecTime = subsectime;
                    string subsectimeoriginal; if (reader.GetTagValue<string>(ExifTags.SubsecTimeOriginal, out subsectimeoriginal)) exifData.SubsecTimeOriginal = subsectimeoriginal;
                    string subsectimedigitized; if (reader.GetTagValue<string>(ExifTags.SubsecTimeDigitized, out subsectimedigitized)) exifData.SubsecTimeDigitized = subsectimedigitized;
                    string xptitle; if (reader.GetTagValue<string>(ExifTags.XPTitle, out xptitle)) exifData.XPTitle = xptitle;
                    string xpcomment; if (reader.GetTagValue<string>(ExifTags.XPComment, out xpcomment)) exifData.XPComment = xpcomment;
                    string xpauthor; if (reader.GetTagValue<string>(ExifTags.XPAuthor, out xpauthor)) exifData.XPAuthor = xpauthor;
                    string xpkeywords; if (reader.GetTagValue<string>(ExifTags.XPKeywords, out xpkeywords)) exifData.XPKeywords = xpkeywords;
                    string xpsubject; if (reader.GetTagValue<string>(ExifTags.XPSubject, out xpsubject)) exifData.XPSubject = xpsubject;
                    byte[] flashpixversion; if (reader.GetTagValue<byte[]>(ExifTags.FlashpixVersion, out flashpixversion)) exifData.FlashpixVersion = flashpixversion;
                    UInt16 colorspace; if (reader.GetTagValue<UInt16>(ExifTags.ColorSpace, out colorspace)) exifData.ColorSpace = colorspace;
                    UInt32 pixelxdimension;
                    try
                    {
                        if (reader.GetTagValue<UInt32>(ExifTags.PixelXDimension, out pixelxdimension))
                            exifData.PixelXDimension = pixelxdimension;
                    }
                    catch (Exception)
                    {
                        UInt16 pixelxdimension16;
                        if (reader.GetTagValue<UInt16>(ExifTags.PixelXDimension, out pixelxdimension16))
                            exifData.PixelXDimension = (UInt32)pixelxdimension16;
                    }
                    UInt32 pixelydimension;
                    try
                    {
                        if (reader.GetTagValue<UInt32>(ExifTags.PixelYDimension, out pixelydimension))
                            exifData.PixelYDimension = pixelydimension;
                    }
                    catch (Exception)
                    {
                        UInt16 pixelydimension16;
                        if (reader.GetTagValue<UInt16>(ExifTags.PixelYDimension, out pixelydimension16))
                            exifData.PixelYDimension = (UInt32)pixelydimension16;
                    }
                    string relatedsoundfile; if (reader.GetTagValue<string>(ExifTags.RelatedSoundFile, out relatedsoundfile)) exifData.RelatedSoundFile = relatedsoundfile;
                    string flashenergy; if (reader.GetTagValue<string>(ExifTags.FlashEnergy, out flashenergy)) exifData.FlashEnergy = flashenergy;
                    string spatialfrequencyresponse; if (reader.GetTagValue<string>(ExifTags.SpatialFrequencyResponse, out spatialfrequencyresponse)) exifData.SpatialFrequencyResponse = spatialfrequencyresponse;
                    string focalplanexresolution; if (reader.GetTagValue<string>(ExifTags.FocalPlaneXResolution, out focalplanexresolution)) exifData.FocalPlaneXResolution = focalplanexresolution;
                    string focalplaneyresolution; if (reader.GetTagValue<string>(ExifTags.FocalPlaneYResolution, out focalplaneyresolution)) exifData.FocalPlaneYResolution = focalplaneyresolution;
                    string focalplaneresolutionunit; if (reader.GetTagValue<string>(ExifTags.FocalPlaneResolutionUnit, out focalplaneresolutionunit)) exifData.FocalPlaneResolutionUnit = focalplaneresolutionunit;
                    string subjectlocation; if (reader.GetTagValue<string>(ExifTags.SubjectLocation, out subjectlocation)) exifData.SubjectLocation = subjectlocation;
                    string exposureindex; if (reader.GetTagValue<string>(ExifTags.ExposureIndex, out exposureindex)) exifData.ExposureIndex = exposureindex;
                    UInt16 sensingmethod; if (reader.GetTagValue<UInt16>(ExifTags.SensingMethod, out sensingmethod)) exifData.SensingMethod = sensingmethod;
                    string filesource; if (reader.GetTagValue<string>(ExifTags.FileSource, out filesource)) exifData.FileSource = filesource;
                    byte scenetype; if (reader.GetTagValue<byte>(ExifTags.SceneType, out scenetype)) exifData.SceneType = scenetype;
                    string cfapattern; if (reader.GetTagValue<string>(ExifTags.CFAPattern, out cfapattern)) exifData.CFAPattern = cfapattern;
                    UInt16 customrendered; if (reader.GetTagValue<UInt16>(ExifTags.CustomRendered, out customrendered)) exifData.CustomRendered = customrendered;
                    UInt16 exposuremode; if (reader.GetTagValue<UInt16>(ExifTags.ExposureMode, out exposuremode)) exifData.ExposureMode = exposuremode;
                    UInt16 whitebalance; if (reader.GetTagValue<UInt16>(ExifTags.WhiteBalance, out whitebalance)) exifData.WhiteBalance = whitebalance;
                    Double digitalzoomratio; if (reader.GetTagValue<Double>(ExifTags.DigitalZoomRatio, out digitalzoomratio)) exifData.DigitalZoomRatio = digitalzoomratio;
                    UInt16 focallengthin35mmfilm; if (reader.GetTagValue<UInt16>(ExifTags.FocalLengthIn35mmFilm, out focallengthin35mmfilm)) exifData.FocalLengthIn35mmFilm = focallengthin35mmfilm;
                    UInt16 scenecapturetype; if (reader.GetTagValue<UInt16>(ExifTags.SceneCaptureType, out scenecapturetype)) exifData.SceneCaptureType = scenecapturetype;
                    string gaincontrol; if (reader.GetTagValue<string>(ExifTags.GainControl, out gaincontrol)) exifData.GainControl = gaincontrol;
                    UInt16 contrast; if (reader.GetTagValue<UInt16>(ExifTags.Contrast, out contrast)) exifData.Contrast = contrast;
                    UInt16 saturation; if (reader.GetTagValue<UInt16>(ExifTags.Saturation, out saturation)) exifData.Saturation = saturation;
                    UInt16 sharpness; if (reader.GetTagValue<UInt16>(ExifTags.Sharpness, out sharpness)) exifData.Sharpness = sharpness;
                    string devicesettingdescription; if (reader.GetTagValue<string>(ExifTags.DeviceSettingDescription, out devicesettingdescription)) exifData.DeviceSettingDescription = devicesettingdescription;
                    UInt16 subjectdistancerange; if (reader.GetTagValue<UInt16>(ExifTags.SubjectDistanceRange, out subjectdistancerange)) exifData.SubjectDistanceRange = subjectdistancerange;
                    string imageuniqueid; if (reader.GetTagValue<string>(ExifTags.ImageUniqueID, out imageuniqueid)) exifData.ImageUniqueID = imageuniqueid;
                    string cameraownername; if (reader.GetTagValue<string>(ExifTags.CameraOwnerName, out cameraownername)) exifData.CameraOwnerName = cameraownername;
                    string bodyserialnumber; if (reader.GetTagValue<string>(ExifTags.BodySerialNumber, out bodyserialnumber)) exifData.BodySerialNumber = bodyserialnumber;
                    string lensspecification; if (reader.GetTagValue<string>(ExifTags.LensSpecification, out lensspecification)) exifData.LensSpecification = lensspecification;
                    string lensmake; if (reader.GetTagValue<string>(ExifTags.LensMake, out lensmake)) exifData.LensMake = lensmake;
                    string lensmodel; if (reader.GetTagValue<string>(ExifTags.LensModel, out lensmodel)) exifData.LensModel = lensmodel;
                    string lensserialnumber; if (reader.GetTagValue<string>(ExifTags.LensSerialNumber, out lensserialnumber)) exifData.LensSerialNumber = lensserialnumber;
                }
            }
            catch (Exception) { }

            return exifData;
        }
    }

}