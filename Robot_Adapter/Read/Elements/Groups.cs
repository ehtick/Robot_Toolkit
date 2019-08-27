/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2018, the respective contributors. All rights reserved.
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

using System.Collections.Generic;
using RobotOM;
using BH.oM.Structure.Elements;
using BH.oM.Structure.Loads;

namespace BH.Adapter.Robot
{
    public partial class RobotAdapter
    {
        /***************************************************/
        /****           Private Methods                 ****/
        /***************************************************/

        private List<BH.oM.Base.IBHoMObject> ReadGroups()
        {
            List<BH.oM.Base.IBHoMObject> groups = new List<oM.Base.IBHoMObject>();
            RobotGroupServer m_groupServ = m_RobotApplication.Project.Structure.Groups;
            for (int i = 1; i <= m_groupServ.GetCount(IRobotObjectType.I_OT_BAR); i++)
            {
                RobotGroup rgroup = m_RobotApplication.Project.Structure.Groups.Get(IRobotObjectType.I_OT_BAR, i);
                if (rgroup != null)
                {
                    List<Bar> obj = ReadBars(BH.Engine.Robot.Convert.FromRobotSelectionString(rgroup.SelList));
                    groups.Add(BH.Engine.Base.Create.BHoMGroup(obj, false, rgroup.Name));
                }
            }
            for (int i = 1; i <= m_groupServ.GetCount(IRobotObjectType.I_OT_NODE); i++)
            {
                RobotGroup rgroup = m_RobotApplication.Project.Structure.Groups.Get(IRobotObjectType.I_OT_NODE, i);
                if (rgroup != null)
                {
                    List<Node> obj = ReadNodes(BH.Engine.Robot.Convert.FromRobotSelectionString(rgroup.SelList));
                    groups.Add(BH.Engine.Base.Create.BHoMGroup(obj, false, rgroup.Name));
                }
            }
            for (int i = 1; i <= m_groupServ.GetCount(IRobotObjectType.I_OT_PANEL); i++)
            {
                RobotGroup rgroup = m_RobotApplication.Project.Structure.Groups.Get(IRobotObjectType.I_OT_PANEL, i);
                if (rgroup != null)
                {
                    List<Panel> obj = ReadPanels(BH.Engine.Robot.Convert.FromRobotSelectionString(rgroup.SelList));
                    groups.Add(BH.Engine.Base.Create.BHoMGroup(obj, false, rgroup.Name));
                }
            }
            return groups;
        }

        /***************************************************/

    }

}

