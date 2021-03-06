﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Xml.Serialization;

namespace GeometryServer.Services
{
    public static class Utilities
    {
        public static dynamic GetGeometries(string GeometryString)
        {
            JObject o = JObject.Parse(GeometryString);
            dynamic inputgeometrylist;
            string geometrytype = (string)o["geometryType"];

            JArray geometries = (JArray)o["geometries"];
            switch (geometrytype)
            {
                case "GeometryPoint":
                case "esriGeometryPoint":
                    inputgeometrylist = new List<DotSpatial.Topology.Point>();
                    for (int i = 0; i < geometries.Count; i++)
                    {
                        var p = geometries[i].ToObject<GISServer.Core.Geometry.Point>();
                        inputgeometrylist.Add(new DotSpatial.Topology.Point(p.X, p.Y));
                    }
                    return inputgeometrylist;
                case "GeometryMultipoint":
                case "esriGeometryMultipoint":
                    inputgeometrylist = new List<DotSpatial.Topology.MultiPoint>();
                    for (int i = 0; i < geometries.Count; i++)
                    {
                        var coorlist = new DotSpatial.Topology.CoordinateList();
                        var mp = geometries[i].ToObject<GISServer.Core.Geometry.MultiPoint>();
                        for (int j = 0; j < mp.Points.Count; j++)
                        {
                            coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Points[j].X, mp.Points[j].Y));

                        }
                        inputgeometrylist.Add(new DotSpatial.Topology.MultiPoint(coorlist));
                    }
                    return inputgeometrylist;
                case "GeometryPolyline":
                case "esriGeometryPolyline":
                    inputgeometrylist = new List<DotSpatial.Topology.MultiLineString>();
                    for (int i = 0; i < geometries.Count; i++)
                    {
                        var ml = new DotSpatial.Topology.MultiLineString();
                        var lineStrings = new List<DotSpatial.Topology.LineString>();
                        var mp = geometries[i].ToObject<GISServer.Core.Geometry.Polyline>();
                        for (int j = 0; j < mp.Paths.Count; j++)
                        {
                            var coorlist = new DotSpatial.Topology.CoordinateList();
                            for (int k = 0; k < mp.Paths[j].Count; k++)
                            {
                                coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Paths[j][k].X, mp.Paths[j][k].Y));
                            }
                            lineStrings.Add(new DotSpatial.Topology.LineString(coorlist));
                        }
                        inputgeometrylist.Add(new DotSpatial.Topology.MultiLineString(lineStrings));
                    }
                    return inputgeometrylist;
                case "GeometryPolygon":
                case "esriGeometryPolygon":
                    inputgeometrylist = new List<DotSpatial.Topology.MultiPolygon>();
                    for (int i = 0; i < geometries.Count; i++)
                    {
                        var ml = new DotSpatial.Topology.MultiLineString();
                        var shellstring = new List<DotSpatial.Topology.Polygon>();
                        var mp = geometries[i].ToObject<GISServer.Core.Geometry.Polygon>();
                        for (int j = 0; j < mp.Rings.Count; j++)
                        {
                            var coorlist = new DotSpatial.Topology.CoordinateList();
                            for (int k = 0; k < mp.Rings[j].Count; k++)
                            {
                                coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Rings[j][k].X, mp.Rings[j][k].Y));
                            }
                            shellstring.Add(new DotSpatial.Topology.Polygon(new DotSpatial.Topology.LinearRing(coorlist)));
                        }
                        inputgeometrylist.Add(new DotSpatial.Topology.MultiPolygon(shellstring.ToArray()));
                    }
                    return inputgeometrylist;
                case "GeometryEnvelope":
                case "esriGeometryEnvelope":
                    inputgeometrylist = new List<DotSpatial.Topology.Envelope>();
                    for (int i = 0; i < geometries.Count; i++)
                    {
                        var p = geometries[i].ToObject<GISServer.Core.Geometry.Envelope>();
                        inputgeometrylist.Add(new DotSpatial.Topology.Envelope(p.XMin, p.XMax, p.YMin, p.YMax));
                    }
                    return inputgeometrylist;
                default:
                    break;
            }
            return null;
        }

        public static dynamic GetGeometry(string GeometryString, string geometrytype)
        {
            JObject o = JObject.Parse(GeometryString);
            dynamic inputgeometrylist;
            

            
            switch (geometrytype)
            {
                case "GeometryPoint":
                case "esriGeometryPoint":
                    {
                        JArray geometries = (JArray)o["geometries"];
                        inputgeometrylist = new List<DotSpatial.Topology.Point>();
                        for (int i = 0; i < geometries.Count; i++)
                        {
                            var p = geometries[i].ToObject<GISServer.Core.Geometry.Point>();
                            inputgeometrylist.Add(new DotSpatial.Topology.Point(p.X, p.Y));
                        }
                        return inputgeometrylist;
                    }
                case "GeometryMultipoint":
                case "esriGeometryMultipoint":
                    {
                        JArray geometries = (JArray)o["geometries"];
                        inputgeometrylist = new List<DotSpatial.Topology.MultiPoint>();
                        for (int i = 0; i < geometries.Count; i++)
                        {
                            var coorlist = new DotSpatial.Topology.CoordinateList();
                            var mp = geometries[i].ToObject<GISServer.Core.Geometry.MultiPoint>();
                            for (int j = 0; j < mp.Points.Count; j++)
                            {
                                coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Points[j].X, mp.Points[j].Y));

                            }
                            inputgeometrylist.Add(new DotSpatial.Topology.MultiPoint(coorlist));
                        }
                        return inputgeometrylist;
                    }
                case "GeometryPolyline":
                case "esriGeometryPolyline":
                    {
                        JArray geometries = (JArray)o["geometries"];
                        inputgeometrylist = new List<DotSpatial.Topology.MultiLineString>();
                        for (int i = 0; i < geometries.Count; i++)
                        {
                            var ml = new DotSpatial.Topology.MultiLineString();
                            var lineStrings = new List<DotSpatial.Topology.LineString>();
                            var mp = geometries[i].ToObject<GISServer.Core.Geometry.Polyline>();
                            for (int j = 0; j < mp.Paths.Count; j++)
                            {
                                var coorlist = new DotSpatial.Topology.CoordinateList();
                                for (int k = 0; k < mp.Paths[j].Count; k++)
                                {
                                    coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Paths[j][k].X, mp.Paths[j][k].Y));
                                }
                                lineStrings.Add(new DotSpatial.Topology.LineString(coorlist));
                            }
                            inputgeometrylist.Add(new DotSpatial.Topology.MultiLineString(lineStrings));
                        }
                        return inputgeometrylist;
                    }
                case "GeometryPolygon":
                case "esriGeometryPolygon":
                    {
                        JArray geometries = (JArray)o["rings"];
                        inputgeometrylist = new List<DotSpatial.Topology.MultiPolygon>();
                        for (int i = 0; i < geometries.Count; i++)
                        {
                            var ml = new DotSpatial.Topology.MultiLineString();
                            var shellstring = new List<DotSpatial.Topology.Polygon>();
                            var mp = geometries[i].ToObject<GISServer.Core.Geometry.Polygon>();
                            for (int j = 0; j < mp.Rings.Count; j++)
                            {
                                var coorlist = new DotSpatial.Topology.CoordinateList();
                                for (int k = 0; k < mp.Rings[j].Count; k++)
                                {
                                    coorlist.Add(new DotSpatial.Topology.Coordinate(mp.Rings[j][k].X, mp.Rings[j][k].Y));
                                }
                                shellstring.Add(new DotSpatial.Topology.Polygon(new DotSpatial.Topology.LinearRing(coorlist)));
                            }
                            inputgeometrylist.Add(new DotSpatial.Topology.MultiPolygon(shellstring.ToArray()));
                        }
                        return inputgeometrylist;
                    }
                case "GeometryEnvelope":
                case "esriGeometryEnvelope":
                    {
                        JArray geometries = (JArray)o["geometries"];
                        inputgeometrylist = new List<DotSpatial.Topology.Envelope>();
                        for (int i = 0; i < geometries.Count; i++)
                        {
                            var p = geometries[i].ToObject<GISServer.Core.Geometry.Envelope>();
                            inputgeometrylist.Add(new DotSpatial.Topology.Envelope(p.XMin, p.XMax, p.YMin, p.YMax));
                        }
                        return inputgeometrylist;
                    }
                default:
                    break;
            }
            return null;
        }

        public static dynamic GetGISGeometries(dynamic Geometry)
        {
            var geometries = new GISServer.Core.Geometry.Geometries();
            var geometrieslist = new List<GISServer.Core.Geometry.Geometry>();
            if (Geometry is DotSpatial.Topology.Point)
            {
                var point = new GISServer.Core.Geometry.Point(Geometry.X, Geometry.Y);
                geometrieslist.Add(point);
                geometries.GeometryType = "GeometryPoint";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is DotSpatial.Topology.MultiPoint)
            {
                var multipoint = new GISServer.Core.Geometry.MultiPoint();
                multipoint.Points = new List<GISServer.Core.Geometry.Point>();
                foreach (var coordinate in Geometry.Coordinates)
                {
                    var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                    multipoint.Points.Add(point);
                }
                geometrieslist.Add(multipoint);
                geometries.GeometryType = "GeometryMultipoint";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is DotSpatial.Topology.MultiLineString)
            {
                var multiline = new GISServer.Core.Geometry.Polyline();
                multiline.Paths = new List<GISServer.Core.Geometry.PointCollection>();

                for (int i = 0; i < Geometry.Count; i++)
                {
                    var pointcollection = new GISServer.Core.Geometry.PointCollection();
                    foreach (var coordinate in Geometry[i].Coordinates)
                    {
                        var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                        pointcollection.Add(point);
                    }
                    multiline.Paths.Add(pointcollection);
                }
                geometrieslist.Add(multiline);
                geometries.GeometryType = "GeometryPolyline";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is DotSpatial.Topology.Polygon)
            {
                var polygon = new GISServer.Core.Geometry.Polygon();
                polygon.Rings = new List<GISServer.Core.Geometry.PointCollection>();
                var pointcollection = new GISServer.Core.Geometry.PointCollection();
                foreach (var coordinate in Geometry.Coordinates)
                {
                    var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                    pointcollection.Add(point);
                }
                polygon.Rings.Add(pointcollection);
                geometrieslist.Add(polygon);
                geometries.GeometryType = "GeometryPolygon";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is DotSpatial.Topology.MultiPolygon)
            {
                var polygon = new GISServer.Core.Geometry.Polygon();
                polygon.Rings = new List<GISServer.Core.Geometry.PointCollection>();

                for (int i = 0; i < Geometry.Count; i++)
                {
                    var pointcollection = new GISServer.Core.Geometry.PointCollection();
                    foreach (var coordinate in Geometry[i].Coordinates)
                    {
                        var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                        pointcollection.Add(point);
                    }
                    polygon.Rings.Add(pointcollection);
                }
                geometrieslist.Add(polygon);
                geometries.GeometryType = "GeometryPolygon";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is DotSpatial.Topology.Envelope)
            {
                var envelope = new GISServer.Core.Geometry.Envelope(Geometry.Minimum.X, Geometry.Minimum.Y, Geometry.Maximum.X, Geometry.Minimum.Y);
                geometrieslist.Add(envelope);
                geometries.GeometryType = "GeometryEnvelope";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            else if (Geometry is List<DotSpatial.Topology.IGeometry>)
            {
                foreach (var geometry in Geometry)
                {
                    var polygon = new GISServer.Core.Geometry.Polygon();
                    polygon.Rings = new List<GISServer.Core.Geometry.PointCollection>();
                    var pointcollection = new GISServer.Core.Geometry.PointCollection();
                    foreach (var coordinate in geometry.Coordinates)
                    {
                        var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                        pointcollection.Add(point);
                    }
                    polygon.Rings.Add(pointcollection);
                    geometrieslist.Add(polygon);
                }
               
                geometries.GeometryType = "GeometryPolygon";
                geometries.geometries = geometrieslist;
                return geometries;
            }
            return null;
        }

        public static dynamic GetGISSingleGeometry(dynamic Geometry)
        {
            var singleGeometry = new GISServer.Core.Geometry.SingleGeometry();
            if (Geometry is DotSpatial.Topology.Point)
            {
                var point = new GISServer.Core.Geometry.Point(Geometry.X, Geometry.Y);
                singleGeometry.GeometryType = "GeometryPoint";
                singleGeometry.geometry = point;
                return singleGeometry;
            }
            else if (Geometry is DotSpatial.Topology.MultiPoint)
            {
                var multipoint = new GISServer.Core.Geometry.MultiPoint();
                multipoint.Points = new List<GISServer.Core.Geometry.Point>();
                foreach (var coordinate in Geometry.Coordinates)
                {
                    var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                    multipoint.Points.Add(point);
                }
                singleGeometry.GeometryType = "GeometryMultipoint";
                singleGeometry.geometry = multipoint;
                return singleGeometry;
            }
            else if (Geometry is DotSpatial.Topology.MultiLineString)
            {
                var multiline = new GISServer.Core.Geometry.Polyline();
                multiline.Paths = new List<GISServer.Core.Geometry.PointCollection>();

                for (int i = 0; i < Geometry.Count; i++)
                {
                    var pointcollection = new GISServer.Core.Geometry.PointCollection();
                    foreach (var coordinate in Geometry[i].Coordinates)
                    {
                        var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                        pointcollection.Add(point);
                    }
                    multiline.Paths.Add(pointcollection);
                }
                singleGeometry.GeometryType = "GeometryPolyline";
                singleGeometry.geometry = multiline;
                return singleGeometry;
            }
            else if (Geometry is DotSpatial.Topology.Polygon)
            {
                var polygon = new GISServer.Core.Geometry.Polygon();
                polygon.Rings = new List<GISServer.Core.Geometry.PointCollection>();
                var pointcollection = new GISServer.Core.Geometry.PointCollection();
                foreach (var coordinate in Geometry.Coordinates)
                {
                    var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                    pointcollection.Add(point);
                }
                polygon.Rings.Add(pointcollection);
                singleGeometry.GeometryType = "GeometryPolygon";
                singleGeometry.geometry = polygon;
                return singleGeometry;
            }
            else if (Geometry is DotSpatial.Topology.MultiPolygon)
            {
                var polygon = new GISServer.Core.Geometry.Polygon();
                polygon.Rings = new List<GISServer.Core.Geometry.PointCollection>();

                for (int i = 0; i < Geometry.Count; i++)
                {
                    var pointcollection = new GISServer.Core.Geometry.PointCollection();
                    foreach (var coordinate in Geometry[i].Coordinates)
                    {
                        var point = new GISServer.Core.Geometry.Point(coordinate.X, coordinate.Y);
                        pointcollection.Add(point);
                    }
                    polygon.Rings.Add(pointcollection);
                }
                singleGeometry.GeometryType = "GeometryPolygon";
                singleGeometry.geometry = polygon;
                return singleGeometry;
            }
            else if (Geometry is DotSpatial.Topology.Envelope)
            {
                var envelope = new GISServer.Core.Geometry.Envelope(Geometry.Minimum.X, Geometry.Minimum.Y, Geometry.Maximum.X, Geometry.Minimum.Y);
                singleGeometry.GeometryType = "GeometryEnvelope";
                singleGeometry.geometry = envelope;
                return singleGeometry;
            }
            return null;
        }

        public static string getJSON(dynamic Result)
        {
            if ((Result is GISServer.Core.Geometry.Geometries) || (Result is GISServer.Core.Geometry.SingleGeometry) || (Result is GISServer.Core.GS.AreasAndLengths))
            {
                return JsonConvert.SerializeObject(Result, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            return null;
        }
        public static string getPJSON(dynamic Geometry)
        {
            if ((Geometry is GISServer.Core.Geometry.Geometries) || (Geometry is GISServer.Core.Geometry.SingleGeometry) || (Geometry is GISServer.Core.GS.AreasAndLengths))
            {
                return JsonConvert.SerializeObject(Geometry, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() });
            }
            return null;
        }
    }
}