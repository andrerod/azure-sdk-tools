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


namespace Microsoft.WindowsAzure.Commands.ServiceManagement.IaaS.AvailabilitySets
{
    using Model;
    using System;
    using System.Management.Automation;

    [Cmdlet(VerbsCommon.Set, "AzureAvailabilitySet"), OutputType(typeof(IPersistentVM))]
    public class SetAzureAvailabilitySet : VirtualMachineConfigurationCmdletBase
    {
        [Parameter(Position = 0, Mandatory = true, HelpMessage = "Availability set name.")]
        [AllowNull]
        [AllowEmptyString]
        public string AvailabilitySetName { get; set; }

        internal void ExecuteCommand()
        {
            this.VM.GetInstance().AvailabilitySetName = this.AvailabilitySetName;
            this.WriteObject(this.VM, true);
        }

        protected override void ProcessRecord()
        {
            try
            {
                base.ProcessRecord();
                this.ExecuteCommand();
            }
            catch (Exception ex)
            {
                this.WriteError(new ErrorRecord(ex, string.Empty, ErrorCategory.CloseError, null));
            }
        }
    }
}