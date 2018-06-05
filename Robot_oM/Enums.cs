﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Adapters.Robot
{
    public enum MaterialDB
    {
        British,
        American
    }

    public enum SectionDB
    {
        UKST,
        AISC
    }

    public enum ObjectProperties
    {
        Name, 
        Number
    }

    public enum BarProperties
    {
        FramingElementDesignProperties,
        StructureObjectType                
    }

    public enum NodeProperties
    {
        CoordinateSystem
    }

    public enum Commands
    {

    }

    public enum DesignCode_SteelFramingElements
    {
        BS5950 = 0,
        BS5950_2000,
        BS_EN_1993_1_2005_NA_2008_A1_2014   
    }
}
