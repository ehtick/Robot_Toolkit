﻿using System.Collections.Generic;
using BH.oM.Base;
using BH.oM.Structural.Elements;
using BH.oM.Structural.Properties;
using BH.oM.Structural.Loads;
using BH.oM.Common.Materials;
using RobotOM;

namespace BH.Adapter.Robot
{
    public partial class RobotAdapter
    {
        /***************************************************/
        /****           Protected Methods               ****/
        /***************************************************/

        protected override bool UpdateObjects<T>(IEnumerable<T> objects)
        {
            bool success = true;
            success = Update(objects as dynamic);
            updateview();
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<IBHoMObject> bhomObjects)
        {
            return true;
        }

        /***************************************************/

        protected bool Update(IEnumerable<Node> nodes)
        {
            Dictionary<int, HashSet<string>> nodeTags = GetTypeTags(typeof(Node));
            foreach (Node node in nodes)
            {
                RobotNode robotNode = m_RobotApplication.Project.Structure.Nodes.Get(System.Convert.ToInt32(node.CustomData[AdapterId])) as RobotNode;
                if (robotNode == null)
                    return false;

                if (node.Constraint != null && !string.IsNullOrWhiteSpace(node.Constraint.Name))
                    robotNode.SetLabel(IRobotLabelType.I_LT_SUPPORT, node.Constraint.Name);

                robotNode.X = node.Position.X;
                robotNode.Y = node.Position.Y;
                robotNode.Z = node.Position.Z;
                nodeTags[System.Convert.ToInt32(node.CustomData[AdapterId])] = node.Tags;
            }
            m_tags[typeof(Node)] = nodeTags;
            return true;
        }

        /***************************************************/

        protected bool Update(IEnumerable<Material> materials)
        {
            List<Material> matToCreate = new List<Material>();

            foreach (Material m in materials)
            {
                string match = BH.Engine.Robot.Convert.Match(m_dbMaterialNames, m);
                if (match == null)
                    matToCreate.Add(m);
            }

            bool success = true;
            success = Create(matToCreate);
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<ISectionProperty> sectionProperties)
        {
            List<ISectionProperty> secPropToCreate = new List<ISectionProperty>();

            foreach (ISectionProperty p in sectionProperties)
            {
                string match = BH.Engine.Robot.Convert.Match(m_dbSecPropNames, p);
                if (match == null)
                    secPropToCreate.Add(p);
            }

            bool success = true;
            success = Create(secPropToCreate);
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<IProperty2D> property2Ds)
        {
            bool success = true;
            success = Create(property2Ds);
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<LinkConstraint> linkConstraints)
        {
            bool success = true;
            success = Create(linkConstraints);
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<Loadcase> loadCases)
        {
            bool success = true;
            foreach (Loadcase lCase in loadCases)
            {
                RobotSimpleCase robotSimpCase = m_RobotApplication.Project.Structure.Cases.Get(System.Convert.ToInt32(lCase.CustomData[AdapterId])) as RobotSimpleCase;
                int subNature;
                IRobotCaseNature rNature = BH.Engine.Robot.Convert.RobotLoadNature(lCase, out subNature);
                robotSimpCase.AnalizeType = IRobotCaseAnalizeType.I_CAT_STATIC_LINEAR;
                robotSimpCase.Nature = rNature;
                robotSimpCase.Number = System.Convert.ToInt32(lCase.CustomData[AdapterId]);
            }
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<LoadCombination> loadCombinations)
        {
            bool success = true;
            success = Create(loadCombinations);
            return success;
        }

        /***************************************************/

        protected bool Update(IEnumerable<Constraint6DOF> nodeConst)
        {
            bool success = true;
            success = Create(nodeConst);
            return success;
        }

        /***************************************************/
    }
}
