/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2020, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */

using BH.oM.Structure.Loads;
using RobotOM;

namespace BH.Adapter.Robot
{
    public static partial class Convert
    {
        /***************************************************/
        /****           Public Methods                  ****/
        /***************************************************/

        public static BarTemperatureLoad FromRobotBarTempLoad(this IRobotLoadRecord loadRecord)
        {
            if (loadRecord == null)
                return null;

            try
            {
                double t = loadRecord.GetValue((short)IRobotBarThermalRecordValues.I_BTRV_TX);
                double ty = loadRecord.GetValue((short)IRobotBarThermalRecordValues.I_BTRV_TY);
                double tz = loadRecord.GetValue((short)IRobotBarThermalRecordValues.I_BTRV_TZ);

                if (ty != 0 || tz != 0)
                {
                    Engine.Reflection.Compute.RecordWarning("Temparature loads in Robot with non axial components found. BHoM Temprature loads only support uniform temprature change, only this value will be extracted.");
                }

                return new BarTemperatureLoad
                {
                    TemperatureChange = t
                };
            }
            catch (System.Exception)
            {
                return null;
            }

        }

        /***************************************************/
    }
}

