# Exif File Reader

Over the weekend I had a play with some cool bits - probably old news to most, but I have about 10000 pics on my "Skydrive Camera Roll" in OneDrive and I wanted to get all the pics taken from one spot.:

I used [ExifLib](https://www.nuget.org/packages/ExifLib) (now .NET Standard so I could use it in my .NET5 console app) to grab all the Exif Data from every jpg using an extension method (so I could do a `filename.ReadExifData()`) and wrote it to a Cosmos container including a new property called location with the lat and long. I made sure the container knew I was using geography data (Lat/Lon) and now I can just do a [Cosmos DB spatial query](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-query-geospatial-query) like

``` SQL
SELECT c.FileName FROM c
WHERE ST_DISTANCE(c.ExifData.location, {"type": "Point", "coordinates":[151.555, -33.143]}) < 10
```

and that returns the names of all the pictures taken within 10 m of that point.

There's also a [LINQ implementation of spatial queries](https://docs.microsoft.com/en-us/azure/cosmos-db/sql-query-geospatial-query#linq-querying-in-the-net-sdk) in the .NET Cosmos SDK

I also worked out how to read User Secrets in .NET Core console apps
​​​​​​
## `GetExifData()` Extension Method

Created the `GetExifData()` extension method to grab all the EXIF data available for a `Path` string.

I generated the first pass of the population of the `ExifData` class and the extraction method by using `Enum.GetValues()` on the `ExifLib.ExifTags` enum and pasting the output into a [spreadsheet](./enum.xlsx). Then used a formula to generate the C# code to paste into the files.

This generated something like this for the extension method:

``` CSharp
string gpsdestlatituderef; if (reader.GetTagValue<string>(ExifTags.GPSDestLatitudeRef, out gpsdestlatituderef)) exifData.GPSDestLatitudeRef = gpsdestlatituderef;
```

and this in the class definition:

``` CSharp
public string GPSDestLatitudeRef { get; set; }
```

Next, I ran the method over a selection of my JPEG files to see what broke.

Sometimes, the type was always wrong, so I updated the spreadsheet to:

``` CSharp
Double gpsimgdirection; if (reader.GetTagValue<Double>(ExifTags.GPSImgDirection, out gpsimgdirection)) exifData.GPSImgDirection = gpsimgdirection;
```

and

``` CSharp
public Double GPSImgDirection { get; set; }
```

Other times, it varied between (I guess) Exif versions, so I ended up with a bunch of conditional logic:

``` CSharp
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
```

Through a series of trial and error, I got to the current implementation which doesn't break on any of my current sample of JPEGs.
