using System;
using System.Collections.Generic;
using System.Text;

namespace Spider.Data.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Struct)]
    public class IdentityAttribute : System.Attribute
    {

        public IdentityAttribute()
        {
        }

    }
}
