﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitConverter.Converters
{
    public class ConvertManager
    {
        public List<IConverter> converters = new List<IConverter>();

        public ConvertManager()
        {
            converters.Add(new LenghtConverter());
            converters.Add(new MassConverter());
            converters.Add(new TemperatureConverter());
        }
        public IConverter GetConverter2(string s)
        {
            foreach (var item in converters)
            {
                foreach (var item2 in item.Units)
                {
                    if (item2.ToUpper().Equals(s.ToUpper()))
                    {
                        return item;

                    }

                }

            }
            return null;
        }
        public IConverter GetConverter(string mainUnit)
        {
            foreach (var item in converters)
            {
                if (item.Name.ToUpper().Equals(mainUnit.ToUpper()))
                {
                    return item;
                }
            }
            return null;
        }
        public List<string> getMainUnits()
        {
            List<string> l = new List<string>();
            foreach (var item in converters)
            {
                l.Add(item.Name);
            }
            return l;
        }
        public double convert(double startValue, string fromUnit, string toUnit)
        {
            double res = 0;

            return res;
        }
    }
}
