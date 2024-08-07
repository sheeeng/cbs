/*---------------------------------------------------------------------------------------------
*  Copyright (c) The International Federation of Red Cross and Red Crescent Societies. All rights reserved.
*  Licensed under the MIT License. See LICENSE in the project root for license information.
*--------------------------------------------------------------------------------------------*/

using Dolittle.Artifacts;
using Dolittle.Events;

namespace Events.Reporting.DataCollectors
{
    [Artifact("5764eae6-fc2e-4c46-a6d7-fd6c978499fc", 1)]
    public class DataCollectorVillageChanged : IEvent
    {
        public string Village { get; }

        public DataCollectorVillageChanged(string village)
        {
            Village = village;
        }
    }
}
