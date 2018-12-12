﻿using System.Collections.Generic;
using BH.oM.Structure.Properties.Surface;
using RobotOM;

namespace BH.Adapter.Robot
{
    public partial class RobotAdapter
    {
        /***************************************************/
        /****           Private Methods                 ****/
        /***************************************************/

        private bool CreateCollection(IEnumerable<ISurfaceProperty> properties)
        {
            RobotLabelServer labelServer = m_RobotApplication.Project.Structure.Labels;
            IRobotLabel lable = null;
            string name = "";
            foreach (ISurfaceProperty property in properties)
            {
                if (property is LoadingPanelProperty)
                {
                    name = BH.Engine.Robot.Convert.ThicknessProperty(lable, property);
                    lable = labelServer.CreateLike(IRobotLabelType.I_LT_CLADDING, property.Name, name);
                }

                else
                {
                    lable = labelServer.Create(IRobotLabelType.I_LT_PANEL_THICKNESS, name);
                    name = BH.Engine.Robot.Convert.ThicknessProperty(lable, property);
                }

                labelServer.StoreWithName(lable, name);
            }

            return true;
        }

        /***************************************************/

    }

}

