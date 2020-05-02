using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FastReslectionForHabrahabr.Hydrators
{
    public class SlowContactHydrator : ContactHydratorBase
    {
        protected static readonly PropertyInfo[] _properties;
        static SlowContactHydrator()
        {
            var type = typeof(Contact);
            _properties = type.GetProperties();
        }
        public SlowContactHydrator(IRawStringParser normalizer, IStorage db) : base(normalizer, db)
        {
        }

        protected override Contact GetContact(PropertyToValueCorrelation[] correlations)
        {
            var contact = new Contact();
            foreach (var property in _properties)
            {
                var correlation = correlations.FirstOrDefault(x => x.PropertyName == property.Name);
                if (correlation?.Value == null)
                    continue;

                property.SetValue(contact, correlation.Value);
            }
            return contact;
        }
    }
}
