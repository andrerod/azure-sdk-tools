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

namespace Microsoft.WindowsAzure.Commands.Utilities.Common
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class ConversionUtils
    {
        public static Dictionary<string, object> ToDictionary(this Hashtable hashtable, bool addValueLayer)
        {
            if (hashtable == null)
            {
                return null;
            }
            else
            {
                var dictionary = new Dictionary<string, object>();
                foreach (var entry in hashtable.Cast<DictionaryEntry>())
                {
                    var valueAsHashtable = entry.Value as Hashtable;

                    if (valueAsHashtable != null)
                    {
                        dictionary[(string)entry.Key] = valueAsHashtable.ToDictionary(addValueLayer);
                    }
                    else
                    {
                        if (addValueLayer)
                        {
                            dictionary[(string) entry.Key] = new Hashtable() {{"value", entry.Value.ToString()}};
                        }
                        else
                        {
                            dictionary[(string) entry.Key] = entry.Value.ToString();
                        }
                    }
                }
                return dictionary;
            }
        }

        public static Hashtable ToHashtable<TV>(this IDictionary<string, TV> dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            else
            {
                return new Hashtable((Dictionary<string, TV>)dictionary);
            }
        }
    }
}
