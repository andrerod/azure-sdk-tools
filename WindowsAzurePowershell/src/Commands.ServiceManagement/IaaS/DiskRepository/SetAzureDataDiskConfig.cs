﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS
{
    using System;
    using System.Linq;
    using System.Management.Automation;
    using Management.Compute;
    using Management.Compute.Models;
    using Model;
    using Model.PersistentVMModel;
    using Utilities.Common;
    using PVM = Model.PersistentVMModel;

    [Cmdlet(
        VerbsCommon.Set,
        AzureDataDiskConfigurationNoun),
    OutputType(
        typeof(VirtualMachineDiskConfigurationSet))]
    public class SetAzureDataDiskConfiguration : PSCmdlet
    {
        protected const string AzureDataDiskConfigurationNoun = "AzureDataDiskConfiguration";

        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "DiskConfigurationSet")]
        [ValidateNotNullOrEmpty]
        public VirtualMachineDiskConfigurationSet DiskConfig { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Name")]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Position = 2,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "HostCaching")]
        [ValidateNotNullOrEmpty]
        public string HostCaching { get; set; }

        [Parameter(
            Position = 3,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "Lun")]
        [ValidateNotNullOrEmpty]
        public int Lun { get; set; }

        [Parameter(
            Position = 4,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "MediaLink")]
        [ValidateNotNullOrEmpty]
        public Uri MediaLink { get; set; }

        [Parameter(
            Position = 5,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = "LogicalDiskSizeInGB")]
        [ValidateNotNullOrEmpty]
        public int LogicalDiskSizeInGB { get; set; }

        protected override void ProcessRecord()
        {
            ServiceManagementProfile.Initialize();

            if (DiskConfig.DataDiskConfigurations == null)
            {
                DiskConfig.DataDiskConfigurations = new PVM.DataDiskConfigurationList();
            }

            var diskConfig = DiskConfig.DataDiskConfigurations.FirstOrDefault(
                d => string.Equals(d.Name, this.Name));

            if (diskConfig == null)
            {
                diskConfig = new PVM.DataDiskConfiguration();
                DiskConfig.DataDiskConfigurations.Add(diskConfig);
            }

            diskConfig.Name                = this.Name;
            diskConfig.HostCaching         = this.HostCaching;
            diskConfig.MediaLink           = this.MediaLink;
            diskConfig.Lun                 = this.Lun;
            diskConfig.LogicalDiskSizeInGB = this.LogicalDiskSizeInGB;

            WriteObject(DiskConfig);
        }
    }
}
