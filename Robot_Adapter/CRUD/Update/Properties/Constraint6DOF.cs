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

using BH.Engine.Robot;
using BH.oM.Structure.Constraints;
using RobotOM;
using System.Collections.Generic;

namespace BH.Adapter.Robot
{
    public partial class RobotAdapter
    {
        /***************************************************/
        /****           Protected Methods               ****/
        /***************************************************/

        protected bool Update(IEnumerable<Constraint6DOF> supports)
        {
            IRobotLabelServer robotLabelServer = m_RobotApplication.Project.Structure.Labels;
            IRobotLabel robotLabel = robotLabelServer.Create(IRobotLabelType.I_LT_LINEAR_RELEASE, "");
            foreach (Constraint6DOF support in supports)
            {
                robotLabel = robotLabelServer.Get(IRobotLabelType.I_LT_SUPPORT, support.Name);
                Constraint6DOF robotConstraint = Convert.ToBHoMObject(robotLabel.Data, robotLabel.Name);
                Constraint6DOFComparer constraint6DOFComparer = new Constraint6DOFComparer();
                if (constraint6DOFComparer.Equals(support, robotConstraint))
                    return true;
                else
                {
                    Convert.RobotConstraint(robotLabel.Data, support);
                    robotLabelServer.StoreWithName(robotLabel, support.Name);
                    BH.Engine.Reflection.Compute.RecordWarning("Support '" + support.Name + "' already exists in the model, the properties have been overwritten");
                }

            }
            return true;

            /***************************************************/
        }
    }
}

