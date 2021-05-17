using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TreeViewWinFormsTest
{
    public enum SampleType
    {
        Category,
        Engine,
        Windows
    }

    public class SampleViewModel: Collection<SampleViewModel>
    {
        public string Name { get; set; }
        public SampleType SampleType { get; set; }

        public SampleViewModel(string name, SampleType sampleType)
        {
            Name = name;
            SampleType = sampleType;
        }
    }
}
