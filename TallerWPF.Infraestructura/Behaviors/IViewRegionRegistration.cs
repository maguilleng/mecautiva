// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.


namespace TallerWPF.Infraestructura
{
    public interface IViewRegionRegistration
    {
        string RegionName { get; }
        bool IsActiveByDefault { get; }
    }
}
