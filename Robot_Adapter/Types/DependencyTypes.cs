﻿using BH.oM.Common.Materials;
using BH.oM.Structural.Elements;
using BH.oM.Structural.Properties;
using BH.oM.Structural.Loads;
using BH.oM.Base;
using System;
using System.Collections.Generic;

namespace BH.Adapter.Robot
{
    public partial class RobotAdapter
    {
        /***************************************************/
        /**** BHoM Adapter Interface                    ****/
        /***************************************************/

        protected override List<Type> DependencyTypes<T>()
        {
            Type type = typeof(T);

            if (m_DependencyTypes.ContainsKey(type))
                return m_DependencyTypes[type];

            else if (m_DependencyTypes.ContainsKey(type.BaseType))
                return m_DependencyTypes[type.BaseType];

            else
                return new List<Type>();
        }


        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        private static Dictionary<Type, List<Type>> m_DependencyTypes = new Dictionary<Type, List<Type>>
        {
            {typeof(Bar), new List<Type> { typeof(ISectionProperty), typeof(Node) } },
            {typeof(ISectionProperty), new List<Type> { typeof(Material) } },
            {typeof(Node), new List<Type> { typeof(Constraint6DOF) } },
            {typeof(ILoad), new List<Type> { typeof(Loadcase), typeof(BHoMGroup<IObject>)} }
        };


        /***************************************************/
    }
}
