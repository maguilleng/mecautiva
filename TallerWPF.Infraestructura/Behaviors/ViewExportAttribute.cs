// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.ComponentModel.Composition;

namespace TallerWPF.Infraestructura
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    [MetadataAttribute]
    public sealed class ViewExportAttribute : ExportAttribute, IViewRegionRegistration
    {
        public ViewExportAttribute()
            : base(typeof(object))
        { }

        public ViewExportAttribute(string viewName)
            : base(viewName, typeof(object))
        { }

        public string ViewName { get { return base.ContractName; } }

        public string RegionName { get; set; } 
        
        private bool isActiveByDefault = true;
        public bool IsActiveByDefault
        {
            get
            {
                return this.isActiveByDefault;
            }
            set
            {
                this.isActiveByDefault = value;
            }
        }
    }
}
