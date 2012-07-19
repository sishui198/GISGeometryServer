﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeometryServer.Geometry
{
    public class Polygon : Geometry
    {
        public System.Collections.ObjectModel.ObservableCollection<PointCollection> Rings { get; set; }

        public SpatialReference SpatialReference { get; set; }
    }
}