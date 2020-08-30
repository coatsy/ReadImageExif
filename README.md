# Exif File Reader

Over the weekend I had a play with some cool bits - probably old news to most, but I have about 10000 pics on my "Skydrive Camera Roll" in OneDrive and I wanted to get all the pics taken from one spot.:

I used [ExifLib](https://www.nuget.org/packages/ExifLib) (now .NET Standard so I could use it in my .NET5 console app) to grab all the Exif Data from every jpg using an extension method (so I could do a `filename.ReadExifData()`) and wrote it to a Cosmos container including a new property called location with the lat and long. I made sure the container knew I was using geography data (Lat/Lon) and now I can just do a query like

```
SELECT c.FileName FROM c 
WHERE ST_DISTANCE(c.ExifData.location, {"type": "Point", "coordinates":[151.555, -33.143]}) <10
```

and that returns the names of all the pictures taken within 10 m of that point.

There's also a LINQ implementation in the .NET Cosmos SDK

I also worked out how to read User Secrets in .NET Core console apps
​​​​​​