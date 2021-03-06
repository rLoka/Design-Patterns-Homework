﻿using System.Collections.Generic;

namespace kgrlic_zadaca_2.Devices
{
    abstract partial class Device
    {
        //Composite methods of Device class
        public abstract void AddChild(Device device);
        public abstract void RemoveChild(Device device);
        public abstract void AddParent(Device device);
        public abstract void RemoveParent(Device device);
        public abstract List<Device> GetChildren();
        public abstract List<Device> GetParents();

        public bool IsLeaf()
        {
            if (GetChildren().Count > 0)
            {
                return false;
            }
            return true;
        }

        public bool IsRoot()
        {
            if (GetParents().Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
